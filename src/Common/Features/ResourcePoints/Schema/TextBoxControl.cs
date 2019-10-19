using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Features.ResourcePoints.Schema
{
	public class TextBoxControl
	{
		public TextBoxControlKind Kind { get; set; }

		public ControlValueKind ValueKind { get; set; }
		public string Id { get; set; }

		public string Label { get; set; }

		public TextBoxControlValidators Validators { get; set; }

		public object Value { get; set; }

		public string Hint { get; set; }
	}
}
