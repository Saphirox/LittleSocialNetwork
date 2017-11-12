import { SigninComponent } from './../signin/signin.component';
import { Routes } from '@angular/router';
import { AppComponent } from '../app.component';
import { AuthComponent } from '../auth/auth.component';

export class RouterSettings {

    public static signin = 'signin';
    public static signup = 'signup';

    static routes: Routes = [
        {
            path: RouterSettings.signup,
            component: AuthComponent
      },
      {
           path: RouterSettings.signin,
           component: SigninComponent
      },
        {
            path: '',
            redirectTo: `/${ RouterSettings.signin }`,
            pathMatch: 'full'
        }
    ];
}
