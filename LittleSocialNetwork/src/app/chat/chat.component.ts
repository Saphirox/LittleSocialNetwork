import { AuthSettings } from './../_settings/auth.settings';
import { ApiSettings } from './../_settings/api.settings';
import { HubConnection } from '@aspnet/signalr-client';
import { MessageService } from './../_services/message.service';
import { MessageModel } from './../_models/message.model';
import { Component, OnInit, ViewEncapsulation, Input } from '@angular/core';

@Component({
  selector: 'app-chat',
  templateUrl: './chat.component.html',
  styleUrls: ['./chat.component.css'],
  encapsulation: ViewEncapsulation.None
})
export class ChatComponent implements OnInit {

  private _messages: MessageModel[];
  private _message: MessageModel;
  private _hubConnection: HubConnection;
  @Input() public otherId: number;

  constructor(private messageService: MessageService) {
  }

  ngOnInit() {
    this._message = {
    //  id: 0,
      text: '',
      // postTime: Date(),
      // lastEdited: Date(),
      // fromId: 0,
      // toId: 0,
      // fullName: ''
    };
    console.log(`Other id: ${this.otherId}`);
    this.getMessages();
    const bearer = `${localStorage.getItem(AuthSettings.tokenKey)}`;
    
    this._hubConnection = new HubConnection(`http://localhost:55872/chat?token=${bearer}`);
    this._hubConnection.on('createMessage', (data: any) => {
      console.log('Added element');
      console.log(data);
      this._messages.push(data);
    });

 this._hubConnection.start()
            .then(() => {
                console.log('Hub connection started');
            })
            .catch(err => {
                console.log(err);
                console.log('Error while establishing connection');
            });
  }

  createMessage() {
    console.log('Create message:');
    console.log(this._message);
    this.messageService.createMessage(this._message, this.otherId).subscribe();
  }

  getMessages() {
    this.messageService.getLongMessages(this.otherId).subscribe((res: MessageModel[]) => {
      this._messages = res;
      console.log(this._messages);
    });
  }
}
