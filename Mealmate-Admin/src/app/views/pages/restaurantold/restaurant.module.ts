// Angular
import { NgModule } from "@angular/core";
import { RouterModule } from "@angular/router";
import { CommonModule } from "@angular/common";
import {
  NgbDropdownModule,
  NgbTabsetModule,
  NgbTooltipModule,
} from "@ng-bootstrap/ng-bootstrap";
// Core Module
import { CoreModule } from "../../../core/core.module";
import { PartialsModule } from "../../partials/partials.module";
import { RestaurantComponent } from "./restaurant.component";

@NgModule({
  imports: [
    CommonModule,
    PartialsModule,
    CoreModule,
    RouterModule.forChild([
      {
        path: "",
        component: RestaurantComponent,
      },
    ]),
    // ng-bootstrap modules
    NgbDropdownModule,
    NgbTabsetModule,
    NgbTooltipModule,
  ],
  providers: [],
  declarations: [RestaurantComponent],
})
export class RestaurantModule {}
