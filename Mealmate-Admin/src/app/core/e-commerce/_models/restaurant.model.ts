import { BaseModel } from "../../_base/crud";

export class RestaurantModel extends BaseModel {
  id: number;
  restaurantname: string;
  email: string;
  address: string;
  owner: string;

  clear() {
    this.id = undefined;
    this.email = "";
    this.address = "";
    this.owner = "";
    this.restaurantname = "";
  }
}
