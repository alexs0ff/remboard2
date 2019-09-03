using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Mvc;

namespace Common.Features.Cruds.Filterable
{
    /// <summary>
    /// example:https://localhost:44378/api/AutocompleteItems?filterColumnName=Title&filterColumnTitleValue=test&filterColumnTitleOperator=Equals&filterColumnIdLogic=and&filterColumnName=Id&filterColumnIdValue=2&filterColumnIdOperator=LessThan&pageSize=10&page=1
    /// </summary>
    [ModelBinder(BinderType = typeof(FilterParametersModelBinder))]
    public class FilterParameters
    {
        /// <summary>Initializes a new instance of the <see cref="T:ommon.Features.Cruds.Filterable.FilterParameters" /> class.</summary>
        public FilterParameters()
        {
            Statements = new List<FilterStatement>();
        }

        public IList<FilterStatement> Statements { get; }

        public int PageSize { get; set; }

        public int CurrentPage { get; set; }
    }
}
