export class BranchModel {
  id: number;
  name: string;
  address: string;
  restaurantId: number;
  clear(restuarantId: number) {
    this.id = undefined;
    this.name = "";
    this.address = "";
    this.restaurantId = restuarantId;
  }
}
