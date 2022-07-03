import { Component, OnInit } from '@angular/core';
import {FileService} from "../../services/file-service/file.service";
import FileModel from "../../models/file-model";

@Component({
  selector: 'app-file-manager',
  templateUrl: './file-manager.component.html',
  styleUrls: ['./file-manager.component.css']
})
export class FileManagerComponent implements OnInit {

  files: FileModel[] = [];

  constructor(private fileUploadService: FileService) { }

  ngOnInit(): void {
    this.fileUploadService.getFiles().subscribe({
      next: (event) => {
        this.files = event;
        console.log(this.files)
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
