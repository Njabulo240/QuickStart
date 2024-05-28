import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { MatDialog, MatDialogRef } from '@angular/material/dialog';
import { ToastrService } from 'ngx-toastr';
import { CustomerForCreationDto } from 'src/app/_interface/customers';
import { DataService } from 'src/app/shared/services/data.service';
import { DialogService } from 'src/app/shared/services/dialog.service';
import { ErrorHandlerService } from 'src/app/shared/services/error-handler.service';
import { RepositoryService } from 'src/app/shared/services/repository.service';

@Component({
  selector: 'app-add-customer',
  templateUrl: './add-customer.component.html'
})
export class AddCustomerComponent implements OnInit {
  dataForm: FormGroup |any;

  constructor( 
    private repoService: RepositoryService,
    private toastr: ToastrService,
    private dataService: DataService,
    private dialogserve: DialogService,
    private Ref: MatDialogRef<AddCustomerComponent >) {}

  ngOnInit() {
    this.dataForm = new FormGroup({
      firstName: new FormControl('', [Validators.required, Validators.maxLength(60)]),
      lastName: new FormControl('', [Validators.required, Validators.maxLength(60)]),
      dateOfBirth: new FormControl(new Date()),
      address: new FormControl('', [Validators.required, Validators.maxLength(100)]),
      country: new FormControl('', [Validators.required, Validators.maxLength(60)]),
    });

  }



  public validateControl = (controlName: string) => {
    return this.dataForm?.get(controlName)?.invalid && this.dataForm?.get(controlName)?.touched
  }
  public hasError = (controlName: string, errorName: string) => {
    return this.dataForm?.get(controlName)?.hasError(errorName)
  }

  public createData = (dataFormValue: any) => {

    if (this.dataForm.valid) {
      this.executeDataCreation(dataFormValue);

    }
  };
  private executeDataCreation = (dataFormValue: any) => {
    let data: CustomerForCreationDto = {
   
      firstName: dataFormValue.firstName,
      lastName: dataFormValue.lastName,
      dateOfBirth: dataFormValue.dateOfBirth,
      address: dataFormValue.address,
      country:dataFormValue.country,

    };

    console.log(data);

    const apiUri: string = `api/customers`;
    this.repoService.create(apiUri, data).subscribe(
      (res) => {
        this.dialogserve.openSuccessDialog("The customer has been added successfully.")
        .afterClosed()
        .subscribe((res) => {
          this.dataService.triggerRefreshTab1();
          this.Ref.close([]);
        });
      },
      (error) => {
        this.toastr.error(error);
      }
    );
  };




  closeModal(){
    this.Ref.close([]);
  }
}
