using System;
using System.Collections.Generic;
using System.Text;
using Common.Features.TypeConverters;
using Newtonsoft.Json;

namespace Orders.OrderTypes
{
	public class OrderTypeDto
	{
		[JsonConverter(typeof(ServerGeneratedGuidConverter))]
		public Guid Id { get; set; }

		/// <summary>
		/// Задает или получает название.
		/// </summary>
		public string Title { get; set; }
	}
}
