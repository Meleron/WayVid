import { Route } from "@angular/compiler/src/core";
import { Component, OnInit } from "@angular/core";
import { ActivatedRoute, NavigationEnd, Router } from "@angular/router";
import { ToastrService } from "ngx-toastr";
import { Observable } from "rxjs";
import { RoleType } from "src/app/enums/role-types";
import { AuthService } from "src/app/services/auth/auth.service";
import { VisitorService } from "src/app/services/visitor.service";
import { map } from "rxjs/operators";

@Component({
  selector: "app-layout",
  templateUrl: "./layout.component.html",
  styleUrls: ["./layout.component.css"],
})
export class LayoutComponent implements OnInit {
  public currentRouteBase: string = "";

  constructor(
    private authService: AuthService,
    private visitorService: VisitorService,
    private toastrService: ToastrService,
    private router: Router,
    private activatedRoute: ActivatedRoute
  ) {
    router.events.subscribe((resp) => {
      if (resp instanceof NavigationEnd) {
        this.currentRouteBase = resp.urlAfterRedirects;
      }
    });
  }

  ngOnInit() {}

  public isOwnerNavPossible(): boolean {
    const temp = this.isOwner() && this.currentRouteBase === "/visitor";
    return temp;
  }

  public isVisitorNavPossible(): boolean {
    return this.isVisitor() && this.currentRouteBase === "/owner";
  }

  public isVisitor(): boolean {
    return this.authService.isInRole(RoleType.Visitor);
  }

  public isOwner(): boolean {
    return this.authService.isInRole(RoleType.Owner);
  }

  public logOut() {
    this.authService.logOut();
  }

  public onOwnerRoleRequest() {
    this.visitorService.onOwnerRoleRequest().subscribe((resp) => {
      this.toastrService.success("Owner role received!", "Success");
    });
  }

  public goToOwnerDashboard() {
    this.router.navigateByUrl("/owner");
  }

  public goToVisitorDashboard() {
    this.router.navigateByUrl("/visitor");
  }
}
