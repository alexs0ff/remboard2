using Common.Features;
using Common.Features.BaseEntity;

namespace Common.FeatureEntities
{
    public class Tenant: BaseEntityGuidKey
    {
        /// <summary>
        /// Задает или получает email регистрации.
        /// </summary>
        public string RegistredEmail { get; set; }

        /// <summary>
        /// Задает или получает код активации.
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// Задает или получает юрназвание фирмы.
        /// </summary>
        public string LegalName { get; set; }

        /// <summary>
        /// Задает или получает торговую марку фирмы.
        /// </summary>
        public string Trademark { get; set; }

        /// <summary>
        /// Задает или получает адрес фирмы.
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Задает или получает логин пользователя создавшего организацию.
        /// </summary>
        public string UserLogin { get; set; }

        /// <summary>
        /// Задает или получает номер арендатора.
        /// </summary>
        public int Number { get; set; }
    }
}
