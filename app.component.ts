import { Component } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Routes, RouterModule } from '@angular/router';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'app';
  public values: string[];
  constructor(private http: HttpClient) {
    //this.http.get('/api/values').subscribe(result => {
    //  this.values = result as string[];
    //}, error => console.error(error));
    //console.log(this.values);
  }
}
