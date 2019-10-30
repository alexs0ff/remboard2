using System;
using System.Collections.Generic;
using System.Text;

namespace Entities
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
