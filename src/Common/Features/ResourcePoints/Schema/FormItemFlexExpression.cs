using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Features.ResourcePoints.Schema
{
	public class FormItemFlexExpression
	{
		public string FxFlexCommonExpression { get; set; }

		public string FxFlexLtmdExpression { get; set; }

		public string FxFlexLtsmExpression { get; set; }

		public static readonly FormItemFlexExpression OneItemExpressions = new FormItemFlexExpression
		{
			FxFlexCommonExpression = "100%",
			FxFlexLtmdExpression = "100%",
			FxFlexLtsmExpression = "100%"
		};

		public static readonly FormItemFlexExpression TwoItemsExpressions = new FormItemFlexExpression
		{
			FxFlexCommonExpression = "0 1 47%",
			FxFlexLtmdExpression = "0 1 47%",
			FxFlexLtsmExpression = "100%"
		};

		public static readonly FormItemFlexExpression ThreeItemsExpressions = new FormItemFlexExpression
		{
			FxFlexCommonExpression = "0 1 31%",
			FxFlexLtmdExpression = "0 1 47%",
			FxFlexLtsmExpression = "100%"
		};
	}
}
