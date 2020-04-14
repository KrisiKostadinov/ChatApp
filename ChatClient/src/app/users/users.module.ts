import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { ReactiveFormsModule } from '@angular/forms';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { ListAllUsersComponent } from './common/list-all-users/list-all-users.component';

@NgModule({
  declarations: [
    LoginComponent,
    RegisterComponent,
    ListAllUsersComponent
  ],
  imports: [
    CommonModule,
    ReactiveFormsModule,
    HttpClientModule
  ]
})
export class UsersModule { }
