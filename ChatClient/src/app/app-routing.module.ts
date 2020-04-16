import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { LoginComponent } from './users/login/login.component';
import { RegisterComponent } from './users/register/register.component';
import { ListAllUsersComponent } from './users/common/list-all-users/list-all-users.component';
import { UserDetailsComponent } from './users/common/user-details/user-details.component';
import { AuthGourdService } from './users/services/auth-guard.service';


const routes: Routes = [
  { path: '', component: ListAllUsersComponent },
  { path: 'auth', children: [
    { path: 'login', component: LoginComponent },
    { path: 'register', component: RegisterComponent }
  ] },
  { path: 'users', canActivate: [AuthGourdService], children: [
    { path: 'details/:id', component: UserDetailsComponent }
  ] }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
