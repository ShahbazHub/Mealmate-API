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
import { AppState } from "../../../../../../core/reducers";
// CRUD
import { TypesUtilsService } from "../../../../../../core/_base/crud";
// Services and Models
import {
  CustomerUpdated,
  CustomerOnServerCreated,
  selectLastCreatedCustomerId,
  selectCustomersActionLoading,
} from "../../../../../../core/e-commerce";
import { RestaurantModel } from "src/app/core/e-commerce/_models/restaurant.model";
import {
  RestaurantUpdated,
  RestaurantOnServerCreated,
} from "src/app/core/e-commerce/_actions/customer.actions";

@Component({
  // tslint:disable-next-line:component-selector
  selector: "kt-customers-edit-dialog",
  templateUrl: "./restaurant-edit.dialog.component.html",
  changeDetection: ChangeDetectionStrategy.OnPush,
  encapsulation: ViewEncapsulation.None,
})
export class RestaurantEditDialogComponent implements OnInit, OnDestroy {
  // Public properties
  customer: RestaurantModel;
  customerForm: FormGroup;
  hasFormErrors = false;
  viewLoading = false;
  // Private properties
  private componentSubscriptions: Subscription;

  /**
   * Component constructor
   *
   * @param dialogRef: MatDialogRef<CustomerEditDialogComponent>
   * @param data: any
   * @param fb: FormBuilder
   * @param store: Store<AppState>
   * @param typesUtilsService: TypesUtilsService
   */
  constructor(
    public dialogRef: MatDialogRef<RestaurantEditDialogComponent>,
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
    this.store
      .pipe(select(selectCustomersActionLoading))
      .subscribe((res) => (this.viewLoading = res));
    this.customer = this.data.customer;
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
    this.customerForm = this.fb.group({
      address: [this.customer.address, Validators.required],
      owner: [this.customer.owner, Validators.required],
      email: [
        this.customer.email,
        Validators.compose([Validators.required, Validators.email]),
      ],
      //   dob: [
      //     this.typesUtilsService.getDateFromString(this.customer.dateOfBbirth),
      //     Validators.compose([Validators.nullValidator]),
      //   ],
      restaurantname: [
        this.customer.restaurantname,
        Validators.compose([Validators.required]),
      ],
      //   gender: [this.customer.gender, Validators.compose([Validators.required])],
      //   ipAddress: [
      //     this.customer.ipAddress,
      //     Validators.compose([Validators.required]),
      //   ],
      // status: [this.customer.status, Validators.compose([Validators.required])],
    });
  }

  /**
   * Returns page title
   */
  getTitle(): string {
    if (this.customer.id > 0) {
      return `Edit customer '${this.customer.address}'`;
    }

    return "No Address Found";
  }

  /**
   * Check control is invalid
   * @param controlName: string
   */
  isControlInvalid(controlName: string): boolean {
    const control = this.customerForm.controls[controlName];
    const result = control.invalid && control.touched;
    return result;
  }

  /** ACTIONS */

  /**
   * Returns prepared customer
   */
  prepareCustomer(): RestaurantModel {
    const controls = this.customerForm.controls;
    const _customer = new RestaurantModel();
    _customer.id = this.customer.id;

    _customer.address = controls.address.value;
    _customer.restaurantname = controls.restaurantname.value;
    _customer.email = controls.email.value;
    _customer.owner = controls.owner.value;

    return _customer;
  }

  /**
   * On Submit
   */
  onSubmit() {
    this.hasFormErrors = false;
    const controls = this.customerForm.controls;
    /** check form */
    if (this.customerForm.invalid) {
      Object.keys(controls).forEach((controlName) =>
        controls[controlName].markAsTouched()
      );

      this.hasFormErrors = true;
      return;
    }

    const editedCustomer = this.prepareCustomer();
    // if (editedCustomer.id > 0) {
    this.updateCustomer(editedCustomer);
    //}
    //  else {
    //   this.createCustomer(editedCustomer);
    // }
  }

  /**
   * Update customer
   *
   * @param _customer: CustomerModel
   */
  updateCustomer(_customer: RestaurantModel) {
    const updateCustomer: Update<RestaurantModel> = {
      id: _customer.id,
      changes: _customer,
    };
    this.store.dispatch(
      new RestaurantUpdated({
        partialCustomer: updateCustomer,
        customer: _customer,
      })
    );

    // Remove this line
    of(undefined)
      .pipe(delay(1000))
      .subscribe(() => this.dialogRef.close({ _customer, isEdit: true }));
    // Uncomment this line
    // this.dialogRef.close({ _customer, isEdit: true }
  }

  /**
   * Create customer
   *
   * @param _customer: CustomerModel
   */
  // createCustomer(_customer: RestaurantModel) {
  //   this.store.dispatch(new RestaurantOnServerCreated({ customer: _customer }));
  //   this.componentSubscriptions = this.store
  //     .pipe(
  //       select(selectLastCreatedCustomerId),
  //       delay(1000) // Remove this line
  //     )
  //     .subscribe((res) => {
  //       if (!res) {
  //         return;
  //       }

  //       this.dialogRef.close({ _customer, isEdit: false });
  //     });
  // }

  /** Alect Close event */
  onAlertClose($event) {
    this.hasFormErrors = false;
  }
}
