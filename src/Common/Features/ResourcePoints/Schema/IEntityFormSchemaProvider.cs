using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Common.Features.ResourcePoints.Schema
{
	public interface IEntityFormSchemaProvider<TCreateEntityDto, TEditEntityDto>
		where TCreateEntityDto:class
		where TEditEntityDto : class
	{
		Task<EntityEditFormModel> GetCreateModelAsync(EntityEditSchemaProviderContext context);
		Task<EntityEditFormModel> GetEditModelAsync(EntityEditSchemaProviderContext context);
	}
}
