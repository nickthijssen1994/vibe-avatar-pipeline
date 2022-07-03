import { Component, OnInit } from '@angular/core';
import AnalysisResultRow from 'src/app/models/analysis-result-row';
import VideoNote from 'src/app/models/video-note';
import VideoSection from 'src/app/models/video-section';
import VideoAnalysisData from '../../models/video-analysis-data';

@Component({
  selector: 'app-avatar-video-analysis-result',
  templateUrl: './avatar-video-analysis-result.component.html',
  styleUrls: ['./avatar-video-analysis-result.component.css']
})
export class AvatarVideoAnalysisResultComponent implements OnInit {

  public analysis_results: VideoAnalysisData = new VideoAnalysisData();
  public current_section: number = 0;
  constructor() { }

  ngOnInit(): void {
    this.analysis_results.name = "OpenFace Analysis Results";
    this.analysis_results.image_url = "/assets/eve.png";
    this.analysis_results.value_name = 'Emotion';
    this.analysis_results.analysis_id = 1;
    this.analysis_results.sections = [new VideoSection(1), new VideoSection(3000), new VideoSection(6000)];
    this.analysis_results.sections[0].confidence = 25;
    this.analysis_results.sections[0].results = [
      new AnalysisResultRow("Joy", 5),
      new AnalysisResultRow("Surprise", 40),
      new AnalysisResultRow("Sadness", 60),
      new AnalysisResultRow("Anger", 20),
    ];
    this.analysis_results.sections[1].confidence = 45;
    this.analysis_results.sections[1].results = [
      new AnalysisResultRow("Joy", 70),
      new AnalysisResultRow("Surprise", 75),
      new AnalysisResultRow("Sadness", 35),
      new AnalysisResultRow("Anger", 20),
    ];
    this.analysis_results.sections[2].confidence = 10;
    this.analysis_results.sections[2].results = [
      new AnalysisResultRow("Joy", 70),
      new AnalysisResultRow("Surprise", 2),
      new AnalysisResultRow("Fear", 80),
      new AnalysisResultRow("Anger", 20),
    ];
    this.analysis_results.notes = [
      new VideoNote("Analysis confidence low", 1, "0:00"),
      new VideoNote("This data is mocked", 1, "0:00"),
      new VideoNote("Analysis confidence low", 6000, "3:33"),
      new VideoNote("The face shows both joy and fear", 6500, "3:61"),
    ];

  }

}
