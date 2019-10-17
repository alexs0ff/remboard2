using System;
using System.Collections.Generic;
using System.Text;
using Common.Features.BaseEntity;

namespace Common.Features.ResourcePoints
{
	public class CrudResourcePointController<TEntity, TEntityDto, TFilterableEntity, TKey>: ResourcePointBaseController<TEntity, TEntityDto, TFilterableEntity, TKey>
		where TEntity : BaseEntity<TKey>
		where TFilterableEntity : class
		where TKey : struct
		where TEntityDto : class
	{

	}
}
