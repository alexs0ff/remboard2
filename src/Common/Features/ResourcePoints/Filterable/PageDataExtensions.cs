using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Features.ResourcePoints.Filterable
{
    public static class PageDataExtensions
    {
        public static (int skip, int take) GetPageData(int pageSize, int currentPage)
        {
            if (pageSize <= 0 || currentPage <= 0)
            {
                return (-1, -1);
            }

            var skip = pageSize * (currentPage - 1);

            return (skip, pageSize);
        }
    }
}
