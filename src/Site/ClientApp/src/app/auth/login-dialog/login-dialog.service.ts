import { Injectable } from '@angular/core';
import { MatDialog} from '@angular/material/dialog';
import { LoginDialogComponent } from "./login-dialog.component";
import {filter} from 'rxjs/operators'
import { AuthCredentials } from "../auth.models";
import { Store } from "@ngrx/store";
import { authStartLogin } from "../auth.actions";

@Injectable()
export class LoginDialogService {

  constructor(private dialog: MatDialog, private store: Store<{}>) {

  }

  public open() {
    const dialogRef = this.dialog.open(LoginDialogComponent, {
    });

    /*dialogRef.afterClosed().pipe(filter(g => g !== false)).subscribe((login: AuthCredentials) => {
      
    });**/

  }
}
