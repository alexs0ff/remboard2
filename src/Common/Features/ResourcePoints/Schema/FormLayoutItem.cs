using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Features.ResourcePoints.Schema
{
	public class FormLayoutItem
	{
		public FormItemFlexExpression FlexExpression { get; set; }

		public ControlBase Control { get; set; }
	}
}
