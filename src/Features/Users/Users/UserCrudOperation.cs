using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Common.Extensions;
using Common.Features;
using Common.Features.BaseEntity;
using Common.Features.ResourcePoints;
using Common.Features.ResourcePoints.Crud;
using Common.Features.Tenant;
using Entities;
using Entities.Dto;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Users.Users
{
	public class UserCrudOperation: ICrudOperation<User,UserDto,Guid>
	{
		private readonly IMapper _mapper;
		
		private readonly ILogger<UserCrudOperation> _logger;

		/// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
		public UserCrudOperation(IMapper mapper, ILogger<UserCrudOperation> logger)
		{
			_mapper = mapper;
			_logger = logger;
		}

		public async Task<UserDto> Get(string id, DbContext context, IResourceMandatoryPredicateFactory<User, Guid> mandatoryPredicateFactory)
		{
			var entity = await GetById(id, context, mandatoryPredicateFactory.GetMandatoryPredicates());

			if (entity == null)
			{
				return null;
			}

			var entityDto = _mapper.Map<UserDto>(entity);

			return entityDto;
		}

		public Task<UserDto> Post(UserDto entityDto, DbContext context, List<IEntityCorrector<User, UserDto, Guid>> correctors)
		{
			throw new NotImplementedException();
		}

		public async Task<UserDto> Put(string id, UserDto entityDto, DbContext context, IResourceMandatoryPredicateFactory<User, Guid> mandatoryPredicateFactory,
			List<IEntityCorrector<User, UserDto, Guid>> correctors)
		{
			var entity = _mapper.Map<User>(entityDto);

			_logger.LogInformation("Start add the new user with id {id}", entity, entity.Id);

			//await correctors.CorrectEntityAsync(entity, entityDto);


			//context.Set<User>().Add(entity);
			//await context.SaveChangesAsync();

			//entityDto = _mapper.Map<UserDto>(entity);

			//await correctors.CorrectEntityDtoAsync(entityDto, entity);

			return entityDto;
		}

		public Task Delete(string id, DbContext context, IResourceMandatoryPredicateFactory<User, Guid> mandatoryPredicateFactory)
		{
			throw new NotImplementedException();
		}

		private Guid GetId(string id)
		{
			Guid result;

			if (!Guid.TryParse(id,out result))
			{
				throw new WrongIdValueException();
			}

			return result;
		}

		private async Task<User> GetById(string idRaw, DbContext context, ExpressionStarter<User> predicate)
		{
			var id = GetId(idRaw);

			var getByIdSpec = new GetByIdSpecification<User, Guid>(id);

			predicate.And(getByIdSpec.IsSatisfiedBy());

			var user = await context.Set<User>()
				.Include(u => u.ProjectRole)
				.Include(u => u.UserBranches)
				.ThenInclude(ub => ub.Branch)
				.AsExpandable()
				.FirstOrDefaultAsync(predicate);

			return user;
		}
	}
}
