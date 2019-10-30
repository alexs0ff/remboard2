using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Common.Features.ResourcePoints.Schema
{
	public interface IEntityEditSchemaProvider<TEntityDto>
		where TEntityDto:class
	{
		Task<EntityEditFormModel> GetModelAsync(EntityEditSchemaProviderContext context);
	}
}
