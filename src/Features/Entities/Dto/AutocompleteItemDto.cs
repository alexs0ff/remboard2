using System;
using System.Collections.Generic;
using System.Text;
using Entities;
using Entities.Dto.Infrastructure;
using Newtonsoft.Json;

namespace Entities.Dto
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
