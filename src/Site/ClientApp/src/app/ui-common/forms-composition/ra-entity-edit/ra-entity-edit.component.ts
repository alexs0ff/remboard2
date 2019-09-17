import { Component, OnInit,OnDestroy,Input } from '@angular/core';
import { Location } from '@angular/common';
import { Subject, Observable } from "rxjs";
import { takeUntil, map } from "rxjs/operators";
import { ActivatedRoute } from '@angular/router';
import { RaEntityEdit, RaFormLayout } from "../forms-composition.models";
import { EntityServiceFabric, IEntityService, EntityResponse, ValidationError } from "../../../features/ra-cruds/ra-cruds.module";
import { FormsCompositionService } from "../forms-composition-service";
import { FormGroup } from "@angular/forms";

@Component({
  selector: 'ra-entity-edit',
  templateUrl: './ra-entity-edit.component.html',
  styleUrls: ['./ra-entity-edit.component.scss']
})
export class RaEntityEditComponent implements OnInit, OnDestroy {

  @Input() model: RaEntityEdit;

  layout: RaFormLayout;

  form: FormGroup;

  hasServerError: Observable<boolean>;

  serverErrors: Observable<ValidationError[]>;
  serverMessage: Observable<string>;

  private lifeTimeObject: Subject<boolean> = new Subject<boolean>();

  private entityService: IEntityService<any>;

  isNewEntity:boolean;

  constructor(private location: Location, private route: ActivatedRoute, private entityServiceFabric: EntityServiceFabric, private compositionService: FormsCompositionService) {
  }

  ngOnInit() {
    this.entityService = this.entityServiceFabric.getService(this.model.entitiesName);

    this.layout = this.model.layout;
    this.form = this.compositionService.toFormGroup(this.layout);
    this.hasServerError = this.entityService.hasError;
    this.serverErrors = this.entityService.errorResponse.pipe(map(r=>r.validationErrors));
    this.serverMessage = this.entityService.errorResponse.pipe(map(r=>r.message));

    this.entityService.currentEntity.pipe(takeUntil(this.lifeTimeObject)).subscribe(entity => {
      if (entity) {
        this.form.setValue(entity);
      }
    });

    this.route.paramMap.pipe(takeUntil(this.lifeTimeObject), map(p => p.get("id"))).subscribe(currentId => {
      if (!currentId) {
        return;
      }
      if (currentId.toLowerCase() === 'new') {
        this.isNewEntity = true;
      } else {
        this.entityService.getById(currentId);
        this.isNewEntity = false;
      }
    });

  }

  ngOnDestroy(): void {
    this.lifeTimeObject.next(true);
    this.lifeTimeObject.complete();
  }

  goBack() {
    this.location.back();
  }

  saveItem() {
    console.log("saved entity",this.form.value);
    console.log("errors entity", this.form.controls);

    if (!this.form.valid) {
      return;
    }

    if (this.isNewEntity) {
      this.entityService.add({ ...this.form.value, id: "newguid" });
    } else {
      this.entityService.update(this.form.value);
    }
  }

  deleteItem() {
    
  }
}
