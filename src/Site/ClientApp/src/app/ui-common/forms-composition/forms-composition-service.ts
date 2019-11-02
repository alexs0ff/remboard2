import { Injectable } from '@angular/core';
import { FormControl, FormGroup, Validators, ValidatorFn, FormGroupDirective, NgForm, AbstractControl  } from '@angular/forms';
import { ErrorStateMatcher } from '@angular/material/core';
import { KeyValue } from "../../app.models";
import {
	RaControls, RaFormLayout, RaFormLayoutRowContent, RaFormLayoutItems, RaFormLayoutHiddenItems, RaTextBox, ControlValueType, RaSelectBox,LayoutGroups,RaMultiselect
} from "../../ra-schema/ra-schema.module";
import { notEmptyArrayValidator } from "../custom.validators";


@Injectable()
export class FormsCompositionService {
	constructor() {}

	public toFormGroup(layouts: LayoutGroups) {
		let group: any = {};

		for (const key in layouts) {
			let layout = layouts[key];
			layout.rows.forEach(row => {
				if (this.isLayoutItems(row.content)) {
					row.content.items.forEach(item => {
						group[item.control.id] = this.createFormControl(item.control);
					});
				}

				if (this.isHiddenItems(row.content)) {
					row.content.items.forEach(hitem => {
						group[hitem] = new FormControl(null);
					});
				}

			});

		}

		return new FormGroup(group);
	}

	private createFormControl(raControl: RaControls): FormControl {
		let validators = Array<ValidatorFn>();
		
		if (raControl.validators.required) {
			validators.push(Validators.required);
		}

		if (this.isRaTextBox(raControl)) {
			if (raControl.validators.maxLength != null) {
				validators.push(Validators.maxLength(raControl.validators.maxLength));
			}

			if (raControl.validators.minLength != null) {
				validators.push(Validators.minLength(raControl.validators.minLength));
			}

			if (raControl.validators.email) {
				validators.push(Validators.email);
			}
		}

		if (this.isRaMultiselect(raControl)) {
			validators = Array<ValidatorFn>();
			validators.push(notEmptyArrayValidator());
			
		}
		const value = this.valueFromControl(raControl);
		const result = new FormControl(raControl.value || value, validators);

		return result;
	}

	public createDefaultObject(layouts: LayoutGroups): any {
		let result = {};

		for (const key in layouts) {
			let layout = layouts[key];
			layout.rows.forEach(row => {
				if (this.isLayoutItems(row.content)) {
					row.content.items.forEach(item => {
						result[item.control.id] = this.valueFromControl(item.control);
					});
				}

				if (this.isHiddenItems(row.content)) {
					row.content.items.forEach(hitem => {
						result[hitem] = null;
					});
				}

			});

		}

		return result;

	}
	private valueFromControl(raControl: RaControls): any {
		let value = raControl.value||null;

		if (!value) {
			if (this.isRaTextBox(raControl)) {
				if (raControl.validators.required) {
					if (raControl.valueKind === 'string') {
						value = '';
					} else  if (raControl.valueKind==='number'){
						value = 0;
					}
				}
			}else if (this.isRaMultiselect(raControl)) {
				value = [];
			}
		}

		return value;
	}

	private isRaTextBox(raControl: RaControls): raControl is RaTextBox {
		return raControl.kind === "textbox";
	}

	private isRaSelectBox(raControl: RaControls): raControl is RaSelectBox {
		return raControl.kind === "selectbox";
	}


	private isRaMultiselect(raControl: RaControls): raControl is RaMultiselect {
		return raControl.kind === "multiselect";
	}

	private isLayoutItems(content: RaFormLayoutRowContent): content is RaFormLayoutItems {
		return content.kind === "controls";
	}

	private isHiddenItems(content: RaFormLayoutRowContent): content is RaFormLayoutHiddenItems {
		return content.kind === "hidden";
	}
}

export class RedirectedErrorStateMatcher implements ErrorStateMatcher {
  constructor(private parentControl: AbstractControl) {}

  isErrorState(control: FormControl | null, form: FormGroupDirective | NgForm | null): boolean {
    const isSubmitted = form && form.submitted;
    return !!(this.parentControl && this.parentControl.invalid && (control.dirty || control.touched || isSubmitted));
  }
}

export class HiddenErrorStateMatcher implements ErrorStateMatcher {
	constructor(private parentControl: AbstractControl) { }
	isErrorState(control: FormControl | null, form: FormGroupDirective | NgForm | null): boolean {
		const isSubmitted = form && form.submitted;
		const state = !!(control && control.invalid && (this.parentControl.dirty || this.parentControl.touched));
		return state;
	}
}

export class TypedItemsUtils {
	static changeKeyTypeInArray(array: KeyValue<any>[], controlValueType: ControlValueType) {
		if (controlValueType === "string") {
			return;
		}
		for (var i = 0; i < array.length; i++) {
			array[i].key = parseInt(<string>array[i].key, 10);
		}
	}
}
