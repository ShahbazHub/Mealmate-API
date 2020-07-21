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
import { BranchModel } from "../../../../core/restaurant/_models/branch.model";
import { RestaurantService } from "src/app/core/restaurant/_services/restaurant.service";

@Component({
  // tslint:disable-next-line:component-selector
  selector: "kt-branchs-edit-dialog",
  templateUrl: "./branch-edit.dialog.component.html",
  changeDetection: ChangeDetectionStrategy.OnPush,
  encapsulation: ViewEncapsulation.None,
})
export class BranchEditDialogComponent implements OnInit, OnDestroy {
  // Public properties
  Branch: BranchModel;
  BranchForm: FormGroup;
  hasFormErrors = false;
  viewLoading = false;
  // Private properties
  private componentSubscriptions: Subscription;

  /**
   * Component constructor
   *
   * @param dialogRef: MatDialogRef<BranchEditDialogComponent>
   * @param data: any
   * @param fb: FormBuilder
   * @param store: Store<AppState>
   * @param typesUtilsService: TypesUtilsService
   */
  constructor(
    public dialogRef: MatDialogRef<BranchEditDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any,
    private fb: FormBuilder,
    private store: Store<AppState>,
    private typesUtilsService: TypesUtilsService,
    private restaurantService: RestaurantService
  ) {}

  /**
   * @ Lifecycle sequences => https://angular.io/guide/lifecycle-hooks
   */

  /**
   * On init
   */
  ngOnInit() {
    // this.store
    //   .pipe(select(selectBranchsActionLoading))
    //   .subscribe((res) => (this.viewLoading = res));
    this.Branch = this.data.Branch;
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
    this.BranchForm = this.fb.group({
      name: [this.Branch.name, Validators.required],
      address: [this.Branch.address, Validators.required],
      // status: [
      //   this.Branch.status + "",
      //   Validators.compose([Validators.required]),
      // ],
    });
  }

  /**
   * Returns page title
   */
  getTitle(): string {
    if (this.Branch.id > 0) {
      return `Edit Branch '${this.Branch.name}'`;
    }

    return "New Branch";
  }

  /**
   * Check control is invalid
   * @param controlName: string
   */
  isControlInvalid(controlName: string): boolean {
    const control = this.BranchForm.controls[controlName];
    const result = control.invalid && control.touched;
    return result;
  }

  /** ACTIONS */

  /**
   * Returns prepared Branch
   */
  prepareBranch(): BranchModel {
    const controls = this.BranchForm.controls;
    const _Branch = new BranchModel();
    _Branch.id = this.Branch.id;
    _Branch.name = controls.name.value;
    _Branch.address = controls.address.value;
    // _Branch.status = controls.status.value;

    return _Branch;
  }

  /**
   * On Submit
   */
  onSubmit() {
    this.hasFormErrors = false;
    const controls = this.BranchForm.controls;
    /** check form */
    if (this.BranchForm.invalid) {
      Object.keys(controls).forEach((controlName) =>
        controls[controlName].markAsTouched()
      );

      this.hasFormErrors = true;
      return;
    }

    const editedBranch = this.prepareBranch();
    if (editedBranch.id > 0) {
      this.updateBranch(editedBranch);
    } else {
      editedBranch.restaurantId = this.data.restaurantId;
      this.createBranch(editedBranch);
    }
  }

  /**
   * Update Branch
   *
   * @param _Branch: BranchModel
   */
  updateBranch(_Branch: BranchModel) {
    _Branch.restaurantId = this.data.Branch.restaurantId;
    //_Branch.id = this.data.Branch.Id;
    //_Branch.created = "2020-07-19T18:30:14.109Z";
    console.log("innnnnn UBRanch");

    console.log(_Branch);

    this.restaurantService.updateBranch(_Branch).subscribe((res) => {
      console.log(res);

      this.dialogRef.close({ _Branch, isEdit: true });
    });
  }

  /**
   * Create Branch
   *
   * @param _Branch: BranchModel
   */
  createBranch(_Branch: any) {
    // this.store.dispatch(new BranchOnServerCreated({ Branch: _Branch }));
    // this.componentSubscriptions = this.store
    //   .pipe(
    //     select(selectLastCreatedBranchId),
    //     delay(1000) // Remove this line
    //   )
    //   .subscribe((res) => {
    //     if (!res) {
    //       return;
    //     }

    _Branch.restaurantId = this.data.Branch.restaurantId;
    _Branch.id = 0;
    //_Branch.created = "2020-07-19T18:30:14.109Z";
    console.log(_Branch);

    this.restaurantService.createBranch(_Branch).subscribe((res) => {
      console.log(res);

      this.dialogRef.close({ _Branch, isEdit: false });
    });

    //});
  }

  /** Alect Close event */
  onAlertClose($event) {
    this.hasFormErrors = false;
  }
}
