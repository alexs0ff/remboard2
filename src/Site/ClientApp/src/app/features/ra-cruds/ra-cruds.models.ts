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

export interface EntityCorrelationIds {
	entityId: string;
  correlationId:string;
}

export interface IState<T extends IEntityBase> extends EntityState<T> {
  selectedId: string | null;
  totalCount: number;
  loading: boolean;
  error: EntityResponse | null;
  hasError: boolean;
	lastRemovedIds: string[] | null;
	lastAddedIds: EntityCorrelationIds[] |null;
}

export interface CrudAdapter<T extends IEntityBase> extends EntityAdapter<T> {

}


export interface QueryParams {
  [name: string]: string | string[];
}

export interface IEntityService<T extends IEntityBase> {
  entities: Observable<T[]>;
  totalLength: Observable<number>;
  currentEntity: Observable<T>;
  isLoading: Observable<boolean>;
  hasError: Observable<boolean>;
  errorResponse: Observable<EntityResponse>;
	lastRemovedIds: Observable<string[] | null>;
	lastAddedIds: Observable<EntityCorrelationIds[] | null>;

  getAll();
  getById(id: string);
  getWithQuery(queryParams: QueryParams);
  addMany(entities: T[]);
  add(entity: T):string;
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

export interface EntityFilter {
  currentPage: number;
  pageSize:number;
}
