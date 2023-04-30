import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, catchError, throwError } from 'rxjs';
import { Auth } from '../models/auth';

@Injectable({
  providedIn: 'root'
})
export class LoginService {

  private apiLoginUrl = 'http://localhost:8000/api/login/';

  constructor(private httpClient: HttpClient) { }

  errorHandler(error: HttpErrorResponse){
    return throwError(() => error);
  }
  
  public login(params: Auth): Observable<Auth>
  {
    return this.httpClient.post<Auth>(this.apiLoginUrl, params).pipe(catchError(this.errorHandler));
  }
  
}
