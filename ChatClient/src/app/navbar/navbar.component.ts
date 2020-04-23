import { Component, OnInit } from '@angular/core';
import { UsersService } from '../users/services/users.service';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent implements OnInit {

  constructor(public usersService: UsersService) {
    
  }

  ngOnInit() {
    
  }

  logout() {
    this.usersService.logout();
  }
}
