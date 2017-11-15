import { ConversationService } from './_services/conversation.service';
import { MessageService } from './_services/message.service';
import { MaterialModule } from './_commons/material.module';
import { SigninComponent } from './signin/signin.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatButtonModule } from '@angular/material';
import { AuthGuard } from './_guards/auth.guard';
import { AuthenticationService } from './_services/auth.service';
import { TokenInterceptor } from './_commons/token.interceptor';
import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { AppComponent } from './app.component';
import { FormsModule } from '@angular/forms';
import { RouterSettings } from './_settings/router.settings';
import { RouterModule } from '@angular/router';
import { AuthComponent } from './auth/auth.component';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { SignupComponent } from './signup/signup.component';
import { ChatComponent } from './chat/chat.component';
import { MessageComponent } from './message/message.component';
import { ConversationComponent } from './conversation/conversation.component';
import { MessageUserComponent } from './message-user/message-user.component';

@NgModule({
  declarations: [
    AppComponent,
    AuthComponent,
    SigninComponent,
    SignupComponent,
    ChatComponent,
    MessageComponent,
    ConversationComponent,
    MessageUserComponent
  ],
  imports: [
    BrowserModule,
    FormsModule,
    HttpClientModule,
    BrowserAnimationsModule,
    MaterialModule,
    RouterModule.forRoot(RouterSettings.routes, { enableTracing: true })
  ],
  providers: [
    ConversationService,    
    AuthenticationService,
    MessageService,
    AuthGuard,
    {
      provide: HTTP_INTERCEPTORS,
      useClass: TokenInterceptor,
      multi: true
    }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }

