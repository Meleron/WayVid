import { Component, OnInit } from '@angular/core';
import { TestService } from 'src/app/services/test/test.service';
import {FormBuilder, FormControl, FormGroup, Validators} from '@angular/forms';
import {AuthService} from '../../../services/auth/auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  loginForm = this.fb.group(
    {
      username: ['', Validators.required],
      password: ['', Validators.required],
      rememberMe: [true]
    }
  );

  constructor(private authService: AuthService, private fb: FormBuilder) {
    this.authService = authService;
  }

  ngOnInit() {

  }

  public onLogin() {
    this.authService.login(this.loginForm.value).subscribe(response => {
      console.log(response);
    });
  }
}
