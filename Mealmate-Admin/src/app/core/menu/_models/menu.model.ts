export class MenuModel {
  id: number;
  time: string;
  description: string;
  title: string;
  clear() {
    this.id = undefined;
    this.time = "";
    this.description = "";
    this.title = "";
  }
}
