import { Component, OnInit } from '@angular/core';
import { UserSearch } from 'src/app/models/user-search.model';
import { UserTab } from 'src/app/models/user-tab.model';
import { SearchService } from 'src/app/services/search.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-search',
  templateUrl: './search.component.html',
  styleUrls: ['./search.component.css']
})
export class SearchComponent implements OnInit {

  public searchData: UserSearch;
  public userList: UserTab[];
  private baseURL = 'https://localhost:44394';

  constructor(private searchService: SearchService,
    private router:Router) { }

  ngOnInit() {
    this.searchService.getAll().subscribe(
      res => {
        this.userList = res as UserTab[];  
        this.userList.forEach(element => {
          element.PhotoPath = this.baseURL + element.PhotoPath;
        });
      },
      err => {
        console.log(err);
      }
    ); 
  }

  goToProfile(id: string){
    this.router.navigate(['/home/profile/'+ id]); 
  }

}
