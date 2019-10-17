using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.Features.ResourcePoints.Filterable
{
    public class PagedResult<TPagedEntity>
    {
        /// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
        public PagedResult(long count, IEnumerable<TPagedEntity> entities)
        {
            Count = count;
            Entities = entities;
        }

        public long Count { get; }

        public IEnumerable<TPagedEntity> Entities { get; }

        /// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
        static PagedResult()
        {
            Empty = new PagedResult<TPagedEntity>(0,Enumerable.Empty<TPagedEntity>());
        }

        public static PagedResult<TPagedEntity> Empty { get; }
    }
}
