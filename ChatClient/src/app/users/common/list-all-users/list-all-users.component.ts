import { Component, OnInit } from '@angular/core';
import { UsersService } from '../../services/users.service';
import { User } from '../../models/user.model';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';

@Component({
  selector: 'app-list-all-users',
  templateUrl: './list-all-users.component.html',
  styleUrls: ['./list-all-users.component.css']
})
export class ListAllUsersComponent implements OnInit {

  users: User[];
  displayedUsers: User[];

  searchForm: FormGroup;

  constructor(
    private usersService: UsersService,
    private fb: FormBuilder) {
      this.searchForm = this.fb.group({
        'search': ['', [Validators.required]]
      });
    }

  ngOnInit(): void {
    this.usersService.getAllUsers().subscribe(data => {
      this.users = data;
      this.displayedUsers = this.users;
    });
  }

  search(text: string = null) {
    this.displayedUsers = this.users.filter(x => x.userName.includes(text));
  }
}
