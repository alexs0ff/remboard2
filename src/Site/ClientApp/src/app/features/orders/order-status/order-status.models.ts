import { IEntityBase } from "../../ra-cruds/ra-cruds.module";

export interface OrderStatus {
	id: string;
	title: string;
	orderStatusKindId: number;
  orderStatusKindTitle:string;
}
