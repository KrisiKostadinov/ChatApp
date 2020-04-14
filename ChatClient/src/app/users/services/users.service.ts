import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class UsersService {

  loginPath: string = environment.apiUrl + 'identity/login';
  registerPath: string = environment.apiUrl + 'identity/register';
  getAllUsersPath: string = environment.apiUrl + 'users/all';

  constructor(private http: HttpClient) { }

  login(data):Observable<any> {
    let headers = new HttpHeaders();
    headers = headers.set('Content-Type', 'application/json; charset=utf-8');
    return this.http.post(this.loginPath, data, { headers: headers });
  }

  register(data):Observable<any> {
    let headers = new HttpHeaders();
    headers = headers.set('Content-Type', 'application/json; charset=utf-8');
    return this.http.post(this.registerPath, data, { headers: headers });
  }

  saveToken(token: string) {
    localStorage.setItem("token", token);
  }

  getToken() {
    return localStorage.getItem("token");
  }

  setUserData(user) {
    localStorage.setItem("user", user);
  }

  get getUserData() {
    return JSON.parse(localStorage.getItem("user"));
  }

  logout() {
    localStorage.clear();
  }

  get isAuthenticated():boolean {
    console.log(0);

    if (this.getToken()) {
      return true;
    }
    return false;
  }

  getAllUsers() {
    return this.http.get(this.getAllUsersPath);
  }

}
