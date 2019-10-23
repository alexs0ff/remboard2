using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace Common.Features.ResourcePoints.Schema
{
	public class SelectBoxItemsSource<TKey> : SelectBoxSourceBase
	{
		[JsonConverter(typeof(StringEnumConverter), typeof(CamelCaseNamingStrategy))]
		public SelectBoxItemsSourceKind Kind { get; set; }

		public KeyValuePair<TKey,string>[] Items { get; set; }
	}
}
