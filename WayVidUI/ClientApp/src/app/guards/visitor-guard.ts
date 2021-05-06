import { Injectable } from "@angular/core";
import { CanLoad, Router, CanActivate } from "@angular/router";
import { Observable } from "rxjs";
import { RoleType } from "../enums/role-types";
import { AuthService } from "../services/auth/auth.service";

@Injectable({
  providedIn: "root",
})
export class VisitorGuard implements CanLoad, CanActivate {
  constructor(private authService: AuthService, private router: Router) {}

  canLoad(): boolean {
    if (this.authService.isInRole(RoleType.Visitor)) return true;

    // if the user is not a visitor, navigate it to the login page
    this.router.navigateByUrl("/");
    return false;
  }

  canActivate(): boolean | Observable<boolean> {
    return this.canLoad();
  }
}
