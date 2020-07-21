export class QrcodesModel {
  id: number;
  title: string;
  status: string;
  clear() {
    this.id = undefined;
    this.title = "";
    this.status = "";
  }
}
