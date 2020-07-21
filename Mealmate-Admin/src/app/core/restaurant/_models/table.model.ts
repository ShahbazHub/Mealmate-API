export class TableModel {
  id: number;
  name: string;
  status: number;
  clear() {
    this.id = undefined;
    this.name = "";
    this.status = 0;
  }
}
