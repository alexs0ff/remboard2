using System;
using System.Collections.Generic;
using System.Text;
using Common.Features.BaseEntity;
using Common.Features.Tenant;

namespace Orders.OrderTypes
{
	public class OrderType : BaseEntityGuidKey, ITenantedEntity
	{
		public Guid TenantId { get; set; }

		/// <summary>
		/// Задает или получает название.
		/// </summary>
		public string Title { get; set; }
	}
}
