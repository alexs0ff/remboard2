import { Component, OnInit,OnDestroy,Input } from '@angular/core';
import { Location } from '@angular/common';
import { Subject, Observable } from "rxjs";
import { takeUntil, map } from "rxjs/operators";
import { ActivatedRoute } from '@angular/router';
import { RaEntityEdit, RaFormLayout } from "../forms-composition.models";
import { EntityServiceFabric, IEntityService } from "../../../features/ra-cruds/ra-cruds.module";
import { FormsCompositionService } from "../forms-composition-service";
import { FormGroup } from "@angular/forms";

@Component({
  selector: 'ra-entity-edit',
  templateUrl: './ra-entity-edit.component.html',
  styleUrls: ['./ra-entity-edit.component.scss']
})
export class RaEntityEditComponent implements OnInit, OnDestroy {

  private lifeTimeObject: Subject<boolean> = new Subject<boolean>();

  @Input() model: RaEntityEdit;

  layout: RaFormLayout;

  form: FormGroup;

  private entityService: IEntityService<any>;
  constructor(private location: Location, private route: ActivatedRoute, private entityServiceFabric: EntityServiceFabric, private compositionService: FormsCompositionService) {
  }

  ngOnInit() {
    this.entityService = this.entityServiceFabric.getService(this.model.entitiesName);

    this.layout = this.model.layout;
    this.form = this.compositionService.toFormGroup(this.layout);

    this.entityService.currentEntity.pipe(takeUntil(this.lifeTimeObject)).subscribe(entity => {
      if (entity) {
        this.form.setValue(entity);
      }
    });

    this.route.paramMap.pipe(takeUntil(this.lifeTimeObject), map(p => p.get("id"))).subscribe(currentId => {
      this.entityService.getById(currentId);
    });

  }

  ngOnDestroy(): void {

  }

  goBack() {
    this.location.back();
  }
  saveItem() {
    console.log("saved entity",this.form.value);
    console.log("errors entity",this.form.controls);
  }

  deleteItem() {
    this.lifeTimeObject.next(true);
    this.lifeTimeObject.complete();
  }
}
