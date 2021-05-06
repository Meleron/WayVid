import { Component, OnInit } from "@angular/core";
import { TestService } from "src/app/services/test/test.service";
import {
  FormBuilder,
  FormControl,
  FormGroup,
  Validators,
} from "@angular/forms";
import { AuthService } from "../../../services/auth/auth.service";
import { ToastrService } from "ngx-toastr";
import { OAuthService } from "angular-oauth2-oidc";
import { RoleType } from "src/app/enums/role-types";
import { Router } from "@angular/router";

@Component({
  selector: "app-login",
  templateUrl: "./login.component.html",
  styleUrls: ["./login.component.css"],
})
export class LoginComponent implements OnInit {
  loginForm = this.fb.group({
    username: [
      "",
      [Validators.required, Validators.minLength(4), Validators.maxLength(25)],
    ],
    password: [
      "",
      [Validators.required, Validators.minLength(8), Validators.maxLength(25)],
    ],
    rememberMe: [true],
  });

  constructor(
    private authService: AuthService,
    private fb: FormBuilder,
    private toastr: ToastrService,
    private ssoService: OAuthService,
    private router: Router
  ) {
    this.authService = authService;
  }

  ngOnInit() {}

  public onLogin() {
    if (this.loginForm.valid) {
      this.authService.login(this.loginForm.value).subscribe((response) => {
        if (response.success) {
          this.toastr.success("Login succeeded", "Success");
          this.router.navigateByUrl("/visitor");
        } else {
          this.toastr.error(response.errorMessage, "Error");
        }
      });
    }
  }

  public test() {
    this.authService.isLoggedIn.subscribe((isLoggedInResp) => {
      console.log(`Login status: ${isLoggedInResp}`);
      console.log(`UserName: ${this.authService.getUsername()}`);
      console.log(`Role: ${JSON.stringify(this.authService.getUserRoles())}`);
      console.log(`AccessToken: ${this.ssoService.getAccessToken()}`);
    });
  }
}
