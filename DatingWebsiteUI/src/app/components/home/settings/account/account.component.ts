import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { UserService } from 'src/app/services/user.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { StaticService } from 'src/app/services/static.service';
import { UserProfile } from 'src/app/models/user-profile.model'; 

@Component({
  selector: 'app-account',
  templateUrl: './account.component.html',
  styleUrls: ['./account.component.css']
})
export class AccountComponent implements OnInit {
  constructor(private router: Router,
              private service: UserService,
              private formBuilder: FormBuilder,
              private toastr: ToastrService,
              private staticService: StaticService) { }

  UploadFile: File = null;
  editInfoForm: FormGroup = this.formBuilder.group({
  Name: ['', [Validators.required]],
  OldPassword: [''],
  NewPassword: [''],
  IsAnonimus: [null, [Validators.required]]
  });
  submitted = false;
  userProfile = new UserProfile();

  async ngOnInit() {
    await this.service.getMyProfile().subscribe(
    res => {
      this.userProfile = res as UserProfile;
      this.editInfoForm.value.Name = this.userProfile.Name;
      this.editInfoForm.value.IsAnonimus = this.userProfile.IsAnonimus;
    },
    err => {
      console.log(err);
    }
    );
  }

  get f() { return this.editInfoForm.controls; }

  onSubmit() {
    this.submitted = true;
    if (this.editInfoForm.invalid) {
        return null;
    }

    this.service.editUserInfo(this.editInfoForm).subscribe(
      (res: any) => {
        this.toastr.success('New user created!', 'Registration successful.'); 
      },
      err => {
       console.log(err);
      }
    );
  }
}
