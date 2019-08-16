import { Injectable } from '@angular/core'
import { MatSnackBar } from '@angular/material/snack-bar';
import { ErrorSnackComponent } from "./error-snack/error-snack.component";
import { ErrorSnackData } from "./message-flow.models";

const errorSnackDuration = 15000;

@Injectable({ providedIn:'root' })
export class MessageFlowService {
  constructor(private snackBar: MatSnackBar) {}

  showMessage(code:string, message:string, details?:string) {

    const data: ErrorSnackData = { code, message,details };
    

    this.snackBar.openFromComponent(ErrorSnackComponent, {
      data: data,
      duration: errorSnackDuration,
      panelClass: 'error-snackbar'

    });
  }
}
