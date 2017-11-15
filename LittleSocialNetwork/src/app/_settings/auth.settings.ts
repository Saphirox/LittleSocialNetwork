import { ApiSettings } from './api.settings';

export class AuthSettings {
  public static tokenKey = 'currentUser';
  public static unauthorizedUrl = ApiSettings.signin;
}
