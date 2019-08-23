using System;
using System.Collections.Generic;
using System.Text;
using Common.Features;

namespace Users
{
    public class User: BaseEntityGuidKey, ITenantedEntity
    {
        public Guid TenantId { get; set; }

        /// <summary>
        /// Задает или получает роль в проекте.
        /// </summary>
        public ProjectRoles ProjectRoleId { get; set; }

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
    }
}
