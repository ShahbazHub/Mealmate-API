export class HallModel {
  id: number;
  name: string;
  status: number;
  clear() {
    this.id = undefined;
    this.status = 0;
    this.name = "";
  }
}
