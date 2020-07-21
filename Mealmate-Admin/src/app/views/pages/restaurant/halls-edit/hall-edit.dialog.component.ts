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
import { HallModel } from "../../../../core/restaurant/_models/hall.model";
// Services and Models
// import {
//   HallModel,
//   HallUpdated,
//   HallOnServerCreated,
//   selectLastCreatedHallId,
//   selectHallsActionLoading,
// } from "../../../../../../core/Hall";

@Component({
  // tslint:disable-next-line:component-selector
  selector: "kt-hall-edit-dialog",
  templateUrl: "./hall-edit.dialog.component.html",
  changeDetection: ChangeDetectionStrategy.OnPush,
  encapsulation: ViewEncapsulation.None,
})
export class HallEditDialogComponent implements OnInit, OnDestroy {
  // Public properties
  Hall: HallModel;
  HallForm: FormGroup;
  hasFormErrors = false;
  viewLoading = false;
  // Private properties
  private componentSubscriptions: Subscription;

  /**
   * Component constructor
   *
   * @param dialogRef: MatDialogRef<HallEditDialogComponent>
   * @param data: any
   * @param fb: FormBuilder
   * @param store: Store<AppState>
   * @param typesUtilsService: TypesUtilsService
   */
  constructor(
    public dialogRef: MatDialogRef<HallEditDialogComponent>,
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
    //   .pipe(select(selectHallsActionLoading))
    //   .subscribe((res) => (this.viewLoading = res));
    this.Hall = this.data.Hall;
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
    this.HallForm = this.fb.group({
      name: [this.Hall.name, Validators.required],
      status: [this.Hall.status + "", Validators.required],
    });
  }

  /**
   * Returns page title
   */
  getTitle(): string {
    if (this.Hall.id > 0) {
      return `Edit Hall '${this.Hall.name}'`;
    }

    return "New Hall";
  }

  /**
   * Check control is invalid
   * @param controlName: string
   */
  isControlInvalid(controlName: string): boolean {
    const control = this.HallForm.controls[controlName];
    const result = control.invalid && control.touched;
    return result;
  }

  /** ACTIONS */

  /**
   * Returns prepared Hall
   */
  prepareHall(): HallModel {
    const controls = this.HallForm.controls;
    const _Hall = new HallModel();
    _Hall.id = this.Hall.id;
    _Hall.name = controls.name.value;

    return _Hall;
  }

  /**
   * On Submit
   */
  onSubmit() {
    this.hasFormErrors = false;
    const controls = this.HallForm.controls;
    /** check form */
    if (this.HallForm.invalid) {
      Object.keys(controls).forEach((controlName) =>
        controls[controlName].markAsTouched()
      );

      this.hasFormErrors = true;
      return;
    }

    const editedHall = this.prepareHall();
    if (editedHall.id > 0) {
      this.updateHall(editedHall);
    } else {
      this.createHall(editedHall);
    }
  }

  /**
   * Update Hall
   *
   * @param _Hall: HallModel
   */
  updateHall(_Hall: HallModel) {
    this.dialogRef.close({ _Hall, isEdit: true });
  }

  /**
   * Create Hall
   *
   * @param _Hall: HallModel
   */
  createHall(_Hall: HallModel) {
    this.dialogRef.close({ _Hall, isEdit: false });
    // });
  }

  /** Alect Close event */
  onAlertClose($event) {
    this.hasFormErrors = false;
  }
}
