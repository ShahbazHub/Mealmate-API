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
import { MenuEditDialogComponent } from "./menu-edit/menu-edit.dialog.component";
import {
  LayoutUtilsService,
  MessageType,
  QueryParamsModel,
} from "../../../core/_base/crud";
import { MatDialog } from "@angular/material/dialog";
import { MenuModel } from "../../../core/menu/_models/menu.model";
import { MenuItemModel } from "../../../core/menu/_models/menuitem.model";
import { MenuItemService } from "../../../core/menu/_services/menuItem.service";
import { MenuItemEditDialogComponent } from "./menuitem-edit/menuitem-edit.dialog.component";
import { MenuOptionEditDialogComponent } from "./menuoptions-edit/menuoptions-edit.dialog.component";
import { MenuOptionModel } from "src/app/core/menu/_models/menuoptions.model";
/**
 * @title Table retrieving data through HTTP
 */
@Component({
  selector: "kt-menu",
  templateUrl: "./menu.component.html",
  styleUrls: ["./menu.component.scss"],
})
export class MenuComponent implements OnInit {
  dataSourceOne = new MatTableDataSource();
  displayedColumnsOne: string[] = ["title", "description", "time", "actions"];
  @ViewChild("TableOnePaginator", { static: true })
  TableOnePaginator: MatPaginator;
  @ViewChild("TableOneSort", { static: true }) tableOneSort: MatSort;

  dataSourceTwo = new MatTableDataSource();
  displayedColumnsTwo: string[] = ["name", "description", "price", "actions"];
  @ViewChild("TableTwoPaginator", { static: true })
  tableTwoPaginator: MatPaginator;
  @ViewChild("TableTwoSort", { static: true }) tableTwoSort: MatSort;

  dataSourceThree = new MatTableDataSource();
  displayedColumnsThree: string[] = [
    "optionname",
    "quantity",
    "price",
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
  isRateLimitReachedMenuItem: boolean;
  isLoadingResultsMenuItem: boolean;
  showMenuItem: boolean = false;

  isRateLimitReachedMenuOption: boolean;
  isLoadingResultsMenuOption: boolean;
  showMenuOption: boolean = false;

  constructor(
    private http: HttpClient,
    public dialog: MatDialog,
    private layoutUtilsService: LayoutUtilsService,
    private menuService: MenuItemService
  ) {}

  ngOnInit() {
    this.loadMenuTimeList();
  }
  loadMenuTimeList() {
    //this.MenuDatabase = new MenuTime(this.http);

    // If the user changes the sort order, reset back to the first page.
    //this.sort.sortChange.subscribe(() => (this.paginator.pageIndex = 0));

    // merge()
    //   .pipe(
    //     startWith({}),
    //     switchMap(() => {
    //       this.isLoadingResults = true;
    //       return this.menuService.getMenuList(this.paginator.pageIndex);
    //     }),
    //     map((data) => {
    //       // Flip flag to show that loading has finished.
    //       this.isLoadingResults = false;
    //       this.isRateLimitReached = false;
    //       this.resultsLength = data.total_count;

    //       return data.items;
    //     }),
    //     catchError(() => {
    //       this.isLoadingResults = false;
    //       // Catch if the GitHub API has reached its rate limit. Return empty data.
    //       this.isRateLimitReached = true;
    //       return observableOf([]);
    //     })
    //   )
    this.menuService.getMenuList(1).subscribe((data) => {
      this.dataSourceOne.data = data.items;
      this.isLoadingResults = false;
      this.isRateLimitReached = false;
      this.showMenuItem = false;
    });
  }
  addMenu() {
    const newMenu = new MenuModel();
    newMenu.clear(); // Set all defaults fields
    this.editMenu(newMenu);
  }

  editMenu(Menu: any) {
    let saveMessageTranslateParam = "Menu ";
    saveMessageTranslateParam +=
      Menu.id > 0 ? "Updated Successfully" : "Added Successfully";
    const _saveMessage = saveMessageTranslateParam;
    const _messageType = Menu.id > 0 ? "Menu Updated " : "Menu Created";
    const dialogRef = this.dialog.open(MenuEditDialogComponent, {
      data: { Menu },
    });
    dialogRef.afterClosed().subscribe((res) => {
      if (!res) {
        return;
      }

      this.layoutUtilsService.showActionNotification(
        _saveMessage,
        _messageType
      );
      this.loadMenuTimeList();
    });
  }

  fetchMenuItems() {
    this.showMenuItem = true;
    console.log("Fetching");
    this.loadMenuItemList();
  }

  loadMenuItemList() {
    this.menuService.getMenuItems(1).subscribe((data) => {
      this.dataSourceTwo.data = data.items;
      this.isLoadingResultsMenuItem = false;
    });
  }
  addMenuItem() {
    const newMenu = new MenuItemModel();
    newMenu.clear(); // Set all defaults fields
    this.editMenuItem(newMenu);
  }

  editMenuItem(Menu: any) {
    let saveMessageTranslateParam = "Menu Item ";
    saveMessageTranslateParam +=
      Menu.id > 0 ? "Updated Successfully" : "Added Successfully";
    const _saveMessage = saveMessageTranslateParam;
    const _messageType = Menu.id > 0 ? "Menu Updated " : "Menu Created";
    const dialogRef = this.dialog.open(MenuItemEditDialogComponent, {
      data: { Menu },
    });
    dialogRef.afterClosed().subscribe((res) => {
      if (!res) {
        return;
      }

      this.layoutUtilsService.showActionNotification(
        _saveMessage,
        _messageType
      );
      this.loadMenuItemList();
    });
  }

  fetchMenuOptions() {
    this.showMenuOption = true;
    console.log("Fetching");
    this.loadMenuOptionList();
  }
  loadMenuOptionList() {
    this.menuService.getMenuOptions(1).subscribe((data) => {
      this.dataSourceThree.data = data.items;
      this.isLoadingResultsMenuOption = false;
    });
  }
  addMenuOption() {
    const newMenu = new MenuOptionModel();
    newMenu.clear(); // Set all defaults fields
    this.editMenuOption(newMenu);
  }

  editMenuOption(Menu: any) {
    let saveMessageTranslateParam = "Menu Option ";
    saveMessageTranslateParam +=
      Menu.id > 0 ? "Updated Successfully" : "Added Successfully";
    const _saveMessage = saveMessageTranslateParam;
    const _messageType = Menu.id > 0 ? "Menu Updated " : "Menu Created";
    const dialogRef = this.dialog.open(MenuOptionEditDialogComponent, {
      data: { Menu },
    });
    dialogRef.afterClosed().subscribe((res) => {
      if (!res) {
        return;
      }

      this.layoutUtilsService.showActionNotification(
        _saveMessage,
        _messageType
      );
      this.loadMenuItemList();
    });
  }
}
