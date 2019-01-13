import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { AppComponent } from './app.component';
import { EmployeeListComponent } from './employee/employee-list/employee-list.component';
import { CreateEmployeeComponent } from './employee/create-employee/create-employee.component';
import { AppRoutingModule } from './app-routing.module' //<-- import
import { EmployeeService } from './services/employee.service';
import { GridModule } from '@progress/kendo-angular-grid';
import { SliderModule } from '@progress/kendo-angular-inputs';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { DropDownsModule } from '@progress/kendo-angular-dropdowns';
import { DialogModule } from '@progress/kendo-angular-dialog';
import { ModalModule } from 'angular-custom-modal';
import { ReactiveFormsModule } from '@angular/forms';


@NgModule({
  declarations: [
    AppComponent,    
    EmployeeListComponent,
    CreateEmployeeComponent,
   ],
  imports: [
    BrowserModule,
    HttpClientModule,    
    AppRoutingModule,
    BrowserAnimationsModule, GridModule, SliderModule,
    DialogModule,
    ModalModule,
    ReactiveFormsModule
  ],
  providers: [
    EmployeeService
    ],
  bootstrap: [AppComponent]
})
export class AppModule { }
