import { Component, OnInit } from '@angular/core';
import { UserTab } from 'src/app/models/user-tab.model';
import { FriendshipService } from 'src/app/services/friendship.service';
import { ToastrService } from 'ngx-toastr';
import { BlackListService } from 'src/app/services/black-list.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-outgoing-friends',
  templateUrl: './outgoing-friends.component.html',
  styleUrls: ['./outgoing-friends.component.css']
})
export class OutgoingFriendsComponent implements OnInit {

  public userList: UserTab[] = null;

  constructor(private friendshipService: FriendshipService,
              private toastr: ToastrService,
              private blackService: BlackListService,
              private router: Router) { }

  ngOnInit() {
    this.resetUserList();
  }

  resetUserList() {
    this.friendshipService.getMyOutgoingRequests().subscribe(
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
        const index = this.userList.indexOf(this.userList.find(m => m.Id == id)); 
        if (index > -1) {
          this.userList.splice(index, 1);
        }
        this.toastr.success('Success delete request', 'Sending request');
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
        const index = this.userList.indexOf(this.userList.find(m => m.Id == id)); 
        if (index > -1) {
          this.userList.splice(index, 1);
        }
        this.toastr.success('Added to BlackList', 'Sending request');
      },
      err => {
        console.log(err);
        this.toastr.error(err.error, 'Error');
      }
    ); 
  }

}
