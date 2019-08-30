using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Common.Features.ErrorFlow;
using LinqKit;

namespace Common.Features.Cruds
{
    public interface ICrudTypedControllerDescriptor<TEntity>: ICrudControllerDescriptor
        where TEntity : Common.Features.BaseEntity.BaseEntityGuidKey
    {
        ExpressionStarter<TEntity> GetMandatoryPredicate();
        Task<ValidationErrorItem[]> ValidateAsync(TEntity entity);
        Task CorrectBeforeAsync(TEntity entity);
        Task CorrectAfterAsync(TEntity entity);
    }
}
