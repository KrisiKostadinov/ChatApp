import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Friend } from '../models/friend.model';
import { environment } from 'src/environments/environment';
import { Message } from '@angular/compiler/src/i18n/i18n_ast';
import { MessageModel } from '../models/message-model.model';

@Injectable({
  providedIn: 'root'
})
export class FriendsService {

  allPath: string = 'friends/all/';

  addPath: string = 'friends/';

  messagesPath: string = 'friends/messages';

  constructor(private http: HttpClient) { }

  all():Observable<Friend[]> {
    return this.http.get<Friend[]>(environment.apiUrl + this.allPath);
  }

  add(userId: string): Observable<string> {
    return this.http.post<string>(environment.apiUrl + this.addPath, userId);
  }

  gerAllMyMessages(): Observable<MessageModel[]> {
    return this.http.get<MessageModel[]>(environment.apiUrl + this.messagesPath);
  }
}
