import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { UserProfile } from 'src/app/models/user-profile.model';
import { UserService } from 'src/app/services/user.service';
import { FriendshipService } from 'src/app/services/friendship.service';
import { ToastrComponentlessModule, ToastrService } from 'ngx-toastr';
import { BlackListService } from 'src/app/services/black-list.service';
import { Lightbox } from 'ngx-lightbox';
import { NgxGalleryOptions, NgxGalleryImage, NgxGalleryAnimation } from 'ngx-gallery';
import { SignalRService } from 'src/app/services/signal-r.service';
import { MessageSend } from 'src/app/models/message-send.model';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css']
})
export class ProfileComponent implements OnInit {

  public userId: any;
  public userProfile = new UserProfile(); 
  public outgoingMessage = new MessageSend();
  UploadFiles: File[] = new Array();
  _albums: any[] = new Array();
  light_image_path = '/assets/img/no-image.png';
  private baseURL = 'https://localhost:44394';
  IsLoad: boolean = false;

  constructor(private service: UserService,
              private toastr: ToastrService,
              private blackService: BlackListService,
              private friendService: FriendshipService,
              private activateRoute: ActivatedRoute,
              private router: Router,
              private _lightbox: Lightbox,
              public signalRService: SignalRService) { }

  async ngOnInit() { 
    await this.activateRoute.params.subscribe(params => this.userId = params.id);
    this.signalRService.startConnection(); 
    if (this.userId == 0) {
      this.service.getMyProfile().subscribe(
        res => {
          this.userProfile = res as UserProfile;
          this.light_image_path = this.baseURL + this.userProfile.PhotoPath;
        },
        err => {
          console.log(err);
          this.toastr.error(err.error, 'Error');
        }
      );
    }
    else {
      this.service.getUserProfileById(this.userId).subscribe(
         res => {
          this.userProfile = res as UserProfile;
           this.service.getMyProfile().subscribe(
            res2 => {
              const myProfile = res2 as UserProfile;
              this.light_image_path = this.baseURL + myProfile.PhotoPath;
              if (this.userId == myProfile.Id)  {
                this.userId = 0;
              }
            },
            err => {
              console.log(err);
              this.toastr.error(err.error, 'Error');
            }
          ); 
        },
        err => {
          console.log(err);
          this.toastr.error(err.error, 'Error');
        }
      );
    } 
    this._albums = new Array();
    const album = {
      src: this.light_image_path,
      caption: '',
      thumb: ''
   };

    this._albums.push(album);
    this.IsLoad = true;
    // this.UserAge = (new Date()).getFullYear() - this.userProfile.DateBirth.getFullYear();
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
      this.userProfile.IsFriend = true;
    },
    err => {
      console.log(err);
      this.toastr.error(err.error, 'Error');
    }
  );
  }

  deleteFriendRequest(id: string) {
    this.friendService.deleteRequest(id).subscribe(
    res => {
      this.toastr.success('Success delete request', 'Sending delete request');
      this.userProfile.IsFriend = false;
    },
    err => {
      console.log(err);
      this.toastr.error(err.error, 'Error');
    }
  );
  }

  addToBlackList(id: string) {
    this.blackService.sendRequest(id).subscribe(
      res => {
        this.toastr.success('Added to BlackList', 'Sending request');
        this.userProfile.IsBlack = true; 
      },
      err => {
        console.log(err);
        this.toastr.error(err.error, 'Error');
      }
    );
  }

  async removeFromBlackList(id: string) {
    this.blackService.removeRequest(id).subscribe(
      res => {
        this.toastr.success('Success delete request', 'Sending request'); 
        this.userProfile.IsBlack = false; 
      },
      err => {
        console.log(err);
        this.toastr.error(err.error, 'Error');
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

  
  onFilesAdded(files: File[]) {
    this.UploadFiles = files;
  }

  onSendMessage() {
    this.UploadFiles.forEach(file => {
      const reader = new FileReader();
      reader.readAsDataURL(file);
    });

      this.outgoingMessage.ReceiverId = this.userId;
      this.signalRService.sendMessageFromProfile(this.outgoingMessage);
      this.outgoingMessage.Text = '';
  }

  onFilesRejected(files: File[]) {
    console.log(files);
  }

}
