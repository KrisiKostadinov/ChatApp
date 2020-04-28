import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Group } from './models/group.model';

@Injectable({
  providedIn: 'root'
})
export class GroupsService {

  allPath: string = 'groups/all';
  byIdPath: string = 'groups/';
  addPath: string = 'groups';

  constructor(private http: HttpClient) { }

  add(group: Group): Observable<Group> {
    return this.http.post<Group>(environment.apiUrl + this.addPath, group);
  }

  all(): Observable<Group> {
    return this.http.get<Group>(environment.apiUrl + this.allPath);
  }

  byId(id: number): Observable<Group> {
    return this.http.get<Group>(environment.apiUrl + this.byIdPath + id);
  }
}
