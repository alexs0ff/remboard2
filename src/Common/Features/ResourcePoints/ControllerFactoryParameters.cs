using System;
using System.Collections.Generic;
using System.Text;
using Autofac;
using Common.Features.ResourcePoints.Filterable;

namespace Common.Features.ResourcePoints
{
	public class ControllerFactoryParameters
	{
		public IResourcePointDescriptor ResourcePoint { get; set; }

		public Type ControllerType { get; set; }

		public EntityFilterOperationParameters FilterableEntityOperationParameters { get; set; }

		public Type FilterableEntityOperationType { get; set; }

		public AccessRuleMap AccessRuleMap { get; set; }

		public IList<Type> MandatorySpecificationTypes { get; set; }
	}
}
