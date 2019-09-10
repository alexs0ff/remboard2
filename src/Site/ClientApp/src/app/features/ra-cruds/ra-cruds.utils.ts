import { EntityResponse, QueryParams } from "./ra-cruds.models"
import {
  HttpParams,
} from '@angular/common/http';


export class RaUtils {
  static parseHttpError(error): EntityResponse {

    return { validationErrors: [], message: "untyped message" };
  }

  static toHttpParams(queryParams: QueryParams): HttpParams {
    const qParams =
      typeof queryParams === 'string'
        ? { fromString: queryParams }
        : { fromObject: queryParams };
    return new HttpParams(qParams);
  }
}

export class QueryParamsConfigurator {
  private parameters = {};

  setPageSize(size: number): QueryParamsConfigurator {
    this.parameters["pageSize"] = size;
    return this;
  }

  setCurrentPage(page: number): QueryParamsConfigurator {
    this.parameters["page"] = page;
    return this;
  }

  toQueryParams(): QueryParams {
    return this.parameters;
  }
}
