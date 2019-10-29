using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Features.ResourcePoints.Schema
{
	public class FormLayoutHiddenItems: FormLayoutRowContent
	{
		public FormLayoutHiddenItems()
		{
			Kind = FormLayoutRowContentKind.Hidden;
		}

		public string[] Items { get; set; }
	}
}
