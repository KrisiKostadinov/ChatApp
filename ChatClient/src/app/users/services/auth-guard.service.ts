import { Injectable } from '@angular/core';
import { CanActivate, Router } from '@angular/router';
import { UsersService } from './users.service';

@Injectable({
  providedIn: 'root'
})
export class AuthGourdService implements CanActivate {

  constructor(
    private usersService: UsersService,
    private router: Router) { }

    canActivate(): boolean {
      if(this.usersService.isAuthenticated) {
        return true;
      }

      this.router.navigate(['auth/login']);
      return false;
    }

}
