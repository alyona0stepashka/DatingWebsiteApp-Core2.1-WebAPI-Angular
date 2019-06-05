import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { UserService } from 'src/app/services/user.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-my-profile',
  templateUrl: './my-profile.component.html',
  styleUrls: ['./my-profile.component.css']
})
export class MyProfileComponent implements OnInit {

  constructor(private router:Router,
    private service:UserService,
    private formBuilder: FormBuilder, 
    private toastr: ToastrService) { }

    editForm: FormGroup;
    submitted = false;

  ngOnInit() { 
    this.editForm = this.formBuilder.group({  
        Email: ['', [Validators.required, Validators.email]],
        Password: ['', [Validators.required, Validators.minLength(6)]]
    });
  }
  
  get f() { return this.editForm.controls; }

  onSubmit() {
    this.submitted = true; 

    if (this.editForm.invalid) {
        return null;
    } 

    this.service.editUserInfo(this.editForm).subscribe(
      (res: any) => {
        this.toastr.success('Success editInfo', 'Edit User Info.');
      },
      err => {
        if (err.status == 400)
          this.toastr.error('Incorrect username or password.', 'Authentication failed.');
        else
          console.log(err);
      }
    );
    
}

    resetForm() { 
      this.editForm.value.Name = '';
      this.editForm.value.Email= '';
      this.editForm.value.Password = ''; 
      } 
    }
