import { EntityState, EntityAdapter} from '@ngrx/entity';
import { Action, ActionReducer } from '@ngrx/store';
import { Observable } from 'rxjs';


export interface ValidationError {
  property: string;
  message: string;
}

export interface EntityResponse {
  validationErrors: ValidationError[];
  message: string;
}


export interface IEntityBase {
  id: string;
}

export interface IState<T extends IEntityBase> extends EntityState<T> {
  selectedId: number | null;
  totalCount: number;
  loading: boolean;
  error: EntityResponse | null;
  hasError:boolean;
}

export interface CrudAdapter<T extends IEntityBase> extends EntityAdapter<T> {

}


export interface IEntityService<T extends IEntityBase> {
  entities: Observable<T[]>;
  getAll();
  addMany(entities: T[]);
  add(entity: T);
  update(entity: T);
  delete(id: string);
}

export interface ICrudEntityConfigurator<T extends IEntityBase> {
  entityReducer: ActionReducer<EntityState<T>, Action>;
  configure(featureName: string, entitiesName: string);
  singleEntityName: string;
}


export interface CrudsEntityMetadata {
  [p: string]: ICrudEntityConfigurator<any>;
}


export interface PagedResult {
  count: number;
  entities:any[];
}
