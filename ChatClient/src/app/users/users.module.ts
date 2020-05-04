import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { ReactiveFormsModule } from '@angular/forms';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { ListAllUsersComponent } from './common/list-all-users/list-all-users.component';
import { RouterModule } from '@angular/router';
import { UserDetailsComponent } from './common/user-details/user-details.component';
import { AuthGourdService } from './services/auth-guard.service';
import { UsersService } from './services/users.service';
import { ChatComponent } from './common/chat/chat.component';
import { IndexComponent } from './common/index/index.component';
import { ListAllGroupsComponent } from './common/groups/list-all-groups/list-all-groups.component';
import { GroupsService } from './common/groups/groups.service';
import { GroupDetailsComponent } from './common/groups/group-details/group-details.component';
import { AddGroupComponent } from './common/groups/add-group/add-group.component';
import { CapitalizePipe } from '../filters/capitalize.pipe';
import { EditGroupComponent } from './common/groups/edit-group/edit-group.component';
import { DismissGroupComponent } from './common/groups/dismiss-group/dismiss-group.component';
import { ToastrModule } from 'ngx-toastr';
import { ListAllFriendsComponent } from './common/list-all-friends/list-all-friends.component';
import { FriendsService } from './services/friends.service';
import { ChatUsersComponent } from './chat/chat-users/chat-users.component';
import { ListAllMyRequestsComponent } from './common/list-all-my-requests/list-all-my-requests.component';
import { ListAllRequestsComponent } from './common/list-all-requests/list-all-requests.component';

@NgModule({
  declarations: [
    CapitalizePipe,
    LoginComponent,
    RegisterComponent,
    ListAllUsersComponent,
    UserDetailsComponent,
    ChatComponent,
    IndexComponent,
    ListAllGroupsComponent,
    GroupDetailsComponent,
    AddGroupComponent,
    EditGroupComponent,
    DismissGroupComponent,
    ListAllFriendsComponent,
    ChatUsersComponent,
    ListAllMyRequestsComponent,
    ListAllRequestsComponent,
  ],
  imports: [
    CommonModule,
    ReactiveFormsModule,
    HttpClientModule,
    RouterModule,
    ToastrModule.forRoot({
      toastComponent: DismissGroupComponent
    })
  ],
  entryComponents: [
    DismissGroupComponent
  ],
  providers: [
    UsersService,
    AuthGourdService,
    GroupsService,
    FriendsService,
  ]
})
export class UsersModule { }
