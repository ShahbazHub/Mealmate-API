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
import { QrcodesModel } from "../../../../core/qrcodes/_models/qrcodes.model";
// Services and Models
// import {
//   QrcodesModel,
//   QrcodesUpdated,
//   QrcodesOnServerCreated,
//   selectLastCreatedQrcodesId,
//   selectQrcodessActionLoading,
// } from "../../../../../../core/Qrcodes";

@Component({
  // tslint:disable-next-line:component-selector
  selector: "kt-qrcodes-edit-dialog",
  templateUrl: "./qrcodes-edit.dialog.component.html",
  changeDetection: ChangeDetectionStrategy.OnPush,
  encapsulation: ViewEncapsulation.None,
})
export class QrcodesEditDialogComponent implements OnInit, OnDestroy {
  // Public properties
  Qrcodes: QrcodesModel;
  QrcodesForm: FormGroup;
  hasFormErrors = false;
  viewLoading = false;
  // Private properties
  private componentSubscriptions: Subscription;

  /**
   * Component constructor
   *
   * @param dialogRef: MatDialogRef<QrcodesEditDialogComponent>
   * @param data: any
   * @param fb: FormBuilder
   * @param store: Store<AppState>
   * @param typesUtilsService: TypesUtilsService
   */
  constructor(
    public dialogRef: MatDialogRef<QrcodesEditDialogComponent>,
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
    //   .pipe(select(selectQrcodessActionLoading))
    //   .subscribe((res) => (this.viewLoading = res));
    this.Qrcodes = this.data.Qrcodes;
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
    this.QrcodesForm = this.fb.group({
      title: [this.Qrcodes.title, Validators.required],
      // description: [this.Qrcodes.description, Validators.required],
      status: [this.Qrcodes.status + "", Validators.required],
    });
  }

  /**
   * Returns page title
   */
  getTitle(): string {
    if (this.Qrcodes.id > 0) {
      return `Edit Qrcodes '${this.Qrcodes.title}'`;
    }

    return "New Qrcodes";
  }

  /**
   * Check control is invalid
   * @param controlName: string
   */
  isControlInvalid(controlName: string): boolean {
    const control = this.QrcodesForm.controls[controlName];
    const result = control.invalid && control.touched;
    return result;
  }

  /** ACTIONS */

  /**
   * Returns prepared Qrcodes
   */
  prepareQrcodes(): QrcodesModel {
    const controls = this.QrcodesForm.controls;
    const _Qrcodes = new QrcodesModel();
    _Qrcodes.id = this.Qrcodes.id;
    _Qrcodes.title = controls.title.value;
    // _Qrcodes.description = controls.description.value;
    _Qrcodes.status = controls.status.value;

    return _Qrcodes;
  }

  /**
   * On Submit
   */
  onSubmit() {
    this.hasFormErrors = false;
    const controls = this.QrcodesForm.controls;
    /** check form */
    if (this.QrcodesForm.invalid) {
      Object.keys(controls).forEach((controlName) =>
        controls[controlName].markAsTouched()
      );

      this.hasFormErrors = true;
      return;
    }

    const editedQrcodes = this.prepareQrcodes();
    if (editedQrcodes.id > 0) {
      this.updateQrcodes(editedQrcodes);
    } else {
      this.createQrcodes(editedQrcodes);
    }
  }

  /**
   * Update Qrcodes
   *
   * @param _Qrcodes: QrcodesModel
   */
  updateQrcodes(_Qrcodes: QrcodesModel) {
    // const updateQrcodes: Update<QrcodesModel> = {
    //   id: _Qrcodes.id,
    //   changes: _Qrcodes,
    // };
    // this.store.dispatch(
    //   new QrcodesUpdated({
    //     partialQrcodes: updateQrcodes,
    //     Qrcodes: _Qrcodes,
    //   })
    // );

    // Remove this line
    // of(undefined)
    //   .pipe(delay(1000))
    //   .subscribe(() => this.dialogRef.close({ _Qrcodes, isEdit: true }));
    // Uncomment this line
    this.dialogRef.close({ _Qrcodes, isEdit: true });
  }

  /**
   * Create Qrcodes
   *
   * @param _Qrcodes: QrcodesModel
   */
  createQrcodes(_Qrcodes: QrcodesModel) {
    // this.store.dispatch(new QrcodesOnServerCreated({ Qrcodes: _Qrcodes }));
    // this.componentSubscriptions = this.store
    //   .pipe(
    //     select(selectLastCreatedQrcodesId),
    //     delay(1000) // Remove this line
    //   )
    //   .subscribe((res) => {
    //     if (!res) {
    //       return;
    //     }

    this.dialogRef.close({ _Qrcodes, isEdit: false });
    // });
  }

  /** Alect Close event */
  onAlertClose($event) {
    this.hasFormErrors = false;
  }
}
