import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { UserProfile } from 'src/app/models/user-profile.model';
import { UserService } from 'src/app/services/user.service';
import { FriendshipService } from 'src/app/services/friendship.service';
import { ToastrComponentlessModule, ToastrService } from 'ngx-toastr';
import { BlackListService } from 'src/app/services/black-list.service';
import { Lightbox } from 'ngx-lightbox';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css']
})
export class ProfileComponent implements OnInit {

  public userId: any;
  public userProfile = new UserProfile();
  _albums: any[] = new Array();
  light_image_path = '/assets/img/no-image.png';
  private baseURL = 'https://localhost:44394';

  constructor(private service: UserService,
              private toastr: ToastrService,
              private blackService: BlackListService,
              private friendService: FriendshipService,
              private activateRoute: ActivatedRoute,
              private router: Router,
              private _lightbox: Lightbox) { }

  async ngOnInit() { 
    await this.activateRoute.params.subscribe(params => this.userId = params.id);
    if (this.userId == 0) {
      this.service.getMyProfile().subscribe(
        res => {
          this.userProfile = res as UserProfile;
          this.light_image_path = this.baseURL + this.userProfile.PhotoPath;
        },
        err => {
          console.log(err);
        }
      );
    }
    else {
      this.service.getUserProfileById(this.userId).subscribe(
        async res => {
          this.userProfile = res as UserProfile;
          await this.service.getMyProfile().subscribe(
            res2 => {
              const myProfile = res2 as UserProfile;
              this.light_image_path = this.baseURL + this.userProfile.PhotoPath;
              if (this.userId == myProfile.Id)  {
                this.userId = 0;
              }
            },
            err => {
              console.log(err);
            }
          ); 
        },
        err => {
          console.log(err);
        }
      );
    }
    const album = {
      src: this.light_image_path,
      caption: '',
      thumb: ''
   };

    this._albums.push(album);
  }

  goToChat(id: string) {
    this.router.navigate(['/home/chats/details/' + id]);
  }

  goToAlbums(id: string) {
    this.router.navigate(['/home/album/' + id]);
  }

  sendFriendRequest(id: string) {
    this.friendService.sendRequest(id).subscribe(
    res => {
      this.toastr.success('Success send request', 'Sending request');
    },
    err => {
      console.log(err);
    }
  );
  }

  deleteFriendRequest(id: string) {
    this.friendService.deleteRequest(id).subscribe(
    res => {
      this.toastr.success('Success delete request', 'Sending delete request');
    },
    err => {
      console.log(err);
    }
  );
  }

  addToBlackList(id: string) {
    this.blackService.sendRequest(id).subscribe(
      res => {
        this.toastr.success('Added to BlackList', 'Sending request');
      },
      err => {
        console.log(err);
      }
    );
  }

  async removeFromBlackList(id: string) {
    this.blackService.removeRequest(id).subscribe(
      res => {
        this.toastr.success('Success delete request', 'Sending request');
      },
      err => {
        console.log(err);
      }
    ); 
  } 
  
  open(): void {
    // open lightbox
    this._lightbox.open(this._albums, 0);
  }
 
  close(): void {
    // close lightbox programmatically
    this._lightbox.close();
  }

}
