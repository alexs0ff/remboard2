﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Features.ResourcePoints.Filterable
{
    public class EntityContextFilterOperationParameters: EntityFilterOperationParameters
    {
        public IDictionary<string, string> SortFieldsMaping { get; } = new Dictionary<string, string>();

        public string[] IncludeProperties { get; set; }

        public bool DirectProject { get; set; } = true;

        public EntityContextFilterOperationParameters AddSortFieldsMapping(string fromColumn, string toColumn)
        {
            SortFieldsMaping.Add(fromColumn.ToLower(),toColumn);
            return this;
        }
    }
}
