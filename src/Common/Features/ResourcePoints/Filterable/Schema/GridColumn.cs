using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Features.ResourcePoints.Filterable.Schema
{
	public class GridColumn
	{
		public string Id { get; set; }
		
		public string Name { get; set; }

		public GridContentOptions Options { get; set; }

		public GridColumn[] Columns { get; set; }
	}
}
