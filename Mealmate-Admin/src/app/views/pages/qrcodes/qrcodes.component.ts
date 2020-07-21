import { Component, OnInit, ViewChild, ViewChildren } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { MatPaginator } from "@angular/material/paginator";
import { MatSort } from "@angular/material/sort";
import { MatTableDataSource } from "@angular/material/table";
import {
  LayoutUtilsService,
  MessageType,
  QueryParamsModel,
} from "../../../core/_base/crud";
import { MatDialog } from "@angular/material/dialog";
import { QrcodesModel } from "src/app/core/qrcodes/_models/qrcodes.model";
import { QrcodesService } from "src/app/core/qrcodes/_services/qrcodes.service";
import { PrintService } from "src/app/core/print/_services/print.service";
import { PrEditDialogComponent } from "./printqr/pr.dialog.component";

/**
 * @title Table retrieving data through HTTP
 */
@Component({
  selector: "kt-qrcodes",
  templateUrl: "./qrcodes.component.html",
  styleUrls: ["./qrcodes.component.scss"],
})
export class QrcodesComponent implements OnInit {
  dataSourceOne = new MatTableDataSource();
  displayedColumnsOne: string[] = ["title", "status", "actions"];
  @ViewChild("TableOnePaginator", { static: true })
  TableOnePaginator: MatPaginator;
  @ViewChild("TableOneSort", { static: true }) tableOneSort: MatSort;

  resultsLength = 0;
  isLoadingResults = true;
  isRateLimitReached = false;
  filterStatus: string = "";

  constructor(
    private http: HttpClient,
    public dialog: MatDialog,
    private layoutUtilsService: LayoutUtilsService,
    private qrcodesServices: QrcodesService,
    private printService: PrintService
  ) {}
  // onPrintInvoice() {
  //   const invoiceIds = ["101", "102"];
  //   this.printService.printDocument("invoice", invoiceIds);
  // }
  ngOnInit() {
    this.loadQrcodesList();
  }
  loadQrcodesList() {
    this.qrcodesServices.getQrcodesList(1).subscribe((data) => {
      this.dataSourceOne.data = data.items;
      this.isLoadingResults = false;
      this.isRateLimitReached = false;
    });
  }
  addQrcodes() {
    const newQrcodes = new QrcodesModel();
    newQrcodes.clear(); // Set all defaults fields
    this.editQrcodes(newQrcodes);
  }
  openDialog() {
    const dialogRef = this.dialog.open(PrEditDialogComponent, {
      height: "650px",
      width: "900px",
      data: { name: "Hamza", width: 800 },
    });

    dialogRef.afterClosed().subscribe((result) => {
      console.log(`Dialog result: ${result}`);
    });
  }

  editQrcodes(Qrcodes: any) {
    window.print();
    // let saveMessageTranslateParam = "Qrcodes ";
    // saveMessageTranslateParam +=
    //   Qrcodes.id > 0 ? "Updated Successfully" : "Added Successfully";
    // const _saveMessage = saveMessageTranslateParam;
    // const _messageType =
    //   Qrcodes.id > 0 ? "Qrcodes Updated " : "Qrcodes Created";
    // const dialogRef = this.dialog.open(QrcodesEditDialogComponent, {
    //   data: { Qrcodes },
    // });
    // dialogRef.afterClosed().subscribe((res) => {
    //   if (!res) {
    //     return;
    //   }

    //   this.layoutUtilsService.showActionNotification(
    //     _saveMessage,
    //     _messageType
    //   );
    //   this.loadQrcodesList();
    // });
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
