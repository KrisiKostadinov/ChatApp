import { Component, OnInit } from '@angular/core';
import { GroupsService } from '../groups.service';
import { Group } from '../models/group.model';

@Component({
  selector: 'app-list-all-groups',
  templateUrl: './list-all-groups.component.html',
  styleUrls: ['./list-all-groups.component.css']
})
export class ListAllGroupsComponent implements OnInit {

  groups: Group[];

  constructor(public groupsService: GroupsService) { }

  ngOnInit(): void {
    this.groupsService.all().subscribe(data => {
      this.groups = data;
    });
  }
}
