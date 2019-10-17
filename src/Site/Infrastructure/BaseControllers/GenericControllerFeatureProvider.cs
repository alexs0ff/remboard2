﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Common.Features.Cruds;
using Common.Features.PermissibleValues;
using Common.Features.ResourcePoints;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.AspNetCore.Mvc.Controllers;
using Remboard.Controllers;

namespace Remboard.Infrastructure.BaseControllers
{
	public class GenericControllerFeatureProvider : IApplicationFeatureProvider<ControllerFeature>
	{
		private readonly EntityControllerRegistry _controllerRegistry;

		private readonly PermissibleValuesControllerRegistry _permissibleValuesControllerRegistry;

		private readonly ResourcePointControllerRegistry _resourcePointControllerRegistry;

		public GenericControllerFeatureProvider(EntityControllerRegistry controllerRegistry,
			PermissibleValuesControllerRegistry permissibleValuesControllerRegistry,
			ResourcePointControllerRegistry resourcePointControllerRegistry)
		{
			_controllerRegistry = controllerRegistry;
			_permissibleValuesControllerRegistry = permissibleValuesControllerRegistry;
			_resourcePointControllerRegistry = resourcePointControllerRegistry;
		}

		public void PopulateFeature(IEnumerable<ApplicationPart> parts, ControllerFeature feature)
		{
			// This is designed to run after the default ControllerTypeProvider, 
			// so the list of 'real' controllers has already been populated.
			foreach (var crudControllerDescriptor in _controllerRegistry.CrudControllerDescriptors)
			{
				var typeName = crudControllerDescriptor.EntityName + "Controller";
				if (feature.Controllers.All(t => t.Name != typeName))
				{
					// There's no 'real' controller for this entity, so add the generic version.
					var controllerType = typeof(CrudController<,,>)
						.MakeGenericType(crudControllerDescriptor.EntityDescriptor.EntityTypeInfo.AsType(),
							crudControllerDescriptor.EntityDescriptor.EntityDtoTypeInfo.AsType(),
							crudControllerDescriptor.EntityDescriptor.FilterableEntityTypeInfo.AsType()).GetTypeInfo();
					feature.Controllers.Add(controllerType);
				}
			}

			foreach (var valuesControllerDescriptor in _permissibleValuesControllerRegistry
				.PermissibleValuesControllerDescriptors)
			{
				var typeName = valuesControllerDescriptor.EntityName + "Controller";
				if (feature.Controllers.All(t => t.Name != typeName))
				{
					// There's no 'real' controller for this entity, so add the generic version.
					var controllerType = typeof(PermissibleValuesController<,>)
						.MakeGenericType(valuesControllerDescriptor.PermissibleValuesDescriptor.EntityTypeInfo.AsType(),
							valuesControllerDescriptor.PermissibleValuesDescriptor.EntityEnumInfo.AsType())
						.GetTypeInfo();
					feature.Controllers.Add(controllerType);
				}
			}

			foreach (var pointControllerDescriptor in _resourcePointControllerRegistry
				.ResourcePointControllerDescriptors)
			{
				var typeName = pointControllerDescriptor.EntityName + "Controller";
				if (feature.Controllers.All(t => t.Name != typeName))
				{
					// There's no 'real' controller for this entity, so add the generic version.
					var controllerType = pointControllerDescriptor.ControllerType.GetTypeInfo();
					feature.Controllers.Add(controllerType);
				}
			}
		}
	}
}
