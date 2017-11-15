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
    console.log(`Other id: ${this.otherId}`);
    this.getMessages();
    this._hubConnection = new HubConnection(ApiSettings.singleChatHub);
    this._hubConnection.on('createMessage', (data: MessageModel) => {
      this._messages.push(data);
    });
  }

  createMessage() {
    this.messageService.createMessage(this._message).subscribe();
  }

  getMessages() {
    this.messageService.getLongMessages(this.otherId).subscribe((res: MessageModel[]) => {
      this._messages = res;
      console.log(this._messages);
    });
  }
}
