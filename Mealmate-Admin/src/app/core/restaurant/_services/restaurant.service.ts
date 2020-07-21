import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { environment } from "../../../../environments/environment";
import { HttpUtilsService } from "../../_base/crud";
import { Observable } from "rxjs";
import * as jwt_decode from "jwt-decode";
import { BranchModel } from "../_models/branch.model";
@Injectable({
  providedIn: "root",
})
export class RestaurantService {
  constructor(private http: HttpClient, private httpUtils: HttpUtilsService) {}
  getUserRestaurant() {
    const userToken = localStorage.getItem(environment.authTokenKey);
    let tokenInfo = jwt_decode(userToken);
    return this.http.get<any>(
      environment.config.MEALMATE_URL + "api/restaurants/" + tokenInfo.nameid
    );
  }
  // CREATE =>  POST: add a new customer to the server
  getBranchList(restaurantId: number, queryParams: any): Observable<any> {
    return this.http.get<any>(
      environment.config.MEALMATE_URL +
        "api/branches/" +
        `${restaurantId}?${queryParams}`
    );
  }
  createBranch(Branch: BranchModel) {
    // console.log(Branch.);
    // console.log(Branch.Name);

    return this.http.post<any>(
      environment.config.MEALMATE_URL + "api/branches",
      Branch
    );
  }

  updateBranch(Branch: BranchModel) {
    // console.log(Branch.);
    // console.log(Branch.Name);

    return this.http.put<any>(
      environment.config.MEALMATE_URL + "api/branches",
      Branch
    );
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
