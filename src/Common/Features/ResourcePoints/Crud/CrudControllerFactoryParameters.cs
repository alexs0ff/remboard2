using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Features.ResourcePoints.Crud
{
	public class CrudControllerFactoryParameters: ControllerFactoryParameters
	{
		public Type EntityValidatorType { get; set; }

		public Type CrudOperationType { get; set; }

		public  IList<Type> EntityCorrectorTypes { get; set; }
	}
}
