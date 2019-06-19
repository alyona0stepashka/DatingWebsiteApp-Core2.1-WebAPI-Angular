import { Component, OnInit } from '@angular/core';
import { UserTab } from 'src/app/models/user-tab.model';
import { FriendshipService } from 'src/app/services/friendship.service';
import { ToastrService } from 'ngx-toastr';
import { BlackListService } from 'src/app/services/black-list.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-incoming-black',
  templateUrl: './incoming-black.component.html',
  styleUrls: ['./incoming-black.component.css']
})
export class IncomingBlackComponent implements OnInit {

  public userList: UserTab[] = new Array();

  constructor(private friendshipService: FriendshipService,
              private toastr: ToastrService,
              private blackService: BlackListService,
              private router: Router) { }

  ngOnInit() {
    this.resetUserList();
  }

  resetUserList() {
    this.blackService.getBlackListWithMe().subscribe(
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

  // addToBlackList(id: string) {
  //   this.blackService.sendRequest(id).subscribe(
  //     res => {
  //       this.toastr.success('Added to BlackList', 'Sending request');
  //     },
  //     err => {
  //       console.log(err);
  //     }
  //   );
  // }
}
