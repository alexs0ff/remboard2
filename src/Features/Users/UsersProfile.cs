using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.Features;
using Entities;
using Entities.Dto;

namespace Users
{
	public class UsersProfile: FeatureMapperProfile
	{
		public UsersProfile()
		{
			/*Many to many example*/
			CreateMap<User, UserDto>()
				.ForMember(i => i.ProjectRoleTitle, m => m.MapFrom(p => p.ProjectRole.Name))
				.ForMember(i=>i.Branches,m=>m.MapFrom(p=>p.UserBranches.Select(y=>y.Branch).ToList()))
				.ReverseMap()
				.ForMember(i => i.ProjectRole, m => m.Ignore());

			CreateMap<Branch, UserBranchDto>()
				.ForMember(b => b.BranchId, opt => opt.MapFrom(d => d.Id))
				.ForMember(b => b.BranchTitle, opt => opt.MapFrom(d => d.Title));

			CreateMap<UserDto, User>()
				.ForMember(u => u.UserBranches, opt => opt.MapFrom(d => d.Branches))
				.AfterMap((dto, user) =>
				{
					foreach (var userBranch in user.UserBranches)
					{
						userBranch.UserId = dto.Id;
					}

				});

			CreateMap<UserBranchDto, UserBranch>()
				.ForMember(d => d.BranchId, opt => opt.MapFrom(s => s.BranchId));
			/*Many to many example*/

		}
	}
}
