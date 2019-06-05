import { Component, OnInit } from '@angular/core';
import { UserSearch } from 'src/app/models/user-search.model';

@Component({
  selector: 'app-search',
  templateUrl: './search.component.html',
  styleUrls: ['./search.component.css']
})
export class SearchComponent implements OnInit {

  public searchData: UserSearch;

  constructor() { }

  ngOnInit() {
  }

}
