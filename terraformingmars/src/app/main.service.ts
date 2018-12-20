import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { throwError as observableThrowError, BehaviorSubject, Observable } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { environment } from '../environments/environment';
import { User } from './model';

@Injectable({
  providedIn: 'root'
})
export class MainService {
  private url = environment.api;

  constructor(
    private http: HttpClient
  ) { }

  registerUser(user: User) {
    return this.http.post<User>(`${this.url}/auth`, user)
      .pipe(catchError(this.errorHandler));
  }
  

  errorHandler(error: HttpErrorResponse) {
    return observableThrowError(error || 'Server Error');
  }
}
