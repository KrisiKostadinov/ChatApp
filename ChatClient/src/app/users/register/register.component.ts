import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { UsersService } from '../services/users.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

  registerForm: FormGroup;

  identityErrors: Array<any>;

  constructor(
    private fb: FormBuilder,
    private usersService: UsersService,
    private router: Router) {
    this.registerForm = this.fb.group({
      'username': ['', Validators.required],
      'email': ['', [Validators.required, Validators.email]],
      'password': ['', [Validators.required, Validators.minLength(6)]],
      'birthday': [''],
    });
  }

  ngOnInit(){
  }

  get username() {
    return this.registerForm.get('username');
  }
  
  get email() {
    return this.registerForm.get('email');
  }
  
  get birthday() {
    return this.registerForm.get('birthday');
  }
  
  get password() {
    return this.registerForm.get('password');
  }

  register() {
    this.usersService.register(this.registerForm.value).subscribe(res => {
      console.log(res);
      this.router.navigate(['/login']);
    }, error => {
      this.identityErrors = error.error;
    });
  }
}
