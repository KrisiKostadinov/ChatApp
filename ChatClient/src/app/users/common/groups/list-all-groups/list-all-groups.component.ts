import { Component, OnInit, Input } from '@angular/core';
import { GroupsService } from '../groups.service';
import { Group } from '../models/group.model';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { SharedService } from 'src/app/common/services/shared.service';
import { User } from 'src/app/users/models/user.model';

@Component({
  selector: 'app-list-all-groups',
  templateUrl: './list-all-groups.component.html',
  styleUrls: ['./list-all-groups.component.css']
})
export class ListAllGroupsComponent implements OnInit {

  myGroupsList: Group[] = [];
  displayedGroups: Group[] = [];
  allGroupsList: Group[] = [];

  searchForm: FormGroup;
  @Input() myGroups;
  
  user: User;

  constructor(
    public groupsService: GroupsService,
    private fb: FormBuilder) {
    this.searchForm = this.fb.group({
      'text': ['', [Validators.required]]
    });
  }

  ngOnInit(): void {
    this.user = JSON.parse(localStorage.getItem('user'));
    this.getAllGroups().then(data => {
      this.allGroupsList = data;
      this.myGroupsList = this.getMyGroups();
      this.checkForGroupsList();
    });
  }
  
  checkForGroupsList() {
    if(this.myGroups === true) {
      this.displayedGroups = this.myGroupsList;
    } else {
      this.displayedGroups = this.allGroupsList;
    }
    
    return this.myGroups;
  }

  getAllGroups() {
    return this.groupsService.all().toPromise();
  }
  
  getMyGroups() {
    return this.allGroupsList.filter(x => x.ownerId === this.user.id);
  }

  search(text: string) {
    if(this.myGroups) {
      this.displayedGroups = this.myGroupsList.filter(x => x.subject.includes(text));
    } else {
      this.displayedGroups = this.allGroupsList.filter(x => x.subject.includes(text));
    }
  }
}
