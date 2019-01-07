import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { throwError as observableThrowError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { environment } from '../environments/environment';
import { User, Game } from './model';

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

  getPlayers() {
    return this.http.get<Array<User>>(`${this.url}/user`)
      .pipe(catchError(this.errorHandler));
  }

  externalRegister(user: User) {
    return this.http.post<any>(`${this.url}/auth/external`, user)
      .pipe(catchError(this.errorHandler));
  }

  addGame(game: Game) {
    return this.http.post<Game>(`${this.url}/game`, game)
      .pipe(catchError(this.errorHandler));
  }

  updateGame(game: Game) {
    return this.http.put<Game>(`${this.url}/game`, game)
      .pipe(catchError(this.errorHandler));
  }

  getGame(id: string) {
    return this.http.get<Game>(`${this.url}/game/${id}`)
      .pipe(catchError(this.errorHandler));
  }

  getGames() {
    return this.http.get<Array<Game>>(`${this.url}/game/all`)
      .pipe(catchError(this.errorHandler));
  }

  getRatings() {
    return this.http.get<Array<User>>(`${this.url}/game/ratings`)
      .pipe(catchError(this.errorHandler));
  }

  deleteGame(id: string) {
    return this.http.delete<any>(`${this.url}/game/${id}`)
      .pipe(catchError(this.errorHandler));
  }


  errorHandler(error: HttpErrorResponse) {
    return observableThrowError(error || 'Server Error');
  }
}
