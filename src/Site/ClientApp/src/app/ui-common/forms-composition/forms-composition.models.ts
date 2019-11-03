import { ValidatorFn, AsyncValidator  } from "@angular/forms";
import { RaEntityEdit } from "../../ra-schema/ra-schema.module";

export interface SchemaFetchEvent {
	isNewEntity: boolean;
	customSchema: {
		editForm: RaEntityEdit;
		layouts:string[];
	}
}

export interface ExtensionParts {
	validators?: {
		[key: string]: ValidatorFn
	};
	asyncValidators?: {
		[key: string]: AsyncValidator
	};
}
