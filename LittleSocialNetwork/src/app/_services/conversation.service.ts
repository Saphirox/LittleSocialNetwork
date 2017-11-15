import { UserModel } from './../_models/user.model';
import { ApiSettings } from './../_settings/api.settings';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';


@Injectable()
export class ConversationService {
    constructor(private http: HttpClient) {
    }

    getConversations() {
        return this.http.get<UserModel[]>(ApiSettings.getCurrentUserConversations).map((data: UserModel[]) => data);
    }
}
