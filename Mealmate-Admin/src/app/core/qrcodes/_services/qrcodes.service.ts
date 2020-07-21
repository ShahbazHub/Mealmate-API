import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { environment } from "../../../../environments/environment";
import { HttpUtilsService } from "../../_base/crud";
import { Observable } from "rxjs";
@Injectable({
  providedIn: "root",
})
export class QrcodesService {
  constructor(private http: HttpClient, private httpUtils: HttpUtilsService) {}

  // CREATE =>  POST: add a new customer to the server
  getQrcodesList(page: number): Observable<any> {
    return this.http.get<any>(environment.config.SERVER_URL + "menu");
  }
}
