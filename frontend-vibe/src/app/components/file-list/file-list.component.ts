import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {ActivatedRoute, Router} from "@angular/router";
import {FileService} from "../../services/file-service/file.service";
import {AvatarService} from "../../services/avatar-service/avatar.service";
import AnalysisModel from "../../models/analysis-model";
import FileModel from "../../models/file-model";

@Component({
  selector: 'app-file-list',
  templateUrl: './file-list.component.html',
  styleUrls: ['./file-list.component.css']
})

export class FileListComponent implements OnInit {

  @Output() newFileSelectedEvent = new EventEmitter<string>();
  @Input() fileModels: FileModel[];
  @Input() avatar = '';
  @Input() analysis = '';
  selectedFile: FileModel;
  public id: string;
  public files: any;
  date: Date = new Date();

  constructor(private route: ActivatedRoute) {
  }

  ngOnInit() {
    this.id = this.route.snapshot.paramMap.get("id");
    if(this.fileModels.length > 0){
      this.selectedFile = this.fileModels[0];
      this.onFileSelectedChange(this.selectedFile.downloadLink);
    }
  }

  onFileSelectedChange(value: string) {
    this.newFileSelectedEvent.emit(value);
  }
}
