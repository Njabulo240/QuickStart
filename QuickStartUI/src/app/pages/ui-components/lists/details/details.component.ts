import { Component, OnInit, inject } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { Router, ActivatedRoute } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { AccountDto, CustomerDto } from 'src/app/_interface/customers';
import { AddAccountComponent } from '../add-account/add-account.component';
import { DialogService } from 'src/app/shared/services/dialog.service';
import { RepositoryService } from 'src/app/shared/services/repository.service';

@Component({
  selector: 'app-details',
  templateUrl: './details.component.html',
})
export class DetailsComponent implements OnInit {
  customer: CustomerDto | any;
  accounts: AccountDto | any;

  constructor(
    private repoService: RepositoryService,
    private router: Router,
    private dialog: MatDialog,
    private dialogserve: DialogService,
    private activeRoute: ActivatedRoute
  ) {
  }
  ngOnInit(): void {
    this.getCustomer();
    this.getCustomerAccount();
  }

  public getCustomer() {
    let id: string = this.activeRoute.snapshot.params['id'];
    this.repoService.getData(`api/customers/${id}`).subscribe(
      (res) => {
        this.customer = res as CustomerDto;
      },
      (err) => {
        this.dialogserve
          .openErrorDialog(err)
          .afterClosed()
          .subscribe((res) => {
            this.router.navigate(['/ui-components/lists']);
          });
      }
    );
  }

  public getCustomerAccount() {
    let id: string = this.activeRoute.snapshot.params['id'];
    this.repoService.getData(`api/customers/${id}/accounts`).subscribe(
      (res) => {
        this.accounts = res as AccountDto[];
      },
      (err) => {
        //  this.toastr.error(err);
      }
    );
  }

  addAccount(id: string) {
    const popup = this.dialog.open(AddAccountComponent, {
      width: '500px',
      height: '270px',
      enterAnimationDuration: '100ms',
      exitAnimationDuration: '100ms',
      data: {
        id: id,
      },
    });
  }
  DeleteCustomer(id: any) {
    this.dialogserve
      .openConfirmDialog('Are you sure, you want to delete the account ?')
      .afterClosed()
      .subscribe((res) => {
        if (res) {
          let customerId: string = this.activeRoute.snapshot.params['id'];
          const deleteUri: string = `api/customers/${customerId}/accounts/${id}`;
          this.repoService.delete(deleteUri).subscribe((res) => {
            this.getCustomer();
          });
        }
      });
  }
  close() {
    this.router.navigate(['/ui-components/lists']);
  }
}
