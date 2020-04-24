import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { User } from '../../models/user.model';
import { UsersService } from '../../services/users.service';

@Component({
  selector: 'app-user-details',
  templateUrl: './user-details.component.html',
  styleUrls: ['./user-details.component.css']
})
export class UserDetailsComponent implements OnInit {

  user: User;

  constructor(
    private route: ActivatedRoute,
    private usersService: UsersService,
    private router: Router) {
  }

  ngOnInit(): void {
    if(!this.usersService.isAuthenticated) {
      this.router.navigate(['']);
    }

    const id = this.route.snapshot.params['id'];
    this.usersService.getUserById(id).subscribe(data => {
      this.user = data;
      console.log(data);
    });
  }
}
