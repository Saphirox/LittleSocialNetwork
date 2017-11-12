import { AuthenticationService } from './../_services/auth.service';
import { Component, OnInit, ViewEncapsulation } from '@angular/core';

@Component({
  selector: 'app-signin',
  templateUrl: './signin.component.html',
  styleUrls: ['./signin.component.css'],
  encapsulation: ViewEncapsulation.None
})
export class SigninComponent implements OnInit {

    public email: string;
    public password: string;

    constructor(private auth: AuthenticationService) {  }

    ngOnInit(): void { }

    public signin() {
        console.log(`${this.email} ${this.password}`);
        this.auth.signin({ email: this.email, password: this.password }).subscribe();
    }
}
