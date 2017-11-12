import { Injectable } from '@angular/core';
import { Router, CanActivate } from '@angular/router';
import { AuthSettings } from '../_settings/auth.settings';

@Injectable()
export class AuthGuard implements CanActivate {

  constructor(private router: Router) {}

  canActivate() {

    if (localStorage.getItem(AuthSettings.tokenKey)) {
      return true;
    }

    this.router.navigate([AuthSettings.unauthorizedUrl]);
    return false;
  }
}
