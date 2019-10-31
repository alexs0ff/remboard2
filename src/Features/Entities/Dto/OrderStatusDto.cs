using System;
using System.Collections.Generic;
using System.Text;
using Entities;
using Entities.Dto.Infrastructure;
using Newtonsoft.Json;

namespace Entities.Dto
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
