import { NgModule } from "@angular/core";
import { VisitorDashboardComponent } from "./dashboard/visitor-dashboard.component";
import { VisitorRoutingModule } from "./visitor-routing.module";

@NgModule({
  declarations: [VisitorDashboardComponent],
  imports: [VisitorRoutingModule],
})
export class VisitorModule {}
