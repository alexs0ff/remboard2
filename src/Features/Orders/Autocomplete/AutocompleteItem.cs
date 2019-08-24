using System;
using Common.Features;

namespace Orders.Autocomplete
{
    public class AutocompleteItem:BaseEntityGuidKey, ITenantedEntity
    {
        public Guid TenantId { get; set; }

        /// <summary>
        /// Задает или получает тип автодополнения.
        /// </summary>
        public AutocompleteKinds AutocompleteKindId { get; set; }

        /// <summary>
        /// Задает или получает название автодополнения.
        /// </summary>
        public string Title { get; set; }

    }
}
