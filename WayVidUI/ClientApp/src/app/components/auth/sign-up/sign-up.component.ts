import { Component, OnInit } from "@angular/core";
import { FormBuilder, Validators } from "@angular/forms";
import { ToastrService } from "ngx-toastr";
import { SignInModel } from "src/app/models/auth/sign-in-model";
import { AuthService } from "src/app/services/auth/auth.service";
import { RoleType } from "../../../enums/role-types";

@Component({
  templateUrl: "./sign-up.component.html",
  styleUrls: ["./sign-up.component.css"],
})
export class SignUpComponent implements OnInit {
  signUpForm = this.fb.group({
    username: [
      "",
      [Validators.required, Validators.minLength(4), Validators.maxLength(25)],
    ],
    password: [
      "",
      [Validators.required, Validators.minLength(8), Validators.maxLength(25)],
    ],
    repeatedPassword: [
      "",
      [Validators.required, Validators.minLength(8), Validators.maxLength(25)],
    ],
    userRole: [RoleType.Visitor],
  });

  constructor(
    private authService: AuthService,
    private fb: FormBuilder,
    private toastr: ToastrService
  ) {}

  ngOnInit() {}

  public onSignUp() {
    if (this.signUpForm.valid) {
      this.authService.signUp(this.signUpForm.value).subscribe(
        (response) => {
          let signInModel: SignInModel = new SignInModel();
          signInModel.username = this.signUpForm.controls.userName.value;
          signInModel.password = this.signUpForm.controls.password.value;
          signInModel.rememberMe = true;
          this.toastr.success("Registration complete", "Success");
          this.authService.login(signInModel).subscribe((resp) => {
            if (resp.success) this.authService.navigateToUserPage();
            else this.toastr.error("But login failed...", "Error");
          });
          console.log(response);
        },
        (error) => {
          this.toastr.error("For more information, go to the console", "Error");
          console.log(error);
        }
      );
    }
  }
}
