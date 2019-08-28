using System;
using System.Collections.Generic;
using System.Text;
using LinqKit;

namespace Common.Features.Cruds
{
    public interface ICrudTypedControllerDescriptor<TEntity>: ICrudControllerDescriptor
        where TEntity : Common.Features.BaseEntity.BaseEntityGuidKey
    {
        ExpressionStarter<TEntity> GetMandatoryPredicate();
    }
}
