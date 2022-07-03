import { Component, OnInit } from '@angular/core';
import {AvatarService} from "../../services/avatar-service/avatar.service";
import AvatarModel from "../../models/avatar.model";

@Component({
  selector: 'avatar-list',
  templateUrl: './avatar-list.component.html',
  styleUrls: ['./avatar-list.component.css']
})

export class AvatarListComponent implements OnInit{

  avatars: AvatarModel[] = [];
  date : Date = new Date();

  constructor(private avatarService: AvatarService) { }

  ngOnInit(): void {
  this.avatarService.getAvatars().subscribe({
      next: (event) => {
        this.avatars = event;
        console.log(this.avatars)
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
