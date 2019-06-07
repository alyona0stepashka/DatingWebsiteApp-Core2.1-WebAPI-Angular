import { Component, OnInit } from '@angular/core';
import { UserSearch } from 'src/app/models/user-search.model';
import { UserTab } from 'src/app/models/user-tab.model';
import { SearchService } from 'src/app/services/search.service';
import { Router } from '@angular/router';
import { Static } from 'src/app/models/static.model';
import { StaticService } from 'src/app/services/static.service';

@Component({
  selector: 'app-search',
  templateUrl: './search.component.html',
  styleUrls: ['./search.component.css']
})
export class SearchComponent implements OnInit {

  public searchData = new UserSearch();
  public userList: UserTab[];
  public staticInfo = new Static(); 
  private baseURL = 'https://localhost:44394';

  constructor(private searchService: SearchService,
    private staticService: StaticService,
    private router:Router) { }

  async ngOnInit() {
    await this.searchService.getAll().subscribe(
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
    await this.staticService.getAll().subscribe(
      res => {
        this.staticInfo = res as Static; 
      },
      err => {
        console.log(err);
      }
    ); 
  }

  goToProfile(id: string){
    this.router.navigate(['/home/profile/'+ id]); 
  }

  async onSearch(){
    await this.searchService.getSearchResult(this.searchData).subscribe(
      res => {
        this.userList = res as UserTab[]; 
      },
      err => {
        console.log(err);
      }
    ); 
  }

}
