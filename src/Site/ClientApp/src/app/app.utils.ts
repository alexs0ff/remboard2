import { Dictionary, KeyValue  } from "./app.models";

export class DictionaryUtils {
  static toArray(dict: Dictionary<any>): KeyValue<any>[] {
    const result = new Array<KeyValue<any>>();
    for (var rawKey in dict) {
      result.push({ key: rawKey, value: dict[rawKey]});
    }
    return result;
  }
}
