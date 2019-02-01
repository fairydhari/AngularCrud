import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import { HttpClient, HttpRequest, HttpEventType, HttpResponse } from '@angular/common/http'


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
  constructor(private formBuilder: FormBuilder, private http: HttpClient) { }
  
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
//  this.http.request(uploadReq).subscribe(event => {
//  if (event.type === HttpEventType.UploadProgress)
    
//  else if (event.type === HttpEventType.Response)
//    this.message = event.body.toString();
//});
//  }
  public addData($event) {
    $event.stopPropagation();
    if (this.loginForm.invalid) {
      return;
    }
    else {
      this.files[0].sa
      console.log("data" + JSON.stringify(this.loginForm.value));
    }
  }
}
