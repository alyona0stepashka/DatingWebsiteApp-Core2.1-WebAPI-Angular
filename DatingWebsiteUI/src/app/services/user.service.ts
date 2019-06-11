import { Injectable } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { HttpClient, HttpHeaders } from '@angular/common/http';
// import 'rxjs/add/operator/map';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(private fb: FormBuilder, private http: HttpClient) { }
  readonly BaseURI = 'https://localhost:44394/api';

  register(registerFormData: FormGroup) {
    const body = {
      Name: registerFormData.value.Name,
      Email: registerFormData.value.Email,
      Password: registerFormData.value.Password
    };
    return this.http.post(this.BaseURI + '/account/register', body);
  }

  login(loginFormData: FormGroup) {
    const body = {
      Email: loginFormData.value.Email,
      Password: loginFormData.value.Password
    };
    return this.http.post(this.BaseURI + '/account/login', body);
  }

  getMyProfile() {
    return this.http.get(this.BaseURI + '/users/current');
  }

  getUserProfileById(id: string) {
    return this.http.get(this.BaseURI + '/users/' + id.toString());
  }

  addUserInfo(form: FormGroup) {
    const body = {
      DateBirth: form.value.DateBirth,
      Sex: form.value.Sex,
      MainGoal: form.value.MainGoal,
      FamilyStatus: form.value.FamilyStatus,
      FinanceStatus: form.value.FinanceStatus,
      Education: form.value.Education,
      Nationality: form.value.Nationality,
      Zodiac: form.value.Zodiac,
      Growth: form.value.Growth,
      Weight: form.value.Weight,
      Languages: form.value.Languages,
      Interests: form.value.Interests,
      BadHabits: form.value.BadHabits
    };
    return this.http.put(this.BaseURI + '/users', body);
  }

  editUserInfo(form: FormGroup) {
    const body = {
      Name: form.value.Name,
      IsAnonimus: form.value.IsAnonimus,
      NewPassword: form.value.Passwords.NewPassword,
      OldPassword: form.value.Passwords.OldPassword
    };
    return this.http.put(this.BaseURI + '/account', body);
  }

  editUserPhoto(fileToUpload: File) {
    const formData: FormData = new FormData();
    formData.append('Image', fileToUpload);
    return this.http.put(this.BaseURI +  '/users/avatar', formData);
  }
}
