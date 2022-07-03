import { Component, OnInit } from '@angular/core';
import AnalysisResultRow from 'src/app/models/analysis-result-row';
import ImageAnalysisData from '../../models/image-analysis-data';

@Component({
  selector: 'app-avatar-image-analysis-result',
  templateUrl: './avatar-image-analysis-result.component.html',
  styleUrls: ['./avatar-image-analysis-result.component.css']
})
export class AvatarImageAnalysisResultComponent implements OnInit {

  public analysis_results: ImageAnalysisData = new ImageAnalysisData();
  constructor() { }

  ngOnInit(): void {
    this.analysis_results.analysis_name = "OpenFace Analysis Results";
    this.analysis_results.image_url = "/assets/eve.png";
    this.analysis_results.value_name = 'Emotion';
    this.analysis_results.analysis_id = 1;
    this.analysis_results.confidence = 25;
    this.analysis_results.results = [
      new AnalysisResultRow("Joy", 5),
      new AnalysisResultRow("Surprise", 40),
      new AnalysisResultRow("Sadness", 60),
      new AnalysisResultRow("Anger", 20),
    ];
    this.analysis_results.notes = [
      "Analysis confidence low",
      "this data is mocked"
    ];

  }

}
