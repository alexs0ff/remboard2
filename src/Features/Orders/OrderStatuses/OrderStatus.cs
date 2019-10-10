using System;
using System.Collections.Generic;
using System.Text;
using Common.Features.BaseEntity;
using Common.Features.Tenant;

namespace Orders.OrderStatuses
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
