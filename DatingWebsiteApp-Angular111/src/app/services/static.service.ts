import { Injectable } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { HttpClient } from 'selenium-webdriver/http';

@Injectable({
  providedIn: 'root'
})
export class StaticService {

  
  constructor(private fb: FormBuilder, private http: HttpClient) { }
  readonly BaseURI = 'http://localhost:44394/api'; 
     
  getAllStatic() { 
    return this.http.get(this.BaseURI + '/search');
  }

  login(loginFormData: FormGroup) {
    var body = { 
      Email: loginFormData.value.Email, 
      Password: loginFormData.value.Passwords.Password
    };
    return this.http.post(this.BaseURI + '/accoun/login', body);
  }
}