import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ApiSettings } from './../_settings/api.settings';
import { MessageModel } from './../_models/message.model';
import { Observable } from 'rxjs/Observable';

@Injectable()
export class MessageService {
    constructor(private http: HttpClient) {}

    getLongMessages(receiverId: number) {
        return this.http.get<MessageModel[]>(ApiSettings.getLongMessages(receiverId))
                    .map((data) => {
                        console.log(data);
                        return data;
                    }, err => Observable.throw(err));
    }

    createMessage(message: MessageModel, otherId: number) {
        message.toId = otherId;
        console.log(message);
        return this.http.post<MessageModel>(ApiSettings.createMessage, message)
            .map(res => res, err => Observable.throw(err));
    }
}
