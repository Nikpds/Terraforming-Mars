import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { Observable, BehaviorSubject } from 'rxjs';
import { map } from 'rxjs/operators';
import { JwtHelperService } from '@auth0/angular-jwt';
import { User } from './model';

import { environment } from '../environments/environment';
@Injectable({
  providedIn: 'root'
})
export class AuthService {
  jwt = new JwtHelperService();
  private authUrl = environment.api + '/auth/token';

  private loggedInSubject$ = new BehaviorSubject<boolean>(false);
  loggedIn$ = this.loggedInSubject$.asObservable();
  private userInSubject$ = new BehaviorSubject<User>(null);
  user$ = this.userInSubject$.asObservable();

  constructor(
    private router: Router,
    private http: HttpClient,
  ) { this.initAuthorization(); }

  get loggedIn(): boolean { return this.loggedInSubject$.getValue(); }
  set loggedIn(value: boolean) { this.loggedInSubject$.next(value); }
  get user(): User { return this.userInSubject$.getValue(); }
  set user(value: User) { this.userInSubject$.next(value); }
  getToken() { return localStorage.getItem('token'); }

  initAuthorization(): boolean {
    this.loggedIn = this.isAuthanticated;
    if (this.loggedIn) {
      this.initializeUser(this.getToken());
      return true;
    } else {
      localStorage.removeItem('token');
      return false;
    }
  }
  get isAuthanticated(): boolean { return !this.jwt.isTokenExpired(this.getToken()); }

  initializeUser(token: any) {
    if (!token) { return; }
    const info = this.jwt.decodeToken(token);
    this.user = new User();
    this.user.id = info.Id;
    this.user.firstname = info.Name;
    this.user.lastname = info.Lastname;
    this.user.email = info.Email;
  }

  login(username: string, password: string): Observable<boolean> {
    const body: any = { Username: username, Password: password };
    return this.http.post(this.authUrl, body)
      .pipe(map((data: any) => {
        const token = data['token'];
        if (token) {
          localStorage.setItem('token', token);
          this.initializeUser(token);
          this.loggedIn = true;
          return true;
        } else {
          return false;
        }
      }));
  }

  logout() {
    localStorage.removeItem('token');
    this.user = null;
    this.loggedIn = false;
    this.router.navigate(['/login']);
  }

  decodeToken(token: any): any {
    return this.jwt.decodeToken(token);
  }
}
