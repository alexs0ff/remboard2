import { Component, OnInit,OnDestroy,Input,EventEmitter,Output } from '@angular/core';
import { Location } from '@angular/common';
import { Subject, Observable,of } from "rxjs";
import { takeUntil, map, filter, bufferWhen,shareReplay,withLatestFrom  } from "rxjs/operators";
import { ActivatedRoute, Router } from '@angular/router';
import { EntityServiceFactory, IEntityService, EntityResponse, ValidationError, EntityEditSchemaServiceFactory,
	IEntityEditSchemaService
} from "../../../features/ra-cruds/ra-cruds.module";
import { FormsCompositionService } from "../forms-composition-service";
import { FormGroup } from "@angular/forms";
import { MatDialog, MatDialogRef} from '@angular/material/dialog';
import { RaEntityEditRemoveDialog } from "./ra-entity-edit-remove-dialog";
import { FormErrorService } from "../../ui-common.module";
import { EntityCorrelationIds } from "../../../features/ra-cruds/ra-cruds.models";
import { RaEntityEdit, RaFormLayout, RemoveDialogData } from "../../../ra-schema/ra-schema.module";
import { SchemaFetchEvent, ExtensionParts } from "../forms-composition.models";

const newEntityId:string = 'newEntity';

@Component({
	selector: 'ra-entity-edit',
	templateUrl: './ra-entity-edit.component.html',
	styleUrls: ['./ra-entity-edit.component.scss']
})
export class RaEntityEditComponent implements OnInit, OnDestroy {

	@Input()
	entitiesName: string;

	@Input()
	extensionParts: ExtensionParts;

	@Output()
	schemaFetch = new EventEmitter <SchemaFetchEvent>();

	layouts$:Observable<string[]>;

	layouts: string[];

	form: FormGroup;

	hasServerError$: Observable<boolean>;

	isLoading$: Observable<boolean>;

	serverErrors$: Observable<ValidationError[]>;
	serverMessage$: Observable<string>;

	editModel$: Observable<RaEntityEdit>;

	private lifeTimeObject: Subject<boolean> = new Subject<boolean>();

	private entityService: IEntityService<any>;

	private entityEditSchemaService: IEntityEditSchemaService<any>;

	private isNewEntity: boolean;

	private currentId: string;

	private lastAddedEntityCorrelationId: string;

	constructor(private location: Location,
		private route: ActivatedRoute,
		private router: Router,
		private entityServiceFactory: EntityServiceFactory,
		private entityEditSchemaServiceFactory: EntityEditSchemaServiceFactory,
		private compositionService: FormsCompositionService,
		private dialog: MatDialog) {
		
	}

	ngOnInit() {
		this.entityService = this.entityServiceFactory.getService(this.entitiesName);
		this.entityEditSchemaService = this.entityEditSchemaServiceFactory.getService(this.entitiesName);

		this.layouts$ = this.entityEditSchemaService.layoutIds.pipe(filter(l => l != null));
		this.hasServerError$ = this.entityService.hasError;
		this.isLoading$ = this.entityService.isLoading;
		this.serverErrors$ = this.entityService.errorResponse.pipe(map(r => r.validationErrors));
		this.serverMessage$ = this.entityService.errorResponse.pipe(map(r => r.message));


		//1) watch for route, take a form schema
		this.route.paramMap.pipe(takeUntil(this.lifeTimeObject), map(p => p.get("id"))).subscribe(currentId => {
			if (!currentId) {
				return;
			}

			if (currentId.toLowerCase() === 'new') {
				this.isNewEntity = true;
				this.currentId = newEntityId;
			} else {
				this.currentId = currentId;
				this.isNewEntity = false;
			}
			let event: SchemaFetchEvent = { isNewEntity: this.isNewEntity, customSchema: null };

			this.schemaFetch.emit(event);

			if (event.customSchema && event.customSchema.layouts && event.customSchema.editForm) {
				this.entityEditSchemaService.updateModel(event.customSchema.editForm, event.customSchema.layouts);
			} else {
				if (this.isNewEntity) {
					this.entityEditSchemaService.getCreateFormWithQuery(null);
				} else {
					this.entityEditSchemaService.getEditFormWithQuery(null);
				}
			}
		});


		const editModel$ = this.entityEditSchemaService.editModel.pipe(
			takeUntil(this.lifeTimeObject)
		);

		this.editModel$ = editModel$;
		//2) watch for schema, take an entity
		editModel$.pipe(
			filter(i => i != null))
			.subscribe((model) => {
				this.form = this.compositionService.toFormGroup(model.layouts,this.extensionParts);
				if (this.currentId === newEntityId) {
					const data = this.compositionService.createDefaultObject(model.layouts);
					const entity = { ...data, id: newEntityId };
					this.entityService.directUpdateCurrentEntity(entity);
				} else if (this.currentId){
					this.entityService.getById(this.currentId);
				}
		});


		//3) watch for an entity
		this.entityService.currentEntity.pipe(takeUntil(this.lifeTimeObject), filter(e => e != null)).subscribe((entity) => {
			this.form.setValue(entity);
		});

		this.entityService.lastRemovedIds.pipe(takeUntil(this.lifeTimeObject)).subscribe(removedIds => {
			if (removedIds != null) {
				let res: boolean = false;

				for (var i = 0; i < removedIds.length; i++) {
					if (removedIds[i] === this.currentId) {
						res = true;
						break;
					}
				}

				if (res) {
					this.router.navigate(["../"], { relativeTo: this.route });
				}
			}
		});

		this.entityService.lastAddedIds.pipe(takeUntil(this.lifeTimeObject)).subscribe(addedIds => {

			if (addedIds != null) {
				let res: EntityCorrelationIds = null;

				for (var i = 0; i < addedIds.length; i++) {
					if (addedIds[i].correlationId === this.lastAddedEntityCorrelationId) {
						res = addedIds[i];
						break;
					}
				}

				if (res != null) {
					this.router.navigate(["../", res.entityId], { relativeTo: this.route });
				}
			}
		});

		

	}

	ngOnDestroy(): void {
		this.lifeTimeObject.next(true);
		this.lifeTimeObject.complete();
	}

	goBack() {
		this.router.navigate(["../"], { relativeTo: this.route });
	}

	saveItem() {
		if (!this.form.valid) {
			this.form.markAllAsTouched();
			return;
		}

		if (this.isNewEntity) {
			this.lastAddedEntityCorrelationId = this.entityService.add({ ...this.form.value, id: "newguid" });
		} else {
			this.entityService.update(this.form.value);
		}
	}

	deleteItem(model:RaEntityEdit) {

		let entity: any = this.form.value || {};
		let dialogData: RemoveDialogData = { name: entity[model.removeDialog.valueId], title: model.title };

		const dialogRef = this.dialog.open(RaEntityEditRemoveDialog,
			{
				data: dialogData
			});

		dialogRef.afterClosed().pipe(takeUntil(this.lifeTimeObject)).subscribe((result: boolean) => {
			if (result) {
				this.entityService.delete(entity.id);
			}
		});
	}
}
