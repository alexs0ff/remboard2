import { RaEntityEdit } from "../../ra-schema/ra-schema.module";

export interface SchemaFetchEvent {
	isNewEntity: boolean;
	customSchema: {
		editForm: RaEntityEdit;
		layouts:string[];
	}
}
