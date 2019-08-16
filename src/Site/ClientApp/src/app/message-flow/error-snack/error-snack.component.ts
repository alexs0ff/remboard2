import { Component, OnInit, Inject } from '@angular/core';
import { MAT_SNACK_BAR_DATA, MatSnackBarRef  } from '@angular/material/snack-bar';
import { ErrorSnackData } from "../message-flow.models";


@Component({
  selector: 'app-error-snack',
  templateUrl: './error-snack.component.html',
  styleUrls: ['./error-snack.component.scss']
})
export class ErrorSnackComponent implements OnInit {

  constructor(@Inject(MAT_SNACK_BAR_DATA) public data: ErrorSnackData, private matSnackBarRef: MatSnackBarRef<ErrorSnackComponent>) { }

  ngOnInit() {

  }

  closeBar() {
    this.matSnackBarRef.dismiss();
  }

}
