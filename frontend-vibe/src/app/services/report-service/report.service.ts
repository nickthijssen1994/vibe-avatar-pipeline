import { Injectable } from '@angular/core';
import {HttpClient, HttpHeaders} from "@angular/common/http";
import {MessageService} from "../message/message.service";
import {Observable, of} from "rxjs";
import {catchError, tap} from "rxjs/operators";
import AnalysisResult from "../../models/analysis-result";
import AnalysisInfo from "../../models/analysis-info";
import {environment} from "../../../environments/environment";

@Injectable({
  providedIn: 'root'
})
export class ReportService {

  httpOptions = {
    headers: new HttpHeaders({
      'Content-Type':  'application/json'
    })
  };

  baseApiUrl = environment.apiURL

  constructor(private http: HttpClient, private messageService: MessageService) {
  }

  getAnalysisResults(info: AnalysisInfo): Observable<AnalysisResult> {
    return this.http.get<AnalysisResult>(this.baseApiUrl + "/api/Report/" + info.avatarName + "/" + info.scenario + "/" + info.algorithm).pipe(
      tap({next: (data) => this.log(JSON.stringify(data))}),
      catchError(this.handleError<any>('getAnalysisResults' ))
    );
  }

  private log(message: string) {
    this.messageService.add(`ReportService: ${message}`);
  }

  private handleError<T>(operation = 'operation', result?: T) {
    return (error: any): Observable<T> => {

      console.error(error);

      this.log(`${operation} failed: ${error.error}`);

      return of(result as T);
    };
  }
}
