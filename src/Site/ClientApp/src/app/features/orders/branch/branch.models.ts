import { IEntityBase } from "../../ra-cruds/ra-cruds.module";

export interface Branch extends IEntityBase {
  id: string,
  title: string,
  address: string;
  legalName: string;
}
