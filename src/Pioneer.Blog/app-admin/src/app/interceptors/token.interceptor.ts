import { HttpInterceptor, HttpHandler, HttpEvent, HttpRequest } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';
import { AuthenticationService } from '../services/authentication.service';
import { Injectable } from '@angular/core';

@Injectable()
export class TokenInterceptor implements HttpInterceptor {

  constructor(public auth: AuthenticationService) { }

  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    const currentToken = this.auth.getCurrentToken();
    if (currentToken && currentToken.token) {
      request = request.clone({
        setHeaders: {
          Authorization: `Bearer ${currentToken.token}`
        }
      });
    }
    return next.handle(request);
  }
}
