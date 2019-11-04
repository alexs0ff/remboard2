using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Data;
using Common.Features;
using Common.Features.ResourcePoints.Crud;
using Entities;
using Entities.Dto;
using Microsoft.EntityFrameworkCore;

namespace Users.Users
{
	public class UserCorrector:IEntityCorrector<User,UserDto, UserDto, Guid>
	{
		private readonly RemboardContext _context;

		/// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
		public UserCorrector(RemboardContext context)
		{
			_context = context;
		}

		public async Task CorrectEntityAsync(EntityCorrectorContext context, User entity, UserDto receivedEntityDto)
		{
			if (context.OperationKind == CrudOperationKind.Put)
			{
				var originalLogin = await _context.Set<User>().Where(u => u.Id == entity.Id).Select(u => u.LoginName).FirstAsync();
				entity.LoginName = originalLogin;
			}
		}

		public Task CorrectEntityDtoAsync(EntityCorrectorContext context, UserDto entityDto, User entity)
		{
			return Task.CompletedTask;
		}
	}
}
