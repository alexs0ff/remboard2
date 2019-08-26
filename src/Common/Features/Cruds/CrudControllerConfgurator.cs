using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.FeatureEntities;
using Microsoft.EntityFrameworkCore.Internal;

namespace Common.Features.Cruds
{
    public class CrudControllerConfgurator<TEntity>
        where TEntity : BaseEntityGuidKey
    {
        private readonly HashSet<ProjectRoles> _readRoles = new HashSet<ProjectRoles>();

        private readonly HashSet<ProjectRoles> _modifyRoles = new HashSet<ProjectRoles>();

        public CrudControllerConfgurator<TEntity> AddReadRoles(params ProjectRoles[] roles)
        {
            AppendRoles(_readRoles, roles);
            return this;
        }

        public CrudControllerConfgurator<TEntity> AddModifyRoles(params ProjectRoles[] roles)
        {
            AppendRoles(_modifyRoles, roles);
            return this;
        }

        private void AppendRoles(HashSet<ProjectRoles> roles,ProjectRoles[] rolesToAppend)
        {
            foreach (var projectRole in rolesToAppend)
            {
                if (!roles.Contains(projectRole))
                {
                    roles.Add(projectRole);
                }
            }
        }
        
        public ICrudControllerDescriptor Finish()
        {
            return new CrudControllerDescriptor(new CrudEntityDescriptor<TEntity>(), new AccessRuleMap(_readRoles.ToArray(), _modifyRoles.ToArray()));
        }
    }
}
