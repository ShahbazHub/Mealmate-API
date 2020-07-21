import { Component, OnInit, ViewChild, ViewChildren } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { MatPaginator } from "@angular/material/paginator";
import { MatSort } from "@angular/material/sort";
import { MatTableDataSource } from "@angular/material/table";
import {
  ConfirmDialogModel,
  ConfirmDialogComponent,
} from "../config/confirm-dialog/confirm-dialog.component";
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
// import { OptionsEditDialogComponent } from "./options-edit/options-edit.dialog.component";
import { ConfigModel } from "src/app/core/config/_models/config.model";
import { ConfigService } from "src/app/core/config/_services/config.service";

/**
 * @title Table retrieving data through HTTP
 */
export interface Tile {
  color: string;
  cols: number;
  rows: number;
  text: string;
}

@Component({
  selector: "kt-states",
  templateUrl: "./states.component.html",
  styleUrls: ["./states.component.scss"],
})
export class StatesComponent implements OnInit {
  tiles: Tile[] = [
    { text: "One", cols: 1, rows: 1, color: "white" },
    { text: "Two", cols: 1, rows: 1, color: "white" },
    { text: "Three", cols: 1, rows: 1, color: "white" },
    // { text: "Four", cols: 2, rows: 1, color: "#DDBDF1" },
  ];
  result: any;

  constructor(
    private http: HttpClient,
    public dialog: MatDialog,
    private layoutUtilsService: LayoutUtilsService,
    private configService: ConfigService
  ) {}

  ngOnInit() {
    this.loadConfigList();
  }
  loadConfigList() {
    // this.configService.getOptionsList(1).subscribe((data) => {
    //   this.dataSourceOne.data = data.items;
    // });
  }

  confirmDialog(): void {
    const message = `Are you sure you want to do this?`;

    const dialogData = new ConfirmDialogModel("Confirm Action", message);

    const dialogRef = this.dialog.open(ConfirmDialogComponent, {
      maxWidth: "300px",
      data: dialogData,
    });

    dialogRef.afterClosed().subscribe((dialogResult) => {
      this.result = dialogResult;
    });
  }
  // addConfig() {
  //   const newConfig = new ConfigModel();
  //   newConfig.clear(); // Set all defaults fields
  //   this.editConfig(newConfig);
  // }

  // editConfig(Config: any) {
  //   let saveMessageTranslateParam = "Config ";
  //   saveMessageTranslateParam +=
  //     Config.id > 0 ? "Updated Successfully" : "Added Successfully";
  //   const _saveMessage = saveMessageTranslateParam;
  //   const _messageType =
  //     Config.id > 0 ? "Config Updated " : "Config Created";
  //   const dialogRef = this.dialog.open(OptionsEditDialogComponent, {
  //     data: { Config },
  //   });
  //   dialogRef.afterClosed().subscribe((res) => {
  //     if (!res) {
  //       return;
  //     }

  //     this.layoutUtilsService.showActionNotification(
  //       _saveMessage,
  //       _messageType
  //     );
  //     this.loadConfigList();
  //   });
  // }
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
