using System;
using System.Collections.Generic;
using System.Text;
using Common.Features.BaseEntity;
using Common.Features.Cruds.Filterable;

namespace Common.Features.Cruds
{
    public interface IFilterableOperationFeature<TEntity, TFilterableEntity>
        where TEntity : BaseEntityGuidKey
    {
        IEntityFilterOperation<TEntity, TFilterableEntity> GetFiltarableOperation();
    }
}
