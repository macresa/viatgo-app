import { HttpClient, HttpErrorResponse, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, catchError, throwError } from 'rxjs';
import { Flight } from '../models/flight';

@Injectable({
  providedIn: 'root'
})
export class ApiService {
  private Url = 'http://localhost:8000/';
  private UrlFlights = this.Url + 'api/flight/search/';
  private UrlAutoComplete = this.Url + 'api/flight/autocomplete/';
  private UrlGetBookings = this.Url + 'api/flight/bookings/';

  constructor(private httpClient: HttpClient ) {  }

  errorHandler(error: HttpErrorResponse){
    return throwError(() => error);
  }

  public getCities(city: string): Observable<any[]>
  {
    return this.httpClient.get<any[]>(`${this.UrlAutoComplete}?city=${city}`)
                          .pipe(catchError(this.errorHandler));
  }

  public getFlightsDepartures(flightRequest: HttpParams): Observable<Flight[]>
  {
    return this.httpClient.get<Flight[]>(this.UrlFlights, {params: flightRequest})
                          .pipe(catchError(this.errorHandler));
  }

  public getFlightById(id: number): Observable<Flight>
  {
    return this.httpClient.get<Flight>(`${this.UrlFlights}${id}`)
                          .pipe(catchError(this.errorHandler));
  }

  public getFlightsReturns(flightRequest: HttpParams): Observable<Flight[]>
  {
    return this.httpClient.get<Flight[]>(this.UrlFlights, {params: flightRequest})
                          .pipe(catchError(this.errorHandler));
  }

  public getBookings(userName: string): Observable<any[]>
  {
    return this.httpClient.get<any[]>(`${this.UrlGetBookings}?user=${userName}`)
                          .pipe(catchError(this.errorHandler));
  }

}
