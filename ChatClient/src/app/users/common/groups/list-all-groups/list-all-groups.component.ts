import { Component, OnInit } from '@angular/core';
import { GroupsService } from '../groups.service';
import { Group } from '../models/group.model';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';

@Component({
  selector: 'app-list-all-groups',
  templateUrl: './list-all-groups.component.html',
  styleUrls: ['./list-all-groups.component.css']
})
export class ListAllGroupsComponent implements OnInit {

  groups: Group[] = [];
  displayedGroups: Group[] = [];

  searchForm: FormGroup;

  constructor(
    public groupsService: GroupsService,
    private fb: FormBuilder) {
    this.searchForm = this.fb.group({
      'text': ['', [Validators.required]]
    });
  }

  ngOnInit(): void {
    this.groupsService.all().subscribe(data => {
      this.groups = data;
      this.displayedGroups = this.groups;
    });
  }

  search(text: string) {
    this.displayedGroups = this.groups.filter(x => x.subject.includes(text));
  }
}
