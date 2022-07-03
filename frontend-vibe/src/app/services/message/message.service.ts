import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class MessageService {

  lastMessage: string = "";
  messages: string[] = [];

  add(message: string) {
    this.lastMessage = message;
    this.messages.push(message);
  }

  clear() {
    this.lastMessage = "";
    this.messages = [];
  }
}
