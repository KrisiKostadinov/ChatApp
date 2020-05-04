import { Component, OnInit, OnDestroy, Input } from '@angular/core';
import { FriendsService } from '../../services/friends.service';
import { RequestModel } from '../../models/request-model.model';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-list-all-my-requests',
  templateUrl: './list-all-my-requests.component.html',
  styleUrls: ['./list-all-my-requests.component.css']
})
export class ListAllMyRequestsComponent implements OnInit, OnDestroy {

  @Input() myRequests: RequestModel[];
  

  subs: Subscription[] = [];

  constructor(private friendsService: FriendsService) { }
  ngOnDestroy(): void {
    this.subs.forEach(sub => {
      sub.unsubscribe();
    });
  }

  ngOnInit(): void {
    const myRequestsSub = this.friendsService.getAllMyRequests().subscribe(data => {
      this.myRequests = data;
    })

    this.subs.push(myRequestsSub);
  }

  updatingFriends(isUpdating: boolean) {
    console.log(isUpdating);
  }
}
