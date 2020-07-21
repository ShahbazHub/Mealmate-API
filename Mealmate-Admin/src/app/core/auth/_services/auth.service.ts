import { Injectable } from "@angular/core";
import { HttpClient, HttpHeaders } from "@angular/common/http";
import { forkJoin, Observable, of } from "rxjs";
import { User } from "../_models/user.model";
import { Permission } from "../_models/permission.model";
import { Role } from "../_models/role.model";
import { catchError, map } from "rxjs/operators";
import { QueryParamsModel, QueryResultsModel } from "../../_base/crud";
import { environment } from "../../../../environments/environment";
import { each, filter, find, some } from "lodash";
import * as jwt_decode from "jwt-decode";
const API_USERS_URL = "api/users";
const API_PERMISSION_URL = "http://localhost:3013/permissions";
const API_ROLES_URL = "http://localhost:3013/roles";

@Injectable()
export class AuthService {
  constructor(private http: HttpClient) {}

  // Authentication/Authorization
  login(email: string, password: string): Observable<any> {
    console.log("Logging in Service");

    return this.http.post<any>(
      environment.config.MEALMATE_URL + "api/accounts/login",
      { email, password }
    );
  }

  getUserByToken(): Observable<User> {
    const userToken = localStorage.getItem(environment.authTokenKey);
    console.log("In get user token " + userToken);
    let tokenInfo = jwt_decode(userToken);
    console.log(tokenInfo);

    // const httpHeaders = new HttpHeaders();
    // httpHeaders.set("Authorization", "Bearer " + userToken);
    return this.http.get<User>(
      environment.config.MEALMATE_URL + "api/users/" + tokenInfo.nameid
    );
  }

  register(user: User): Observable<any> {
    const httpHeaders = new HttpHeaders();
    httpHeaders.set("Content-Type", "application/json");
    return this.http
      .post<User>(
        environment.config.MEALMATE_URL + "api/accounts/register",
        user,
        { headers: httpHeaders }
      )
      .pipe(
        map((res: User) => {
          return res;
        }),
        catchError((err) => {
          return null;
        })
      );
  }

  /*
   * Submit forgot password request
   *
   * @param {string} email
   * @returns {Observable<any>}
   */
  public requestPassword(email: string): Observable<any> {
    return this.http
      .get(API_USERS_URL + "/forgot?=" + email)
      .pipe(catchError(this.handleError("forgot-password", [])));
  }

  getAllUsers(): Observable<User[]> {
    return this.http.get<User[]>(environment.config.MEALMATE_URL + "api/users");
  }

  getUserById(userId: number): Observable<User> {
    return this.http.get<User>(API_USERS_URL + `/${userId}`);
  }

  // DELETE => delete the user from the server
  deleteUser(userId: number) {
    const url = `${API_USERS_URL}/${userId}`;
    return this.http.delete(url);
  }

  // UPDATE => PUT: update the user on the server
  // tslint:disable-next-line
  updateUser(_user: User): Observable<any> {
    const httpHeaders = new HttpHeaders();
    httpHeaders.set("Content-Type", "application/json");
    return this.http.put(API_USERS_URL, _user, { headers: httpHeaders });
  }

  // CREATE =>  POST: add a new user to the server
  createUser(user: User): Observable<User> {
    const httpHeaders = new HttpHeaders();
    httpHeaders.set("Content-Type", "application/json");
    return this.http.post<User>(environment.config.MEALMATE_URL, user, {
      headers: httpHeaders,
    });
  }

  // Method from server should return QueryResultsModel(items: any[], totalsCount: number)
  // items => filtered/sorted result
  findUsers(queryParams: any): Observable<any> {
    const httpHeaders = new HttpHeaders();
    httpHeaders.set("Content-Type", "application/json");
    return this.http.get<any>(environment.config.MEALMATE_URL + "api/users");
  }

  // Permission
  // getAllPermissions(): Observable<Permission[]> {
  //   return this.http.get<Permission[]>(API_PERMISSION_URL);
  // }

  // getRolePermissions(roleId: number): Observable<Permission[]> {
  //   return this.http.get<Permission[]>(
  //     API_PERMISSION_URL + "/getRolePermission?=" + roleId
  //   );
  // }
  // getRolePermissions(roleId: number = 1): Observable<Permission[]> {
  //   const allRolesRequest = this.http.get<Permission[]>(API_PERMISSION_URL);
  //   const roleRequest = roleId ? this.getRoleById(roleId) : of(null);
  //   return forkJoin(allRolesRequest, roleRequest).pipe(
  //     map((res) => {
  //       const allPermissions: Permission[] = res[0];
  //       const role: Role = res[1];
  //       if (!allPermissions || allPermissions.length === 0) {
  //         return [];
  //       }

  //       const rolePermission = role ? role.permissions : [];
  //       return this.getRolePermissionsTree(allPermissions, rolePermission);
  //     })
  //   );
  // }
  // private getRolePermissionsTree(
  //   allPermission: Permission[] = [],
  //   rolePermissionIds: number[] = []
  // ): Permission[] {
  //   const result: Permission[] = [];
  //   const root: Permission[] = filter(
  //     allPermission,
  //     (item: Permission) => !item.parentId
  //   );
  //   each(root, (rootItem: Permission) => {
  //     rootItem._children = [];
  //     rootItem._children = this.collectChildrenPermission(
  //       allPermission,
  //       rootItem.id,
  //       rolePermissionIds
  //     );
  //     rootItem.isSelected = some(
  //       rolePermissionIds,
  //       (id: number) => id === rootItem.id
  //     );
  //     result.push(rootItem);
  //   });
  //   return result;
  // }

  // private collectChildrenPermission(
  //   allPermission: Permission[] = [],
  //   parentId: number,
  //   rolePermissionIds: number[] = []
  // ): Permission[] {
  //   const result: Permission[] = [];
  //   // tslint:disable-next-line
  //   const _children: Permission[] = filter(
  //     allPermission,
  //     (item: Permission) => item.parentId === parentId
  //   );
  //   if (_children.length === 0) {
  //     return result;
  //   }

  //   each(_children, (childItem: Permission) => {
  //     childItem._children = [];
  //     childItem._children = this.collectChildrenPermission(
  //       allPermission,
  //       childItem.id,
  //       rolePermissionIds
  //     );
  //     childItem.isSelected = some(
  //       rolePermissionIds,
  //       (id: number) => id === childItem.id
  //     );
  //     result.push(childItem);
  //   });
  //   return result;
  // }

  // // Roles
  // getAllRoles(): Observable<Role[]> {
  //   return this.http.get<Role[]>(API_ROLES_URL);
  // }

  // getRoleById(roleId: number): Observable<Role> {
  //   return this.http.get<Role>(API_ROLES_URL + `/${roleId}`);
  // }

  // // CREATE =>  POST: add a new role to the server
  // createRole(role: Role): Observable<Role> {
  //   // Note: Add headers if needed (tokens/bearer)
  //   const httpHeaders = new HttpHeaders();
  //   httpHeaders.set("Content-Type", "application/json");
  //   return this.http.post<Role>(API_ROLES_URL, role, { headers: httpHeaders });
  // }

  // // UPDATE => PUT: update the role on the server
  // updateRole(role: Role): Observable<any> {
  //   const httpHeaders = new HttpHeaders();
  //   httpHeaders.set("Content-Type", "application/json");
  //   return this.http.put(API_ROLES_URL, role, { headers: httpHeaders });
  // }

  // // DELETE => delete the role from the server
  // deleteRole(roleId: number): Observable<Role> {
  //   const url = `${API_ROLES_URL}/${roleId}`;
  //   return this.http.delete<Role>(url);
  // }

  // // Check Role Before deletion
  // isRoleAssignedToUsers(roleId: number): Observable<boolean> {
  //   return this.http.get<boolean>(
  //     API_ROLES_URL + "/checkIsRollAssignedToUser?roleId=" + roleId
  //   );
  // }

  // findRoles(queryParams: QueryParamsModel): Observable<QueryResultsModel> {
  //   // This code imitates server calls
  //   const httpHeaders = new HttpHeaders();
  //   httpHeaders.set("Content-Type", "application/json");
  //   return this.http.post<QueryResultsModel>(
  //     API_ROLES_URL + "/findRoles",
  //     queryParams,
  //     { headers: httpHeaders }
  //   );
  // }

  /*
   * Handle Http operation that failed.
   * Let the app continue.
   *
   * @param operation - name of the operation that failed
   * @param result - optional value to return as the observable result
   */
  private handleError<T>(operation = "operation", result?: any) {
    return (error: any): Observable<any> => {
      // TODO: send the error to remote logging infrastructure
      console.error(error); // log to console instead

      // Let the app keep running by returning an empty result.
      return of(result);
    };
  }
}
