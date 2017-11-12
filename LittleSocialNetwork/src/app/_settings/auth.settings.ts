import { ApiSettings } from './api.settings';

export class AuthSettings {
  public static tokenKey = 'currentUser';
  public static unauthorizeUrl = ApiSettings.signin;
}
