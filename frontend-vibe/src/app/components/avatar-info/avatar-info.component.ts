import {Component, Input, OnInit} from '@angular/core';
import ContainerModel from "../../models/container-model";
import AvatarModel from "../../models/avatar.model";

@Component({
  selector: 'app-avatar-info',
  templateUrl: './avatar-info.component.html',
  styleUrls: ['./avatar-info.component.css']
})
export class AvatarInfoComponent implements OnInit {

  @Input() avatar: AvatarModel;
  date : Date = new Date();

  constructor() { }

  ngOnInit(): void {

  }

}
