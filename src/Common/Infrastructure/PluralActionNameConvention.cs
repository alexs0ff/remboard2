using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.Extensions;
using Common.Features.PermissibleValues;
using Common.Features.ResourcePoints;
using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace Common.Infrastructure
{
	[AttributeUsage(AttributeTargets.Method)]
	public class PluralActionNameConventionAttribute : Attribute
	{

	}

	public class PluralActionNameConvention : IActionModelConvention
	{

		private readonly PermissibleValuesControllerRegistry _permissibleValuesControllerRegistry;

		private readonly ResourcePointControllerRegistry _resourcePointControllerRegistry;

		/// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
		public PluralActionNameConvention(PermissibleValuesControllerRegistry permissibleValuesControllerRegistry,
			ResourcePointControllerRegistry resourcePointControllerRegistry)
		{
			_permissibleValuesControllerRegistry = permissibleValuesControllerRegistry;
			_resourcePointControllerRegistry = resourcePointControllerRegistry;
		}

		/// <summary>
		/// Called to apply the convention to the <see cref="T:Microsoft.AspNetCore.Mvc.ApplicationModels.ActionModel" />.
		/// </summary>
		/// <param name="action">The <see cref="T:Microsoft.AspNetCore.Mvc.ApplicationModels.ActionModel" />.</param>
		public void Apply(ActionModel action)
		{
			var hasAttribute = action.Attributes.Cast<Attribute>()
				.Any(a => a.GetType() == typeof(PluralActionNameConventionAttribute));

			if (hasAttribute && action.Controller.ControllerType.IsGenericType)
			{
				var genericType = action.Controller.ControllerType.GetGenericTypeDefinition();
				var name = genericType.Name;
				if (name == "PermissibleValuesController`2")
				{
					var entityType = action.Controller.ControllerType.GenericTypeArguments[0];
					action.ActionName = _permissibleValuesControllerRegistry[entityType.Name]
						.PermissibleValuesDescriptor.EntityPluralName;
				}
			}
			
			
			if (hasAttribute &&
			         action.Controller.ControllerType.InheritsOrImplements(typeof(ResourcePointBaseController<,,>)))
			{
				var entityType = action.Controller.ControllerType.GetEntityTypeOrNull();
				if (entityType != null)
				{
					action.ActionName =
						_resourcePointControllerRegistry[entityType.Name].ResourcePoint.EntityPluralName;
				}
			}

		}
	}
}
