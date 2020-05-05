import { Component, OnInit, EventEmitter, Output, Input } from '@angular/core';
import { Friend } from '../../models/friend.model';
import { FriendsService } from '../../services/friends.service';
import { UsersService } from '../../services/users.service';
import { User } from '../../models/user.model';
import { RequestModel } from '../../models/request-model.model';
import { SharedService } from 'src/app/common/services/shared.service';

@Component({
  selector: 'app-list-all-friends',
  templateUrl: './list-all-friends.component.html',
  styleUrls: ['./list-all-friends.component.css']
})
export class ListAllFriendsComponent implements OnInit {

  @Input() friends: Friend[] = [];
  users: User[] = [];
  logedUser: any;
  
  @Output() onSelect: EventEmitter<Friend> = new EventEmitter<Friend>();
  @Output() onUpdated: EventEmitter<RequestModel[]> = new EventEmitter<RequestModel[]>();

  constructor(
    private friendsService: FriendsService,
    private usersService: UsersService,
    private sharedService: SharedService) {
      
    }

  ngOnInit(): void {
    this.logedUser = JSON.parse(localStorage.getItem('user'));

    this.friendsService.all().subscribe(data => {
      this.friends = data;
    });

    this.sharedService.getClickEvent().subscribe(data => {
      this.fintFriends();
      console.log('send event');
    });
  }

  fintFriends() {
    this.getAllUsers().then(users => {
      this.getAllMyRequests().then(requests => {
        this.updateRequests(users, requests);
      });
    });
  }

  updateRequests(users: User[], requests: RequestModel[]) {
    this.users = [];
    users.forEach(user => {
      user.isFriends = this.friends.find(x => x.userId === user.userId)?.userId ? true : false;
      let userId = requests.find(x => x.userIdTo === user.userId && x.userIdTo !== this.logedUser.id);
      if(userId) {
        user.isRequested = true;
      }
      if(user) {
        this.users.push(user);
      }
    });
    this.onUpdated.emit(requests);
  }

  getAllMyRequests() {
    return this.friendsService.getAllMyRequests().toPromise();
  }

  getAllUsers() {
    return this.usersService.getAllUsers().toPromise();
  }

  addRequest(userId: string) {
    this.friendsService.addRequest(userId).subscribe(data => {
      this.getAllMyRequests().then(requests => {
        this.updateRequests(this.users, requests);
      });
    });
  }

  selectFriend(userId: string) {
     this.onSelect.emit(this.friends.find(x => x.userId === userId));
  }
}
