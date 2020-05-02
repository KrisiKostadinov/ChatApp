import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { LoginComponent } from './users/login/login.component';
import { RegisterComponent } from './users/register/register.component';
import { UserDetailsComponent } from './users/common/user-details/user-details.component';
import { AuthGourdService } from './users/services/auth-guard.service';
import { IndexComponent } from './users/common/index/index.component';
import { GroupDetailsComponent } from './users/common/groups/group-details/group-details.component';
import { AddGroupComponent } from './users/common/groups/add-group/add-group.component';
import { EditGroupComponent } from './users/common/groups/edit-group/edit-group.component';
import { ChatUsersComponent } from './users/chat/chat-users/chat-users.component';


const routes: Routes = [
  { path: '', component: IndexComponent, canActivate: [AuthGourdService] },
  { path: 'auth', children: [
    { path: 'login', component: LoginComponent },
    { path: 'register', component: RegisterComponent }
  ] },
  { path: 'users', canActivate: [AuthGourdService], children: [
    { path: 'details/:id', component: UserDetailsComponent },
    { path: 'chat', component: ChatUsersComponent }
  ] },
  {
    path: 'groups', children: [
      { path: 'details/:id', component: GroupDetailsComponent },
      { path: 'add', component: AddGroupComponent },
      { path: 'edit/:id', component: EditGroupComponent }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
