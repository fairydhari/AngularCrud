import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-productimageupload',
  templateUrl: './productimageupload.component.html',
  styleUrls: ['./productimageupload.component.css']
})
export class ProductimageuploadComponent implements OnInit {
  urls = [];
  constructor() { }

  ngOnInit() {
  }
  onSelectFile(event) {
    if (event.target.files && event.target.files[0]) {
        var filesAmount = event.target.files.length;
        for (let i = 0; i < filesAmount; i++) {
                var reader = new FileReader();

                reader.onload = (event) => {
                  console.log(reader.result)
                 // console.log(event.target.result);
                   this.urls.push(reader.result); 
                }

                reader.readAsDataURL(event.target.files[i]);
        }
    }
  }
}
