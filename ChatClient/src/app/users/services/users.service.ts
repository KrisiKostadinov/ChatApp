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

  getToken(token: string) {
    return localStorage.getItem("token");
  }
}
