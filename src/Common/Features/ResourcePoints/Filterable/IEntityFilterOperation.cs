using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Common.Features.BaseEntity;
using Microsoft.EntityFrameworkCore;

namespace Common.Features.ResourcePoints.Filterable
{
    public interface IEntityFilterOperation<TEntity,TFilterableEntity, TKey>
        where TEntity : BaseEntity<TKey>
        where TKey : struct
	{
        Task<PagedResult<TFilterableEntity>> FilterAsync(DbContext context, IResourceMandatoryPredicateFactory<TEntity,TKey> mandatoryPredicateFactory, FilterParameters filterParameters);
    }
}
