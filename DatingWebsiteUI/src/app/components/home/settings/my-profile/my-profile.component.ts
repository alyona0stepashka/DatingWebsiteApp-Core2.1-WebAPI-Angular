import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { UserService } from 'src/app/services/user.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { StaticService } from 'src/app/services/static.service';
import { Static } from 'src/app/models/static.model';
import { UserProfile } from 'src/app/models/user-profile.model';

@Component({
  selector: 'app-my-profile',
  templateUrl: './my-profile.component.html',
  styleUrls: ['./my-profile.component.css']
})
export class MyProfileComponent implements OnInit {

  constructor(private router: Router,
              private service: UserService,
              private formBuilder: FormBuilder,
              private toastr: ToastrService,
              private staticService: StaticService) { }

  UploadFile: File = null;
  editInfoForm: FormGroup = this.formBuilder.group({
    DateBirth: [null/*, [Validators.required]*/],
    // Name: [this.userProfile.Name, [Validators.required]],
    // OldPassword: [''],
    // NewPassword: [''],
    // IsAnonimus: [this.userProfile.IsAnonimus, [Validators.required]],
    Sex: [''/*, [Validators.required]*/],
    MainGoal: [''/*, [Validators.required]*/],
    FamilyStatus: [''/*, [Validators.required]*/],
    FinanceStatus: [''/*, [Validators.required]*/],
    Education: [''/*, [Validators.required]*/],
    Nationality: [''/*, [Validators.required]*/],
    Zodiac: [''/*, [Validators.required]*/],
    Growth: [''/*, [Validators.required]*/],
    Weight: [''/*, [Validators.required]*/],
    Photo: [null/*, [Validators.required]*/],
    BadHabits: [null/*, [Validators.required]*/],
    Interests: [null/*, [Validators.required]*/],
    Languages: [null/*, [Validators.required]*/],
  });
  submitted = false;
  imageUrl = 'https://localhost:44394';
  userProfile = new UserProfile();
  // editLanguages: number[] = new Array();
  // editBadHabits: number[] = new Array();
  // editInterests: number[] = new Array();
  public staticInfo = new Static();

  async ngOnInit() {
    await this.staticService.getAll().subscribe(
      res => {
        this.staticInfo = res as Static;
      },
      err => {
        console.log(err);
        this.toastr.error(err.error, 'Error');
      }
    );
    await this.service.getMyProfile().subscribe(
    res => {
      console.log(res);
      this.userProfile = res as UserProfile;
      this.imageUrl = this.imageUrl + this.userProfile.PhotoPath;
      if (this.userProfile.DateBirth) {
        this.editInfoForm.value.DateBirth = this.userProfile.DateBirth;
      }
      if (this.userProfile.Sex) {
        this.editInfoForm.value.Sex = this.userProfile.Sex.Id;
      }
      if (this.userProfile.MainGoal) {
        this.editInfoForm.value.MainGoal = this.userProfile.MainGoal.Id;
      }
      if (this.userProfile.FamilyStatus) {
        this.editInfoForm.value.FamilyStatus = this.userProfile.FamilyStatus.Id;
      }
      if (this.userProfile.FinanceStatus) {
        this.editInfoForm.value.FinanceStatus = this.userProfile.FinanceStatus.Id;
      }
      if (this.userProfile.Education) {
        this.editInfoForm.value.Education = this.userProfile.Education.Id;
      }
      if (this.userProfile.Nationality) {
        this.editInfoForm.value.Nationality = this.userProfile.Nationality.Id;
      }
      if (this.userProfile.Zodiac) {
        this.editInfoForm.value.Zodiac = this.userProfile.Zodiac.Id;
      }
      if (this.userProfile.Growth) {
        this.editInfoForm.value.Growth = this.userProfile.Growth;
      }
      if (this.userProfile.Weight) {
        this.editInfoForm.value.Weight = this.userProfile.Weight;
      }
      if (this.userProfile.Languages && this.userProfile.Languages.length > 0) {
        this.userProfile.Languages.forEach(element => {
          this.editInfoForm.value.Languages.push(element.Id);
        });
      }
      if (this.userProfile.Interests && this.userProfile.Interests.length > 0) {
        this.userProfile.Interests.forEach(element => {
          this.editInfoForm.value.Interests.push(element.Id);
        });
      }
      if (this.userProfile.BadHabits && this.userProfile.BadHabits.length > 0) {
        this.userProfile.BadHabits.forEach(element => {
          this.editInfoForm.value.BadHabits.push(element.Id);
        });
      }
    },
    err => {
      console.log(err);
      this.toastr.error(err.error, 'Error');
    }
  );
  }

  get f() { return this.editInfoForm.controls; }

  onSubmit() {
    this.submitted = true;
    if (this.editInfoForm.invalid) {
      return null;
    }

    this.service.addUserInfo(this.editInfoForm).subscribe(
      (res: any) => {
        this.toastr.success('New info added!', 'Success.');
        this.router.navigate(['/home/profile/0']);
      },
      err => {
        console.log(err);
        this.toastr.success('New info added!', 'Success.');
        this.router.navigate(['/home/profile/0']);
        // this.toastr.error(err, 'Error');
      }
    );
  }

  uploadPhoto(file: FileList) {
    this.UploadFile = file.item(0);
    const reader = new FileReader();
    reader.onload = (event: any) => {
      this.imageUrl = event.target.result;
    };
    reader.readAsDataURL(this.UploadFile);

    this.service.editUserPhoto(this.UploadFile).subscribe(
      (res: any) => {
          this.toastr.success('New photo created!', 'Success.');
      },
      err => {
        console.log(err);
        this.toastr.error(err.error, 'Error');
      }
    );
  }


}
