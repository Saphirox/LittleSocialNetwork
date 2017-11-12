import { Injectable } from '@angular/core';
import 'rxjs/add/operator/map';
import { ApiSettings } from '../_settings/api.settings';
import { AuthSettings } from '../_settings/auth.settings';
import { SigninModel } from '../_models/signin.model';
import { HttpClient } from '@angular/common/http';

@Injectable()
export class AuthenticationService {

  public token: string;

  constructor(private http: HttpClient) { }

  signin(model: SigninModel): any {
    return this.http.post(ApiSettings.signin, model)
               .map((data: Response) => {

                let token = data['token'];

                  if (token) {
                    this.token = token;
                    console.log(token);
                    localStorage.setItem(AuthSettings.tokenKey, token);
                    return true;
                  }

                  return false;
      });
  }

  signout(): void {
    this.token = null;
    localStorage.removeItem(AuthSettings.tokenKey);
  }
}
