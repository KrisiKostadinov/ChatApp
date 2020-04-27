import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor
} from '@angular/common/http';
import { Observable } from 'rxjs';
import { UsersService } from '../users/services/users.service';

@Injectable()
export class TokenInterceptor implements HttpInterceptor {

  constructor(public usersService: UsersService) {}

  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    request = request.clone({
      setHeaders: {
        Authorization: `Bearer ${this.usersService.getToken()}`
      }
    });
    return next.handle(request);
  }
}
