import {Component, OnInit} from '@angular/core';
import {TestService} from 'src/app/services/test/test.service';
import {FormBuilder, FormControl, FormGroup, Validators} from '@angular/forms';
import {AuthService} from '../../../services/auth/auth.service';
import {ToastrService} from 'ngx-toastr';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  loginForm = this.fb.group(
    {
      username: ['', [Validators.required, Validators.minLength(4), Validators.maxLength(25)]],
      password: ['', [Validators.required, Validators.minLength(8), Validators.maxLength(25)]],
      rememberMe: [true]
    }
  );

  constructor(private authService: AuthService, private fb: FormBuilder, private toastr: ToastrService) {
    this.authService = authService;
  }

  ngOnInit() {

  }

  public onLogin() {
    if (!this.loginForm.invalid) {
      this.authService.login(this.loginForm.value).subscribe(response => {
          this.toastr.success('Logged as', 'Success');
          console.log(response);
        },
        error => {
          if (error.status === 401) {
            this.toastr.error('Invalid login attempt', 'Error');
          } else {
            this.toastr.error('For more information, go to the console', 'Error');
          }
          console.log(error);
        });
    }
  }
}
