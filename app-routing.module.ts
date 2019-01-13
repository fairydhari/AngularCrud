import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { EmployeeListComponent } from './employee/employee-list/employee-list.component'
import { CreateEmployeeComponent } from './employee/create-employee/create-employee.component';
import { EmployeeService } from '../app/services/employee.service';
import { Routes, RouterModule } from '@angular/router';
import { GridModule } from '@progress/kendo-angular-grid';


const routes: Routes = [
  { path: '', redirectTo: '/employeeList', pathMatch: 'full' },
  { path: 'employeeList', component: EmployeeListComponent },
  { path: 'create', component: CreateEmployeeComponent }//,
  //{ path: 'heroes', component: HeroesComponent }
];
@NgModule({
  imports: [
    CommonModule,
    [RouterModule.forRoot(routes)],
    GridModule
  ],
  exports: [RouterModule],
  declarations: []
})
export class AppRoutingModule { }
