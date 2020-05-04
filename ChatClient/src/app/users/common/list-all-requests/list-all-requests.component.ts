import { Component, OnInit, EventEmitter, Output } from '@angular/core';
import { FriendsService } from '../../services/friends.service';
import { RequestModel } from '../../models/request-model.model';
import { Subscription } from 'rxjs';
import { Friend } from '../../models/friend.model';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-list-all-requests',
  templateUrl: './list-all-requests.component.html',
  styleUrls: ['./list-all-requests.component.css']
})
export class ListAllRequestsComponent implements OnInit {

  requests: RequestModel[];

  subs: Subscription[] = [];

  @Output() onUpdated: EventEmitter<boolean> = new EventEmitter<boolean>();

  constructor(
    private friendsService: FriendsService,
    private toastrService: ToastrService) { }
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

  reject(userId: string) {

  }

  confirm(userId: string) {
    this.friendsService.confirm(userId).subscribe(data => {
      // this.dismiss(userId).then(data => {
        
      // });
      this.onUpdated.emit(data);
        this.toastrService.success('', 'Confirmed successfully', {
          closeButton: true,
          tapToDismiss: true,
        });
    });
  }

  // dismiss(userId: string) {
  //   return this.friendsService.dismiss(userId).toPromise();
  // }
}
