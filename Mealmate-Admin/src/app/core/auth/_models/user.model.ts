import { BaseModel } from "../../_base/crud";
import { Address } from "./address.model";
import { SocialNetworks } from "./social-networks.model";

export class User extends BaseModel {
  id: number;
  firstName: string;
  lastName: string;

  password: string;
  email: string;
  token: string;
  restaurantDescription: string;
  refreshToken: string;
  roles: number[];
  pic: string;
  restaurantName: string;
  occupation: string;
  companyName: string;
  phone: string;
  // address: Address;
  // socialNetworks: SocialNetworks;
  isRestaurantAdmin: boolean;
  clear(): void {
    this.id = undefined;
    this.firstName = "";
    this.lastName = "";
    this.password = "";
    this.email = "";
    this.roles = [];
    this.restaurantName = "";
    this.restaurantDescription = "";
    this.token = "";
    this.refreshToken = "access-token-" + Math.random();
    this.pic = "./assets/media/users/default.jpg";
    this.occupation = "";
    this.companyName = "";
    this.phone = "";
    this.isRestaurantAdmin = false;
  }
}
