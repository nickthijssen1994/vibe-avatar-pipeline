import {Component, OnInit} from '@angular/core';
import AnalysisModel from "../../models/analysis-model";
import {ActivatedRoute} from "@angular/router";
import {FileService} from "../../services/file-service/file.service";
import ContainerModel from "../../models/container-model";
import {AvatarService} from "../../services/avatar-service/avatar.service";
import AvatarModel from "../../models/avatar.model";

@Component({
  selector: 'app-avatar-details',
  templateUrl: './avatar-details.component.html',
  styleUrls: ['./avatar-details.component.css']
})
export class AvatarDetailsComponent implements OnInit {

  public avatar: string;
  avatarModel: AvatarModel;
  date: Date = new Date();

  constructor(private route: ActivatedRoute, private avatarService: AvatarService) {
  }

  ngOnInit() {
    this.avatar = this.route.snapshot.paramMap.get("avatar");
    this.avatarService.getAvatar(this.avatar).subscribe({
      next: (event) => {
        this.avatarModel = event;
        console.log(this.avatarModel)
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
}
