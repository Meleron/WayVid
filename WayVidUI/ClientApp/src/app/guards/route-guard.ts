import { Injectable } from "@angular/core";
import { CanLoad, Router, CanActivate } from "@angular/router";
import { OAuthService } from "angular-oauth2-oidc";
import { Observable } from "rxjs";
import { AuthService } from "../services/auth/auth.service";

@Injectable({
  providedIn: "root",
})
export class RouteGuard implements CanLoad, CanActivate {
  constructor(
    private ssoService: OAuthService,
    private authService: AuthService,
    private router: Router
  ) {}

  canLoad(): boolean {
    if (this.ssoService.hasValidAccessToken()) return true;
    this.authService.clearLocalStorage();
    this.router.navigateByUrl("/");
    return false;
  }

  canActivate(): boolean | Observable<boolean> {
    return this.canLoad();
  }
}
