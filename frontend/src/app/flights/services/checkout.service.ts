import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, catchError, throwError } from 'rxjs';
import { Booking } from '../models/booking';

@Injectable({
  providedIn: 'root'
})
export class CheckoutService {
  private url = 'http://localhost:8000/api/flight/booking/';

  constructor(private httpClient: HttpClient) { }

  errorHandler(error: HttpErrorResponse){
    return throwError(() => error);
  }
  
  public checkOut(params: Booking): Observable<Booking>
  {
    return this.httpClient.post<Booking>(this.url, params).pipe(catchError(this.errorHandler));
  }
}
