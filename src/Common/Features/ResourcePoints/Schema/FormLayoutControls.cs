using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Features.ResourcePoints.Schema
{
	public class FormLayoutControls: FormLayoutRowContent
	{
		public FormLayoutControls()
		{
			Kind = FormLayoutRowContentKind.Controls;
		}

		public FormLayoutItem[] Items { get; set; }
	}
}
