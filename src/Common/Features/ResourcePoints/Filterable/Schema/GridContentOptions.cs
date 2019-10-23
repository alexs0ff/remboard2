using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Common.Features.ResourcePoints.Filterable.Schema
{
	public class GridContentOptions
	{
		public GridColumnContentKind ValueKind { get; set; }

		public bool CanOrder { get; set; }
	}
}
