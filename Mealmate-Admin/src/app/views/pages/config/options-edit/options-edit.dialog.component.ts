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
import { OptionsModel } from "src/app/core/options/_models/options.model";
// Services and Models
// import {
//   OptionsModel,
//   OptionsUpdated,
//   OptionsOnServerCreated,
//   selectLastCreatedOptionsId,
//   selectOptionssActionLoading,
// } from "../../../../../../core/Options";

@Component({
  // tslint:disable-next-line:component-selector
  selector: "kt-options-edit-dialog",
  templateUrl: "./options-edit.dialog.component.html",
  changeDetection: ChangeDetectionStrategy.OnPush,
  encapsulation: ViewEncapsulation.None,
})
export class OptionsEditDialogComponent implements OnInit, OnDestroy {
  // Public properties
  Options: OptionsModel;
  OptionsForm: FormGroup;
  hasFormErrors = false;
  viewLoading = false;
  // Private properties
  private componentSubscriptions: Subscription;

  /**
   * Component constructor
   *
   * @param dialogRef: MatDialogRef<OptionsEditDialogComponent>
   * @param data: any
   * @param fb: FormBuilder
   * @param store: Store<AppState>
   * @param typesUtilsService: TypesUtilsService
   */
  constructor(
    public dialogRef: MatDialogRef<OptionsEditDialogComponent>,
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
    //   .pipe(select(selectOptionssActionLoading))
    //   .subscribe((res) => (this.viewLoading = res));
    this.Options = this.data.Options;
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
    this.OptionsForm = this.fb.group({
      name: [this.Options.name, Validators.required],
      description: [this.Options.description, Validators.required],
      status: [this.Options.status + "", Validators.required],
    });
  }

  /**
   * Returns page title
   */
  getTitle(): string {
    if (this.Options.id > 0) {
      return `Edit Options '${this.Options.name}'`;
    }

    return "New Options";
  }

  /**
   * Check control is invalid
   * @param controlName: string
   */
  isControlInvalid(controlName: string): boolean {
    const control = this.OptionsForm.controls[controlName];
    const result = control.invalid && control.touched;
    return result;
  }

  /** ACTIONS */

  /**
   * Returns prepared Options
   */
  prepareOptions(): OptionsModel {
    const controls = this.OptionsForm.controls;
    const _Options = new OptionsModel();
    _Options.id = this.Options.id;
    _Options.name = controls.name.value;
    _Options.description = controls.description.value;
    _Options.status = controls.status.value;

    return _Options;
  }

  /**
   * On Submit
   */
  onSubmit() {
    this.hasFormErrors = false;
    const controls = this.OptionsForm.controls;
    /** check form */
    if (this.OptionsForm.invalid) {
      Object.keys(controls).forEach((controlName) =>
        controls[controlName].markAsTouched()
      );

      this.hasFormErrors = true;
      return;
    }

    const editedOptions = this.prepareOptions();
    if (editedOptions.id > 0) {
      this.updateOptions(editedOptions);
    } else {
      this.createOptions(editedOptions);
    }
  }

  /**
   * Update Options
   *
   * @param _Options: OptionsModel
   */
  updateOptions(_Options: OptionsModel) {
    // const updateOptions: Update<OptionsModel> = {
    //   id: _Options.id,
    //   changes: _Options,
    // };
    // this.store.dispatch(
    //   new OptionsUpdated({
    //     partialOptions: updateOptions,
    //     Options: _Options,
    //   })
    // );

    // Remove this line
    // of(undefined)
    //   .pipe(delay(1000))
    //   .subscribe(() => this.dialogRef.close({ _Options, isEdit: true }));
    // Uncomment this line
    this.dialogRef.close({ _Options, isEdit: true });
  }

  /**
   * Create Options
   *
   * @param _Options: OptionsModel
   */
  createOptions(_Options: OptionsModel) {
    // this.store.dispatch(new OptionsOnServerCreated({ Options: _Options }));
    // this.componentSubscriptions = this.store
    //   .pipe(
    //     select(selectLastCreatedOptionsId),
    //     delay(1000) // Remove this line
    //   )
    //   .subscribe((res) => {
    //     if (!res) {
    //       return;
    //     }

    this.dialogRef.close({ _Options, isEdit: false });
    // });
  }

  /** Alect Close event */
  onAlertClose($event) {
    this.hasFormErrors = false;
  }
}
