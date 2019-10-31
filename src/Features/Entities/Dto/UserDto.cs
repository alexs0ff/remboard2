using System;
using System.Collections.Generic;
using System.Text;
using Entities.Dto.Infrastructure;
using Newtonsoft.Json;

namespace Entities.Dto
{
	public class UserDto
	{
		[JsonConverter(typeof(ServerGeneratedGuidConverter))]
		public Guid Id { get; set; }

		/// <summary>
		/// Задает или получает роль в проекте.
		/// </summary>
		public ProjectRoles ProjectRoleId { get; set; }

		public string ProjectRoleTitle { get; set; }

		/// <summary>
		/// Задает или получает логин пользователя.
		/// </summary>
		public string LoginName { get; set; }

		/// <summary>
		/// Задает или получает имя пользователя.
		/// </summary>
		public string FirstName { get; set; }

		/// <summary>
		/// Задает или получает Фамилию пользователя.
		/// </summary>
		public string LastName { get; set; }

		/// <summary>
		/// Задает или получает отчетство пользователя.
		/// </summary>
		public string MiddleName { get; set; }

		/// <summary>
		/// Задает или получает телефон пользователя.
		/// </summary>
		public string Phone { get; set; }

		/// <summary>
		/// Задает или получает Email пользователя.
		/// </summary>
		public string Email { get; set; }

		/// <summary>
		/// Задает или получает филиалы пользователя.
		/// </summary>
		public List<UserBranchDto> Branches { get; set; }
	}
}
