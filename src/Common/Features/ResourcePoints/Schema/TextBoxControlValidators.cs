using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Features.ResourcePoints.Schema
{
	public class TextBoxControlValidators
	{
		public bool Required { get; set; }

		public int? MaxLength { get; set; }

		public int? MinLength { get; set; }
	}
}
