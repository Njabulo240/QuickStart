import { HttpErrorResponse } from '@angular/common/http';
import {
  AfterViewInit,
  Component,
  OnInit,
  ViewChild,
  inject,
} from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { CustomerDto } from 'src/app/_interface/customers';
import { AddCustomerComponent } from './add-customer/add-customer.component';
import { UpdateCustomerComponent } from './update-customer/update-customer.component';
import { DialogService } from 'src/app/shared/services/dialog.service';
import { RepositoryService } from 'src/app/shared/services/repository.service';
import { Subscription } from 'rxjs';
import { DataService } from 'src/app/shared/services/data.service';

@Component({
  selector: 'app-lists',
  templateUrl: './lists.component.html',
})
export class AppListsComponent implements OnInit, AfterViewInit {
  displayedColumns: string[] = [
    'action',
    'firstName',
    'lastName',
    'dateOfBirth',
    'country',
    'type',
    'status',
  ];
  public dataSource = new MatTableDataSource<CustomerDto>();
  errorMessage: any;
  showError: boolean;
  private refreshSubscription!: Subscription;

  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;

  constructor(
    private repoService: RepositoryService,
    private router: Router,
    private toastr: ToastrService,
    private dialog: MatDialog,
    private dataService: DataService,
    private dialogserve: DialogService
  ) {
    this.refreshSubscription = this.dataService.refreshTab1$.subscribe(() => {
      this.getCustomers();
    });
  }
  ngOnInit(): void {
    this.getCustomers();
  }

  public getCustomers() {
    this.repoService.getData('api/customers').subscribe(
      (res) => {
        this.dataSource.data = res as CustomerDto[];
      },
      (err) => {
        console.log(err);
      }
    );
  }

  ngAfterViewInit(): void {
    this.dataSource.sort = this.sort;
    this.dataSource.paginator = this.paginator;
  }
  public doFilter = (value: string) => {
    this.dataSource.filter = value.trim().toLocaleLowerCase();
  };

  addCustomer() {
    const popup = this.dialog.open(AddCustomerComponent, {
      width: '500px',
      height: '545px',
      enterAnimationDuration: '100ms',
      exitAnimationDuration: '100ms',
    });
  }
  updateCustomer(id: string) {
    const popup = this.dialog.open(UpdateCustomerComponent, {
      width: '500px',
      height: '545px',
      enterAnimationDuration: '100ms',
      exitAnimationDuration: '100ms',
      data: {
        id: id,
      },
    });
  }
  DeleteCustomer(id: any) {
    this.dialogserve
      .openConfirmDialog('Are you sure, you want to delete the customer ?')
      .afterClosed()
      .subscribe((res) => {
        if (res) {
          const deleteUri: string = `api/customers/${id}`;
          this.repoService.delete(deleteUri).subscribe((res) => {
            this.dialogserve
              .openSuccessDialog('The customer has been deleted successfully.')
              .afterClosed()
              .subscribe((res) => {
                this.getCustomers();
              });
          });
        }
      });
  }

  public redirectToDetails = async (id: string) => {
    let url: string = `/ui-components/customer-details/${id}`;
    this.router.navigate([url]);
  };
}
