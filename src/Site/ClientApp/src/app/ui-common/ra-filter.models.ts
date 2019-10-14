export enum LogicalOperators {
	None = "",
	And = "and",
	Or = "or"
}

export enum ComparisonOperators {
	None = "",
	Equals = "equals",
	Contains = "contains",
	LessThan = "lessthan",
	GreaterThan = "greaterthan"
}

export interface FilterStatement {
	field: string;
	comparison: ComparisonOperators;
	logicalOperator: LogicalOperators;
	value:number|string;
}

export interface FilterData {
	statements: FilterStatement[]
}
