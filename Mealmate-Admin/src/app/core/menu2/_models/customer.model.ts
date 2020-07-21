import { BaseModel } from "../../_base/crud";

export class CustomerModel extends BaseModel {
  id: number;
  title: string;
  description: string;
  time: string;
  // userName: string;
  // gender: string;
  status: number; // 0 = Active | 1 = Suspended | Pending = 2
  // dateOfBbirth: string;
  // dob: Date;
  // ipAddress: string;
  // type: number; // 0 = Business | 1 = Individual

  clear() {
    // this.dob = new Date();
    this.title = "";
    this.description = "";
    this.time = "";
    // this.userName = "";
    // this.gender = "Female";
    // this.ipAddress = '';
    // this.type = 1;
    this.status = 1;
  }
}
