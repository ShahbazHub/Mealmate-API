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
import { OptionsModel } from "../../../core/options/_models/options.model";
import { OptionsService } from "../../../core/options/_services/oprions.service";
import { OptionsEditDialogComponent } from "./options-edit/options-edit.dialog.component";

/**
 * @title Table retrieving data through HTTP
 */
@Component({
  selector: "kt-options",
  templateUrl: "./options.component.html",
  styleUrls: ["./options.component.scss"],
})
export class OptionsComponent implements OnInit {
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
    private optionsService: OptionsService
  ) {}

  ngOnInit() {
    this.loadOptionsList();
  }
  loadOptionsList() {
    this.optionsService.getOptionsList(1).subscribe((data) => {
      this.dataSourceOne.data = data.items;
      this.isLoadingResults = false;
      this.isRateLimitReached = false;
    });
  }
  addOptions() {
    const newOptions = new OptionsModel();
    newOptions.clear(); // Set all defaults fields
    this.editOptions(newOptions);
  }

  editOptions(Options: any) {
    let saveMessageTranslateParam = "Options ";
    saveMessageTranslateParam +=
      Options.id > 0 ? "Updated Successfully" : "Added Successfully";
    const _saveMessage = saveMessageTranslateParam;
    const _messageType =
      Options.id > 0 ? "Options Updated " : "Options Created";
    const dialogRef = this.dialog.open(OptionsEditDialogComponent, {
      data: { Options },
    });
    dialogRef.afterClosed().subscribe((res) => {
      if (!res) {
        return;
      }

      this.layoutUtilsService.showActionNotification(
        _saveMessage,
        _messageType
      );
      this.loadOptionsList();
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
