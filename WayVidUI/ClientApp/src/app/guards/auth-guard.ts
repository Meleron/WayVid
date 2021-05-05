import { Injectable } from "@angular/core";
import { CanLoad, Router, CanActivate } from "@angular/router";
import { OAuthService } from "angular-oauth2-oidc";
import { Observable } from "rxjs";
import { flatMap } from "rxjs/operators";
import { RoleType } from "../enums/role-types";
import { AuthService } from "../services/auth/auth.service";
import { map } from "rxjs/operators";

@Injectable({
  providedIn: "root",
})
export class AuthGuard implements CanActivate {
  constructor(private authService: AuthService, private router: Router) {}

  canActivate(): boolean | Observable<boolean> {
    return this.authService.isLoggedIn.pipe(
      map((resp) => {
        // if the user is logged in, navigate it to the appropriate page based on its Role
        if (resp) this.authService.navigateToUserPage();
        // otherwise just let it open the login page
        return true;
      })
    );
  }
}
