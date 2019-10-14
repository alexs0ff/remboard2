import { EntityResponse, QueryParams } from "./ra-cruds.models"
import {
  HttpParams,
} from '@angular/common/http';


export class RaUtils {
  static parseHttpError(error): EntityResponse {
    console.log("http error:", error);
    const entityError: EntityResponse = { validationErrors: [], message: error.message };

    if (error.error) {
      if (error.error.message) {
        entityError.message = error.error.message;
      }

      if (error.error.validationErrors && error.error.validationErrors.length) {
        error.error.validationErrors.forEach(verror => {
          entityError.validationErrors.push({ message: verror.message, property: verror.property});
        });
      }
    }

    return entityError;
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
	private parameters = { "filterColumnName":[]};

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
		this.parameters["orderKind"] = isAscending ? "asc" : "desc";
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

	addFilter(column: string, columnValue: string | number, operator: string, logicalOperation: string) {
		this.parameters["filterColumnName"].push(column);

		if (this.parameters["filterColumn" + column + "Value"]) {
			this.parameters["filterColumn" + column + "Value"].push(columnValue);
			this.parameters["filterColumn" + column + "Operator"].push(operator);
			this.parameters["filterColumn" + column + "Logic"].push(logicalOperation);	
		} else {
			this.parameters["filterColumn" + column + "Value"] = [columnValue];
			this.parameters["filterColumn" + column + "Operator"] = [operator];
			this.parameters["filterColumn" + column + "Logic"] = [logicalOperation];	
		}
		

	}

	toQueryParams(): QueryParams {
		return this.parameters;
	}
}
