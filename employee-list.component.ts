import { Component, OnInit, TemplateRef } from '@angular/core';
import { EmployeeService } from '../../services/employee.service';
import { Employee } from '../../models/employee';
import { GridDataResult, PageChangeEvent } from '@progress/kendo-angular-grid';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { DialogService, DialogCloseResult } from '@progress/kendo-angular-dialog';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
    selector: 'app-employee-list',
  template: `
   <div class="example-wrapper">
<kendo-grid
          [kendoGridBinding]="data"
          [pageSize]="pageSize"
          [skip]="skip"
          [pageable]="true"
          [height]="200"
          (pageChange)="onPageChange($event)">
         <kendo-grid-column field="id" title="ID" width="40">
         </kendo-grid-column>
         <kendo-grid-column field="cmp_Name" title="Name" width="250">
         </kendo-grid-column>
         <kendo-grid-column field="address" title="address" width="80">
         </kendo-grid-column>
  <kendo-grid-command-column title="command" width="120">
              <ng-template kendoGridCellTemplate let-dataItem> 
                  <button kendoGridEditCommand class="k-primary">Edit</button>
                  <button kendoGridRemoveCommand (click)="showConfirmation(dataItem)" class="k-button">Remove</button>
              </ng-template>
          </kendo-grid-command-column>
         <ng-template kendoPagerTemplate let-totalPages="totalPages" let-currentPage="currentPage">
            <kendo-pager-prev-buttons></kendo-pager-prev-buttons>
            <kendo-slider [showButtons]="true" [tickPlacement]="'none'"
                [max]="totalPages" min]="1" (valueChange)="sliderChange($event)"
                [value]="currentPage">
            </kendo-slider>
            <kendo-pager-info></kendo-pager-info>
            <kendo-pager-next-buttons></kendo-pager-next-buttons>
            <kendo-pager-page-sizes [pageSizes]="[1,5, 10, 40]"></kendo-pager-page-sizes>
         </ng-template>
      </kendo-grid>  </div>
<div kendoDialogContainer></div>  ` 
})

export class EmployeeListComponent implements OnInit {  
  public data: Employee[];
  public pageSize = 10;
  public skip = 0;
  constructor(private employeeService: EmployeeService, private dialogService: DialogService, private route: ActivatedRoute, private router: Router) { }
  ngOnInit() {    
    this.getEmployeess();
  }
  getEmployeess() {
    return this.employeeService.getEmployees()
      .subscribe(res => {
        this.data = res;
        console.log(this.data);
      }, err => {
        console.log(err);
      });
  }
  public sliderChange(pageIndex: number): void {
    this.skip = (pageIndex - 1) * this.pageSize;
  }
  public onPageChange(state: any): void {
    this.pageSize = state.take;
  }


  deleteEmployee(id:any) {   
    this.employeeService.deleteEmployee(id)
      .subscribe(res => {      
        this.router.navigate(['/employeeList']);
      }, (err) => {
        console.log(err);        
      }
      );
  }
  public showConfirmation(item: any) {
    console.log("button" + item.id);
    //id: string;
    const id = item.id;
    var dialog=this.dialogService.open({
      title: 'Please confirm',
      content: "Are you sure you want to delete this item?",
      actions: [
        { text: 'No' },
        { text: 'Yes', primary: true }
      ]
    });
    
    dialog.result.subscribe((result) => {
       if (result instanceof DialogCloseResult) {
         console.log("close");
       } else {
         console.log(id);
         this.deleteEmployee(id)
         console.log("action", result.text);
       }
     });
   }
  }

