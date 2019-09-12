import { Component, OnInit,OnDestroy,Input } from '@angular/core';
import { Location } from '@angular/common';
import { Subject, Observable } from "rxjs";
import { takeUntil, map } from "rxjs/operators";
import { ActivatedRoute } from '@angular/router';
import { RaEntityEdit, RaFormLayout } from "../forms-composition.models";
import { EntityServiceFabric, IEntityService } from "../../../features/ra-cruds/ra-cruds.module";

@Component({
  selector: 'ra-entity-edit',
  templateUrl: './ra-entity-edit.component.html',
  styleUrls: ['./ra-entity-edit.component.scss']
})
export class RaEntityEditComponent implements OnInit, OnDestroy {

  private lifeTimeObject: Subject<boolean> = new Subject<boolean>();

  @Input() model: RaEntityEdit;
  layout: RaFormLayout;

  private entityService: IEntityService<any>;
  constructor(private location: Location, private route: ActivatedRoute, private entityServiceFabric: EntityServiceFabric) {
  }

  ngOnInit() {
    this.entityService = this.entityServiceFabric.getService(this.model.entitiesName);

    this.route.paramMap.pipe(takeUntil(this.lifeTimeObject), map(p => p.get("id"))).subscribe(currentId => {
      this.entityService.getById(currentId);
    });
    this.layout = this.model.layout;
  }

  ngOnDestroy(): void {

  }

  goBack() {
    this.location.back();
  }
  saveItem() {

  }

  deleteItem() {
    this.lifeTimeObject.next(true);
    this.lifeTimeObject.complete();
  }
}
