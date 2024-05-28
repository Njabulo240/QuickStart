import { Component, Inject, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import {
  MatDialog,
  MAT_DIALOG_DATA,
  MatDialogRef,
} from '@angular/material/dialog';
import { ToastrService } from 'ngx-toastr';
import { AccountForCreationDto } from 'src/app/_interface/customers';
import { DataService } from 'src/app/shared/services/data.service';
import { DialogService } from 'src/app/shared/services/dialog.service';
import { ErrorHandlerService } from 'src/app/shared/services/error-handler.service';
import { RepositoryService } from 'src/app/shared/services/repository.service';

@Component({
  selector: 'app-add-account',
  templateUrl: './add-account.component.html',
})
export class AddAccountComponent implements OnInit {
  dataForm: FormGroup | any;
  result: any;

  constructor(
    private repoService: RepositoryService,
    private toastr: ToastrService,
    private dataService: DataService,
    private dialogserve: DialogService,
    @Inject(MAT_DIALOG_DATA) public data: any,
    private Ref: MatDialogRef<AddAccountComponent>
  ) {}

  ngOnInit() {
    this.dataForm = new FormGroup({
      accountType: new FormControl('', [
        Validators.required,
        Validators.maxLength(60),
      ]),
    });

    this.result = this.data;
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
    let data: AccountForCreationDto = {
      accountType: dataFormValue.accountType,
    };
    let id = this.result.id;
    const apiUri: string = `api/customers/${id}/accounts`;
    this.repoService.create(apiUri, data).subscribe(
      (res) => {
        this.dialogserve.openSuccessDialog("The account has been added successfully.")
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

  closeModal() {
    this.Ref.close([]);
  }
}
