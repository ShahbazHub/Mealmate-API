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
import {
  LayoutUtilsService,
  MessageType,
  QueryParamsModel,
} from "../../../core/_base/crud";
import { MatDialog } from "@angular/material/dialog";
import { DietariesModel } from "../../../core/dietaries/_models/dietaries.model";
import { DietariesService } from "../../../core/dietaries/_services/dietaries.service";
import { DietariesEditDialogComponent } from ".//dietaries-edit/dietariesedit.dialog.component";
/**
 * @title Table retrieving data through HTTP
 */
@Component({
  selector: "kt-dietaries",
  templateUrl: "./dietaries.component.html",
  styleUrls: ["./dietaries.component.scss"],
})
export class DietariesComponent implements OnInit {
  dataSourceOne = new MatTableDataSource();
  displayedColumnsOne: string[] = ["name", "description", "status", "actions"];
  @ViewChild("TableOnePaginator", { static: true })
  TableOnePaginator: MatPaginator;
  @ViewChild("TableOneSort", { static: true }) tableOneSort: MatSort;

  resultsLength = 0;
  isLoadingResults = true;
  isRateLimitReached = false;

  constructor(
    private http: HttpClient,
    public dialog: MatDialog,
    private layoutUtilsService: LayoutUtilsService,
    private dietariesService: DietariesService
  ) {}

  ngOnInit() {
    this.loadDietariesList();
  }
  loadDietariesList() {
    this.dietariesService.getDietariesList(1).subscribe((data) => {
      this.dataSourceOne.data = data.items;
      this.isLoadingResults = false;
      this.isRateLimitReached = false;
    });
  }
  addDietaries() {
    const newDietaries = new DietariesModel();
    newDietaries.clear(); // Set all defaults fields
    this.editDietaries(newDietaries);
  }

  editDietaries(Dietaries: any) {
    let saveMessageTranslateParam = "Dietaries ";
    saveMessageTranslateParam +=
      Dietaries.id > 0 ? "Updated Successfully" : "Added Successfully";
    const _saveMessage = saveMessageTranslateParam;
    const _messageType =
      Dietaries.id > 0 ? "Dietaries Updated " : "Dietaries Created";
    const dialogRef = this.dialog.open(DietariesEditDialogComponent, {
      data: { Dietaries },
    });
    dialogRef.afterClosed().subscribe((res) => {
      if (!res) {
        return;
      }

      this.layoutUtilsService.showActionNotification(
        _saveMessage,
        _messageType
      );
      this.loadDietariesList();
    });
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
        return "In Active";
      case 1:
        return "Active";
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
        return "danger";
      case 1:
        return "success";
    }
    return "";
  }

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
