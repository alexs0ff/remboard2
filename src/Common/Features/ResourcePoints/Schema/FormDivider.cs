using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Features.ResourcePoints.Schema
{
	public class FormDivider: FormLayoutRowContent
	{
		public FormDivider()
		{
			Kind = FormLayoutRowContentKind.Divider;
		}
	}
}
