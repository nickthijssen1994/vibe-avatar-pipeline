import { Component, OnInit } from '@angular/core';
import {MessageService} from "../../services/message/message.service";

@Component({
  selector: 'app-messages-log',
  templateUrl: './messages-log.component.html',
  styleUrls: ['./messages-log.component.css']
})
export class MessagesLogComponent implements OnInit {

  constructor(public messageService: MessageService) {}

  ngOnInit(): void {
  }

}
