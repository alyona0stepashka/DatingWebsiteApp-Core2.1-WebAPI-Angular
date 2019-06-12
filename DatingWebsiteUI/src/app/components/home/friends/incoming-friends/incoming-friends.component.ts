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

  async ngOnInit() {
    await this.resetUserList();
  }

  async resetUserList() {
    await this.friendshipService.getMyIncomingRequests().subscribe(
      res => {
        this.userList = res as UserTab[];
      },
      err => {
        console.log(err);
      }
    );
  }

  goToProfile(id: string) {
    this.router.navigate(['/home/profile/' + id]);
  }

  goToChat(id: string) {
    this.router.navigate(['/home/chats/details/' + id]);
  }

  async deleteFriendRequest(id: string) {
    this.friendshipService.deleteRequest(id).subscribe(
      res => {
        this.toastr.success('Success delete request', 'Sending request');
      },
      err => {
        console.log(err);
      }
    );
    await this.resetUserList();
  }

  async confirmFriendRequest(id: string) {
    this.friendshipService.confirmRequest(id).subscribe(
      res => {
        this.toastr.success('Success delete request', 'Sending request');
      },
      err => {
        console.log(err);
      }
    );
    await this.resetUserList();
  }

  async addToBlackList(id: string) {
    this.blackService.sendRequest(id).subscribe(
      res => {
        this.toastr.success('Added to BlackList', 'Sending request');
      },
      err => {
        console.log(err);
      }
    );
    await this.resetUserList();
  }

}
