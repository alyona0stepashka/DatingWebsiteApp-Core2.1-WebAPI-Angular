import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ToastrModule } from 'ngx-toastr';


import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { AuthComponent } from './components/auth/auth.component';
import { LoginComponent } from './components/auth/login/login.component';
import { RegisterComponent } from './components/auth/register/register.component';
import { HomeComponent } from './components/home/home.component';
import { SearchComponent } from './components/home/search/search.component';
import { ProfileComponent } from './components/home/profile/profile.component';
import { SettingsComponent } from './components/home/settings/settings.component';
import { RouterModule } from '@angular/router';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { CommonModule } from '@angular/common';
import { AuthInterceptor } from './guards/auth.interceptor';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { AccountComponent } from './components/home/settings/account/account.component';
import { MyProfileComponent } from './components/home/settings/my-profile/my-profile.component';
import { FriendsComponent } from './components/home/friends/friends.component';
import { MyFriendsComponent } from './components/home/friends/my-friends/my-friends.component';
import { OutgoingFriendsComponent } from './components/home/friends/outgoing-friends/outgoing-friends.component';
import { IncomingFriendsComponent } from './components/home/friends/incoming-friends/incoming-friends.component';
import { BlackComponent } from './components/home/black/black.component';
import { OutgoingBlackComponent } from './components/home/black/outgoing-black/outgoing-black.component';
import { IncomingBlackComponent } from './components/home/black/incoming-black/incoming-black.component';
import { AlbumComponent } from './components/home/album/album.component';
import { AlbumDetailComponent } from './components/home/album-detail/album-detail.component';
import { FirstLoginComponent } from './components/home/first-login/first-login.component';

@NgModule({
  declarations: [
    AppComponent,
    AuthComponent,
    LoginComponent,
    RegisterComponent,
    HomeComponent,
    SearchComponent,
    ProfileComponent,
    SettingsComponent,
    AccountComponent,
    MyProfileComponent,
    FriendsComponent,
    MyFriendsComponent,
    OutgoingFriendsComponent,
    IncomingFriendsComponent,
    BlackComponent,
    OutgoingBlackComponent,
    IncomingBlackComponent,
    AlbumComponent,
    AlbumDetailComponent,
    FirstLoginComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    RouterModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    CommonModule,
    BrowserAnimationsModule,
    ToastrModule.forRoot()
  ],
  providers: [
    {
      provide: HTTP_INTERCEPTORS,
      useClass: AuthInterceptor,
      multi: true
    }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
