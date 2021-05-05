import { Injectable } from "@angular/core";
import { CanLoad, Router, CanActivate } from "@angular/router";
import { OAuthService } from "angular-oauth2-oidc";
import { Observable } from "rxjs";
import { AuthService } from "../services/auth/auth.service";
import { map, take } from "rxjs/operators";

@Injectable({
  providedIn: "root",
})
export class RouteGuard implements CanLoad, CanActivate {
  constructor(
    private authService: AuthService,
    private ssoService: OAuthService,
    private router: Router
  ) {}

  canLoad(): boolean | Observable<boolean> {
    return this.authService.isLoggedIn.pipe(
      map((isLoggedInResp) => {
        if (isLoggedInResp) {
          return true;
        }
        this.authService.clearAuthLocalStorage();
        this.router.navigateByUrl("/");
        return false;
      }),
      // Please don't forget it for another time!!!
      take(1)
    );
  }

  canActivate(): boolean | Observable<boolean> {
    return this.canLoad();
  }
}
