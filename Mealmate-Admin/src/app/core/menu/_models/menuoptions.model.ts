export class MenuOptionModel {
  id: number;
  optionname: string;
  quantity: number;
  price: number;
  clear() {
    this.id = undefined;
    this.price = 0;
    this.optionname = "";
    this.quantity = 0;
  }
}
