using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Common.Features.ResourcePoints.Filterable.Schema
{
	public interface IEntitySchemaProvider<TFilterableEntity>
		where TFilterableEntity : class
	{
		Task<ServerDataGridModel> GetModelAsync(EntitySchemaProviderContext context);
	}
}
