import { Component, OnInit, Input, OnDestroy } from '@angular/core';
import { FormGroup, FormControl } from "@angular/forms";
import { RaControls, RaMaskBox } from "../../../ra-schema/ra-schema.module";
import { FormErrorService } from "../form-error-service";
import { HiddenErrorStateMatcher } from "../forms-composition-service";
import { ControlMasks } from "../forms-composition.models";
import { Subject } from "rxjs";
import { takeUntil} from "rxjs/operators";
import { conformToMask } from 'angular2-text-mask';


@Component({
	selector: 'ra-maskbox',
	template: `
<div [formGroup]="form">
  <mat-form-field class="ra-mat-field" [floatLabel]="model.floatLabel">
	<input #hiddenValueContainer matInput class="input-invisible" name="{{model.id}}" [formControlName]="model.id" [errorStateMatcher]="matcher"/>
	<mat-label>{{model.label}}</mat-label>
    <mat-hint>{{model.hint}}</mat-hint>
	<input matInput [textMask]="{mask: currentMask,guide:guide,keepCharPositions:keepCharPositions,showMask:showMask}" [placeholder]="model.label"
	[formControl]="maskBoxCtrl"/>
    <mat-error  *ngIf="form.controls[model.id].invalid">
      {{formErrorService.getErrorMessage(form,model.id)}}
    </mat-error>
  </mat-form-field>
</div>
  `,
	styles: []
})
export class RaMaskboxComponent implements OnInit, OnDestroy {
	//@Input()
	//model: RaControls;
	

	@Input()
	form: FormGroup;

	@Input()
	model: RaMaskBox;

	@Input()
	customMasks: ControlMasks;

	currentMask:any;

	matcher: HiddenErrorStateMatcher;

	maskBoxCtrl = new FormControl();

	guide: boolean;
	keepCharPositions: boolean;
	showMask:boolean;


	private lifeTimeObject: Subject<boolean> = new Subject<boolean>();

	constructor(public formErrorService: FormErrorService) {
		this.guide = true;
		this.keepCharPositions = false;
		this.showMask = true;
	}

	ngOnInit() {
		this.maskBoxCtrl.valueChanges.pipe(takeUntil(this.lifeTimeObject)).subscribe(value => {
			this.onMaskBoxChanged(value);
		});

		this.form.controls[this.model.id].valueChanges.pipe(takeUntil(this.lifeTimeObject)).subscribe(value => {
			this.onModelValueChanged(value);
		});

		this.matcher = new HiddenErrorStateMatcher(this.maskBoxCtrl);

		this.currentMask = this.getMask();

		if (this.model.textMask.keepCharPositions!=undefined) {
			this.keepCharPositions = this.model.textMask.keepCharPositions;
		}

		if (this.model.textMask.guide != undefined) {
			this.guide = this.model.textMask.guide;
		}

		if (this.model.textMask.showMask != undefined) {
			this.showMask = this.model.textMask.showMask;
		}
	}

	private getMask(): any {
		if (this.model.textMask.maskId) {
			if (!this.customMasks || !this.customMasks.masks) {
				throw new Error("the instance of custom masks for control ''" + this.model.id + "'' should be defined");
			}

			const mask = this.customMasks.masks[this.model.textMask.maskId];

			if (!mask) {
				throw new Error("the custom mask ''" + this.model.textMask.maskId + "'' for control ''" + this.model.id + "'' should be defined");
			}

			return mask;
		}

		return this.model.textMask.mask;
	}

	private onMaskBoxChanged(value: string) {
		const path = {};
		path[this.model.id] = value;
		this.form.patchValue(path);
	}

	private onModelValueChanged(modelValue: string) {
		const currentMaskedValue = this.maskBoxCtrl.value;
		
		let conformedValue:any = modelValue;

		if (this.model.textMask.conformToMask) {

			let mask = this.getMask();
			if (mask && typeof mask === "function") {

				mask = mask(modelValue);
				mask = mask.filter((val) => val != '[]');
			}
			const conformedResult = conformToMask(
				modelValue,
				mask,
				{ guide: false }
			);

			conformedValue = conformedResult.conformedValue;
		}

		console.log("set new value", conformedValue);
		console.log("set current value", currentMaskedValue);

		if (currentMaskedValue!=conformedValue) {
			this.maskBoxCtrl.setValue(conformedValue);
		}
	}

	ngOnDestroy(): void {
		this.lifeTimeObject.next(true);
		this.lifeTimeObject.complete();
	}
}
