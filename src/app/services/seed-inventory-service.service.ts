import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Observable, throwError, of } from 'rxjs';
import { tap, catchError } from 'rxjs/operators';
import { Inventory } from '../inventory';
@Injectable({
  providedIn: 'root'
})
export class SeedInventoryServiceService {
  //private getInventoryUrl = 'https://mocki.io/v1/0077e191-c3ae-47f6-bbbd-3b3b905e4a60';
  private getCurrentInventoryUrl = 'https://localhost:44362/api/Inventory/GetCurrentInventory';
  constructor(private http: HttpClient) { }

  getAvailableInventory(): Observable<Inventory[]> {
    return this.http.get<Inventory[]>(this.getCurrentInventoryUrl).pipe(
      tap(data => { console.log('All: ' + JSON.stringify(data)); }),
      catchError(this.handleError)
    );
  }

  private handleError(err: HttpErrorResponse) {
    let errorMessage = '';
    if (err.error instanceof ErrorEvent) {
      // A client-side or network error occurred. Handle it accordingly.
      errorMessage = `An error occurred: ${err.error.message}`;
    } else {
      // The backend returned an unsuccessful response code.
      // The response body may contain clues as to what went wrong,
      errorMessage = `Server returned code: ${err.status}, error message is: ${err.message}`;
    }
    console.error(errorMessage);
    return throwError(errorMessage);
  }
}
