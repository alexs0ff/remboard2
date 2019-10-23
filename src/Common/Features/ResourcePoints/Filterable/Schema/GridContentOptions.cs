using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace Common.Features.ResourcePoints.Filterable.Schema
{
	public class GridContentOptions
	{
		[JsonConverter(typeof(StringEnumConverter), typeof(CamelCaseNamingStrategy))]
		public GridColumnContentKind ValueKind { get; set; }

		public bool CanOrder { get; set; }
	}
}
