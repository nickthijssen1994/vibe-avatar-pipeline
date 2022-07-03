import { Component, OnInit } from '@angular/core';
import {AvatarService} from "../../services/avatar-service/avatar.service";
import AnalysisModel from "../../models/analysis-model";
import FileModel from "../../models/file-model";
import AvatarModel from "../../models/avatar.model";

@Component({
  selector: 'app-avatar-editor',
  templateUrl: './avatar-editor.component.html',
  styleUrls: ['./avatar-editor.component.css']
})
export class AvatarEditorComponent implements OnInit {
  // @ts-ignore
  file: File = null;
  avatarName: string = '';

  constructor(private avatarService: AvatarService) { }

  ngOnInit(): void {
  }

  // @ts-ignore
  onChange(event) {
    this.file = event.target.files[0];
  }

  onSubmit() {
    var newAvatar = new AvatarModel(0, this.avatarName)
    this.avatarService.addAvatar(newAvatar).subscribe({
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
}
