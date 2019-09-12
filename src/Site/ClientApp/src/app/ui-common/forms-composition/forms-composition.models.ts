export enum RaControlTypes {
  Textbox = "TEXTBOX",
  Selectbox = "SELECTBOX"
}

type ControlKind = 'textbox' | 'selectbox';
type ControlValueType = 'number' | 'string';

export interface RaValidators {
  required: boolean;
  maxLength?: number | null;
  minLength?: number | null;
}

export interface RaControl {
  kind: ControlKind;
  valueKind: ControlValueType;
  id: string;
  label: string;
  validators: RaValidators;
  value?: any | null;
  hint?: string |null;
}

interface RaFormLayoutItemBase {
  kind: 'controls' | 'devider' |'caption';
}

export interface RaFormItemFlexExpression {
  fxFlexCommonExpression: string;
  fxFlexLtmdExpression: string;
  fxFlexLtsmExpression: string;
}

export interface RaFormLayoutItem {
  flexExpression: RaFormItemFlexExpression;
  control: RaControl;
}

export const flexExpressions = {
  oneItemExpressions: {
    fxFlexCommonExpression: "100%",
    fxFlexLtmdExpression: "100%",
    fxFlexLtsmExpression:"100%"
  },
  twoItemsExpressions: {
    fxFlexCommonExpression: "0 1 calc(50% - 32px)",
    fxFlexLtmdExpression: "0 1 calc(50% - 32px)",
    fxFlexLtsmExpression: "100%"
  },
  threeItemsExpressions: {
    fxFlexCommonExpression: "0 1 calc(33.3% - 32px)",
    fxFlexLtmdExpression: "0 1 calc(50% - 32px)",
    fxFlexLtsmExpression: "100%"
  }
}

export interface RaFormLayoutItems  {
  kind: 'controls',
  items: RaFormLayoutItem[];
}

export interface RaFormDevider {
  kind:'devider'
}

export interface RaFormCaption {
  kind: 'caption',
  title:string;
}
export type RaFormLayoutRowContent = RaFormLayoutItems | RaFormDevider | RaFormCaption;



export interface RaFormLayoutRow {
  content: RaFormLayoutRowContent;
}

export interface RaFormLayout {
  rows: RaFormLayoutRow[];
}
