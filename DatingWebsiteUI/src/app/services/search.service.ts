import { Injectable } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { HttpClient } from '@angular/common/http';
import { UserSearch } from '../models/user-search.model';

@Injectable({
  providedIn: 'root'
})
export class SearchService {

  constructor(private fb: FormBuilder, private http: HttpClient) { }
  readonly BaseURI = 'https://localhost:44394/api'; 

getAll(){
  return this.http.get(this.BaseURI + '/search');
}
  
getSearchResult(body: UserSearch){
  return this.http.post(this.BaseURI + '/search', body);
}

}
