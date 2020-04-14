import { Component, OnInit } from '@angular/core';
import { UsersService } from '../../services/users.service';

@Component({
  selector: 'app-list-all-users',
  templateUrl: './list-all-users.component.html',
  styleUrls: ['./list-all-users.component.css']
})
export class ListAllUsersComponent implements OnInit {

  users;

  constructor(private usersService: UsersService) { }

  ngOnInit(): void {
    this.usersService.getAllUsers().subscribe(data => {
      this.users = data;
    });
  }

}
