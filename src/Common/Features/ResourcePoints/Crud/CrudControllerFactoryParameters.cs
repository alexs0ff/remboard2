using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Features.ResourcePoints.Crud
{
	public class CrudControllerFactoryParameters: ControllerFactoryParameters
	{
		public Type CreateEntityDtoValidatorType { get; set; }

		public Type EditEntityDtoValidatorType { get; set; }

		public Type CrudOperationType { get; set; }

		public  IList<Type> EntityCorrectorTypes { get; set; }


		public Type EntityFormSchemaProviderType { get; set; }

		public CrudOperationParameters CrudOperationParameters { get; set; }
	}
}
