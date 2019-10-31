using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Features.ResourcePoints.Crud
{
	public class EntityContextCrudOperationParameters: CrudOperationParameters
	{
		public string[] IncludeProperties { get; set; }
	}
}
