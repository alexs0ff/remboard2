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

export interface RaFormItemFlexExpression {
  fxFlexCommonExpression: string;
  fxFlexLtmdExpression: string;
  fxFlexLtsmExpression: string;
}

export interface RaFormLayoutItem {
  flexExpression: RaFormItemFlexExpression;
  control: RaControl;
}

export interface RaFormLayoutHiddenItems {
  kind: 'hidden',
  items: string[];
}

export interface RaFormLayoutItems  {
  kind: 'controls',
  items: RaFormLayoutItem[];
}

export interface RaFormDivider {
  kind:'divider'
}

export interface RaFormCaption {
  kind: 'caption',
  title:string;
}
export type RaFormLayoutRowContent = RaFormLayoutItems | RaFormDivider | RaFormCaption | RaFormLayoutHiddenItems;



export interface RaFormLayoutRow {
  content: RaFormLayoutRowContent;
}

export interface RaFormLayout {
  rows: RaFormLayoutRow[];
}

export interface RaEntityEdit {
  entitiesName:string;
  title: string;
  layout: RaFormLayout;
}


export const flexExpressions = {
  oneItemExpressions: {
    fxFlexCommonExpression: "100%",
    fxFlexLtmdExpression: "100%",
    fxFlexLtsmExpression: "100%"
  },
  twoItemsExpressions: {
    fxFlexCommonExpression: "0 1 47%",
    fxFlexLtmdExpression: "0 1 47%",
    fxFlexLtsmExpression: "100%"
  },
  threeItemsExpressions: {
    fxFlexCommonExpression: "0 1 31%",
    fxFlexLtmdExpression: "0 1 47%",
    fxFlexLtsmExpression: "100%"
  }
}
