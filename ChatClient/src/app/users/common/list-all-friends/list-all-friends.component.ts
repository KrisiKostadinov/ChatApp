import { Component, OnInit } from '@angular/core';
import { Friend } from '../../models/friend.model';
import { FriendsService } from '../../services/friends.service';

@Component({
  selector: 'app-list-all-friends',
  templateUrl: './list-all-friends.component.html',
  styleUrls: ['./list-all-friends.component.css']
})
export class ListAllFriendsComponent implements OnInit {

  friends: Friend[];

  constructor(private friendsService: FriendsService) { }

  ngOnInit(): void {
    this.friendsService.all().subscribe(data => {
      this.friends = data;
    });
  }
}
