using System;
using System.Collections.Generic;
using System.Text;
using JetBrains.Annotations;

namespace Common.Features.ResourcePoints.Filterable.Schema
{
	public class ServerDataGridModel
	{
		public string EntitiesName { get; set; }

		public GridColumn[] Columns { get; set; }

		public int? PageSize { get; set; }

		public GridControlPanel Panel { get; set; }

		public GridFilter Filter { get; set; }

	}
}
