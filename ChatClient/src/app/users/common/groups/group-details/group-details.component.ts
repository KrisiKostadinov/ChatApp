import { Component, OnInit } from '@angular/core';
import { GroupsService } from '../groups.service';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-group-details',
  templateUrl: './group-details.component.html',
  styleUrls: ['./group-details.component.css']
})
export class GroupDetailsComponent implements OnInit {

  group;

  constructor(
    public groupService: GroupsService,
    private route: ActivatedRoute) { }

  ngOnInit(): void {
    const id = this.route.snapshot.params['id'];
    this.groupService.byId(id).subscribe(data => {
      this.group = data;
      console.log(data);
    });
  }

}
