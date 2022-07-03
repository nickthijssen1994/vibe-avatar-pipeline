import {Component, Input, OnInit} from '@angular/core';
import {ActivatedRoute} from "@angular/router";
import {FileService} from "../../services/file-service/file.service";
import AnalysisModel from "../../models/analysis-model";
import AvatarModel from "../../models/avatar.model";

@Component({
  selector: 'app-avatar-analysis-list',
  templateUrl: './avatar-analysis-list.component.html',
  styleUrls: ['./avatar-analysis-list.component.css']
})
export class AvatarAnalysisListComponent implements OnInit {

  @Input() avatar: AvatarModel;
  date : Date = new Date();

  constructor() {}

  ngOnInit() {
  }
}
