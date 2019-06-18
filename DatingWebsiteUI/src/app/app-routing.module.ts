import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AuthComponent } from './components/auth/auth.component';
import { LoginComponent } from './components/auth/login/login.component';
import { RegisterComponent } from './components/auth/register/register.component';
import { SearchComponent } from './components/home/search/search.component';
import { ProfileComponent } from './components/home/profile/profile.component';
import { SettingsComponent } from './components/home/settings/settings.component';
import { AuthGuard } from './guards/auth.guard';
import { HomeComponent } from './components/home/home.component';
import { MyProfileComponent } from './components/home/settings/my-profile/my-profile.component';
import { AccountComponent } from './components/home/settings/account/account.component';
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
import { ChatsComponent } from './components/home/chats/chats.component';

const routes: Routes = [
  {path: '', redirectTo: '/auth/login', pathMatch: 'full'},
  // { path: '**', redirectTo: 'home' },
  {
    path: 'auth', component: AuthComponent,
    children: [
      {path: 'login', component: LoginComponent},
      {path: 'register', component: RegisterComponent},
      {path: 'first', component: FirstLoginComponent}
    ]
  },
  {
    path: 'home', component: HomeComponent,
    children: [
      {path: 'search', component: SearchComponent, canActivate: [AuthGuard]},
      {path: 'profile/:id', component: ProfileComponent, canActivate: [AuthGuard]},
      // {path:'profile', component: ProfileComponent,canActivate:[AuthGuard]} ,
      {path: 'settings', component: SettingsComponent, canActivate: [AuthGuard],
        children: [
          {path: 'profile', component: MyProfileComponent},
          {path: 'account', component: AccountComponent},
          {path: 'black', component: BlackComponent, canActivate: [AuthGuard],
            children: [
              {path: 'outgoing', component: OutgoingBlackComponent},
              {path: 'incoming', component: IncomingBlackComponent}
            ]
          }
        ]
      },
      {path: 'friends', component: FriendsComponent, canActivate: [AuthGuard],
        children: [
          {path: 'my', component: MyFriendsComponent},
          {path: 'outgoing', component: OutgoingFriendsComponent},
          {path: 'incoming', component: IncomingFriendsComponent}
        ]
      },
      {path: 'album/:id', component: AlbumComponent, canActivate: [AuthGuard],
        // children: [
        //   {path: 'album-details/:id', component: AlbumDetailComponent, canActivate: [AuthGuard]}
        // ]
      },
      {path: 'chats', component: ChatsComponent, canActivate: [AuthGuard],
        // children: [
        //   {path: 'album-details/:id', component: AlbumDetailComponent, canActivate: [AuthGuard]}
        // ]
      }
    ]
  },
];


@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
