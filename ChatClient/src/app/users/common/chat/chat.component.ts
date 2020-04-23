import { Component, OnInit } from '@angular/core';
import { environment } from 'src/environments/environment';
import { UsersService } from '../../services/users.service';
import * as signalR from "@aspnet/signalr";
import { FormGroup, FormBuilder, Validators } from '@angular/forms';

@Component({
  selector: 'app-chat',
  templateUrl: './chat.component.html',
  styleUrls: ['./chat.component.css']
})
export class ChatComponent implements OnInit {

  connection: signalR.HubConnection;
  messages: string[] = [];
 
  testForm: FormGroup;
  users: string[] = [];

  constructor(
    public usersService: UsersService,
    private formBuilder: FormBuilder) {
      this.testForm = this.formBuilder.group({
        'text': ['', Validators.required],
        'to': ['']
      });
  }
  
  public connect() {
    if (this.connection.state === signalR.HubConnectionState.Disconnected) {
      this.connection.start().then(() => {
        console.log('Started connection');
      }).catch(err => console.log(err));
    }
  }
  
  public getMessages() {
      this.connection.on('ReceiveMessage', (data) => {
        this.messages.push(data);
        console.log(data);
      });
      
      this.connection.on('UserConnected', (data) => {
        console.log(data);
        this.users.push(data);
      });
  }
  
  public disconnect() {
    this.connection.stop();
  }

  ngOnInit() {
    this.connection = new signalR.HubConnectionBuilder()
    .withUrl(environment.apiUrl + "users/chat", {
      accessTokenFactory: () => localStorage.getItem('token')
    })
    .build();
    this.connect();
    this.getMessages();
  }

  sendToAll() {
    if(this.testForm.value.to !== 'all') {
      this.connection.invoke('SendMessageToUser', this.testForm.value.to, this.testForm.value.text).then(data => {
        console.log('SendMessageToAll ' + data);
      });
    } else {
      this.connection.invoke('SendMessageToAll', this.testForm.value.text).then(data => {
        console.log('SendMessageToAll ' + data);
      });
    }
  }
}
