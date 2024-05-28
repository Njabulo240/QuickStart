import {
  Component,
  ViewEncapsulation,
} from '@angular/core';
import {
  MonthDto,
  MonthTotalDto,
  AuditLogDto,
} from 'src/app/_interface/audit-log';
import {
  salesOverviewChart,
} from 'src/app/_interface/chart';
import { CustomerDto } from 'src/app/_interface/customers';
import { UserDto } from 'src/app/_interface/user';
import { RepositoryService } from 'src/app/shared/services/repository.service';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  encapsulation: ViewEncapsulation.None,
})
export class AppDashboardComponent {
  public errorMessage: string = '';
  public showError?: boolean;
  months: MonthDto[];
  selectedMonth: string | null = null;
  monthDetails: MonthTotalDto;
  public salesOverviewChart!: Partial<salesOverviewChart> | any;
  customers: CustomerDto[] | any;
  users: UserDto[] | any;
  logs: AuditLogDto[] | any;
  public totalUsers: number;
  public totalCustomers: number;
  public totalLogs: number;

  constructor(private repoService: RepositoryService) { }

  ngOnInit() {
    this.getMonths();
    this.initializeChart();
    this.getCustomers();
    this.getUsers();
    this.getAuditLog();
  }

  public getCustomers() {
    this.repoService.getData('api/customers').subscribe(
      (res) => {
        this.customers = res as CustomerDto[];
        this.totalCustomers = this.customers.length;
      },
      (err) => {
        console.log(err);
      }
    );
  }

  public getAuditLog() {
    this.repoService.getData('api/audits').subscribe(
      (res) => {
        this.logs = res as AuditLogDto[];
        this.totalLogs = this.logs.length;
      },
      (err) => {
        console.log(err);
      }
    );
  }

  public getUsers() {
    this.repoService.getData('api/users').subscribe(
      (res) => {
        this.users = res as UserDto[];
        this.totalUsers = this.users.length;
      },
      (err) => {
        console.log(err);
      }
    );
  }

  public getMonths() {
    this.repoService.getData('api/audits/GetMonths').subscribe(
      (res) => {
        this.months = res as MonthDto[];
        if (this.months.length > 0) {
          this.selectedMonth = this.months[0].month;
          this.fetchDataForSelectedMonth();
        }
      },
      (err) => {
        console.log(err);
      }
    );
  }

  public onMonthSelectionChange() {
    this.fetchDataForSelectedMonth();
  }

  private fetchDataForSelectedMonth() {
    this.repoService
      .getData(`api/audits/totalForSingleMonth/${this.selectedMonth}`)
      .subscribe((res) => {
        this.monthDetails = res as MonthTotalDto;
        this.updateChart();
      });
  }

  private updateChart() {
    this.salesOverviewChart.series = [
      {
        name: 'Audit this month',
        data: this.monthDetails.dayTotals.map(
          (dayTotal) => dayTotal.totalAudit
        ),
        color: '#5D87FF',
      },
    ];

    this.salesOverviewChart.xaxis = {
      categories: this.monthDetails.dayTotals.map(
        (dayTotal) => dayTotal.dayDate
      ),
    };
  }

  private initializeChart() {
    this.salesOverviewChart = {
      series: [
        {
          name: 'Audit this month',
          data: [],
          color: '#5D87FF',
        },
      ],
      grid: {
        borderColor: 'rgba(0,0,0,0.1)',
        strokeDashArray: 3,
        xaxis: {
          lines: {
            show: false,
          },
        },
      },
      plotOptions: {
        bar: { horizontal: false, columnWidth: '35%', borderRadius: [4] },
      },
      chart: {
        type: 'bar',
        height: 380,
        offsetX: -15,
        toolbar: { show: true },
        foreColor: '#adb0bb',
        fontFamily: 'inherit',
        sparkline: { enabled: false },
      },
      dataLabels: { enabled: false },
      markers: { size: 0 },
      legend: { show: false },
      xaxis: {
        type: 'Date',
        categories: [],
        labels: {
          style: { cssClass: 'grey--text lighten-2--text fill-color' },
        },
      },
      yaxis: {
        show: true,
        min: 0,
        max: 500,
        tickAmount: 4,
        labels: {
          style: {
            cssClass: 'grey--text lighten-2--text fill-color',
          },
        },
      },
      stroke: {
        show: true,
        width: 3,
        lineCap: 'butt',
        colors: ['transparent'],
      },
      tooltip: { theme: 'light' },
      responsive: [
        {
          breakpoint: 600,
          options: {
            plotOptions: {
              bar: {
                borderRadius: 3,
              },
            },
          },
        },
      ],
    };
  }
}
