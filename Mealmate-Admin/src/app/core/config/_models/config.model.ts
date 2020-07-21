export class ConfigModel {
  id: number;
  branchId: string;
  requestId: string;
  status: string;
  clear() {
    this.id = undefined;
    this.branchId = "";
    this.requestId = "";
    this.status = "";
  }
}
