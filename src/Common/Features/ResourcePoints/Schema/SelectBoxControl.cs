using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Features.ResourcePoints.Schema
{
	public class SelectBoxControl: ControlBase
	{
		public SelectBoxControlKind Kind { get; set; }

		public ControlValueKind ValueKind { get; set; }

		public string Id { get; set; }

		public string Label { get; set; }

		public SelectBoxControlValidators Validators { get; set; }

		public SelectBoxSourceBase Source { get; set; }

		public object Value { get; set; }

		public string Hint { get; set; }
	}
}
