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

  ngOnInit() {
    this.resetUserList();
  }

  resetUserList() {
    this.blackService.getMyBlackList().subscribe(
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

  removeRequest(id: string) {
    this.blackService.removeRequest(id).subscribe(
      res => {
        const index = this.userList.indexOf(this.userList.find(m => m.Id == id)); 
        if (index > -1) {
          this.userList.splice(index, 1);
        }
        this.toastr.success('Success delete request', 'Sending request');
      },
      err => {
        this.toastr.error(err.error, 'Error');
        console.log(err);
      }
    ); 
  }

}
