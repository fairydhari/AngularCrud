import { Injectable } from '@angular/core';

import { Http, Response, Headers, RequestOptions, RequestMethod } from '@angular/http';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/toPromise';
 
import {UserDetails} from'../model/user-details.model'

@Injectable()
export class UserDetailsService {

  selectedUser : UserDetails;
  userList : UserDetails[];
  constructor(private http : Http) { }

  postUserDetails(userDetails : UserDetails){
    var body = JSON.stringify(userDetails);
    var headerOptions = new Headers({'Content-Type':'application/json'});
    var requestOptions = new RequestOptions({method : RequestMethod.Post,headers : headerOptions});
    return this.http.post('http://localhost:28750/api/Employee',body,requestOptions).map(x => x.json());
  }
 
  putUserDetails(id, userDetails) {
    var body = JSON.stringify(userDetails);
    var headerOptions = new Headers({ 'Content-Type': 'application/json' });
    var requestOptions = new RequestOptions({ method: RequestMethod.Put, headers: headerOptions });
    return this.http.put('http://localhost:28750/api/Employee/' + id,
      body,
      requestOptions).map(res => res.json());
  }
 
  getUserList(){
    this.http.get('http://localhost:28750/api/Employee')
    .map((data : Response) =>{
      return data.json() as UserDetails[];
    }).toPromise().then(x => {
      this.userList = x;
    })
  }
 
  deleteUser(id: number) {
    return this.http.delete('http://localhost:28750/api/Employee/' + id).map(res => res.json());
  }
}
