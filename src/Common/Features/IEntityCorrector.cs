using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Entities;

namespace Common.Features
{
	public interface IEntityCorrector<in TEntity,in TCreateEntityDto,in TEditEntityDto, TKey>
        where TEntity : BaseEntity<TKey>
		where TKey:struct
		where TEditEntityDto : class
		where TCreateEntityDto : class
    {
        Task CorrectEntityAsync(EntityCorrectorContext context, TEntity entity, TCreateEntityDto receivedEntityDto);
        Task CorrectEntityAsync(EntityCorrectorContext context, TEntity entity, TEditEntityDto receivedEntityDto);

        Task CorrectEntityDtoAsync(EntityCorrectorContext context, TCreateEntityDto entityDto, TEntity entity);

        Task CorrectEntityDtoAsync(EntityCorrectorContext context, TEditEntityDto entityDto, TEntity entity);
    }
}
