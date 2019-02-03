//import { Component, OnInit } from '@angular/core';
import { Component, OnInit, TemplateRef, Inject } from '@angular/core';
import { HttpClient, HttpRequest, HttpEventType, HttpResponse } from '@angular/common/http'
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { HelpcontentService } from '../services/helpcontent.service';
import { HelpContent } from '../model/helpContent';
import { DomSanitizer, SafeResourceUrl, SafeUrl } from '@angular/platform-browser';
import { DialogService, DialogCloseResult } from '@progress/kendo-angular-dialog';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { State } from '@progress/kendo-data-query';

@Component({
  selector: 'app-content-list',
  templateUrl: './content-list.component.html',
  styleUrls: ['./content-list.component.css']
})
export class ContentListComponent implements OnInit {
  entryForm: FormGroup;
  urls = new Array<string>();
  helpContent: HelpContent;
  fileName: string;
  disable: boolean;
  gridData: any[];
  constructor(private formBuilder: FormBuilder, private http: HttpClient, private helpcontentService: HelpcontentService, private dialogService: DialogService, private sanitizer: DomSanitizer) {
    this.helpContent = new HelpContent();
  }
  ngOnInit() {
    localStorage.setItem('language', 'En');
    this.disable = true;
    this.entryForm = this.formBuilder.group({
      description: ['', Validators.required],
      fldContentImage: ['']
    });
    this.entryForm.disable();
    this.disable = true;
  }
  public progress: number;
  public message: string;

  upload(files) {
    if (files.length === 0)
      return;

    const formData = new FormData();
    let reader = new FileReader();
    for (let file of files) {
      formData.append(file.name, file);
     
      this.fileName = file.name;
    }
    this.urls = [];
    const uploadReq = new HttpRequest('POST', `api/upload`, formData, {
      reportProgress: true,

    });
    reader.onload = (e: any) => {
      this.urls.push(e.target.result);
    }
    reader.readAsDataURL(files[0]);
    
    this.http.request(uploadReq).subscribe(event => {
      if (event.type === HttpEventType.UploadProgress)
        this.progress = Math.round(100 * event.loaded / event.total);
      else if (event.type === HttpEventType.Response)
        this.message = event.body.toString();
    });
  }

  onSubmit() {
    
    // stop here if form is invalid
    if (this.entryForm.invalid) {
      return;
    }
    else {
     // if (this.helpContent.ContentId 0) {}
      //this.helpContent.ContentId = 0;
      this.helpContent.PermissionId = 2;
      this.helpContent.DescriptionAr = this.entryForm.get("description").value;
      this.helpContent.DescriptionEn = this.entryForm.get("description").value;
      //if (this.fileName != null || this.fileName != "") {
        this.helpContent.ContentImage = this.fileName;
      //}
      //else {
      //  this.helpContent.ContentImage = null;
     // }
      // var v = this.registerForm.value;
      console.log(this.helpContent);
      this.helpcontentService.addHelpContent(this.helpContent)
        .subscribe(res => {
          console.log(res);
          alert('SUCCESS!! :-');
          this.getHelpContent();
          this.onCancel();
          // 
          // this.router.navigate(['/employeeList']);
        }, (err) => {
          console.log(err);
        }
        );
    }
   
 
  }
  onCancel() {
    this.urls = [];
    this.entryForm.reset();
    this.helpContent.ContentId = 0;
    this.fileName = null;
  }
  onActivate() {
    this.entryForm.enable();
    this.disable = false;
    
    this.getHelpContent();
    console.log(localStorage.getItem('language'));
  }
  getHelpContent() {
    var permissionId = 2;
    return this.helpcontentService.getHelpContents(permissionId, localStorage.getItem('language'))
      .subscribe(res => {
        this.gridData = res;
     
      }, err => {
        console.log(err);
      });
  }
  public editHandler({ dataItem }) {
    console.log(dataItem.contentId);
    this.helpContent.ContentId = dataItem.contentId;
    this.urls = [];
    this.helpContent.PermissionId = 2;
    if (dataItem.descriptionAr != null || dataItem.descriptionAr != "") {
      this.entryForm.get("description").setValue(dataItem.descriptionAr);
      
    }
    if (dataItem.descriptionEn != null || dataItem.descriptionEn != "") {
      this.entryForm.get("description").setValue(dataItem.descriptionEn);
    }
    this.urls.push(dataItem.contentImage);
    //this.isNew = false;
  }
  deleteHelpContent(id: any) {
    this.helpcontentService.deleteHelpContent(id)
      .subscribe(res => {
        //this.router.navigate(['/employeeList']);
      }, (err) => {
        console.log(err);
      }
      );
  }
  public showConfirmation(item: any) {
    console.log("button" + item.contentId);
    //id: string;
    const id = item.contentId;
    var dialog = this.dialogService.open({
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
        this.deleteHelpContent(id)
        console.log("action", result);
      }
    });
  }
  public removeHandler({ dataItem }) {
    this.showConfirmation(dataItem);
  }

}


