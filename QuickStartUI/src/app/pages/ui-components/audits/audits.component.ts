import { HttpErrorResponse } from '@angular/common/http';
import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { AuditLogDto } from 'src/app/_interface/audit-log';
import { RepositoryService } from 'src/app/shared/services/repository.service';

@Component({
  selector: 'app-audits',
  templateUrl: './audits.component.html',
})
export class AuditsComponent implements OnInit, AfterViewInit {
  displayedColumns: string[] = ['name', 'action', 'date', 'change'];
  public dataSource = new MatTableDataSource<AuditLogDto>();
  errorMessage: any;
  showError: boolean;
  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;

  constructor(private repoService: RepositoryService ) { }

  ngOnInit(): void {
    this.getRoles();
  }

  public getRoles() {
    this.repoService.getData('api/audits').subscribe(
      (res) => {
        this.dataSource.data = res as AuditLogDto[];
      },
      (err: HttpErrorResponse) => {
        console.log(err);
      }
    );
  }

  ngAfterViewInit(): void {
    this.dataSource.sort = this.sort;
    this.dataSource.paginator = this.paginator;
  }

  refresh() {
    this.getRoles();
  }

}
