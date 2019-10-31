using System;
using System.Collections.Generic;
using System.Text;
using Entities.Dto.Infrastructure;
using Newtonsoft.Json;

namespace Entities.Dto
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
