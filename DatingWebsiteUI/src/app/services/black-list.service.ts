import { Injectable } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class BlackListService {

  constructor(private fb: FormBuilder, private http: HttpClient) { }
  readonly BaseURI = 'https://localhost:44394/api'; 

  sendRequest(id: string){
    return this.http.get(this.BaseURI + '/blacklist/'+id.toString());
  }

  removeRequest(id: string){
    return this.http.delete(this.BaseURI + '/blacklist/'+id.toString());
  }

  getMyBlackList(){
    return this.http.get(this.BaseURI + '/blacklist/outgoing');
  }

  getBlackListWithMe(){
    return this.http.get(this.BaseURI + '/blacklist/incoming');
  }
}
