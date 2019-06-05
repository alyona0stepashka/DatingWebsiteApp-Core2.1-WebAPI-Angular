import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { UserProfile } from 'src/app/models/user-profile.model';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css']
})
export class ProfileComponent implements OnInit {

  public userId:any;
  public userProfile: UserProfile;
  private baseURL = 'localhost:44394';

  constructor(private service: UserService,
    private activateRoute: ActivatedRoute) { }

  async ngOnInit() {
    await this.activateRoute.params.subscribe(params => this.userId = params['id']);
    if (this.userId==0){
      this.userProfile = this.service.getMyProfile();
      // this.service.getMyProfile().subscribe(
      //   (res: any) => {
      //     this.userProfile = res.data;
      //     //this.userProfile.PhotoPath = this.baseURL+this.userProfile.PhotoPath;
      //   },
      //   err => { 
      //       console.log(err);
      //   }
      // );
    } 
    else{
      this.service.getUserProfileById(this.userId).subscribe(
            (res: any) => {
              this.userProfile = res.data;
              this.userProfile.PhotoPath = this.baseURL+this.userProfile.PhotoPath;
            },
            err => { 
                console.log(err);
            }
          );
    }    
  }

}
