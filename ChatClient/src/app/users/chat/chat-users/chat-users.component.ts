import { Component, OnInit, OnDestroy, ViewChild, ElementRef, AfterViewChecked } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import * as signalR from "@aspnet/signalr";
import { environment } from 'src/environments/environment';
import { MessageModel } from '../../models/message-model.model';
import { Friend } from '../../models/friend.model';
import { FriendsService } from '../../services/friends.service';
import { User } from '../../models/user.model';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-chat-users',
  templateUrl: './chat-users.component.html',
  styleUrls: ['./chat-users.component.css']
})
export class ChatUsersComponent implements OnInit, OnDestroy, AfterViewChecked {

  connection: signalR.HubConnection;
  chatForm: FormGroup;
  messages: MessageModel[] = [];
  friends: Friend[] = [];

  user: User;

  currentFriend: Friend = new Friend();

  @ViewChild('scroll') private scroll: ElementRef;
  initialUserId: string;

  constructor(
    private fb: FormBuilder,
    private friendsService: FriendsService,
    private route: ActivatedRoute) {
    this.chatForm = this.fb.group({
      'text': ['', [Validators.required]]
    });
  }

  get text() {
    return this.chatForm.get('text');
  }

  selectedFriend(friend: Friend) {
    this.initialUserId = friend.userId;

    for(let i = 0; i < this.friends.length; i++) {
      if(this.friends[i].userId === friend.userId) {
        this.currentFriend = this.friends[i];
        console.log(this.friends);
      }
    }
    
    this.getAllMyMessages(this.initialUserId).then(data => {
      this.messages = data;
    });
  }

  ngAfterViewChecked() {        
    this.scrollToBottom();
  } 

  public scrollToBottom() {
    try {
      this.scroll.nativeElement.scrollTop = this.scroll.nativeElement.scrollHeight - this.scroll.nativeElement.offsetHeight;
    } catch {
    }
  }
  
  ngOnDestroy(): void {
    this.connection.stop();
  }

  getFriends() {
    return this.friendsService.all().toPromise();
  }

  getAllMyMessages(friendUserId: string = null) {
    return this.friendsService.gerAllMyMessages(this.user.id, friendUserId).toPromise();
  }

  ngOnInit(): void {
    this.user = JSON.parse(localStorage.getItem('user'));

    this.initialUserId = this.route.snapshot.params['id'];
    this.getAllMyMessages(this.initialUserId).then(data => {
      this.messages = data;
    });
    this.connect();
    this.getMessages();
    this.getFriends().then(data => {
      this.friends = data;
      this.currentFriend = this.friends.find(x => x.userId === this.initialUserId);
      console.log(this.currentFriend);
    });
  }

  getMessages() {
    this.connection.on('ReceiveMsg', (receiverUserName, receiverId, senderUserName, senderId, content) => {
      var model = new MessageModel(receiverUserName, senderUserName, receiverId, senderId, content);
      this.messages.push(model);
    });
    
    this.connection.on('UserConnected', (connectionId, userId) => {
      for(let i = 0; i < this.friends.length; i++) {
        if(this.friends[i].userId === userId) {
          this.friends[i].liveOn = true;
          this.friends[i].connectionId = connectionId;
          this.currentFriend.liveOn = true;
          this.currentFriend.connectionId = connectionId;
        }
      }
    });
    this.connection.on('UserDisconnected', (userId) => {
      for(let i = 0; i < this.friends.length; i++) {
        if(this.friends[i].userId === userId) {
          this.friends[i].liveOn = false;
          this.friends[i].connectionId = '';
        }
      }
    });
  }

  connect() {
    this.connection = new signalR.HubConnectionBuilder()
    .withUrl(environment.apiUrl + `users/chat/`, {
      accessTokenFactory: () => localStorage.getItem('token')
    })
    .build();
    if(this.connection.state === signalR.HubConnectionState.Disconnected) {
      this.connection.start().then(() => {
        console.log('Started connection');
      });
    }
  }

  send() {
    if(this.chatForm.valid) {
      this.connection.invoke('SendMessageToUser', this.currentFriend.connectionId, this.chatForm.value.text).then(() => {
        this.chatForm.reset();
        try {
          this.scrollToBottom();
        } catch {
        }
      });
    } else {
      this.chatForm.get('text').setErrors(['danger']);
    }
  }
}
