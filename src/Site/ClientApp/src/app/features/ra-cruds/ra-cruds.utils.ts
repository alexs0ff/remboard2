import { EntityResponse } from "./ra-cruds.models"

export class RaUtils {
  static parseHttpError(error): EntityResponse {

    return { validationErrors: [], message: "untyped message" };
  }
}
