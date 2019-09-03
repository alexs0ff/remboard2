using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Common.Features.ErrorFlow;
using LinqKit;

namespace Common.Features.Cruds
{
    public interface ICrudTypedControllerDescriptor<TEntity, TEntityDto> : ICrudControllerDescriptor, ICrudPredicateFeature<TEntity>
        where TEntity : Common.Features.BaseEntity.BaseEntityGuidKey
    {
        Task<ValidationErrorItem[]> ValidateAsync(TEntityDto entityDto);
        Task CorrectEntityAsync(TEntity entity,TEntityDto receivedEntityDto);
        Task CorrectEntityDtoAsync(TEntityDto entityDto, TEntity entity);
    }
}
