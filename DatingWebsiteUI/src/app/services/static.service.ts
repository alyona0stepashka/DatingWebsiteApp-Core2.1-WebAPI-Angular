import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class StaticService {

  constructor(private http: HttpClient) { }
  readonly BaseURI = 'https://localhost:44394/api'; 

  getAll(){
    return this.http.get(this.BaseURI + '/static');
  }
}
