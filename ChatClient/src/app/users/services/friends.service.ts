import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Friend } from '../models/friend.model';
import { environment } from 'src/environments/environment';
import { MessageModel } from '../models/message-model.model';
import { RequestModel } from '../models/request-model.model';

@Injectable({
  providedIn: 'root'
})
export class FriendsService {

  allPath: string = 'friends/all';
  addRequestPath: string = 'friends/request/';
  messagesPath: string = 'friends/messages';
  listAllMyRequestsPath: string = 'friends/requests/my';
  listAllRequestsPath: string = 'friends/requests';
  addFriendPath: string = 'friends/';
  dismissRequestPath: string = 'friends/request/';

  constructor(private http: HttpClient) { }

  all():Observable<Friend[]> {
    return this.http.get<Friend[]>(environment.apiUrl + this.allPath);
  }

  addRequest(userId: string): Observable<boolean> {
    return this.http.post<boolean>(environment.apiUrl + this.addRequestPath + userId, userId);
  }

  gerAllMyMessages(firstUserId: string, secondUserId: string): Observable<MessageModel[]> {
    return this.http.get<MessageModel[]>(environment.apiUrl + this.messagesPath + `/${firstUserId}/${secondUserId}`);
  }

  getAllMyRequests(): Observable<RequestModel[]> {
    return this.http.get<RequestModel[]>(environment.apiUrl + this.listAllMyRequestsPath);
  }
  
  getAllRequests(): Observable<RequestModel[]> {
    return this.http.get<RequestModel[]>(environment.apiUrl + this.listAllRequestsPath);
  }

  confirm(userId: string): Observable<boolean> {
    return this.http.post<boolean>(environment.apiUrl + this.addFriendPath + userId, userId);
  }

  dismiss(userId: string): Observable<boolean> {
    return this.http.delete<boolean>(environment.apiUrl + this.dismissRequestPath + userId);
  }
}
