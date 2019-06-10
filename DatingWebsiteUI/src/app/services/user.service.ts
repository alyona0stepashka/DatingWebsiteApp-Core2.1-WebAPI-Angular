import { Injectable } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { HttpClient, HttpHeaders } from "@angular/common/http";  
//import 'rxjs/add/operator/map';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(private fb: FormBuilder, private http: HttpClient) { }
  readonly BaseURI = 'https://localhost:44394/api'; 
     
  register(registerFormData: FormGroup) { 
    var body = {
      Name: registerFormData.value.Name,
      Email: registerFormData.value.Email, 
      Password: registerFormData.value.Password
    };
    return this.http.post(this.BaseURI + '/account/register', body);
  }

  login(loginFormData: FormGroup) {
    var body = { 
      Email: loginFormData.value.Email, 
      Password: loginFormData.value.Password
    };
    return this.http.post(this.BaseURI + '/account/login', body);
  }

  getMyProfile()
  {
    return this.http.get(this.BaseURI + '/users/current');
  }

  getUserProfileById(id: string)
  {
    return this.http.get(this.BaseURI + '/users/'+id.toString());
  }

  uploadAvatar(file: any) {
    let input = new FormData();
    input.append("filesData", file);
    return this.http.put(this.BaseURI + '/users', input);
}
  editUserInfo(body: any)
  {
    return this.http.put(this.BaseURI + '/users', body)
  }

  editUserPhoto(body: any)
  {
    return this.http.put(this.BaseURI + '/users', body)
  }
}
