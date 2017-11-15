import { ConversationService } from './../_services/conversation.service';
import { UserModel } from './../_models/user.model';
import { MessageModel } from './../_models/message.model';
import { UserMock } from './../_mocks/user.mock';
import { Component, OnInit, ViewEncapsulation } from '@angular/core';

@Component({
  selector: 'app-conversation',
  templateUrl: './conversation.component.html',
  styleUrls: ['./conversation.component.css'],
  encapsulation: ViewEncapsulation.None
})
export class ConversationComponent implements OnInit {

  private _users: UserModel[];
  private _user: any;
  private _activeChatWith: number;

  constructor(private _conv: ConversationService) { }

  ngOnInit() {
    // this._activeChatWith = 1;
     this._conv.getConversations().subscribe(res => {
       this._users = res;
      });
  }
}
