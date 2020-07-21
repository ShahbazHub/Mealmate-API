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
import { AllergensModel } from "../../../../core/allergens/_models/allergens.model";
// Services and Models
// import {
//   AllergensModel,
//   AllergensUpdated,
//   AllergensOnServerCreated,
//   selectLastCreatedAllergensId,
//   selectAllergenssActionLoading,
// } from "../../../../../../core/Allergens";

@Component({
  // tslint:disable-next-line:component-selector
  selector: "kt-allergens-edit-dialog",
  templateUrl: "./allergens-edit.dialog.component.html",
  changeDetection: ChangeDetectionStrategy.OnPush,
  encapsulation: ViewEncapsulation.None,
})
export class AllergensEditDialogComponent implements OnInit, OnDestroy {
  // Public properties
  Allergens: AllergensModel;
  AllergensForm: FormGroup;
  hasFormErrors = false;
  viewLoading = false;
  // Private properties
  private componentSubscriptions: Subscription;

  /**
   * Component constructor
   *
   * @param dialogRef: MatDialogRef<AllergensEditDialogComponent>
   * @param data: any
   * @param fb: FormBuilder
   * @param store: Store<AppState>
   * @param typesUtilsService: TypesUtilsService
   */
  constructor(
    public dialogRef: MatDialogRef<AllergensEditDialogComponent>,
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
    //   .pipe(select(selectAllergenssActionLoading))
    //   .subscribe((res) => (this.viewLoading = res));
    this.Allergens = this.data.Allergens;
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
    this.AllergensForm = this.fb.group({
      name: [this.Allergens.name, Validators.required],
      description: [this.Allergens.description, Validators.required],
      status: [this.Allergens.status + "", Validators.required],
    });
  }

  /**
   * Returns page title
   */
  getTitle(): string {
    if (this.Allergens.id > 0) {
      return `Edit Allergens '${this.Allergens.name}'`;
    }

    return "New Allergens";
  }

  /**
   * Check control is invalid
   * @param controlName: string
   */
  isControlInvalid(controlName: string): boolean {
    const control = this.AllergensForm.controls[controlName];
    const result = control.invalid && control.touched;
    return result;
  }

  /** ACTIONS */

  /**
   * Returns prepared Allergens
   */
  prepareAllergens(): AllergensModel {
    const controls = this.AllergensForm.controls;
    const _Allergens = new AllergensModel();
    _Allergens.id = this.Allergens.id;
    _Allergens.name = controls.name.value;
    _Allergens.description = controls.description.value;
    _Allergens.status = controls.status.value;

    return _Allergens;
  }

  /**
   * On Submit
   */
  onSubmit() {
    this.hasFormErrors = false;
    const controls = this.AllergensForm.controls;
    /** check form */
    if (this.AllergensForm.invalid) {
      Object.keys(controls).forEach((controlName) =>
        controls[controlName].markAsTouched()
      );

      this.hasFormErrors = true;
      return;
    }

    const editedAllergens = this.prepareAllergens();
    if (editedAllergens.id > 0) {
      this.updateAllergens(editedAllergens);
    } else {
      this.createAllergens(editedAllergens);
    }
  }

  /**
   * Update Allergens
   *
   * @param _Allergens: AllergensModel
   */
  updateAllergens(_Allergens: AllergensModel) {
    // const updateAllergens: Update<AllergensModel> = {
    //   id: _Allergens.id,
    //   changes: _Allergens,
    // };
    // this.store.dispatch(
    //   new AllergensUpdated({
    //     partialAllergens: updateAllergens,
    //     Allergens: _Allergens,
    //   })
    // );

    // Remove this line
    // of(undefined)
    //   .pipe(delay(1000))
    //   .subscribe(() => this.dialogRef.close({ _Allergens, isEdit: true }));
    // Uncomment this line
    this.dialogRef.close({ _Allergens, isEdit: true });
  }

  /**
   * Create Allergens
   *
   * @param _Allergens: AllergensModel
   */
  createAllergens(_Allergens: AllergensModel) {
    // this.store.dispatch(new AllergensOnServerCreated({ Allergens: _Allergens }));
    // this.componentSubscriptions = this.store
    //   .pipe(
    //     select(selectLastCreatedAllergensId),
    //     delay(1000) // Remove this line
    //   )
    //   .subscribe((res) => {
    //     if (!res) {
    //       return;
    //     }

    this.dialogRef.close({ _Allergens, isEdit: false });
    // });
  }

  /** Alect Close event */
  onAlertClose($event) {
    this.hasFormErrors = false;
  }
}
