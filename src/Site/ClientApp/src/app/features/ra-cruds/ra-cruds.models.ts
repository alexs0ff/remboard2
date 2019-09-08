import { EntityState, EntityAdapter } from '@ngrx/entity';
import { Action,ActionReducer} from '@ngrx/store';

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

}

export interface ICrudEntityConfigurator<T extends IEntityBase> {
  entityReducer: ActionReducer<EntityState<T>, Action>;
  configure(entityName: string);
}


export interface CrudsEntityMetadata {
  [p: string]: ICrudEntityConfigurator<any>
}
