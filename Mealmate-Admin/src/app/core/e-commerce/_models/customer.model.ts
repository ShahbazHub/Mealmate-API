import { BaseModel } from "../../_base/crud";

export class CustomerModel extends BaseModel {
  id: number;
  address: string;
  phoneno: string;
  email: string;
  halls: string;
  //gender: string;
  status: number; // 0 = Active | 1 = Suspended | Pending = 2
  //action: string;
  // dob: Date;
  // ipAddress: string;
  // type: number; // 0 = Business | 1 = Individual

  clear() {
    //this.dob = new Date();
    //this.firstName = "";
    //this.lastName = "";
    this.email = "";
    this.address = "";
    this.phoneno = "";
    this.halls = "";
    // this.userName = "";
    // this.gender = "Female";
    // this.ipAddress = "";
    // this.type = 1;
    this.status = 1;
  }
}
