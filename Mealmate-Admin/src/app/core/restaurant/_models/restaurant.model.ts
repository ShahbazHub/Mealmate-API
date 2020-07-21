export class RestaurantModel {
  id: number;
  restaurant: string;
  first_name: string;
  last_name: string;
  email: string;
  address: string;
  clear() {
    this.id = undefined;
    this.restaurant = "";
    this.first_name = "";
    this.last_name = "";
    this.email = "";
    this.address = "";
  }
}
