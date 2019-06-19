import { Component, OnInit } from '@angular/core';
import { UserTab } from 'src/app/models/user-tab.model';
import { FriendshipService } from 'src/app/services/friendship.service';
import { ToastrService } from 'ngx-toastr';
import { BlackListService } from 'src/app/services/black-list.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-incoming-friends',
  templateUrl: './incoming-friends.component.html',
  styleUrls: ['./incoming-friends.component.css']
})
export class IncomingFriendsComponent implements OnInit {

  public userList: UserTab[] = null;

  constructor(private friendshipService: FriendshipService,
              private toastr: ToastrService,
              private blackService: BlackListService,
              private router: Router) { }

  ngOnInit() {
    this.resetUserList();
  }

  resetUserList() {
    this.friendshipService.getMyIncomingRequests().subscribe(
      res => {
        this.userList = res as UserTab[];
      },
      err => {
        console.log(err);
        this.toastr.error(err.error, 'Error');
      }
    );
  }

  goToProfile(id: string) {
    this.router.navigate(['/home/profile/' + id]);
  }

  goToChat(id: string) {
    this.router.navigate(['/home/chats/details/' + id]);
  }

  deleteFriendRequest(id: string) {
    this.friendshipService.deleteRequest(id).subscribe(
      res => {
        this.toastr.success('Success delete request', 'Sending request');
      },
      err => {
        console.log(err);
        this.toastr.error(err.error, 'Error');
      }
    );
    this.resetUserList();
  }

  confirmFriendRequest(id: string) {
    this.friendshipService.confirmRequest(id).subscribe(
      res => {
        this.toastr.success('Success confirm request', 'Sending request');
      },
      err => {
        console.log(err);
        this.toastr.error(err.error, 'Error');
      }
    );
    this.resetUserList();
  }

  addToBlackList(id: string) {
    this.blackService.sendRequest(id).subscribe(
      res => {
        this.toastr.success('Added to BlackList', 'Sending request');
      },
      err => {
        console.log(err);
        this.toastr.error(err.error, 'Error');
      }
    );
    this.resetUserList();
  }

}
