import { Component, OnInit, EventEmitter, Output } from '@angular/core';
import { Friend } from '../../models/friend.model';
import { FriendsService } from '../../services/friends.service';
import { UsersService } from '../../services/users.service';
import { User } from '../../models/user.model';

@Component({
  selector: 'app-list-all-friends',
  templateUrl: './list-all-friends.component.html',
  styleUrls: ['./list-all-friends.component.css']
})
export class ListAllFriendsComponent implements OnInit {

  friends: Friend[] = [];

  users: User[] = [];
  @Output() onSelect: EventEmitter<Friend> = new EventEmitter<Friend>();

  constructor(
    private friendsService: FriendsService,
    private usersService: UsersService) { }

  ngOnInit(): void {
    this.friendsService.all().subscribe(data => {
      this.friends = data;
    });
  }

  fintFriends() {
    this.usersService.getAllUsers().subscribe(users => {
      this.users = users;
    });
  }

  addFriend(userId: string) {
    this.friendsService.add(userId).subscribe(userId => {
      console.log(userId);
    });
  }

  selectFriend(userId: string) {
     this.onSelect.emit(this.friends.find(x => x.userId === userId));
  }
}
