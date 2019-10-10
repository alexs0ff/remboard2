import { IEntityBase } from "../../ra-cruds/ra-cruds.module";

export interface OrderStatus extends IEntityBase {
	title: string;
	orderStatusKindId: number;
  orderStatusKindTitle:string;
}
