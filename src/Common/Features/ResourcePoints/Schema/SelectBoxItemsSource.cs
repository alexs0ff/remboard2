using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Common.Features.ResourcePoints.Schema
{
	public class SelectBoxItemsSource<TKey> : SelectBoxSourceBase
	{
		public SelectBoxItemsSourceKind Kind { get; set; }

		public KeyValuePair<TKey,string>[] Items { get; set; }
	}
}
