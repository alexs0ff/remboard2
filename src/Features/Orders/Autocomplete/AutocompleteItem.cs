using System;
using Common.Features;
using Common.Features.BaseEntity;
using Common.Features.Tenant;

namespace Orders.Autocomplete
{
    public class AutocompleteItem:BaseEntityGuidKey, ITenantedEntity
    {
        public Guid TenantId { get; set; }

        /// <summary>
        /// Задает или получает тип автодополнения.
        /// </summary>
        public AutocompleteKinds AutocompleteKindId { get; set; }

        public AutocompleteKind AutocompleteKind { get; set; }

        /// <summary>
        /// Задает или получает название автодополнения.
        /// </summary>
        public string Title { get; set; }

    }
}
