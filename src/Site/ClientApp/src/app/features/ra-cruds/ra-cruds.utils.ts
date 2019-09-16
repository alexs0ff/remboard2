import { EntityResponse, QueryParams } from "./ra-cruds.models"
import {
  HttpParams,
} from '@angular/common/http';


export class RaUtils {
  static parseHttpError(error): EntityResponse {
    console.log("http error:",error);
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

  setSort(column: string, isAscending: boolean = true): QueryParamsConfigurator {
    this.parameters["orderBy"] = column;
    this.parameters["orderKind"] = isAscending?"asc":"desc";
    return this;
  }

  andContains(column: string, value: string): QueryParamsConfigurator {
    this.addFilter(column, value, "contains", "and");
    return this;
  }

  orContains(column: string, value: string): QueryParamsConfigurator {
    this.addFilter(column, value, "contains", "or");
    return this;
  }

  andEquals(column: string, value: string): QueryParamsConfigurator {
    this.addFilter(column, value, "equals", "and");
    return this;
  }

  orEquals(column: string, value: string): QueryParamsConfigurator {
    this.addFilter(column, value, "equals", "or");
    return this;
  }

  private addFilter(column: string, columnValue: string, operator: string, logicalOperation: string) {
    this.parameters["filterColumnName"] = column;
    this.parameters["filterColumn"+column+"Value"] = columnValue;
    this.parameters["filterColumn" + column + "Operator"] = operator;
    this.parameters["filterColumn" + column + "Logic"] = logicalOperation;

  }

  toQueryParams(): QueryParams {
    return this.parameters;
  }
}
