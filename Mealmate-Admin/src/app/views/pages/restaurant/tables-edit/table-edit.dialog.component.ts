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
import { TableModel } from "../../../../core/restaurant/_models/table.model";

@Component({
  // tslint:disable-next-line:component-selector
  selector: "kt-table-edit-dialog",
  templateUrl: "./table-edit.dialog.component.html",
  changeDetection: ChangeDetectionStrategy.OnPush,
  encapsulation: ViewEncapsulation.None,
})
export class TableEditDialogComponent implements OnInit, OnDestroy {
  // Public properties
  Table: TableModel;
  TableForm: FormGroup;
  hasFormErrors = false;
  viewLoading = false;
  // Private properties
  private componentSubscriptions: Subscription;

  /**
   * Component constructor
   *
   * @param dialogRef: MatDialogRef<TableEditDialogComponent>
   * @param data: any
   * @param fb: FormBuilder
   * @param store: Store<AppState>
   * @param typesUtilsService: TypesUtilsService
   */
  constructor(
    public dialogRef: MatDialogRef<TableEditDialogComponent>,
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
    //   .pipe(select(selectTablesActionLoading))
    //   .subscribe((res) => (this.viewLoading = res));
    this.Table = this.data.Table;
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
    this.TableForm = this.fb.group({
      name: [this.Table.name, Validators.required],
      status: [this.Table.status + "", Validators.required],
    });
  }

  /**
   * Returns page title
   */
  getTitle(): string {
    if (this.Table.id > 0) {
      return `Edit Table '${this.Table.name}'`;
    }

    return "New Table";
  }

  /**
   * Check control is invalid
   * @param controlName: string
   */
  isControlInvalid(controlName: string): boolean {
    const control = this.TableForm.controls[controlName];
    const result = control.invalid && control.touched;
    return result;
  }

  /** ACTIONS */

  /**
   * Returns prepared Table
   */
  prepareTable(): TableModel {
    const controls = this.TableForm.controls;
    const _Table = new TableModel();
    _Table.id = this.Table.id;
    _Table.name = controls.name.value;
    _Table.status = controls.status.value;

    return _Table;
  }

  /**
   * On Submit
   */
  onSubmit() {
    this.hasFormErrors = false;
    const controls = this.TableForm.controls;
    /** check form */
    if (this.TableForm.invalid) {
      Object.keys(controls).forEach((controlName) =>
        controls[controlName].markAsTouched()
      );

      this.hasFormErrors = true;
      return;
    }

    const editedTable = this.prepareTable();
    if (editedTable.id > 0) {
      this.updateTable(editedTable);
    } else {
      this.createTable(editedTable);
    }
  }

  /**
   * Update Table
   *
   * @param _Table: TableModel
   */
  updateTable(_Table: TableModel) {
    // const updateTable: Update<TableModel> = {
    //   id: _Table.id,
    //   changes: _Table,
    // };
    // this.store.dispatch(
    //   new TableUpdated({
    //     partialTable: updateTable,
    //     Table: _Table,
    //   })
    // );

    // Remove this line
    // of(undefined)
    //   .pipe(delay(1000))
    //   .subscribe(() => this.dialogRef.close({ _Table, isEdit: true }));
    // Uncomment this line
    this.dialogRef.close({ _Table, isEdit: true });
  }

  /**
   * Create Table
   *
   * @param _Table: TableModel
   */
  createTable(_Table: TableModel) {
    // this.store.dispatch(new TableOnServerCreated({ Table: _Table }));
    // this.componentSubscriptions = this.store
    //   .pipe(
    //     select(selectLastCreatedTableId),
    //     delay(1000) // Remove this line
    //   )
    //   .subscribe((res) => {
    //     if (!res) {
    //       return;
    //     }

    this.dialogRef.close({ _Table, isEdit: false });
    // });
  }

  /** Alect Close event */
  onAlertClose($event) {
    this.hasFormErrors = false;
  }
}
