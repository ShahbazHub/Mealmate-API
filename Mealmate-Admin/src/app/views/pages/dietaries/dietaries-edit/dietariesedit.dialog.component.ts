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
import { DietariesModel } from "../../../../core/dietaries/_models/dietaries.model";
// Services and Models
// import {
//   DietariesModel,
//   DietariesUpdated,
//   DietariesOnServerCreated,
//   selectLastCreatedDietariesId,
//   selectDietariessActionLoading,
// } from "../../../../../../core/Dietaries";

@Component({
  // tslint:disable-next-line:component-selector
  selector: "kt-dietaries-edit-dialog",
  templateUrl: "./dietaries-edit.dialog.component.html",
  changeDetection: ChangeDetectionStrategy.OnPush,
  encapsulation: ViewEncapsulation.None,
})
export class DietariesEditDialogComponent implements OnInit, OnDestroy {
  // Public properties
  Dietaries: DietariesModel;
  DietariesForm: FormGroup;
  hasFormErrors = false;
  viewLoading = false;
  // Private properties
  private componentSubscriptions: Subscription;

  /**
   * Component constructor
   *
   * @param dialogRef: MatDialogRef<DietariesEditDialogComponent>
   * @param data: any
   * @param fb: FormBuilder
   * @param store: Store<AppState>
   * @param typesUtilsService: TypesUtilsService
   */
  constructor(
    public dialogRef: MatDialogRef<DietariesEditDialogComponent>,
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
    //   .pipe(select(selectDietariessActionLoading))
    //   .subscribe((res) => (this.viewLoading = res));
    this.Dietaries = this.data.Dietaries;
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
    this.DietariesForm = this.fb.group({
      name: [this.Dietaries.name, Validators.required],
      description: [this.Dietaries.description, Validators.required],
      status: [this.Dietaries.status + "", Validators.required],
    });
  }

  /**
   * Returns page title
   */
  getTitle(): string {
    if (this.Dietaries.id > 0) {
      return `Edit Dietaries '${this.Dietaries.name}'`;
    }

    return "New Dietaries";
  }

  /**
   * Check control is invalid
   * @param controlName: string
   */
  isControlInvalid(controlName: string): boolean {
    const control = this.DietariesForm.controls[controlName];
    const result = control.invalid && control.touched;
    return result;
  }

  /** ACTIONS */

  /**
   * Returns prepared Dietaries
   */
  prepareDietaries(): DietariesModel {
    const controls = this.DietariesForm.controls;
    const _Dietaries = new DietariesModel();
    _Dietaries.id = this.Dietaries.id;
    _Dietaries.name = controls.name.value;
    _Dietaries.description = controls.description.value;
    _Dietaries.status = controls.status.value;

    return _Dietaries;
  }

  /**
   * On Submit
   */
  onSubmit() {
    this.hasFormErrors = false;
    const controls = this.DietariesForm.controls;
    /** check form */
    if (this.DietariesForm.invalid) {
      Object.keys(controls).forEach((controlName) =>
        controls[controlName].markAsTouched()
      );

      this.hasFormErrors = true;
      return;
    }

    const editedDietaries = this.prepareDietaries();
    if (editedDietaries.id > 0) {
      this.updateDietaries(editedDietaries);
    } else {
      this.createDietaries(editedDietaries);
    }
  }

  /**
   * Update Dietaries
   *
   * @param _Dietaries: DietariesModel
   */
  updateDietaries(_Dietaries: DietariesModel) {
    // const updateDietaries: Update<DietariesModel> = {
    //   id: _Dietaries.id,
    //   changes: _Dietaries,
    // };
    // this.store.dispatch(
    //   new DietariesUpdated({
    //     partialDietaries: updateDietaries,
    //     Dietaries: _Dietaries,
    //   })
    // );

    // Remove this line
    // of(undefined)
    //   .pipe(delay(1000))
    //   .subscribe(() => this.dialogRef.close({ _Dietaries, isEdit: true }));
    // Uncomment this line
    this.dialogRef.close({ _Dietaries, isEdit: true });
  }

  /**
   * Create Dietaries
   *
   * @param _Dietaries: DietariesModel
   */
  createDietaries(_Dietaries: DietariesModel) {
    // this.store.dispatch(new DietariesOnServerCreated({ Dietaries: _Dietaries }));
    // this.componentSubscriptions = this.store
    //   .pipe(
    //     select(selectLastCreatedDietariesId),
    //     delay(1000) // Remove this line
    //   )
    //   .subscribe((res) => {
    //     if (!res) {
    //       return;
    //     }

    this.dialogRef.close({ _Dietaries, isEdit: false });
    // });
  }

  /** Alect Close event */
  onAlertClose($event) {
    this.hasFormErrors = false;
  }
}
