import { Injectable } from '@angular/core';
import { MatDialog} from '@angular/material/dialog';
import { LoginDialogComponent } from "./login-dialog.component";

@Injectable()
export class LoginDialogService {

  constructor(private dialog: MatDialog) {

  }

  public open() {
    const dialogRef = this.dialog.open(LoginDialogComponent, {
    });


  }
}
