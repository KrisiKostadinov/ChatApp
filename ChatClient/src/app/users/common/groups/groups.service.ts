import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class GroupsService {

  allPath: string = 'groups/all';

  constructor(private http: HttpClient) { }

  all(): Observable<any> {
    const header = new HttpHeaders({
      'Authorization': 'Bearer ' + localStorage.getItem('token')
    });
    
    console.log(environment.apiUrl + this.allPath);

    return this.http.get(environment.apiUrl + this.allPath, { headers: header });
  }

}
