import { Component, OnInit, ViewChild } from '@angular/core';
import { Router } from '@angular/router';
import { UserService } from 'src/app/services/user.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-first-login',
  templateUrl: './first-login.component.html',
  styleUrls: ['./first-login.component.css']
})
export class FirstLoginComponent implements OnInit {
  
  constructor(private router:Router,
    private service : UserService,
    private formBuilder : FormBuilder, 
    private toastr : ToastrService) { }

    firstLoginForm: FormGroup;
    submitted = false;

  ngOnInit() {
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
    });
  }
  
  get f() { return this.firstLoginForm.controls; }

  onSubmit() {
    this.submitted = true; 

    if (this.firstLoginForm.invalid) {
        return null;
    } 

    this.service.editUserInfo(this.firstLoginForm).subscribe(
      (res: any) => {  
          this.toastr.success('New user created!', 'Registration successful.');
          this.router.navigate(['/home/profile/0']); 
      },
      err => {
        console.log(err);
      }
    );
}
    
  uploadPhoto(){ 
    this.service.uploadAvatar(this.firstLoginForm.value.Photo).subscribe(
      (res: any) => {  
          this.toastr.success('New photo created!', 'Registration successful.'); 
      },
      err => {
        console.log(err);
      }
    );
  }
  }
