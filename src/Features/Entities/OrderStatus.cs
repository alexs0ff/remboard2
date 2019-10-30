using System;
using System.Collections.Generic;
using System.Text;

namespace Entities
{
	public class OrderStatus : BaseEntityGuidKey, ITenantedEntity
	{
		public Guid TenantId { get; set; }

		/// <summary>
		/// Задает или получает название.
		/// </summary>
		public string Title { get; set; }

		public OrderStatusKinds OrderStatusKindId { get; set; }

		public OrderStatusKind OrderStatusKind { get; set; }
	}
}
