import {Component, OnInit, Input, OnDestroy, ChangeDetectorRef} from '@angular/core';
import {Router, ActivatedRoute, ParamMap} from '@angular/router';
import {IMAGEERRORS} from 'src/app/mocks/mock-avatar-image-analysis';
import {ReportService} from "../../services/report-service/report.service";
import {AvatarService} from "../../services/avatar-service/avatar.service";
import AnalysisInfo from "../../models/analysis-info";
import AnalysisResult from "../../models/analysis-result";
import AvatarModel from "../../models/avatar.model";
import AnalysisModel from "../../models/analysis-model";
import AnalysisResultRow from 'src/app/models/analysis-result-row';
import { Observable, interval } from 'rxjs'

@Component({
  selector: 'avatar-image-analysis',
  templateUrl: './avatar-image-analysis.component.html',
  styleUrls: ['./avatar-image-analysis.component.css']
})

export class AvatarImageAnalysisComponent implements OnInit {

  @Input() avatarModel: AvatarModel;
  @Input() analysisModel: AnalysisModel;
  @Input() info: AnalysisInfo;
  result: AnalysisResult;
  @Input() avatar = '';
  @Input() analysis = '';
  @Input() filePath = '';
  public id: string;
  public files: any;
  public myInterval: any;
  public imageErrors: any;
  results : AnalysisResultRow[] = [];
  constructor(private route: ActivatedRoute, private reportService: ReportService, private avatarService: AvatarService, private cd: ChangeDetectorRef ) {
  }
  
  

 ngOnInit() {
    this.avatar = this.route.snapshot.paramMap.get("avatar");
    this.analysis = this.route.snapshot.paramMap.get("analysis");

   
    /*interval(5000).subscribe(x => {
      this.getResults()
    })*/
    this.getResults()
    
  }
  
  getResults(){
    //if(this.avatarModel == undefined || this.analysisModel == undefined){
      console.log("avatarmodel or analysismodel is undifined")
      this.avatarService.getAvatar(this.avatar).subscribe({
        next: (event) => {
          this.avatarModel = event;
          this.analysisModel = this.avatarModel.analysis.find(e => e.id == Number(this.analysis));
          console.log(this.avatarModel, "forReportService")
          console.log(this.analysisModel, "forReportService")
          if (typeof (event) === 'object') {
          }
        },
        error: (e) => {
          console.log(e)
          if (typeof (e) === 'object') {
          }
        },
        complete: () => {
          this.getReport();
        }
        
      })
    //}
    /*else{
      console.log(this.avatarModel, "using in report service")
      console.log(this.analysisModel, "using in report service")
      this.info = new AnalysisInfo(this.avatarModel.name, this.analysisModel.algorithm, this.analysisModel.scenario)
      this.reportService.getAnalysisResults(this.info).subscribe({
      next: (event) => {
        this.result = event;
        console.log(this.result, "Report results")
        if (typeof (event) === 'object') {
        }
      },
      error: (e) => {
        console.log(e)
        if (typeof (e) === 'object') {
        }
      }
    })
    }*/
  }

  getReport(){
      console.log(this.avatarModel, "using in report service")
      console.log(this.analysisModel, "using in report service")
      this.info = new AnalysisInfo(this.avatarModel.name, this.analysisModel.algorithm, this.analysisModel.scenario)
      this.reportService.getAnalysisResults(this.info).subscribe({
      next: (event) => {
        console.log(event)
        console.log(event.results)
        this.results = event.results;
        console.log(this.results, "Report results")
        //this.results = this.result.Results
        this.cd.detectChanges();
        if (typeof (event) === 'object') {
        }
      },
      error: (e) => {
        console.log(e)
        if (typeof (e) === 'object') {
        }
      }
    })
  }
  ngOnDestroy(){
    clearInterval
  }
  
}
