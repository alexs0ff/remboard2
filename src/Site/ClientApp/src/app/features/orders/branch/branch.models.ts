import { IEntityBase } from "../../ra-cruds/ra-cruds.module";

export interface Branch extends IEntityBase {
  title: string,
  address: string;
  legalName: string;
}
