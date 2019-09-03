using System;
using System.Collections.Generic;
using System.Text;
using LinqKit;

namespace Common.Features.Cruds
{
    public interface ICrudPredicateFeature<TEntity>
        where TEntity : Common.Features.BaseEntity.BaseEntityGuidKey
    {
        ExpressionStarter<TEntity> GetMandatoryPredicate();
    }
}
