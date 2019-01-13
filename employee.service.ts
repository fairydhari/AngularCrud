import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpErrorResponse } from '@angular/common/http';
import { catchError, tap, map } from 'rxjs/operators';
import { Observable } from 'rxjs';
import { FormsModule } from '@angular/forms';
import { Employee } from '../models/employee';
import { of } from 'rxjs/observable/of';

const httpOptions = {
  headers: new HttpHeaders({ 'Content-Type': 'application/json' })
};
const apiUrl = "/api/Test";

@Injectable()
export class EmployeeService {

  constructor(private http: HttpClient) { }

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
  
  getEmployees(): Observable<Employee[]> {
    return this.http.get<Employee[]>(apiUrl +"/GetEmployeeList")
      .pipe(
        tap(heroes => console.log('fetched employees')),
      catchError(this.handleError('getEmployees', []))
      );
  }

  getEmployee(id: number): Observable<Employee> {
    const url = `${apiUrl}/${id}`;
    return this.http.get<Employee>(url).pipe(
      tap(_ => console.log('fetched employee id=${id}')),
      catchError(this.handleError<Employee>('getEmployee id=${id}'))
    );
  }
  addEmployee(employee): Observable<Employee> {
    return this.http.post<Employee>(apiUrl, employee, httpOptions).pipe(
      tap((employee: Employee) => console.log('added product w/ id=${employee.id}')),
      catchError(this.handleError<Employee>('addEmployee'))
    );
  }

  updateEmployee(id, employee): Observable<any> {
    const url = `${apiUrl}/${id}`;
    return this.http.put(url, employee, httpOptions).pipe(
      tap(_ => console.log('updated employee id=${id}')),
      catchError(this.handleError<any>('updateEmployee'))
    );
  }

  deleteEmployee(id): Observable<Employee> {
    const url = apiUrl + "/EmployeeDelete/" + id; //'${apiUrl}/${id}';
    console.log(url);
    return this.http.delete<Employee>(url, httpOptions).pipe(
      tap(_ => console.log('deleted employee id=${id}')),
      catchError(this.handleError<Employee>('deleteEmployee'))
    );
  }
}
