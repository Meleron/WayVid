import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";
import { OwnerDashboardComponent } from "./dashboard/owner-dashboard.component";

const routes: Routes = [
  { path: "", component: OwnerDashboardComponent, pathMatch: "full" },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class OwnerRoutingModule {}
