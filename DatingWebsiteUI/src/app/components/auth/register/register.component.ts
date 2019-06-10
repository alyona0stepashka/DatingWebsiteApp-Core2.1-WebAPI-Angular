import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: []
})
export class RegisterComponent implements OnInit {

  constructor(private router: Router,
              private service: UserService,
              private formBuilder: FormBuilder,
              private toastr: ToastrService) { }

    registerForm: FormGroup;
    submitted = false;
    userId = 0;

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
        return null;
    }

    this.service.register(this.registerForm).subscribe(
      (res: any) => {
          this.resetForm();
          this.toastr.success('New user created!', 'Registration successful.');
      },
      err => {
        console.log(err);
      }
    );

    if (this.userId != 0) {
          this.router.navigate(['/auth/first']);
    }

}

    resetForm() {
      this.registerForm.value.Name = '';
      this.registerForm.value.Email = '';
      this.registerForm.value.Password = '';
      }
    }
