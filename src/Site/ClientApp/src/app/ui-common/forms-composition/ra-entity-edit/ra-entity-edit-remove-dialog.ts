import { Component, Inject } from '@angular/core';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { RemoveDialogData } from "../forms-composition.models";

@Component({
    selector: 'ra-entity-edit-remove-dialog',
    template: `
<h1 mat-dialog-title>Удаление записи</h1>
<div mat-dialog-content>
  <p>Вы действительно хотите удалить из <strong>{{data.title}}</strong>?</p>  
    <i>{{data.name}}</i>
</div>
<div mat-dialog-actions  fxLayout="row" fxLayoutAlign="end center">
  <button mat-button (click)="onNoClick();" cdkFocusInitial color="primary">Нет</button>
  <button mat-button (click)="onYesClick();">Да</button>
</div>
`,
    styles: [],
    providers: []
})
export class RaEntityEditRemoveDialog {

  constructor(
      public dialogRef: MatDialogRef<RaEntityEditRemoveDialog>,
      @Inject(MAT_DIALOG_DATA) public data: RemoveDialogData) { }

  onNoClick(): void {
    this.dialogRef.close(false);
    }

  onYesClick(): void {
    this.dialogRef.close(true);
  }

}
