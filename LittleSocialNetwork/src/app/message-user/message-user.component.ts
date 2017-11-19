import { UserModel } from './../_models/user.model';
import { Component, OnInit, ViewEncapsulation, Input } from '@angular/core';

@Component({
  selector: 'app-message-user',
  templateUrl: './message-user.component.html',
  styleUrls: ['./message-user.component.css'],
  encapsulation: ViewEncapsulation.None
})
export class MessageUserComponent implements OnInit {

  @Input() user: UserModel;

  constructor() { }

  ngOnInit() {
    console.log(this.user);
  }
}
