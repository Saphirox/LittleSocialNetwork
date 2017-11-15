import { MessageModel } from './../_models/message.model';
import { Component, OnInit, ViewEncapsulation, Input } from '@angular/core';

@Component({
  selector: 'app-message',
  templateUrl: './message.component.html',
  styleUrls: ['./message.component.css'],
  encapsulation: ViewEncapsulation.None
})
export class MessageComponent implements OnInit {

  @Input() message: MessageModel;

  constructor() { }

  ngOnInit() {
  }

  updateMessage() {
  }

  deleteMessage(): void {

  }
}
