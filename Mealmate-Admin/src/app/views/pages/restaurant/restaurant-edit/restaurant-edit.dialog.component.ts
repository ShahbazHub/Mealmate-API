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
import { RestaurantModel } from "../../../../core/restaurant/_models/restaurant.model";
// Services and Models
// import {
//   RestaurantModel,
//   RestaurantUpdated,
//   RestaurantOnServerCreated,
//   selectLastCreatedRestaurantId,
//   selectRestaurantsActionLoading,
// } from "../../../../../../core/Restaurant";

@Component({
  // tslint:disable-next-line:component-selector
  selector: "kt-Restaurants-edit-dialog",
  templateUrl: "./Restaurant-edit.dialog.component.html",
  changeDetection: ChangeDetectionStrategy.OnPush,
  encapsulation: ViewEncapsulation.None,
})
export class RestaurantEditDialogComponent implements OnInit, OnDestroy {
  // Public properties
  Restaurant: RestaurantModel;
  RestaurantForm: FormGroup;
  hasFormErrors = false;
  viewLoading = false;
  // Private properties
  private componentSubscriptions: Subscription;

  /**
   * Component constructor
   *
   * @param dialogRef: MatDialogRef<RestaurantEditDialogComponent>
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
    // this.store
    //   .pipe(select(selectRestaurantsActionLoading))
    //   .subscribe((res) => (this.viewLoading = res));
    this.Restaurant = this.data.Restaurant;
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
    this.RestaurantForm = this.fb.group({
      restaurant: [this.Restaurant.restaurant, Validators.required],
      first_name: [this.Restaurant.first_name, Validators.required],
      last_name: [
        this.Restaurant.last_name,
        Validators.compose([Validators.required]),
      ],
      email: [this.Restaurant.email, Validators.required],
      address: [this.Restaurant.address, Validators.required],
    });
  }

  /**
   * Returns page title
   */
  getTitle(): string {
    if (this.Restaurant.id > 0) {
      return `Edit Restaurant '${this.Restaurant.restaurant}'`;
    }

    return "New Restaurant";
  }

  /**
   * Check control is invalid
   * @param controlName: string
   */
  isControlInvalid(controlName: string): boolean {
    const control = this.RestaurantForm.controls[controlName];
    const result = control.invalid && control.touched;
    return result;
  }

  /** ACTIONS */

  /**
   * Returns prepared Restaurant
   */
  prepareRestaurant(): RestaurantModel {
    const controls = this.RestaurantForm.controls;
    const _Restaurant = new RestaurantModel();
    _Restaurant.id = this.Restaurant.id;
    _Restaurant.restaurant = controls.restaurant.value;
    _Restaurant.email = controls.email.value;
    _Restaurant.first_name = controls.first_name.value;
    _Restaurant.last_name = controls.last_name.value;
    _Restaurant.address = controls.address.value;

    return _Restaurant;
  }

  /**
   * On Submit
   */
  onSubmit() {
    this.hasFormErrors = false;
    const controls = this.RestaurantForm.controls;
    /** check form */
    if (this.RestaurantForm.invalid) {
      Object.keys(controls).forEach((controlName) =>
        controls[controlName].markAsTouched()
      );

      this.hasFormErrors = true;
      return;
    }

    const editedRestaurant = this.prepareRestaurant();
    if (editedRestaurant.id > 0) {
      this.updateRestaurant(editedRestaurant);
    } else {
      this.createRestaurant(editedRestaurant);
    }
  }

  /**
   * Update Restaurant
   *
   * @param _Restaurant: RestaurantModel
   */
  updateRestaurant(_Restaurant: RestaurantModel) {
    // const updateRestaurant: Update<RestaurantModel> = {
    //   id: _Restaurant.id,
    //   changes: _Restaurant,
    // };
    // this.store.dispatch(
    //   new RestaurantUpdated({
    //     partialRestaurant: updateRestaurant,
    //     Restaurant: _Restaurant,
    //   })
    // );

    // Remove this line
    // of(undefined)
    //   .pipe(delay(1000))
    //   .subscribe(() => this.dialogRef.close({ _Restaurant, isEdit: true }));
    // Uncomment this line
    this.dialogRef.close({ _Restaurant, isEdit: true });
  }

  /**
   * Create Restaurant
   *
   * @param _Restaurant: RestaurantModel
   */
  createRestaurant(_Restaurant: RestaurantModel) {
    // this.store.dispatch(new RestaurantOnServerCreated({ Restaurant: _Restaurant }));
    // this.componentSubscriptions = this.store
    //   .pipe(
    //     select(selectLastCreatedRestaurantId),
    //     delay(1000) // Remove this line
    //   )
    //   .subscribe((res) => {
    //     if (!res) {
    //       return;
    //     }

    this.dialogRef.close({ _Restaurant, isEdit: false });
    // });
  }

  /** Alect Close event */
  onAlertClose($event) {
    this.hasFormErrors = false;
  }
}
