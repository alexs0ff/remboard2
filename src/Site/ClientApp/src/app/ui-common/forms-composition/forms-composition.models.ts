export enum RaControlTypes {
  Textbox = "TEXTBOX",
  Selectbox = "SELECTBOX"
}




export abstract class RaBaseControl {
  controlType: RaControlTypes;
  value: string;
  key: string;
  label: string;
  hint:string;
  required: boolean;
  order: number;
  column: number;

  constructor(options: {
    value?: string,
    key?: string,
    label?: string,
    hint?: string,
    required?: boolean,
    order?: number,
    column?: number,

  } = {}) {

    this.value = options.value;
    this.key = options.key || '';
    this.label = options.label || '';
    this.hint = options.hint || '';
    this.required = options.required;
    this.order = options.order;
    this.column = options.column;
  }

}

export class RaTextbox extends RaBaseControl {
  controlType: RaControlTypes.Textbox;
  constructor(options: {} = {}) {
    super(options);
  }
}


export class RaSelectbox extends RaBaseControl {
  controlType: RaControlTypes.Selectbox;
  constructor(options: {} = {}) {
    super(options);
  }
}
