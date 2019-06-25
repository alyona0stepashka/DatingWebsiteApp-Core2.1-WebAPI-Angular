import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ToastrModule } from 'ngx-toastr';
import { LightboxModule } from 'ngx-lightbox';
import { NgxDropzoneModule } from 'ngx-dropzone';
import { NgxGalleryModule } from 'ngx-gallery';

import { DatePipe } from '@angular/common';
import { PickerModule } from '@ctrl/ngx-emoji-mart';
// import { MatAutocompleteModule } from '@angular/material/autocomplete';
// import { MatCheckboxModule } from '@angular/material/checkbox';
// import { MatDatepickerModule } from '@angular/material/datepicker';
// import { MatFormFieldModule } from '@angular/material/form-field';
// import { MatInputModule } from '@angular/material/input';
// import { MatRadioModule } from '@angular/material/radio';
// import { MatSelectModule } from '@angular/material/select';
// import { MatSliderModule } from '@angular/material/slider';
// import { MatSlideToggleModule } from '@angular/material/slide-toggle';

// import { MatNativeDateModule } from '@angular/material';

// import { LayoutModule } from '@angular/cdk/layout';

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
import { BlackComponent } from './components/home/settings/black/black.component';
import { OutgoingBlackComponent } from './components/home/settings/black/outgoing-black/outgoing-black.component';
import { IncomingBlackComponent } from './components/home/settings/black/incoming-black/incoming-black.component';
import { AlbumComponent } from './components/home/album/album.component';
import { AlbumDetailComponent } from './components/home/album/album-detail/album-detail.component';
import { FirstLoginComponent } from './components/auth/first-login/first-login.component';
import { UserService } from './services/user.service';
import { BlackListService } from './services/black-list.service';
import { FriendshipService } from './services/friendship.service';
import { PhotoAlbumService } from './services/photo-album.service';
import { SearchService } from './services/search.service';
import { ChatsComponent } from './components/home/chats/chats.component';
import { ChatDetailComponent } from './components/home/chats/chat-detail/chat-detail.component';
import 'hammerjs';

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
    FirstLoginComponent,
    ChatsComponent,
    ChatDetailComponent
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
    LightboxModule,
    NgxDropzoneModule,
    NgxGalleryModule,
    PickerModule,
    ToastrModule.forRoot()
  ],
  providers: [
    UserService,
    BlackListService,
    FriendshipService,
    PhotoAlbumService,
    SearchService,
    DatePipe,
    {
      provide: HTTP_INTERCEPTORS,
      useClass: AuthInterceptor,
      multi: true
    }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
