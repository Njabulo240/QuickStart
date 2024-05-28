
import { Component, Inject, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import {
  MatDialog,
  MAT_DIALOG_DATA,
  MatDialogRef,
} from '@angular/material/dialog';
import { ToastrService } from 'ngx-toastr';
import {
  CustomerForUpdateDto,
} from 'src/app/_interface/customers';
import { DataService } from 'src/app/shared/services/data.service';
import { DialogService } from 'src/app/shared/services/dialog.service';
import { ErrorHandlerService } from 'src/app/shared/services/error-handler.service';
import { RepositoryService } from 'src/app/shared/services/repository.service';

@Component({
  selector: 'app-update-customer',
  templateUrl: './update-customer.component.html',
})
export class UpdateCustomerComponent implements OnInit {
  dataForm: FormGroup | any;
  customer: CustomerForUpdateDto | any;
  result: any;

  constructor(
    private repoService: RepositoryService,
    private toastr: ToastrService,
    private dataService: DataService,
    private dialogserve: DialogService,
    @Inject(MAT_DIALOG_DATA) public data: any,
    private Ref: MatDialogRef<UpdateCustomerComponent>
  ) {}

  ngOnInit() {
    this.dataForm = new FormGroup({
      firstName: new FormControl('', [
        Validators.required,
        Validators.maxLength(60),
      ]),
      lastName: new FormControl('', [
        Validators.required,
        Validators.maxLength(60),
      ]),
      dateOfBirth: new FormControl(new Date()),
      address: new FormControl('', [
        Validators.required,
        Validators.maxLength(100),
      ]),
      country: new FormControl('', [
        Validators.required,
        Validators.maxLength(60),
      ]),
    });

    this.result = this.data;
    this.getCustomertoUpdate();
  }

  public validateControl = (controlName: string) => {
    return (
      this.dataForm?.get(controlName)?.invalid &&
      this.dataForm?.get(controlName)?.touched
    );
  };
  public hasError = (controlName: string, errorName: string) => {
    return this.dataForm?.get(controlName)?.hasError(errorName);
  };

  public createData = (dataFormValue: any) => {
    if (this.dataForm.valid) {
      this.executeDataCreation(dataFormValue);
    }
  };
  private executeDataCreation = (dataFormValue: any) => {
    let data: CustomerForUpdateDto = {
      firstName: dataFormValue.firstName,
      lastName: dataFormValue.lastName,
      dateOfBirth: dataFormValue.dateOfBirth,
      address: dataFormValue.address,
      country: dataFormValue.country,
    };
    let id = this.result.id;
    const apiUri: string = `api/customers/${id}`;
    this.repoService.update(apiUri, data).subscribe(
      (res) => {
        this.dialogserve.openSuccessDialog("The customer has been updated successfully.")
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

  private getCustomertoUpdate = () => {
    let id = this.result.id;
    const Uri: string = `api/customers/${id}`;
    console.log(Uri);
    this.repoService.getData(Uri).subscribe({
      next: (own: CustomerForUpdateDto | any) => {
        this.customer = { ...own };
        this.dataForm.patchValue(this.customer);
      },
      error: (err) => {
        this.toastr.success(err);
      },
    });
  };

  closeModal() {
    this.Ref.close([]);
  }
}
