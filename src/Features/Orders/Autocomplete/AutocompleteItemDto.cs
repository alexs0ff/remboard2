using System;
using System.Collections.Generic;
using System.Text;

namespace Orders.Autocomplete
{
    public class AutocompleteItemDto
    {
        public Guid Id { get; set; }

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
