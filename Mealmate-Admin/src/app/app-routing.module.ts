// Angular
import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";
import { RouterModule, Routes } from "@angular/router";
// Components
import { BaseComponent } from "./views/theme/base/base.component";
// Auth
import { AuthGuard } from "./core/auth";
import { MenuComponent } from "./views/pages/menu/menu.component";
import { RestaurantComponent } from "./views/pages/restaurant/restaurant.component";
import { AllergensComponent } from "./views/pages/allergens/allergens.component";
import { DietariesComponent } from "./views/pages/dietaries/dietaries.component";
import { OptionsComponent } from "./views/pages/options/options.component";
import { ConfigComponent } from "./views/pages/config/config.component";
import { StatesComponent } from "./views/pages/states/states.component";
import { QrcodesComponent } from "./views/pages/qrcodes/qrcodes.component";

const routes: Routes = [
  {
    path: "auth",
    loadChildren: () =>
      import("./views/pages/auth/auth.module").then((m) => m.AuthModule),
  },
  {
    path: "error",
    loadChildren: () =>
      import("./views/pages/error/error.module").then((m) => m.ErrorModule),
  },
  {
    path: "",
    component: BaseComponent,
    canActivate: [AuthGuard],
    children: [
      {
        path: "dashboard",
        loadChildren: () =>
          import("./views/pages/dashboard/dashboard.module").then(
            (m) => m.DashboardModule
          ),
      },
      {
        path: "menu", // <= Page URL
        component: MenuComponent, // <= Page component registration
      },
      {
        path: "restaurant", // <= Page URL
        component: RestaurantComponent, // <= Page component registration
      },
      {
        path: "settings/allergens", // <= Page URL
        component: AllergensComponent, // <= Page component registration
      },
      {
        path: "settings/dietaries", // <= Page URL
        component: DietariesComponent, // <= Page component registration
      },
      {
        path: "settings/options", // <= Page URL
        component: OptionsComponent, // <= Page component registration
      },
      {
        path: "settings/configurations", // <= Page URL
        component: ConfigComponent, // <= Page component registration
      },
      {
        path: "admin", // <= Page URL
        component: StatesComponent, // <= Page component registration
      },
      {
        path: "qrcodes", // <= Page URL
        component: QrcodesComponent, // <= Page component registration
      },

      // {
      //   path: "restaurant",
      //   loadChildren: () =>
      //     import("./views/pages/apps/e-commerce/e-commerce.module").then(
      //       (m) => m.ECommerceModule
      //     ),
      // },
      // {
      //   path: "menu",
      //   loadChildren: () =>
      //     import("./views/pages/apps/menu/menu.module").then(
      //       (m) => m.MenuModule
      //     ),
      // },
      // {
      //   path: "qrcodes",
      //   loadChildren: () =>
      //     import("./views/pages/apps/qr-management/qr.module").then(
      //       (m) => m.QrModule
      //     ),
      // },
      {
        path: "mail",
        loadChildren: () =>
          import("./views/pages/apps/mail/mail.module").then(
            (m) => m.MailModule
          ),
      },
      {
        path: "ecommerce",
        loadChildren: () =>
          import("./views/pages/apps/e-commerce/e-commerce.module").then(
            (m) => m.ECommerceModule
          ),
      },
      {
        path: "ngbootstrap",
        loadChildren: () =>
          import("./views/pages/ngbootstrap/ngbootstrap.module").then(
            (m) => m.NgbootstrapModule
          ),
      },
      {
        path: "material",
        loadChildren: () =>
          import("./views/pages/material/material.module").then(
            (m) => m.MaterialModule
          ),
      },
      {
        path: "user-management",
        loadChildren: () =>
          import("./views/pages/user-management/user-management.module").then(
            (m) => m.UserManagementModule
          ),
      },
      {
        path: "wizard",
        loadChildren: () =>
          import("./views/pages/wizard/wizard.module").then(
            (m) => m.WizardModule
          ),
      },
      {
        path: "builder",
        loadChildren: () =>
          import("./views/theme/content/builder/builder.module").then(
            (m) => m.BuilderModule
          ),
      },
      { path: "", redirectTo: "dashboard", pathMatch: "full" },
      { path: "**", redirectTo: "dashboard", pathMatch: "full" },
    ],
  },
  { path: "**", redirectTo: "error/403", pathMatch: "full" },
];

@NgModule({
  imports: [CommonModule, RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
