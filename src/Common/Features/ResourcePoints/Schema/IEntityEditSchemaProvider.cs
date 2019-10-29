using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Features.ResourcePoints.Schema
{
	public interface IEntityEditSchemaProvider<TEntityDto>
		where TEntityDto:class
	{
		EntityEditModel GetModel(EntityEditSchemaProviderContext context);
	}
}
