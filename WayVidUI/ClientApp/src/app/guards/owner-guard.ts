import { Injectable } from "@angular/core";
import { CanActivate, CanLoad, Router } from "@angular/router";
import { RoleType } from "../enums/role-types";
import { AuthService } from "../services/auth/auth.service";

@Injectable({
  providedIn: "root",
})
export class OwnerGuard implements CanLoad, CanActivate {
  constructor(private authService: AuthService, private router: Router) {}

  public canActivate(): boolean {
    if (this.authService.isInRole(RoleType.Owner)) return true;

    // if the user is not an owner, navigate it to the login page
    this.router.navigateByUrl("/");
    return false;
  }

  public canLoad(): boolean {
    return this.canActivate();
  }
}
