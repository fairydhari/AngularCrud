import { Injectable } from '@angular/core';

import { HttpClient, HttpHeaders, HttpErrorResponse } from '@angular/common/http';
import { catchError, tap, map } from 'rxjs/operators';
import { Observable } from 'rxjs';
import { FormsModule } from '@angular/forms';
//import { Menu } from '../models/menu';
import { of } from 'rxjs/observable/of';
import { Menu } from '../model/menu';
const httpOptions = {
  headers: new HttpHeaders({ 'Content-Type': 'application/json' })
};
const apiUrl = "/api/Menu";
@Injectable()
export class HelpContentService {
  menuItem: Menu;
  menu: Menu;
  constructor(private http: HttpClient) { this.menuItem = new Menu(); }
  private handleError<T>(operation = 'operation', result?: T) {
    return (error: any): Observable<T> => {

      // TODO: send the error to remote logging infrastructure
      console.error(error); // log to console instead

      // Let the app keep running by returning an empty result.
      return of(result as T);
      //return of(result[]);
      //return of([]);
    };
  }
  addMenu(menu, fileName: string): Observable<Menu> {
    console.log("services");
    this.menuItem.MenuId = 0;
    this.menuItem.MenuNameAr = "AA";
    this.menuItem.MenuNameEn = "SS";
    this.menuItem.ParentId = null;
    this.menuItem.MenuImage = fileName;
    return this.http.post<Menu>(apiUrl + '/saveMenu/', this.menuItem, httpOptions).pipe(
      tap((menu: Menu) => console.log('added product w/ id=${employee.id}')),
      catchError(this.handleError<Menu>('addEmployee'))
    );
  }
}
