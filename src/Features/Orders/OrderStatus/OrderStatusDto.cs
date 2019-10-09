﻿using System;
using System.Collections.Generic;
using System.Text;
using Common.Features.TypeConverters;
using Newtonsoft.Json;

namespace Orders.OrderStatus
{
	public class OrderStatusDto
	{
		[JsonConverter(typeof(ServerGeneratedGuidConverter))]
		public Guid Id { get; set; }


		/// <summary>
		/// Задает или получает название.
		/// </summary>
		public string Title { get; set; }

		public OrderStatusKinds OrderStatusKindId { get; set; }

		public string OrderStatusKindTitle { get; set; }
	}
}
