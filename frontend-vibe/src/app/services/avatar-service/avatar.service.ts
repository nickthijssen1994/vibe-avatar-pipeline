import { Injectable } from '@angular/core';
import {HttpClient, HttpHeaders} from "@angular/common/http";
import {MessageService} from "../message/message.service";
import {Observable, of} from "rxjs";
import {catchError, tap} from "rxjs/operators";
import AvatarModel from "../../models/avatar.model";
import {environment} from "../../../environments/environment";

@Injectable({
  providedIn: 'root'
})
export class AvatarService {

  httpOptions = {
    headers: new HttpHeaders({
      'Content-Type':  'application/json'
    })
  };

  baseApiUrl = environment.apiURL

  constructor(private http: HttpClient, private messageService: MessageService) {
  }

  startAvatarAnalysis(avatarName: string, scenario: string, algorithm: string): Observable<any[]> {
    return this.http.get<any[]>(this.baseApiUrl + "/api/Analysis/" + avatarName + "/" + scenario + "/" + algorithm).pipe(
      tap({next: (data) => this.log(JSON.stringify(data))}),
      catchError(this.handleError<any>('getAvatars', []))
    );
  }

  getAvatars(): Observable<any[]> {
    return this.http.get<any[]>(this.baseApiUrl+ "/api/Avatar").pipe(
      tap({next: (data) => this.log(JSON.stringify(data))}),
      catchError(this.handleError<any>('getAvatars', []))
    );
  }

  getAvatar(id: string): Observable<AvatarModel> {
    return this.http.get<AvatarModel>(this.baseApiUrl + "/api/Avatar/" + id).pipe(
      tap({next: (data) => this.log(JSON.stringify(data))}),
      catchError(this.handleError<any>('getAvatar' ))
    );
  }

  addAvatar(avatar: AvatarModel): Observable<AvatarModel> {
    return this.http.post<AvatarModel>(this.baseApiUrl + "/api/Avatar", avatar, this.httpOptions)
      .pipe(
        tap({next: (data) => this.log(JSON.stringify(data))}),
        catchError(this.handleError('addAvatar', avatar))
      );
  }

  updateAvatar(avatar: AvatarModel): Observable<AvatarModel> {
    console.log(JSON.stringify(avatar));
    return this.http.put<AvatarModel>(this.baseApiUrl + "/api/Avatar/" + avatar.id, avatar, this.httpOptions)
      .pipe(
        tap({next: (data) => this.log(JSON.stringify(data))}),
        catchError(this.handleError('updateAvatar', avatar))
      );
  }

  deleteAvatar(id: number): Observable<unknown> {
    const url = `${this.baseApiUrl + "/api/Avatar/"}${id}`;
    return this.http.delete(url, )
      .pipe(
        catchError(this.handleError('deleteAvatar'))
      );
  }

  private log(message: string) {
    this.messageService.add(`AvatarService: ${message}`);
  }

  private handleError<T>(operation = 'operation', result?: T) {
    return (error: any): Observable<T> => {

      console.error(error);

      this.log(`${operation} failed: ${error.error}`);

      return of(result as T);
    };
  }
}
