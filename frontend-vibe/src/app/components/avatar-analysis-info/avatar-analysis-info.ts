import {Component, Input, OnInit} from '@angular/core';
import {ActivatedRoute, Router} from '@angular/router';
import {FileService} from "../../services/file-service/file.service";
import {AvatarService} from "../../services/avatar-service/avatar.service";
import AvatarModel from "../../models/avatar.model";
import AnalysisModel from "../../models/analysis-model";
import FileModel from "../../models/file-model";

@Component({
  selector: 'app-avatar-analysis-info',
  templateUrl: './avatar-analysis-info.html',
  styleUrls: ['./avatar-analysis-info.css']
})

export class AvatarAnalysisInfoComponent implements OnInit {

  imageFileSelected: boolean = false;
  videoFileSelected: boolean = false;
  filePath: string = '';
  @Input() avatar = '';
  @Input() analysis = '';

  avatarModel: AvatarModel;
  analysisModel: AnalysisModel;

  public id: string;
  public files: any[];

  constructor(private route: ActivatedRoute, private router: Router, private avatarService: AvatarService, private avatarFileService: FileService) {
  }

  ngOnInit() {
    this.avatar = this.route.snapshot.paramMap.get("avatar");
    this.analysis = this.route.snapshot.paramMap.get("analysis");
    this.id = this.route.snapshot.paramMap.get("id");

    this.avatarService.getAvatar(this.avatar).subscribe({
      next: (event) => {
        this.avatarModel = event;
        this.analysisModel = this.avatarModel.analysis.find(e => e.id == Number(this.analysis));
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

  onFileChanged(newFilePath: string) {
    if (newFilePath.endsWith(".png") || newFilePath.endsWith(".jpeg") || newFilePath.endsWith(".jpg")) {
      this.imageFileSelected = true;
      this.videoFileSelected = false;
    } else if (newFilePath.endsWith(".mp4") || newFilePath.endsWith(".mkv") || newFilePath.endsWith(".avi")) {
      this.imageFileSelected = false;
      this.videoFileSelected = true;
    } else {
      this.imageFileSelected = false;
      this.videoFileSelected = false;
    }
    this.filePath = newFilePath;
  }

  public goBack() {
    this.router.navigate(['../avatars/' + this.avatarModel.id]);
  }
}
