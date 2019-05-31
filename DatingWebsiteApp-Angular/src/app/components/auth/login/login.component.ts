import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { Router } from '@angular/router';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  constructor(private router:Router,
    private service:UserService,
    private formBuilder: FormBuilder, 
    private toastr: ToastrService) { }

    loginForm: FormGroup;
    submitted = false;

  ngOnInit() {
    if (localStorage.getItem('token') != null)
  this.router.navigateByUrl('/home/search');
    this.loginForm = this.formBuilder.group({  
        Email: ['', [Validators.required, Validators.email]],
        Password: ['', [Validators.required, Validators.minLength(6)]]
    });
  }
  
  get f() { return this.loginForm.controls; }

  onSubmit() {
    this.submitted = true; 

    if (this.loginForm.invalid) {
        return;
    } 

    this.service.login(this.loginForm).subscribe(
      (res: any) => {
        localStorage.setItem('token', res.token); 
        //localStorage.setItem('currentUser', res.currentUser);
        this.router.navigateByUrl('/home/search');
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
      //this.registerForm ???
      
      } 
    }
