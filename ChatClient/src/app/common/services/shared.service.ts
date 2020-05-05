import { Injectable } from '@angular/core';
import { Subject, Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class SharedService {

  private subject = new Subject<any>();
  
  sendClickEvent(...data: any[]) {
    this.subject.next(data);
  }

  getClickEvent(): Observable<any> {
    return this.subject.asObservable();
  }
}
