import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-search',
  templateUrl: './search.component.html',
  styleUrls: ['./search.component.css']
})
export class SearchComponent implements OnInit {

  constructor() { }

  ngOnInit() {
    // this.userService.getUserProfile().subscribe(
    //   res => {
    //     this.userDetails = res as UserDetail; 
    //   },
    //   err => {
    //     console.log(err);
    //   },
    // );
  }

}
