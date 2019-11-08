import { ValidatorFn, AsyncValidatorFn } from "@angular/forms";
import { RaEntityEdit } from "../../ra-schema/ra-schema.module";

export interface SchemaFetchEvent {
	isNewEntity: boolean;
	customSchema: {
		editForm: RaEntityEdit;
		layouts:string[];
	}
}

export interface ControlMasks {
	masks: {
		 [key: string]: Array<string | RegExp>
	};
	sanitizeFunctions?: {
		[key: string]: (value:string)=>string|number;
	}
}

export interface ExtensionParts {
	validators?: {
		[key: string]: ValidatorFn
	};
	asyncValidators?: {
		[key: string]: AsyncValidatorFn
	};

	controlMasks?: ControlMasks;
}
