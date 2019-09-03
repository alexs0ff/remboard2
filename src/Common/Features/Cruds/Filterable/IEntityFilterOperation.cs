using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Common.Features.BaseEntity;
using Microsoft.EntityFrameworkCore;

namespace Common.Features.Cruds.Filterable
{
    public interface IEntityFilterOperation<TEntity,TFilterableEntity>
        where TEntity : BaseEntityGuidKey
    {
        Task<PagedResult<TFilterableEntity>> FilterAsync(DbContext context, ICrudPredicateFeature<TEntity> predicateFactory, FilterParameters filterParameters);
    }
}
