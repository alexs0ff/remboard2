using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Features.ResourcePoints.Schema
{
	public class SelectBoxItemsSource<TKey> : SelectBoxSourceBase
	{
		public SelectBoxItemsSourceKind Kind { get; set; }

		public KeyValuePair<TKey,string> Items { get; set; }
	}
}
