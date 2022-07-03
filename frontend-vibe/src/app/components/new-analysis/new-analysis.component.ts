import {Component, OnInit} from '@angular/core';
import {FileService} from "../../services/file-service/file.service";
import {AvatarService} from "../../services/avatar-service/avatar.service";
import AvatarModel from "../../models/avatar.model";
import {UploadRequest} from "../../services/file-service/upload-request";
import {UploadResponse} from "../../services/file-service/upload-response";
import AnalysisModel from "../../models/analysis-model";
import FileModel from "../../models/file-model";
import FileType from "../../models/file-type";

@Component({
  selector: 'file-service',
  templateUrl: './new-analysis.component.html',
  styleUrls: ['./new-analysis.component.css']
})
export class NewAnalysisComponent implements OnInit {

  algorithms: string[] = [ "OpenFace", "Amazon Rekognition", "Cognitec", "DeepVision AI"];
  selectedAlgorithm: string = this.algorithms[0];
  scenarios: string[] = [ "Face", "Emotion", "Eyes"];
  selectedScenario: string = this.scenarios[0];
  selected: string = '';
  avatars: AvatarModel[] = [];
  selectedAvatar: AvatarModel;
  description: string = '';
  avatarName: string = 'Eve';
  message: string = "";
  downloadLink: string = "";
  loading: boolean = false;
  // @ts-ignore
  file: File = null;
  files: FileList;

  uploadResponse: UploadResponse = null;

  constructor(private avatarService: AvatarService, private fileUploadService: FileService) {
  }

  ngOnInit(): void {
    console.log(this.avatarService.getAvatars().subscribe({
      next: (event) => {
        this.avatars = event;
        this.selectedAvatar = this.avatars[0];
        this.selected = this.selectedAvatar.name;
        console.log(this.avatars)
        if (typeof (event) === 'object') {
        }
      },
      error: (e) => {
        console.log(e)
        if (typeof (e) === 'object') {
        }
      }
    }))
  }

  // @ts-ignore
  onChange(event) {
    console.log(event);
    this.file = event.target.files[0];
    this.files = event.target.files;
  }

  // @ts-ignore
  onSelectedChange() {
    this.avatarName = this.selected;
    this.selectedAvatar = this.avatars.find(e => e.name == this.avatarName);
  }

  // @ts-ignore
  onSelectedScenarioChange() {

  }

  // @ts-ignore
  onSelectedAnalysisChange() {

  }

  startAnalysis() {
      // var newAnalysis = new AnalysisModel();
      // newAnalysis.id = 0;
      // newAnalysis.algorithm = this.selectedAlgorithm;
      // newAnalysis.scenario = this.selectedScenario;
      // newAnalysis.description = this.description;
      //
      // var newFile = new FileModel()
      // newFile.id = 0;
      // newFile.name = this.file.name;
      // newFile.fileType = FileType.Image;
      // newFile.downloadLink = this.downloadLink;
      //
      // newAnalysis.files.push(newFile);
      //
      // this.selectedAvatar.analysis = [];
      // this.selectedAvatar.analysis.push(newAnalysis)

      console.log(this.selectedAvatar);

      this.avatarService.startAvatarAnalysis(this.selectedAvatar.name, this.selectedScenario, this.selectedAlgorithm).subscribe({
        next: (event) => {
          console.log(event)
          if (typeof (event) === 'object') {
          }
        },
        error: (e) => {
          console.log(e)
          if (typeof (e) === 'object') {
          }
        }
      });
  }

  onUpload() {
    this.loading = !this.loading;

    let uploadRequest: UploadRequest = {
      avatar: this.selectedAvatar.name,
      scenario: this.selectedScenario,
      algorithm: this.selectedAlgorithm,
      files: this.files
    };

    console.log(uploadRequest.files);

    this.fileUploadService.upload(uploadRequest).subscribe({
      next: (event) => {
        console.log(event)
        if (typeof (event) === 'object') {
          this.uploadResponse = event;
          this.message = this.uploadResponse.message;
          this.loading = false;
        }
      },
      error: (e) => {
        console.log(e)
        if (typeof (e) === 'object') {
          this.message = JSON.stringify(e);
          console.log(this.message)
          this.loading = false;
        }
      }
    });
  }
}
