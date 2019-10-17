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
	}
}
