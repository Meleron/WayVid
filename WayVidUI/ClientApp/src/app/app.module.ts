import {BrowserModule} from '@angular/platform-browser';
import {NgModule} from '@angular/core';
import {FormsModule, ReactiveFormsModule} from '@angular/forms';
import {HttpClientModule} from '@angular/common/http';
import {RouterModule} from '@angular/router';

import {AppComponent} from './app.component';
import {FetchDataComponent} from './components/fetch-data/fetch-data.component';
import {LoginComponent} from './components/auth/login/login.component';
import {NotFoundComponent} from './components/common/not-found/not-found.component';
import {VisitorModule} from './components/visitor/visitor.module';
import {BrowserAnimationsModule} from '@angular/platform-browser/animations';
import {ToastrModule, ToastrService} from 'ngx-toastr';
import { SignUpComponent } from './components/auth/sign-up/sign-up.component';

@NgModule({
  declarations: [
    AppComponent,
    FetchDataComponent,
    LoginComponent,
    NotFoundComponent,
    SignUpComponent
  ],
  imports: [
    BrowserModule.withServerTransition({appId: 'ng-cli-universal'}),
    HttpClientModule,
    FormsModule,
    VisitorModule,
    BrowserAnimationsModule,
    ToastrModule.forRoot(),
    RouterModule.forRoot([
      {path: '', component: LoginComponent, pathMatch: 'full'},
      {path: 'login', component: LoginComponent, pathMatch: 'full'},
      {path: 'signup', component: SignUpComponent, pathMatch: 'full'},
      {path: '**', component: NotFoundComponent}
    ]),
    ReactiveFormsModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule {
}
