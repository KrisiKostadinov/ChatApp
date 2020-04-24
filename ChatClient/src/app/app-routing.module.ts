import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { LoginComponent } from './users/login/login.component';
import { RegisterComponent } from './users/register/register.component';
import { UserDetailsComponent } from './users/common/user-details/user-details.component';
import { AuthGourdService } from './users/services/auth-guard.service';
import { ChatComponent } from './users/common/chat/chat.component';
import { IndexComponent } from './users/common/index/index.component';


const routes: Routes = [
  { path: '', component: IndexComponent },
  { path: 'auth', children: [
    { path: 'login', component: LoginComponent },
    { path: 'register', component: RegisterComponent }
  ] },
  { path: 'users', canActivate: [AuthGourdService], children: [
    { path: 'details/:id', component: UserDetailsComponent },
    { path: 'chat', component: ChatComponent }
  ] }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
