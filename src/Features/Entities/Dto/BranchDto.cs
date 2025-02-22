﻿using System;
using System.Collections.Generic;
using System.Text;
using Entities.Dto.Infrastructure;
using Newtonsoft.Json;

namespace Entities.Dto
{
	public class BranchDto
	{
		[JsonConverter(typeof(ServerGeneratedGuidConverter))]
		public Guid Id { get; set; }

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
