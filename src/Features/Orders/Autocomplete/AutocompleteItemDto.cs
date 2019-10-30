using System;
using System.Collections.Generic;
using System.Text;
using Common.Features.Binders;
using Common.Features.TypeConverters;
using Entities;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Orders.Autocomplete
{
    public class AutocompleteItemDto
    {
        [JsonConverter(typeof(ServerGeneratedGuidConverter))]
        public Guid Id { get; set; }

        /// <summary>
        /// Задает или получает тип автодополнения.
        /// </summary>
        public AutocompleteKinds AutocompleteKindId { get; set; }

        public string AutocompleteKindTitle { get; set; }

        /// <summary>
        /// Задает или получает название автодополнения.
        /// </summary>
        public string Title { get; set; }
    }
}
