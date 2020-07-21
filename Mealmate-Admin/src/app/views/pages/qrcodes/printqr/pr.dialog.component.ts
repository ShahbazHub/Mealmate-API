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

@Component({
  // tslint:disable-next-line:component-selector
  selector: "kt-pr-dialog",
  templateUrl: "./pr.dialog.component.html",
  changeDetection: ChangeDetectionStrategy.OnPush,
  encapsulation: ViewEncapsulation.None,
})
export class PrEditDialogComponent implements OnInit, OnDestroy {
  // Public properties
  viewLoading = false;
  data1: any;

  /**
   * Component constructor
   *
   * @param dialogRef: MatDialogRef<QrcodesEditDialogComponent>
   * @param data: any
  
   */
  constructor(
    public dialogRef: MatDialogRef<PrEditDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any
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
    this.data1 = this.data;
    console.log(this.data);

    // this.createForm();
  }

  /**
   * On destroy
   */
  ngOnDestroy() {}

  /**
   * Returns page title
   */
  getTitle(): string {
    if (this.data1 > 0) {
      return `Edit Qrcodes`;
    }

    return "New Qrcodes";
  }

  /**
   * On Submit
   */
  onSubmit() {
    this.dialogRef.close({ isEdit: false });
  }
  print() {
    window.print();
  }
  /** Alect Close event */
}
