import {Component, OnInit, Input} from '@angular/core';
import {ActivatedRoute} from '@angular/router';
import {VIDEOERRORS} from 'src/app/mocks/mock-avatar-video-analysis';

@Component({
  selector: 'avatar-video-analysis',
  templateUrl: './avatar-video-analysis.component.html',
  styleUrls: ['./avatar-video-analysis.component.css']
})

export class AvatarVideoAnalysisComponent implements OnInit {

  @Input() avatar = '';
  @Input() analysis = '';
  @Input() filePath = '';
  public id: any;
  public videoErrors: any;

  constructor(private route: ActivatedRoute) {
  }

  ngOnInit() {
    this.id = this.route.snapshot.paramMap.get("id");
    this.videoErrors = VIDEOERRORS;
  }
}
