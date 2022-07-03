import {Injectable} from '@angular/core';
import {HttpClient, HttpHeaders} from "@angular/common/http";
import {Observable, of} from "rxjs";
import {catchError, map, tap} from 'rxjs/operators';
import {MessageService} from "../message/message.service";
import {UploadRequest} from "./upload-request";
import {environment} from "../../../environments/environment";

@Injectable({
  providedIn: 'root'
})
export class FileService {

  baseApiUrl = "http://localhost:7012"

  constructor(private http: HttpClient, private messageService: MessageService) {
  }

  getFiles(): Observable<any[]> {
    return this.http.get<any[]>(this.baseApiUrl + "/api/File").pipe(
      tap({next: (data) => this.log(JSON.stringify(data))}),
      catchError(this.handleError<any>('getFiles', []))
    );
  }

  getAvatarAnalysis(avatarName: string): Observable<any[]> {
    return this.http.get<any[]>(this.baseApiUrl + "/api/File/" + avatarName).pipe(
      tap({next: (data) => this.log(JSON.stringify(data))}),
      catchError(this.handleError<any>('getAvatarAnalysis', []))
    );
  }

  getFilesFromAvatarAnalysis(avatarName:string, analysisId:string): Observable<any[]> {
    return this.http.get<any[]>(this.baseApiUrl + "/api/File/" + avatarName + "/" + analysisId).pipe(
      tap({next: (data) => this.log(JSON.stringify(data))}),
      catchError(this.handleError<any>('getFilesFromAvatarAnalysis', []))
    );
  }

  upload(uploadRequest: UploadRequest): Observable<any> {
    const formData = new FormData();
    for (let i = 0; i < uploadRequest.files.length; i++) {
      formData.append("files", uploadRequest.files[i], uploadRequest.files[i].name);
    }
    formData.append("avatarName", uploadRequest.avatar);
    formData.append("scenario", uploadRequest.scenario);
    formData.append("algorithm", uploadRequest.algorithm);

    return this.http.post(this.baseApiUrl + "/api/File/Upload", formData, {responseType: "json"}).pipe(
      tap({next: (data) => this.log(JSON.stringify(data))}),
      catchError(this.handleError<any>('upload', []))
    );
  }

  private log(message: string) {
    this.messageService.add(`FileUploadService: ${message}`);
  }

  private handleError<T>(operation = 'operation', result?: T) {
    return (error: any): Observable<T> => {

      console.error(error);

      this.log(`${operation} failed: ${error.error}`);

      return of(result as T);
    };
  }
}
