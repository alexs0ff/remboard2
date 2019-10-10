import { IEntityBase } from "../../ra-cruds/ra-cruds.module";

export interface AutocompleteItem extends IEntityBase {
  title: string,
  autocompleteKindTitle: string,
  autocompleteKindId: number,

}
