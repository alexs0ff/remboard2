using System;
using System.Collections.Generic;
using System.Text;
using Common.FeatureEntities;
using Common.Features.BaseEntity;

namespace Common.Features.ResourcePoints
{
	public class CrudResourcePointConfigurator<TEntity, TEntityDto, TFilterableEntity, TKey>: ResourcePointConfigurator<TEntity, TEntityDto, TFilterableEntity, TKey>
		where TEntity : BaseEntity<TKey>
		where TFilterableEntity : class
		where TEntityDto : class
		where TKey : struct
	{
		private readonly HashSet<ProjectRoles> _modifyRoles = new HashSet<ProjectRoles>();

		private Type _entityValidator = null;

		public CrudResourcePointConfigurator<TEntity, TEntityDto, TFilterableEntity, TKey> AddModifyRoles(params ProjectRoles[] roles)
		{
			AppendRoles(_modifyRoles, roles);
			return this;
		}

		public CrudResourcePointConfigurator<TEntity, TEntityDto, TFilterableEntity, TKey> UseValidator<TValidator>()
			where TValidator : BaseEntityDtoValidator<TEntityDto>
		{
			_entityValidator = typeof(TValidator);
			return this;
		}
	}
}
