using System;
using System.Collections.Generic;
using System.Text;
using Common.Features;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Users
{
    public class ProjectRoleConfiguration: BasePermissibleValueConfiguration<ProjectRole>
    {
        public override void Configure(EntityTypeBuilder<ProjectRole> builder)
        {
            base.Configure(builder);
            FillData<ProjectRoles>(builder);
        }
    }
}
