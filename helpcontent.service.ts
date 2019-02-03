import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpErrorResponse, HttpParams } from '@angular/common/http';
import { catchError, tap, map } from 'rxjs/operators';
import { HelpContent } from '../model/helpContent';
import { Observable } from 'rxjs/Observable';
import { of } from 'rxjs/observable/of';

const httpOptions = {
  headers: new HttpHeaders({ 'Content-Type': 'application/json' })
};
const apiUrl = "/api/HelpContents/";

@Injectable()
export class HelpcontentService {

  constructor(private http: HttpClient) { }
  //baseUrl: string = '/api/HelpContents/';
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
  getHelpContents(permissionId: number, language: string): Observable<HelpContent[]> {
    //const data = new URLSearchParams();
    //params.append('permissionId', permissionId.toString());
    //params.append('language', language);
    //var data = {
    //  "permissionId": permissionId,
    //  "language": language
    //};
    const params = new HttpParams()
      .set('permissionId', permissionId.toString())
      .set('language', language);

    return this.http.get<any[]>(apiUrl + "GetHelpContents", { params: params }).pipe(
        tap(heroes => console.log('fetched employees')),
        catchError(this.handleError('getEmployees', []))
      );
  }

  getHelpContent(id: number): Observable<HelpContent> {
    const url = `${apiUrl}/${id}`;
    return this.http.get<HelpContent>(url).pipe(
      tap(_ => console.log('fetched employee id=${id}')),
      catchError(this.handleError<HelpContent>('getEmployee id=${id}'))
    );
  }
  addHelpContent(helpContent): Observable<HelpContent> {
    console.log(JSON.stringify(helpContent));
    return this.http.post<HelpContent>(apiUrl + 'SaveHelpContent/',JSON.stringify(helpContent), httpOptions).pipe(
      tap((helpContent: HelpContent) => console.log('added product w/ id=${helpContent.id}')),
      catchError(this.handleError<HelpContent>('addEmployee'))
    );
  }

  updateHelpContent(id, employee): Observable<any> {
    const url = `${apiUrl}/${id}`;
    return this.http.put(url, employee, httpOptions).pipe(
      tap(_ => console.log('updated employee id=${id}')),
      catchError(this.handleError<any>('updateEmployee'))
    );
  }
  deleteHelpContent(id): Observable<any> {
    // const url = `${apiUrl}/${id}`;
    const params = new HttpParams()
      .set('id', id);
      

    return this.http.delete(apiUrl + 'DeleteContent/'+id, httpOptions).pipe(
      tap(_ => console.log('updated employee id=${id}')),
      catchError(this.handleError<any>('updateEmployee'))
    );
  }
}
