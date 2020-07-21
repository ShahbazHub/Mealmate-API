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
import { AllergensModel } from "../../../core/allergens/_models/allergens.model";
import { AllergensService } from "../../../core/allergens/_services/allergens.service";
import { AllergensEditDialogComponent } from "./allergens-edit/allergens-edit.dialog.component";
/**
 * @title Table retrieving data through HTTP
 */
@Component({
  selector: "kt-Allergens",
  templateUrl: "./allergens.component.html",
  styleUrls: ["./allergens.component.scss"],
})
export class AllergensComponent implements OnInit {
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
    private allergensService: AllergensService
  ) {}

  ngOnInit() {
    this.loadAllergensList();
  }
  loadAllergensList() {
    this.allergensService.getAllergensList(1).subscribe((data) => {
      this.dataSourceOne.data = data.items;
      this.isLoadingResults = false;
      this.isRateLimitReached = false;
    });
  }
  addAllergens() {
    const newAllergens = new AllergensModel();
    newAllergens.clear(); // Set all defaults fields
    this.editAllergens(newAllergens);
  }

  editAllergens(Allergens: any) {
    let saveMessageTranslateParam = "Allergens ";
    saveMessageTranslateParam +=
      Allergens.id > 0 ? "Updated Successfully" : "Added Successfully";
    const _saveMessage = saveMessageTranslateParam;
    const _messageType =
      Allergens.id > 0 ? "Allergens Updated " : "Allergens Created";
    const dialogRef = this.dialog.open(AllergensEditDialogComponent, {
      data: { Allergens },
    });
    dialogRef.afterClosed().subscribe((res) => {
      if (!res) {
        return;
      }

      this.layoutUtilsService.showActionNotification(
        _saveMessage,
        _messageType
      );
      this.loadAllergensList();
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
