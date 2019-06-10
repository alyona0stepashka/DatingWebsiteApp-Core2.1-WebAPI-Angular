import { Component, OnInit, ViewChild } from '@angular/core';
import { Router } from '@angular/router';
import { UserService } from 'src/app/services/user.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { Static } from 'src/app/models/static.model';
import { StaticService } from 'src/app/services/static.service';

@Component({
  selector: 'app-first-login',
  templateUrl: './first-login.component.html',
  styleUrls: ['./first-login.component.css']
})
export class FirstLoginComponent implements OnInit {

  constructor(private router: Router,
              private service: UserService,
              private formBuilder: FormBuilder,
              private toastr: ToastrService,
              private staticService: StaticService) { }

    UploadFile: File = null;
    firstLoginForm: FormGroup;
    submitted = false;
    public staticInfo = new Static();
    imageUrl = '/assets/img/no-image.png';

  async ngOnInit() {
    this.firstLoginForm = this.formBuilder.group({
      DateBirth: [null, [Validators.required]],
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
      Languages: [null, [Validators.required]],
      Interests: [null, [Validators.required]],
    });
    await this.staticService.getAll().subscribe(
      res => {
        this.staticInfo = res as Static;
      },
      err => {
        console.log(err);
      }
    );
  }

  get f() { return this.firstLoginForm.controls; }

  onSubmit() {
    this.submitted = true;
    if (this.firstLoginForm.invalid) {
        return null;
    }

    this.service.addUserInfo(this.firstLoginForm).subscribe(
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
