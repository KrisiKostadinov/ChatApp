import { Component, OnInit } from '@angular/core';
import { FriendsService } from '../../services/friends.service';
import { RequestModel } from '../../models/request-model.model';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-list-all-requests',
  templateUrl: './list-all-requests.component.html',
  styleUrls: ['./list-all-requests.component.css']
})
export class ListAllRequestsComponent implements OnInit {

  requests: RequestModel[];

  subs: Subscription[] = [];

  constructor(private friendsService: FriendsService) { }
  ngOnDestroy(): void {
    this.subs.forEach(sub => {
      sub.unsubscribe();
    });
  }
  
  ngOnInit(): void {
    const requestsSub = this.friendsService.getAllRequests().subscribe(data => {
      this.requests = data;
    })

    this.subs.push(requestsSub);
  }

}
