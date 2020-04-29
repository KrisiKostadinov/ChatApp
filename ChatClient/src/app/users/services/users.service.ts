import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { User } from '../models/user.model';

@Injectable({
  providedIn: 'root'
})
export class UsersService {

  loginPath: string = environment.apiUrl + 'identity/login';
  registerPath: string = environment.apiUrl + 'identity/register';
  getAllUsersPath: string = environment.apiUrl + 'users/all';
  byId: string = environment.apiUrl + 'users/';

  constructor(private http: HttpClient) { }

  login(data):Observable<User> {
    return this.http.post<User>(this.loginPath, data);
  }

  register(data):Observable<User> {
    return this.http.post<User>(this.registerPath, data);
  }

  saveToken(token: string) {
    localStorage.setItem("token", token);
  }

  getToken() {
    return localStorage.getItem("token");
  }

  setUserData(user: User) {
    localStorage.setItem("user", JSON.stringify(user));
  }

  get getUserData() {
    return JSON.parse(localStorage.getItem("user"));
  }

  logout() {
    localStorage.clear();
  }

  get isAuthenticated():boolean {
    if (this.getToken()) {
      return true;
    }
    return false;
  }

  getAllUsers(): Observable<User[]> {
    return this.http.get<User[]>(this.getAllUsersPath);
  }

  getUserById(id: string): Observable<User> {
    return this.http.get<User>(this.byId + id);
  }
}
