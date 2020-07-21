import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { environment } from "../../../../environments/environment";
import { HttpUtilsService } from "../../_base/crud";
import { Observable } from "rxjs";
@Injectable({
  providedIn: "root",
})
export class MenuItemService {
  constructor(private http: HttpClient, private httpUtils: HttpUtilsService) {}

  // CREATE =>  POST: add a new customer to the server
  getMenuList(page: number): Observable<any> {
    const href = "http://localhost:3013/menu";
    // const requestUrl = `${href}?q=repo:angular/material2&sort=${sort}&order=${order}&page=${
    //   page + 1
    // }`;

    return this.http.get<any>(href);
  }
  getMenuItems(id): Observable<any> {
    // Note: Add headers if needed (tokens/bearer)
    const httpHeaders = this.httpUtils.getHTTPHeaders();
    return this.http.get<any>(environment.config.SERVER_URL + "menu", {
      headers: httpHeaders,
    });
  }

  getMenuOptions(id): Observable<any> {
    // Note: Add headers if needed (tokens/bearer)
    const httpHeaders = this.httpUtils.getHTTPHeaders();
    return this.http.get<any>(environment.config.SERVER_URL + "menu", {
      headers: httpHeaders,
    });
  }
}
