import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Friend } from '../models/friend.model';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class FriendsService {

  allPath: string = 'friends/all/';

  addPath: string = 'friends/';

  constructor(private http: HttpClient) { }

  all():Observable<Friend[]> {
    return this.http.get<Friend[]>(environment.apiUrl + this.allPath);
  }

  add(userId: string): Observable<string> {
    return this.http.post<string>(environment.apiUrl + this.addPath, userId);
  }
}
