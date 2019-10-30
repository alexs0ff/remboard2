using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Entities;

namespace Common.Features
{
	public interface IEntityCorrector<in TEntity,in TEntityDto,TKey>
        where TEntity : BaseEntity<TKey>
		where TKey:struct
    {
        Task CorrectEntityAsync(TEntity entity, TEntityDto receivedEntityDto);

        Task CorrectEntityDtoAsync(TEntityDto entityDto, TEntity entity);
    }
}
