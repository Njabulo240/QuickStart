import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MaterialModule } from '../../material.module';
import { TablerIconsModule } from 'angular-tabler-icons';
import * as TablerIcons from 'angular-tabler-icons/icons';
import { UiComponentsRoutes } from './ui-components.routing';
import { AppListsComponent } from './lists/lists.component';
import { MatNativeDateModule } from '@angular/material/core';
import { UsersComponent } from './users/users.component';
import { RolesComponent } from './roles/roles.component';
import { AuditsComponent } from './audits/audits.component';
import { ToastrModule } from 'ngx-toastr';
import { AddCustomerComponent } from './lists/add-customer/add-customer.component';
import { UpdateCustomerComponent } from './lists/update-customer/update-customer.component';
import { DetailsComponent } from './lists/details/details.component';
import { AddAccountComponent } from './lists/add-account/add-account.component';
import { AddUserComponent } from './users/add-user/add-user.component';
import { UpdateUserComponent } from './users/update-user/update-user.component';
import { AddRoleComponent } from './roles/add-role/add-role.component';
import { UpdateRoleComponent } from './roles/update-role/update-role.component';
import { AboutComponent } from './about/about.component';

@NgModule({
  imports: [
    CommonModule,
    RouterModule.forChild(UiComponentsRoutes),
    MaterialModule,
    FormsModule,
    ReactiveFormsModule,
    TablerIconsModule.pick(TablerIcons),
    MatNativeDateModule,
    ToastrModule.forRoot({
      timeOut: 3000, 
      positionClass: 'toast-top-right', 
      preventDuplicates: true, 
      progressBar: true, 
      closeButton: true 
    })
  ],
  declarations: [
    AppListsComponent,
    UsersComponent,
    RolesComponent,
    AuditsComponent,
    AddCustomerComponent,
    UpdateCustomerComponent,
    DetailsComponent,
    AddAccountComponent,
    AddUserComponent,
    UpdateUserComponent,
    AddRoleComponent,
    UpdateRoleComponent,
    AboutComponent
    
  ],
})
export class UicomponentsModule {}
