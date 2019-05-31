import { Injectable } from '@angular/core';
import { FormBuilder, Validators, FormGroup, ReactiveFormsModule } from '@angular/forms';
import { HttpClient, HttpHeaders } from "@angular/common/http"; 

@Injectable({
  providedIn: 'root'
})

export class UserService {

  constructor(private fb: FormBuilder, private http: HttpClient) { }
  readonly BaseURI = 'http://localhost:44394/api'; 
     
  register(registerFormData: FormGroup) { 
    var body = {
      Name: registerFormData.value.Name,
      Email: registerFormData.value.Email, 
      Password: registerFormData.value.Passwords.Password
    };
    return this.http.post(this.BaseURI + '/account/register', body);
  }

  login(loginFormData: FormGroup) {
    var body = { 
      Email: loginFormData.value.Email, 
      Password: loginFormData.value.Passwords.Password
    };
    return this.http.post(this.BaseURI + '/accoun/login', body);
  }
}
