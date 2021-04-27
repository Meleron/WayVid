import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";
import { VisitorDashboardComponent } from "./dashboard/visitor-dashboard.component";

const routes: Routes = [
  {
    path: "",
    children: [
      { path: "", component: VisitorDashboardComponent, pathMatch: "full" },
    ],
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class VisitorRoutingModule {}
