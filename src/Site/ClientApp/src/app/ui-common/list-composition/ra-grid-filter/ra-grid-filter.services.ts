import { Injectable } from '@angular/core';
import { KeyValue } from "../../../app.models";
import { FilterControlKinds, RaGridFilterModel } from "../list-composition.models";
import { RaTextBox, RaSelectBox } from "../../forms-composition/forms-composition.models";

@Injectable()
export class GridFilterCompositionService {

  public toColumns(model: RaGridFilterModel): KeyValue<string>[] {
    let result: KeyValue<string>[] = [];

    for (let i = 0; i < model.columns.length; i++) {
      let column: FilterControlKinds = model.columns[i];
      if (this.isFilterTextControl(column)) {
        result.push({ key: column.id, value: column.label });
      }
    }
    return result;
	}

  public getControlById(model: RaGridFilterModel, columnId: string): FilterControlKinds {
    let result = null;
    for (let i = 0; i < model.columns.length; i++) {
      let column: FilterControlKinds = model.columns[i];
      if (column.id === columnId) {
		  result = {...column,id:"value"}
      }

    }
    return result;
  }

  private isFilterTextControl(column: FilterControlKinds): column is RaTextBox {
    return column.kind === "textbox";
	}

  private isFilterSelectControl(column: FilterControlKinds): column is RaSelectBox {
    return column.kind === "selectbox";
  }
}
