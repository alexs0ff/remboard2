using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Entities;
using Microsoft.EntityFrameworkCore;

namespace Common.Features.ResourcePoints.Crud
{
	public interface ICrudOperation<TEntity, TCreateEntityDto, TEditEntityDto, TKey>
		where TEntity : BaseEntity<TKey>
		where TKey : struct
		where TCreateEntityDto : class
		where TEditEntityDto : class
	{
		Task<TEditEntityDto> Get(string id, DbContext context, IResourceMandatoryPredicateFactory<TEntity, TKey> mandatoryPredicateFactory);

		Task<TEditEntityDto> Post(TCreateEntityDto entityDto, DbContext context,List<IEntityCorrector<TEntity, TCreateEntityDto,TEditEntityDto, TKey>> correctors);

		Task<TEditEntityDto> Put(string id, TEditEntityDto entityDto, DbContext context,IResourceMandatoryPredicateFactory<TEntity, TKey> mandatoryPredicateFactory,List<IEntityCorrector<TEntity, TCreateEntityDto, TEditEntityDto, TKey>> correctors);

		Task Delete(string id, DbContext context,IResourceMandatoryPredicateFactory<TEntity, TKey> mandatoryPredicateFactory);
	}
}
