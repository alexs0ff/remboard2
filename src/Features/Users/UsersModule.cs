using System;
using System.Collections.Generic;
using System.Text;
using Autofac;
using Common.Data;
using Common.Features;
using Common.Features.ResourcePoints;
using Common.Features.ResourcePoints.Crud;
using Common.Features.ResourcePoints.Filterable;
using Entities;
using Entities.Dto;
using Microsoft.EntityFrameworkCore;
using Users.Api;
using Users.Users;

namespace Users
{
    public sealed class UsersModule: FeatureModule, IConfigureModelFeature
    { 
        protected override void RegisterServices(ContainerBuilder builder)
        {
            builder.RegisterType<UserService>().As<IUserService>();
            AddMapperProfile<UsersProfile>(builder);
        }

        public void OnContextFeatureCreating(ModelBuilder modelBuilder, RemboardContextParameters contextParameters)
        {
			modelBuilder.ApplyConfiguration(new UserEntityConfiguration());
			modelBuilder.ApplyConfiguration(new ProjectRoleConfiguration());
			modelBuilder.ApplyConfiguration(new UserBranchConfiguration());
		}

        protected override IEnumerable<IResourcePointConfigurator> RegisterResourcePoints()
        {
	        var userIncludeProperties = new[] {"ProjectRole", "UserBranches", "UserBranches.Branch"};
			yield return new CrudResourcePointConfigurator<User, UserCreateDto, UserEditDto, UserEditDto, Guid>()
				.AddModifyRoles()
				.UseValidators<UserCreateDtoValidator, UserEditDtoValidator>()
				.UseEntityContextCrudOperation<EntityContextCrudOperation<User, UserCreateDto, UserEditDto, Guid>>(p =>
					{
						p.IncludeProperties = userIncludeProperties;
					})
				.SetEntityPluralName("Users")
				.UseFilterableEntityOperation<EntityContextFilterOperation<
					User, UserEditDto, Guid>>(
					parameters =>
					{
						parameters.DirectProject = false;
						parameters.IncludeProperties = userIncludeProperties;
						parameters.AddSortFieldsMapping(nameof(UserCreateDto.ProjectRoleTitle), nameof(User.ProjectRole) + "." + nameof(User.ProjectRole.Name));
					});
		}
    }
}
