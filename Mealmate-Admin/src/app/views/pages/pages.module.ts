// Angular
import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";
import { FormsModule } from "@angular/forms";
import { HttpClientModule } from "@angular/common/http";
// Partials
import { PartialsModule } from "../partials/partials.module";
// Pages
import { CoreModule } from "../../core/core.module";
import { MailModule } from "./apps/mail/mail.module";
import { ECommerceModule } from "./apps/e-commerce/e-commerce.module";
import { UserManagementModule } from "./user-management/user-management.module";
import { MyPageComponent } from "./my-page/my-page.component";
import { MenuComponent } from "./menu/menu.component";
import { MatAutocompleteModule } from "@angular/material/autocomplete";
import { MatBadgeModule } from "@angular/material/badge";
import { MatBottomSheetModule } from "@angular/material/bottom-sheet";
import { MatButtonModule } from "@angular/material/button";
import { MatButtonToggleModule } from "@angular/material/button-toggle";
import { MatCardModule } from "@angular/material/card";
import { MatCheckboxModule } from "@angular/material/checkbox";
import { MatChipsModule } from "@angular/material/chips";
import { MatStepperModule } from "@angular/material/stepper";
import { MatDatepickerModule } from "@angular/material/datepicker";
import { MatDialogModule } from "@angular/material/dialog";
import { MatDividerModule } from "@angular/material/divider";
import { MatExpansionModule } from "@angular/material/expansion";
import { MatGridListModule } from "@angular/material/grid-list";
import { MatIconModule } from "@angular/material/icon";
import { MatInputModule } from "@angular/material/input";
import { MatListModule } from "@angular/material/list";
import { MatMenuModule } from "@angular/material/menu";
import { MatNativeDateModule, MatRippleModule } from "@angular/material/core";
import { MatPaginatorModule } from "@angular/material/paginator";
import { MatProgressBarModule } from "@angular/material/progress-bar";
import { MatProgressSpinnerModule } from "@angular/material/progress-spinner";
import { MatRadioModule } from "@angular/material/radio";
import { MatSelectModule } from "@angular/material/select";
import { MatSidenavModule } from "@angular/material/sidenav";
import { MatSliderModule } from "@angular/material/slider";
import { MatSlideToggleModule } from "@angular/material/slide-toggle";
import { MatSnackBarModule } from "@angular/material/snack-bar";
import { MatSortModule } from "@angular/material/sort";
import { MatTableModule } from "@angular/material/table";
import { MatTabsModule } from "@angular/material/tabs";
import { MatToolbarModule } from "@angular/material/toolbar";
import { MatTooltipModule } from "@angular/material/tooltip";
import { MatTreeModule } from "@angular/material/tree";
import { NgxPermissionsModule } from "ngx-permissions";
import { MenuEditDialogComponent } from "./menu/menu-edit/menu-edit.dialog.component";
import { ReactiveFormsModule } from "@angular/forms";
import { NgbAlertConfig, NgbModule } from "@ng-bootstrap/ng-bootstrap";
import { MenuItemEditDialogComponent } from "./menu/menuitem-edit/menuitem-edit.dialog.component";
import { MenuOptionEditDialogComponent } from "./menu/menuoptions-edit/menuoptions-edit.dialog.component";
import { RestaurantComponent } from "./restaurant/restaurant.component";
import { BranchEditDialogComponent } from "./restaurant/branch-edit/branch-edit.dialog.component";
import { HallEditDialogComponent } from "./restaurant/halls-edit/hall-edit.dialog.component";
import { TableEditDialogComponent } from "./restaurant/tables-edit/table-edit.dialog.component";
import { RestaurantEditDialogComponent } from "./restaurant/restaurant-edit/restaurant-edit.dialog.component";
import { AllergensEditDialogComponent } from "./allergens/allergens-edit/allergens-edit.dialog.component";
import { AllergensComponent } from "./allergens/allergens.component";
import { DietariesComponent } from "./dietaries/dietaries.component";
import { DietariesEditDialogComponent } from "./dietaries/dietaries-edit/dietariesedit.dialog.component";
import { OptionsComponent } from "./options/options.component";
import { OptionsEditDialogComponent } from "./options/options-edit/options-edit.dialog.component";
import { ConfigComponent } from "./config/config.component";
import { ConfirmDialogComponent } from "./config/confirm-dialog/confirm-dialog.component";
import { StatesComponent } from "./states/states.component";
import { QrcodesEditDialogComponent } from "./qrcodes/qrcodes-edit/qrcodes.dialog.component";
import { QrcodesComponent } from "./qrcodes/qrcodes.component";
import { PrintService } from "src/app/core/print/_services/print.service";
import { PrintqrComponent } from "./printqr/printqr.component";
import { PrEditDialogComponent } from "./qrcodes/printqr/pr.dialog.component";
import { QRCodeModule } from "angularx-qrcode";
import { NgxPrintModule } from "ngx-print";
@NgModule({
  declarations: [
    MyPageComponent,
    MenuComponent,
    MenuEditDialogComponent,
    MenuItemEditDialogComponent,
    MenuOptionEditDialogComponent,
    RestaurantComponent,
    BranchEditDialogComponent,
    HallEditDialogComponent,
    TableEditDialogComponent,
    RestaurantEditDialogComponent,
    AllergensComponent,
    AllergensEditDialogComponent,
    DietariesComponent,
    DietariesEditDialogComponent,
    OptionsComponent,
    OptionsEditDialogComponent,
    ConfigComponent,
    ConfirmDialogComponent,
    StatesComponent,
    QrcodesComponent,
    QrcodesEditDialogComponent,
    PrintqrComponent,
    PrEditDialogComponent,
  ],
  exports: [],

  imports: [
    CommonModule,
    HttpClientModule,
    FormsModule,
    CoreModule,
    PartialsModule,
    NgbModule,
    MailModule,
    ECommerceModule,
    NgxPermissionsModule.forChild(),
    UserManagementModule,
    MatAutocompleteModule,
    MatBadgeModule,
    MatBottomSheetModule,
    MatButtonModule,
    MatButtonToggleModule,
    MatCardModule,
    MatCheckboxModule,
    MatChipsModule,
    MatStepperModule,
    MatDatepickerModule,
    MatDialogModule,
    MatDividerModule,
    MatExpansionModule,
    MatGridListModule,
    MatIconModule,
    MatInputModule,
    MatListModule,
    MatMenuModule,
    MatNativeDateModule,
    MatPaginatorModule,
    MatProgressBarModule,
    MatProgressSpinnerModule,
    MatRadioModule,
    MatRippleModule,
    MatSelectModule,
    MatSidenavModule,
    MatSliderModule,
    MatSlideToggleModule,
    MatSnackBarModule,
    MatSortModule,
    MatTableModule,
    MatTabsModule,
    MatToolbarModule,
    MatTooltipModule,
    MatTreeModule,
    ReactiveFormsModule,
    QRCodeModule,
    NgxPrintModule,
  ],
  providers: [NgbAlertConfig, PrintService],
  entryComponents: [
    MenuEditDialogComponent,
    MenuItemEditDialogComponent,
    MenuOptionEditDialogComponent,
    BranchEditDialogComponent,
    HallEditDialogComponent,
    TableEditDialogComponent,
    RestaurantEditDialogComponent,
    AllergensEditDialogComponent,
    DietariesEditDialogComponent,
    OptionsEditDialogComponent,
    ConfirmDialogComponent,
    QrcodesEditDialogComponent,
    PrEditDialogComponent,
  ],
})
export class PagesModule {}
