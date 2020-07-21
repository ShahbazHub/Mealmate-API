// Angular
import {
  Component,
  OnInit,
  Inject,
  ChangeDetectionStrategy,
  ViewEncapsulation,
  OnDestroy,
} from "@angular/core";
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
// Material
import { MatDialogRef, MAT_DIALOG_DATA } from "@angular/material/dialog";
// RxJS
import { Subscription, of } from "rxjs";
import { delay } from "rxjs/operators";
// NGRX
import { Update } from "@ngrx/entity";
import { Store, select } from "@ngrx/store";
// State
import { AppState } from "../../../../core/reducers";
// CRUD
import { TypesUtilsService } from "../../../../core/_base/crud";
import { MenuModel } from "../../../../core/menu/_models/menu.model";
// Services and Models
// import {
//   MenuModel,
//   MenuUpdated,
//   MenuOnServerCreated,
//   selectLastCreatedMenuId,
//   selectMenusActionLoading,
// } from "../../../../../../core/menu";

@Component({
  // tslint:disable-next-line:component-selector
  selector: "kt-menus-edit-dialog",
  templateUrl: "./menu-edit.dialog.component.html",
  changeDetection: ChangeDetectionStrategy.OnPush,
  encapsulation: ViewEncapsulation.None,
})
export class MenuEditDialogComponent implements OnInit, OnDestroy {
  // Public properties
  Menu: MenuModel;
  MenuForm: FormGroup;
  hasFormErrors = false;
  viewLoading = false;
  // Private properties
  private componentSubscriptions: Subscription;

  /**
   * Component constructor
   *
   * @param dialogRef: MatDialogRef<MenuEditDialogComponent>
   * @param data: any
   * @param fb: FormBuilder
   * @param store: Store<AppState>
   * @param typesUtilsService: TypesUtilsService
   */
  constructor(
    public dialogRef: MatDialogRef<MenuEditDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any,
    private fb: FormBuilder,
    private store: Store<AppState>,
    private typesUtilsService: TypesUtilsService
  ) {}

  /**
   * @ Lifecycle sequences => https://angular.io/guide/lifecycle-hooks
   */

  /**
   * On init
   */
  ngOnInit() {
    // this.store
    //   .pipe(select(selectMenusActionLoading))
    //   .subscribe((res) => (this.viewLoading = res));
    this.Menu = this.data.Menu;
    console.log(this.data);

    this.createForm();
  }

  /**
   * On destroy
   */
  ngOnDestroy() {
    if (this.componentSubscriptions) {
      this.componentSubscriptions.unsubscribe();
    }
  }

  createForm() {
    this.MenuForm = this.fb.group({
      title: [this.Menu.title, Validators.required],
      description: [this.Menu.description, Validators.required],
      time: [this.Menu.time, Validators.compose([Validators.required])],
    });
  }

  /**
   * Returns page title
   */
  getTitle(): string {
    if (this.Menu.id > 0) {
      return `Edit Menu '${this.Menu.title}'`;
    }

    return "New Menu";
  }

  /**
   * Check control is invalid
   * @param controlName: string
   */
  isControlInvalid(controlName: string): boolean {
    const control = this.MenuForm.controls[controlName];
    const result = control.invalid && control.touched;
    return result;
  }

  /** ACTIONS */

  /**
   * Returns prepared Menu
   */
  prepareMenu(): MenuModel {
    const controls = this.MenuForm.controls;
    const _Menu = new MenuModel();
    _Menu.id = this.Menu.id;
    _Menu.title = controls.title.value;
    _Menu.description = controls.description.value;
    _Menu.time = controls.time.value;

    return _Menu;
  }

  /**
   * On Submit
   */
  onSubmit() {
    this.hasFormErrors = false;
    const controls = this.MenuForm.controls;
    /** check form */
    if (this.MenuForm.invalid) {
      Object.keys(controls).forEach((controlName) =>
        controls[controlName].markAsTouched()
      );

      this.hasFormErrors = true;
      return;
    }

    const editedMenu = this.prepareMenu();
    if (editedMenu.id > 0) {
      this.updateMenu(editedMenu);
    } else {
      this.createMenu(editedMenu);
    }
  }

  /**
   * Update Menu
   *
   * @param _Menu: MenuModel
   */
  updateMenu(_Menu: MenuModel) {
    // const updateMenu: Update<MenuModel> = {
    //   id: _Menu.id,
    //   changes: _Menu,
    // };
    // this.store.dispatch(
    //   new MenuUpdated({
    //     partialMenu: updateMenu,
    //     Menu: _Menu,
    //   })
    // );

    // Remove this line
    // of(undefined)
    //   .pipe(delay(1000))
    //   .subscribe(() => this.dialogRef.close({ _Menu, isEdit: true }));
    // Uncomment this line
    this.dialogRef.close({ _Menu, isEdit: true });
  }

  /**
   * Create Menu
   *
   * @param _Menu: MenuModel
   */
  createMenu(_Menu: MenuModel) {
    // this.store.dispatch(new MenuOnServerCreated({ Menu: _Menu }));
    // this.componentSubscriptions = this.store
    //   .pipe(
    //     select(selectLastCreatedMenuId),
    //     delay(1000) // Remove this line
    //   )
    //   .subscribe((res) => {
    //     if (!res) {
    //       return;
    //     }

    this.dialogRef.close({ _Menu, isEdit: false });
    // });
  }

  /** Alect Close event */
  onAlertClose($event) {
    this.hasFormErrors = false;
  }
}
