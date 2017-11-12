
import { HttpInterceptor } from '@angular/common/http';
import {HttpRequest} from '@angular/common/http/src/request';
import {HttpHandler} from '@angular/common/http/src/backend';
import { AuthenticationService } from '../_services/auth.service';
import { AuthSettings } from '../_settings/auth.settings';

export class TokenInterceptor implements HttpInterceptor {

  intercept(req: HttpRequest<any>, next: HttpHandler): any {

    req = req.clone({
      setHeaders: {
          Authorization: `Bearer ${localStorage.getItem(AuthSettings.tokenKey)}`
        }
    });

    return next.handle(req);
  }
}
