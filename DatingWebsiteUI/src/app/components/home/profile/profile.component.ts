import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { UserProfile } from 'src/app/models/user-profile.model';
import { UserService } from 'src/app/services/user.service';
import { FriendshipService } from 'src/app/services/friendship.service';
import { ToastrComponentlessModule, ToastrService } from 'ngx-toastr';
import { BlackListService } from 'src/app/services/black-list.service';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css']
})
export class ProfileComponent implements OnInit {

  public userId: any;
  public userProfile = new UserProfile();
  private baseURL = 'https://localhost:44394';

  constructor(private service: UserService,
              private toastr: ToastrService,
              private blackService: BlackListService,
              private friendService: FriendshipService,
              private activateRoute: ActivatedRoute,
              private router: Router) { }

  async ngOnInit() {
    await this.activateRoute.params.subscribe(params => this.userId = params.id);
    if (this.userId == 0) {
      this.service.getMyProfile().subscribe(
        res => {
          this.userProfile = res as UserProfile; 
        },
        err => {
          console.log(err);
        }
      );
    } else {
      this.service.getUserProfileById(this.userId).subscribe(
        res => {
          this.userProfile = res as UserProfile; 
        },
        err => {
          console.log(err);
        }
          );
    }
  }

  goToChat() {
    this.router.navigate(['/home/chats/details/' + this.userId]);
  }

  sendFriendRequest() {
    this.friendService.sendRequest(this.userId).subscribe(
    res => {
      this.toastr.success('Success send request', 'Sending request');
    },
    err => {
      console.log(err);
    }
  );
  }

  addToBlackList() {
    this.blackService.sendRequest(this.userId).subscribe(
      res => {
        this.toastr.success('Added to BlackList', 'Sending request');
      },
      err => {
        console.log(err);
      }
    );
  }

}
