using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Features.ResourcePoints.Filterable.Schema
{
	public interface IEntitySchemaProvider<TFilterableEntity>
		where TFilterableEntity : class
	{
		ServerDataGridModel GetModel(EntitySchemaProviderContext context);
	}
}
