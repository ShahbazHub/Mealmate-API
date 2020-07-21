import { Component, OnInit, ViewChild, ViewChildren } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { MatPaginator } from "@angular/material/paginator";
import { MatSort } from "@angular/material/sort";
import { MatTableDataSource } from "@angular/material/table";
import { Observable } from "rxjs";
import { merge } from "rxjs";
import { of as observableOf } from "rxjs";
import { catchError } from "rxjs/operators";
import { map } from "rxjs/operators";
import { startWith } from "rxjs/operators";
import { switchMap } from "rxjs/operators";

import { LayoutUtilsService } from "../../../core/_base/crud";
import { MatDialog } from "@angular/material/dialog";

import { RestaurantModel } from "../../../core/restaurant/_models/restaurant.model";
import { BranchModel } from "../../../core/restaurant/_models/branch.model";
import { HallModel } from "../../../core/restaurant/_models/hall.model";
import { TableModel } from "src/app/core/restaurant/_models/table.model";

import { RestaurantService } from "../../../core/restaurant/_services/restaurant.service";

//import { RestaurantEditComponent } from "./restaurant-edit/restaurant-edit.dialog.component";

import { TableEditDialogComponent } from "./tables-edit/table-edit.dialog.component";
import { BranchEditDialogComponent } from "./branch-edit/branch-edit.dialog.component";
import { HallEditDialogComponent } from "./halls-edit/hall-edit.dialog.component";
import { RestaurantEditDialogComponent } from "./restaurant-edit/restaurant-edit.dialog.component";

/**
 * @title Table retrieving data through HTTP
 */
@Component({
  selector: "kt-restaurant",
  templateUrl: "./restaurant.component.html",
  styleUrls: ["./restaurant.component.scss"],
})
export class RestaurantComponent implements OnInit {
  dataSourceOne = new MatTableDataSource();
  displayedColumnsOne: string[] = [
    "name",
    "address",
    "locations",
    "status",
    "created",
    "actions",
  ];
  @ViewChild("TableOnePaginator", { static: true })
  TableOnePaginator: MatPaginator;
  @ViewChild("TableOneSort", { static: true }) tableOneSort: MatSort;

  dataSourceTwo = new MatTableDataSource();
  displayedColumnsTwo: string[] = ["name", "status", "actions"];
  @ViewChild("TableTwoPaginator", { static: true })
  tableTwoPaginator: MatPaginator;
  @ViewChild("TableTwoSort", { static: true }) tableTwoSort: MatSort;

  dataSourceThree = new MatTableDataSource();
  displayedColumnsThree: string[] = [
    "name",
    "status",
    // "price",
    "actions",
  ];
  @ViewChild("TableThreePaginator", { static: true })
  tableThreePaginator: MatPaginator;
  @ViewChild("TableThreeSort", { static: true }) tableThreeSort: MatSort;

  // displayedColumns = ["title", "description", "time", "actions"];
  // displayedColumnsMenuItems = ["name", "description", "price", "actions"];
  // MenuDatabase: MenuTime | null;

  // dataSource = new MatTableDataSource();
  // dataSource1 = new MatTableDataSource();
  resultsLength = 0;
  isLoadingResults = true;
  isRateLimitReached = false;

  // @ViewChild(MatPaginator, { static: false }) paginator: MatPaginator;
  // @ViewChild(MatSort, { static: false }) sort: MatSort;
  isRateLimitReachedHalls: boolean;
  isLoadingResultsHalls: boolean;
  showHalls: boolean = false;

  isRateLimitReachedMenuOption: boolean;
  isLoadingResultsMenuOption: boolean;
  showTable: boolean = false;
  restaurantName: any;
  restaurantDescription: any;
  restaurantId: any;
  totalCountOne: any;

  constructor(
    private http: HttpClient,
    public dialog: MatDialog,
    private layoutUtilsService: LayoutUtilsService,
    private restaurantService: RestaurantService
  ) {}

  ngOnInit() {
    this.restaurantService.getUserRestaurant().subscribe((args) => {
      console.log(args);

      this.restaurantName = args[0].name;
      this.restaurantDescription = args[0].description;
      this.restaurantId = args[0].id;
      this.loadBranchList(this.restaurantId);
    });
  }
  filterConfiguration(): any {
    const filter: any = {};
    // const searchText: string = this.searchInput.nativeElement.value;

    // if (this.filterStatus && this.filterStatus.length > 0) {
    //   filter.status = +this.filterStatus;
    // }

    // if (this.filterType && this.filterType.length > 0) {
    //   filter.type = +this.filterType;
    // }

    // filter.lastName = searchText;
    // if (!searchText) {
    //   return filter;
    // }

    // filter.firstName = searchText;
    // filter.email = searchText;
    // filter.ipAddress = searchText;
    return filter;
  }

  loadBranchList(id: number) {
    // this.dataSourceOne.
    //this.filterConfiguration(),

    const queryParams = new QueryParamModel(
      //this.filterConfiguration(),
      this.tableOneSort.direction,
      this.tableOneSort.active,
      this.TableOnePaginator.pageIndex,
      this.TableOnePaginator.pageSize
    );
    console.log(queryParams);

    this.restaurantService.getBranchList(id, queryParams).subscribe((data) => {
      console.log(data);

      this.dataSourceOne.data = data.items;
      console.log(this.dataSourceOne.data);
      this.totalCountOne = data.totalCount;
      //this.TableOnePaginator.pageIndex = 0;
      this.isLoadingResults = false;
      this.isRateLimitReached = false;
      this.showHalls = false;
    });
  }

  addBranch() {
    const newBranch = new BranchModel();
    newBranch.clear(this.restaurantId); // Set all defaults fields
    this.editBranch(newBranch);
  }

  editBranch(Branch: any) {
    let saveMessageTranslateParam = "Branch ";
    saveMessageTranslateParam +=
      Branch.id > 0 ? "Updated Successfully" : "Added Successfully";
    const _saveMessage = saveMessageTranslateParam;
    const _messageType = Branch.id > 0 ? "Branch Updated " : "Branch Created";
    const dialogRef = this.dialog.open(BranchEditDialogComponent, {
      data: { Branch },
    });
    dialogRef.afterClosed().subscribe((res) => {
      if (!res) {
        return;
      }

      this.layoutUtilsService.showActionNotification(
        _saveMessage,
        _messageType
      );
      this.loadBranchList(this.restaurantId);
    });
  }

  fetchRestaurantHalls() {
    this.showHalls = true;
    console.log("Fetching");
    this.loadHallList();
  }

  loadHallList() {
    this.restaurantService.getMenuItems(1).subscribe((data) => {
      this.dataSourceTwo.data = data.items;
      this.isLoadingResultsHalls = false;
    });
  }
  addHall() {
    const newHall = new HallModel();
    newHall.clear(); // Set all defaults fields
    this.editHall(newHall);
  }

  editHall(Hall: any) {
    let saveMessageTranslateParam = "Hall Item ";
    saveMessageTranslateParam +=
      Hall.id > 0 ? "Updated Successfully" : "Added Successfully";
    const _saveMessage = saveMessageTranslateParam;
    const _messageType = Hall.id > 0 ? "Hall Updated " : "Hall Created";
    const dialogRef = this.dialog.open(HallEditDialogComponent, {
      data: { Hall },
    });
    dialogRef.afterClosed().subscribe((res) => {
      if (!res) {
        return;
      }

      this.layoutUtilsService.showActionNotification(
        _saveMessage,
        _messageType
      );
      this.loadHallList();
    });
  }

  fetchTables() {
    this.showTable = true;
    console.log("Fetching");
    this.loadTablesList();
  }
  loadTablesList() {
    this.restaurantService.getMenuOptions(1).subscribe((data) => {
      this.dataSourceThree.data = data.items;
      this.isLoadingResultsMenuOption = false;
    });
  }
  addTable() {
    const newMTable = new TableModel();
    newMTable.clear(); // Set all defaults fields
    this.editTable(newMTable);
  }

  editTable(Table: any) {
    let saveMessageTranslateParam = "Table Option ";
    saveMessageTranslateParam +=
      Table.id > 0 ? "Updated Successfully" : "Added Successfully";
    const _saveMessage = saveMessageTranslateParam;
    const _messageType = Table.id > 0 ? "Table Updated " : "Table Created";
    const dialogRef = this.dialog.open(TableEditDialogComponent, {
      data: { Table },
    });
    dialogRef.afterClosed().subscribe((res) => {
      if (!res) {
        return;
      }

      this.layoutUtilsService.showActionNotification(
        _saveMessage,
        _messageType
      );
      this.loadTablesList();
    });
  }

  editRestaurant(Restaurant1: any) {
    var Restaurant = {
      restaurant: "hamza",
      email: "h@live.com",
      first_name: "hamza",
      last_name: "Malik",
      address: "Islamabad",
      id: 1,
    };
    let saveMessageTranslateParam = "Restaurant ";
    saveMessageTranslateParam +=
      Restaurant.id > 0 ? "Updated Successfully" : "Added Successfully";
    const _saveMessage = saveMessageTranslateParam;
    const _messageType =
      Restaurant.id > 0 ? "Restaurant Updated " : "Restaurant Created";
    const dialogRef = this.dialog.open(RestaurantEditDialogComponent, {
      data: { Restaurant },
    });
    dialogRef.afterClosed().subscribe((res) => {
      if (!res) {
        return;
      }

      this.layoutUtilsService.showActionNotification(
        _saveMessage,
        _messageType
      );
      this.loadRestaurantList();
    });
  }
  loadRestaurantList() {
    //pending api
  }

  /* UI */
  /**
   * Returns status string
   *
   * @param status: number
   */
  getItemStatusString(status: number = 0): string {
    switch (status) {
      case 0:
        return "Active";
      case 1:
        return "InActive";
    }
    return "";
  }

  /**
   * Returns CSS Class by status
   *
   * @param status: number
   */
  getItemCssClassByStatus(status: number = 0): string {
    switch (status) {
      case 0:
        return "success";
      case 1:
        return "danger";
    }
    return "";
  }

  // openDialog() {
  //   this.dialog.open(DialogDataExampleDialog, {
  //     data: {
  //       animal: 'panda'
  //     }
  //   });
  // }

  /**
   * Rerurns condition string
   *
   * @param condition: number
   */
  getItemConditionString(condition: number = 0): string {
    switch (condition) {
      case 0:
        return "New";
      case 1:
        return "Used";
    }
    return "";
  }

  /**
   * Returns CSS Class by condition
   *
   * @param condition: number
   */
  getItemCssClassByCondition(condition: number = 0): string {
    switch (condition) {
      case 0:
        return "danger";
      case 1:
        return "primary";
    }
    return "";
  }
}

export class QueryParamModel {
  // fields
  filter: any;
  SortOrder: string; // asc || desc
  SortField: string;
  PageIndex: number;
  PageSize: number;
  PagingStrategy: string;

  // constructor overrides
  constructor(
    SortOrder = "asc",
    SortField = "",
    PageNumber = 0,
    PageSize = 10
  ) {
    this.SortOrder = SortOrder;
    this.SortField = SortField;
    this.PageIndex = PageNumber;
    this.PageSize = PageSize;
    this.PagingStrategy = "withCount";
  }
}
