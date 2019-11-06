using System;
using System.Collections.Generic;
using System.Text;
using Common.Features.ResourcePoints.Crud.Messaging;

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

		public CrudCommandsProducerParameters CrudCommandsProducerParameters { get; set; }
	}
}
