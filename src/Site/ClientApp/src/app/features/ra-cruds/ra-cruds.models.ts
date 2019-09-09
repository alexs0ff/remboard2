import { EntityState, EntityAdapter} from '@ngrx/entity';
import { Action, ActionReducer } from '@ngrx/store';
import { Observable } from 'rxjs';

export interface IEntityBase {
  id: string;
}

export interface IState<T extends IEntityBase> extends EntityState<T> {
  selectedId: number | null,
  totalCount: number,
}

export interface CrudAdapter<T extends IEntityBase> extends EntityAdapter<T> {

}


export interface IEntityService<T extends IEntityBase> {
  entities: Observable<T[]>;
  getAll();
  addMany(entities: T[]);
}

export interface ICrudEntityConfigurator<T extends IEntityBase> {
  entityReducer: ActionReducer<EntityState<T>, Action>;
  configure(featureName:string,entityName: string);
}


export interface CrudsEntityMetadata {
  [p: string]: ICrudEntityConfigurator<any>
}
