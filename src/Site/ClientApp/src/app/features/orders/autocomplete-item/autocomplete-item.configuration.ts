import { Injectable } from '@angular/core';
import { EntityCollectionServiceBase, EntityMetadataMap, EntityCollectionServiceElementsFactory } from '@ngrx/data';
import { HttpClient } from '@angular/common/http';
import { AutocompleteItem } from "./autocomplete-item.models";


import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

import {
  EntityCollectionDataService,
  DefaultDataService,
  HttpUrlGenerator,
  Logger,
  QueryParams
} from '@ngrx/data';


export const autocompleteItemMetadata: EntityMetadataMap = {
  Autocompleteitem: {
    entityName: "autocompleteitem",
    entityDispatcherOptions: { optimisticAdd: false, optimisticUpdate: false,optimisticDelete:false },
    selectId: (item: AutocompleteItem) => item.id,
    additionalCollectionState: {
      total: 0,
      currentPage: 1,
      pageSize:10
    }
  }
}

@Injectable()
export class AutocompleteItemService extends EntityCollectionServiceBase<AutocompleteItem>{
  constructor(serviceElementsFactory: EntityCollectionServiceElementsFactory) {
    super('autocompleteitem', serviceElementsFactory);
  }

}



@Injectable()
export class AutocompleteItemDataService extends DefaultDataService<AutocompleteItem> {
  constructor(http: HttpClient, httpUrlGenerator: HttpUrlGenerator, logger: Logger) {
    super('autocompleteitem', http, httpUrlGenerator);
  }

  getAll(): Observable<AutocompleteItem[]> {
    return super.getAll().pipe(map(pagedData => this.mapPagedResult(pagedData)));
  }

  getById(id: string | number): Observable<AutocompleteItem> {
    return super.getById(id);
  }

  getWithQuery(params: string | QueryParams): Observable<AutocompleteItem[]> {
    return super.getWithQuery(params).pipe(map(pagedData => this.mapPagedResult(pagedData)));
  }

  private mapPagedResult(pagedResult: any): AutocompleteItem[] {
    return pagedResult.entities;
  }
}
