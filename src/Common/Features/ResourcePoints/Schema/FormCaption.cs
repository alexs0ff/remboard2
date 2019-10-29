using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Features.ResourcePoints.Schema
{
	public class FormCaption: FormLayoutRowContent
	{
		public FormCaption()
		{
			Kind = FormLayoutRowContentKind.Caption;
		}

		public string Title { get; set; }
	}
}
