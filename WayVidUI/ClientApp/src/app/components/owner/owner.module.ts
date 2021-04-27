import { NgModule } from "@angular/core";
import { RouterModule } from "@angular/router";
import { OwnerDashboardComponent } from "./dashboard/owner-dashboard.component";
import { OwnerRoutingModule } from "./owner-routing.module";

@NgModule({
  declarations: [OwnerDashboardComponent],
  imports: [OwnerRoutingModule]
})
export class OwnerModule {}
