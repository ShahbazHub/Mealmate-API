export class OptionsModel {
  id: number;
  name: string;
  description: string;
  status: string;
  clear() {
    this.id = undefined;
    this.name = "";
    this.description = "";
    this.status = "";
  }
}
