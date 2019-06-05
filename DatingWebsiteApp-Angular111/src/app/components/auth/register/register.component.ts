import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms'; 
import { ToastrService } from 'ngx-toastr';
import { UserService } from 'src/app/services/user.service'; 
import { Router } from '@angular/router';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styles: []
})
export class RegisterComponent implements OnInit {

  constructor(private router:Router,
    private service:UserService,
    private formBuilder: FormBuilder, 
    private toastr: ToastrService) { }

    registerForm: FormGroup;
    submitted = false;

  ngOnInit() {
    this.registerForm = this.formBuilder.group({ 
      Name: ['', [Validators.required]],
        Email: ['', [Validators.required, Validators.email]],
        Password: ['', [Validators.required, Validators.minLength(6)]]
    });
  }
  
  get f() { return this.registerForm.controls; }

  onSubmit() {
    this.submitted = true; 

    if (this.registerForm.invalid) {
        return;
    } 

    this.service.register(this.registerForm).subscribe(
      (res: any) => {
        if (res.succeeded) {
          this.resetForm();
          this.toastr.success('New user created!', 'Registration successful.');
          this.router.navigate(['/auth/login']);
        } else {
          res.errors.forEach(element => {
            switch (element.code) {
              case 'DuplicateUserName':
                this.toastr.error('Username is already taken','Registration failed.');
                break;

              default:
              this.toastr.error(element.description,'Registration failed.');
                break;
            }
          });
        }
      },
      err => {
        console.log(err);
      }
    );
    
}

    resetForm() { 
      //this.registerForm ???
      
      } 
    }
