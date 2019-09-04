using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Features.Cruds.Filterable
{
    public class EntitySqlFilterOperationParameters: EntityFilterOperationParameters
    {
        public string Sql { get; set; }

        public string DefaultOrderColumn { get; set; }

        public string AliasName { get; set; }
    }
}
