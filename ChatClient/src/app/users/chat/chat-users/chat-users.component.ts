import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import * as signalR from "@aspnet/signalr";
import { environment } from 'src/environments/environment';
import { FriendModel } from '../../models/friend-model.model';
import { MessageModel } from '../../models/message-model.model';
import { User } from '../../models/user.model';

@Component({
  selector: 'app-chat-users',
  templateUrl: './chat-users.component.html',
  styleUrls: ['./chat-users.component.css']
})
export class ChatUsersComponent implements OnInit {

  connection: signalR.HubConnection;
  chatForm: FormGroup;
  messages: MessageModel[] = [];
  friend: FriendModel = new FriendModel(null, null);
  user;

  constructor(private fb: FormBuilder) {
    this.chatForm = this.fb.group({
      'text': ['', [Validators.required]]
    });
  }

  ngOnInit(): void {
    this.user = JSON.parse(localStorage.getItem('user'));
    this.connection = new signalR.HubConnectionBuilder()
    .withUrl(environment.apiUrl + "users/chat", {
      accessTokenFactory: () => localStorage.getItem('token')
    })
    .build();
    this.connect();
    this.getMessages();
  }

  getMessages() {
    this.connection.on('ReceiveMsg', (userName, content) => {
      var model = new MessageModel(userName, content);
      this.messages.push(model);
      console.log(userName, content);
    });
    this.connection.on('UserConnected', (connectionId, userId) => {
      this.friend.userName = userId;
      this.friend.connectionId = connectionId;

      console.log(this.friend);
    });
  }

  connect() {
    if (this.connection.state === signalR.HubConnectionState.Disconnected) {
      this.connection.start().then(() => {
        console.log('Started connection');
      }).catch(err => console.log(err));
    }
  }

  send() {
    this.connection.invoke('SendMessageToUser', this.friend.connectionId, this.chatForm.value.text);
  }
}
