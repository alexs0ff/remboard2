using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Common.Features.BaseEntity;

namespace Common.Features
{
	[Obsolete]
    public interface IEntityCorrectorGuid<in TEntity,in TEntityDto>
        where TEntity : BaseEntityGuidKey
    {
        Task CorrectEntityAsync(TEntity entity, TEntityDto receivedEntityDto);

        Task CorrectEntityDtoAsync(TEntityDto entityDto, TEntity entity);
    }
}
