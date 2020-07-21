export class MenuItemModel {
  id: number;
  name: string;
  description: string;
  price: string;
  clear() {
    this.id = undefined;
    this.price = "";
    this.description = "";
    this.name = "";
  }
}
