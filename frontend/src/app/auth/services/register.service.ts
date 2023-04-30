import { Injectable } from '@angular/core';
import { Observable, catchError, throwError } from 'rxjs';
import { Auth } from '../models/auth';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class RegisterService {

  private apiRegisterUrl = 'http://localhost:8000/api/register/';

  constructor(private httpClient: HttpClient) { }

  errorHandler(error: HttpErrorResponse){
    return throwError(() => error);
  }
  
  public register(params: Auth): Observable<Auth>
  {
    return this.httpClient.post<Auth>(this.apiRegisterUrl, params).pipe(catchError(this.errorHandler));
  }
}
