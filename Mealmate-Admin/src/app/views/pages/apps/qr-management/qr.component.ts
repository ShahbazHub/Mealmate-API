// Angular
import { Component, OnInit, ChangeDetectionStrategy } from "@angular/core";

@Component({
  templateUrl: "./qr.component.html",
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class QrComponent implements OnInit {
  /**
   * Component constructor
   */
  constructor() {}

  /*
   * @ Lifecycle sequences => https://angular.io/guide/lifecycle-hooks
   */

  /**
   * On init
   */
  ngOnInit() {}
}
