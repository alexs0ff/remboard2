using System;
using System.Collections.Generic;
using System.Text;
using Common.Features.BaseEntity;
using Microsoft.AspNetCore.Mvc;

namespace Common.Features.ResourcePoints
{
	public class ResourcePointBaseController<TEntity, TEntityDto, TFilterableEntity, TKey> : ControllerBase
		where TEntity : BaseEntity<TKey>
		where TFilterableEntity : class
		where TKey:struct
		where TEntityDto :class
	{

	}
}
