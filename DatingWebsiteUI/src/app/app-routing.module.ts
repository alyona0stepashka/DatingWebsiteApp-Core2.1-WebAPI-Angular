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

const routes: Routes = [
  {path:'',redirectTo:'/auth/login', pathMatch:'full'},
  {
    path:'auth', component: AuthComponent,
    children:[
      {path:'login', component:LoginComponent},
      {path:'register', component:RegisterComponent}
    ]
  },
  {
    path:'home', component: HomeComponent,
    children:[
      {path:'search', component: SearchComponent,canActivate:[AuthGuard]},
      {path:'profile/:id', component: ProfileComponent,canActivate:[AuthGuard]} ,
      //{path:'profile', component: ProfileComponent,canActivate:[AuthGuard]} ,
      {path:'settings', component: SettingsComponent,canActivate:[AuthGuard],
        children:[
          {path:'profile', component:MyProfileComponent},
          {path:'account', component:AccountComponent}
        ]
      } 
    ]
  },
];


@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
