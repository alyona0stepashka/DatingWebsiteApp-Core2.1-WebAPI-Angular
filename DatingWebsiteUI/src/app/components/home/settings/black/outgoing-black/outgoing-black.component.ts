import { Component, OnInit } from '@angular/core';
import { UserTab } from 'src/app/models/user-tab.model';
import { FriendshipService } from 'src/app/services/friendship.service';
import { ToastrService } from 'ngx-toastr';
import { BlackListService } from 'src/app/services/black-list.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-outgoing-black',
  templateUrl: './outgoing-black.component.html',
  styleUrls: ['./outgoing-black.component.css']
})
export class OutgoingBlackComponent implements OnInit {

  public userList: UserTab[] = null;

  constructor(private friendshipService: FriendshipService,
              private toastr: ToastrService,
              private blackService: BlackListService,
              private router: Router) { }

  async ngOnInit() {
    await this.resetUserList();
  }

  async resetUserList() {
    await this.blackService.getMyBlackList().subscribe(
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

  async removeRequest(id: string) {
    this.blackService.removeRequest(id).subscribe(
      res => {
        this.toastr.success('Success delete request', 'Sending request');
      },
      err => {
        this.toastr.error(err);
        console.log(err);
      }
    );
    await this.resetUserList();
  }

}
