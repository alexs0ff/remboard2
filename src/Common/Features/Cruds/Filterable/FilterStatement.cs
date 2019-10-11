using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Features.Cruds.Filterable
{
    public class FilterStatement
    {
        public string ParameterName { get; set; }

        public string ParameterValue { get; set; }

        public FilterComparisonOperators ComparisonOperator { get; set; }

        public FilterLogicalOperators LogicalOperator { get; set; }
    }
}
