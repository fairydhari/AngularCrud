import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import { HttpClient, HttpRequest, HttpEventType, HttpResponse } from '@angular/common/http'
import { HelpContentService } from '../services/help-content.service';


@Component({
  selector: 'app-new-menu',
  templateUrl: './new-menu.component.html',
  styleUrls: ['./new-menu.component.css']
})
export class NewMenuComponent implements OnInit {
  urls = new Array<string>();
  public gridData: any[];
  loginForm: FormGroup;
  files: any;
  public progress: number;
  fileName: string;
  //public progress: number;
  public message: string;
  constructor(private formBuilder: FormBuilder, private http: HttpClient, private helpContentService: HelpContentService) { }
  
  ngOnInit() {
    this.loginForm = this.formBuilder.group({
      description: ['', Validators.required],
      fldContentImage: ['', Validators.required]
    });
  }
public autoGrow() {
  let textArea = document.getElementById("description");
 //textArea.style.overflow = 'hidden';
 textArea.style.height = '100px';
 textArea.style.height = textArea.scrollHeight + 'px';
}
  detectFiles(event) {
    this.urls = [];
     this.files = event.target.files;
    if (this.files) {
      for (let file of this.files) {
        let reader = new FileReader();
        this.progress = Math.round(100 * event.loaded / event.total);
        reader.onload = (e: any) => {
          this.urls.push(e.target.result);
        }
        reader.readAsDataURL(file);
      }
    }
  }
  upload(files) {
    if (files.length === 0)
      return;

    const formData = new FormData();

    for (let file of files) {
      {
        formData.append(file.name, file);
        let reader = new FileReader();
        reader.onload = (e: any) => {
          this.urls.push(e.target.result);
        }
        reader.readAsDataURL(file);
        this.fileName = file.name;
      }
      const uploadReq = new HttpRequest('POST', `api/upload`, formData, {
        reportProgress: true,
      });

      this.http.request(uploadReq).subscribe(event => {
        if (event.type === HttpEventType.UploadProgress)
          this.progress = Math.round(100 * event.loaded / event.total);
        else if (event.type === HttpEventType.Response)
          this.message = event.body.toString();
      });
    }
  }
//  this.http.request(uploadReq).subscribe(event => {
//  if (event.type === HttpEventType.UploadProgress)
    
//  else if (event.type === HttpEventType.Response)
//    this.message = event.body.toString();
//});
//  }
  public addData($event) {
    console.log("click");
    $event.stopPropagation();
    if (this.loginForm.invalid) {
      return;
    }
    else {
      console.log("valid")
      console.log(JSON.stringify(this.loginForm.value));
     // this.helpContentService.addMenu(this.loginForm.value, this.fileName);
      this.helpContentService.addMenu(JSON.stringify(this.loginForm.value), this.fileName)
        .subscribe(res => {
         // this.router.navigate(['/employeeList']);
        }, (err) => {
          console.log(err);
        }
        );
     // console.log("data" + JSON.stringify(this.loginForm.value));
    }
  }
}
