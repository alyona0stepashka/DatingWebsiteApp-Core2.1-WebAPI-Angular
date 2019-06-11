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
    DateBirth: ['', [Validators.required]],
    // Name: [this.userProfile.Name, [Validators.required]],
    // OldPassword: [''],
    // NewPassword: [''],
    // IsAnonimus: [this.userProfile.IsAnonimus, [Validators.required]],
    Sex: ['', [Validators.required]],
    MainGoal: ['', [Validators.required]],
    FamilyStatus: ['', [Validators.required]],
    FinanceStatus: ['', [Validators.required]],
    Education: ['', [Validators.required]],
    Nationality: ['', [Validators.required]],
    Zodiac: ['', [Validators.required]],
    Growth: ['', [Validators.required]],
    Weight: ['', [Validators.required]],
    Photo: [null, [Validators.required]],
    BadHabits: [null, [Validators.required]],
    Interests: [null, [Validators.required]],
    Languages: [null, [Validators.required]],
  });
  submitted = false;
  imageUrl = 'https://localhost:44394';
  userProfile = new UserProfile();
  editLanguages: number[] = null;
  editBadHabits: number[] = null;
  editInterests: number[] = null;
  public staticInfo = new Static();

  async ngOnInit() {
    await this.staticService.getAll().subscribe(
      res => {
        this.staticInfo = res as Static;
      },
      err => {
        console.log(err);
      }
    );
    await this.service.getMyProfile().subscribe(
    res => {
      this.userProfile = res as UserProfile;
      this.imageUrl = this.imageUrl + this.userProfile.PhotoPath;
      this.userProfile.Languages.forEach(element => {
        this.editLanguages.push(element.Id);
      });
      this.userProfile.Interests.forEach(element => {
        this.editInterests.push(element.Id);
      });
      this.userProfile.BadHabits.forEach(element => {
        this.editBadHabits.push(element.Id);
      });
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

    this.service.addUserInfo(this.editInfoForm).subscribe(
      (res: any) => {
        this.toastr.success('New user created!', 'Registration successful.');
        this.router.navigate(['/home/search']);
      },
      err => {
        console.log(err);
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
          this.toastr.success('New photo created!', 'Registration successful.');
      },
      err => {
        console.log(err);
      }
    );
  }


}
