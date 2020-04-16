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

@NgModule({
  declarations: [
    LoginComponent,
    RegisterComponent,
    ListAllUsersComponent,
    UserDetailsComponent
  ],
  imports: [
    CommonModule,
    ReactiveFormsModule,
    HttpClientModule,
    RouterModule
  ],
  providers: [
    UsersService,
    AuthGourdService,
  ]
})
export class UsersModule { }
