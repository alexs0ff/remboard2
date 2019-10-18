using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Common.Features.BaseEntity;
using Microsoft.EntityFrameworkCore;

namespace Common.Features.ResourcePoints.Crud
{
	public interface ICrudOperation<TEntity, TEntityDto, TKey>
		where TEntity : BaseEntity<TKey>
		where TKey : struct
		where TEntityDto : class
	{
		Task<TEntityDto> Get(string id, DbContext context, IResourceMandatoryPredicateFactory<TEntity, TKey> mandatoryPredicateFactory);

		Task<TEntityDto> Post(TEntityDto entityDto, DbContext context,List<IEntityCorrector<TEntity, TEntityDto, TKey>> correctors);

		Task<TEntityDto> Put(string id, TEntityDto entityDto, DbContext context,IResourceMandatoryPredicateFactory<TEntity, TKey> mandatoryPredicateFactory,List<IEntityCorrector<TEntity, TEntityDto, TKey>> correctors);

		Task Delete(string id, DbContext context,IResourceMandatoryPredicateFactory<TEntity, TKey> mandatoryPredicateFactory);
	}
}
