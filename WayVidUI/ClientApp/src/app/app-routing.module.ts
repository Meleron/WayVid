import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";
import { LoginComponent } from "./components/auth/login/login.component";
import { SignUpComponent } from "./components/auth/sign-up/sign-up.component";
import { NotFoundComponent } from "./components/common/not-found/not-found.component";
import { RouteGuard } from "./guards/route-guard";
import { VisitorGuard } from "./guards/visitor-guard";

const routes: Routes = [
  { path: "", component: LoginComponent, pathMatch: "full" },
  { path: "login", component: LoginComponent, pathMatch: "full" },
  { path: "signup", component: SignUpComponent, pathMatch: "full" },
  {
    path: "owner",
    pathMatch: "full",
    loadChildren: () =>
      import("./components/owner/owner.module").then((m) => m.OwnerModule),
  },
  {
    path: "visitor",
    pathMatch: "full",
    canActivate: [RouteGuard, VisitorGuard],
    canLoad: [RouteGuard, VisitorGuard],
    loadChildren: () =>
      import("./components/visitor/visitor.module").then(
        (m) => m.VisitorModule
      ),
  },
  { path: "**", component: NotFoundComponent },
];

@NgModule({
  declarations: [],
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
