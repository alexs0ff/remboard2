using System;
using System.Collections.Generic;
using System.Text;
using Common.Features.BaseEntity;
using Common.Features.Tenant;

namespace Orders.Branches
{
	public class Branch : BaseEntityGuidKey, ITenantedEntity
	{
		public Guid TenantId { get; set; }


		/// <summary>
		/// Задает или получает название.
		/// </summary>
		public string Title { get; set; }

		/// <summary>
		/// Задает или получает адрес филиала.
		/// </summary>
		public string Address { get; set; }

		/// <summary>
		/// Задает или получает юр название филиала.
		/// </summary>
		public string LegalName { get; set; }

	}
}
