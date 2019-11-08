using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Common.Extensions;
using Common.Features.BaseEntity;
using Common.Features.ResourcePoints.Filterable;
using Common.Features.Tenant;
using LinqKit;
using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Common.Features.ResourcePoints.Crud
{
	public class EntityContextCrudOperation<TEntity, TCreateEntityDto, TEditEntityDto, TKey> : ICrudOperation<TEntity, TCreateEntityDto,TEditEntityDto, TKey>
		where TEntity : BaseEntity<TKey>
		where TKey : struct
		where TCreateEntityDto : class
		where TEditEntityDto : class
	{
		private readonly IMapper _mapper;
		private readonly ILogger<EntityContextCrudOperation<TEntity, TCreateEntityDto, TEditEntityDto,TKey>> _logger;
		private readonly OnlyTenantEntitiesSpecification<TEntity, TKey> _onlyTenantEntitiesSpecification;
		private readonly EntityContextCrudOperationParameters _parameters;

		/// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
		public EntityContextCrudOperation(IMapper mapper,ILogger<EntityContextCrudOperation<TEntity, TCreateEntityDto,TEditEntityDto, TKey>> logger, OnlyTenantEntitiesSpecification<TEntity,TKey> onlyTenantEntitiesSpecification, EntityContextCrudOperationParameters parameters)
		{
			_mapper = mapper;
			_logger = logger;
			_onlyTenantEntitiesSpecification = onlyTenantEntitiesSpecification;
			_parameters = parameters;
		}

		public async Task<TEditEntityDto> Get(string id, DbContext context, IResourceMandatoryPredicateFactory<TEntity, TKey> mandatoryPredicateFactory)
		{
			var entity = await GetById(id, context, mandatoryPredicateFactory.GetMandatoryPredicates());

			if (entity == null)
			{
				return null;
			}

			var entityDto = _mapper.Map<TEditEntityDto>(entity);

			return entityDto;
		}

		protected async Task<TEntity> GetById(string id, DbContext context,ExpressionStarter<TEntity> predicate)
		{
			var idRaw = GetIdRaw(id);

			var getByIdSpec = new GetByIdSpecification<TEntity, TKey>((TKey) idRaw);
			
			predicate.And(getByIdSpec.IsSatisfiedBy());

			var query = context.Set<TEntity>().AsQueryable();

			if (_parameters.IncludeProperties != null)
			{
				foreach (var property in _parameters.IncludeProperties)
				{
					query = query.Include(property);
				}
			}


			var entity = await query.AsExpandable().FirstOrDefaultAsync(predicate);

			return entity;
		}

		private static object GetIdRaw(string id)
		{
			var idRaw = FilterTypeCorrector.ChangeType<TEntity>(nameof(BaseEntity<TKey>.Id), id);

			if (idRaw == null)
			{
				throw new WrongIdValueException();
			}

			return idRaw;
		}

		public async Task<(TEditEntityDto entityDto, TEntity entity)> Post(TCreateEntityDto entityDto, DbContext context, List<IEntityCorrector<TEntity,TCreateEntityDto,TEditEntityDto, TKey>> correctors)
		{
			var entity = _mapper.Map<TEntity>(entityDto);

			_logger.LogInformation("Start add the new entity {entity} with id {id}", entity, entity.Id);
			var correctorContext = new EntityCorrectorContext
			{
				OperationKind = CrudOperationKind.Post
			};
			await correctors.CorrectCreateEntityAsync(correctorContext,entity, entityDto);

			context.Set<TEntity>().Add(entity);
			await context.SaveChangesAsync();

			var savedDto = _mapper.Map<TEditEntityDto>(entity);

			await correctors.CorrectEditEntityDtoAsync(correctorContext, savedDto, entity);

			return (savedDto,entity);
		}

		public async Task<(TEditEntityDto entityDto, TEntity entity)> Put(string id, TEditEntityDto entityDto, DbContext context, IResourceMandatoryPredicateFactory<TEntity, TKey> mandatoryPredicateFactory,List<IEntityCorrector<TEntity, TCreateEntityDto, TEditEntityDto, TKey>> correctors)
		{
			var foundEntity = await GetById(id, context, mandatoryPredicateFactory.GetMandatoryPredicates());

			if (foundEntity == null)
			{
				throw new EntityNotFoundException();
			}

			_mapper.Map(entityDto, foundEntity);

			var correctorContext = new EntityCorrectorContext
			{
				OperationKind = CrudOperationKind.Put
			};

			await correctors.CorrectEditEntityAsync(correctorContext, foundEntity, entityDto);

			context.Set<TEntity>().Update(foundEntity);
			
			await context.SaveChangesAsync();

			foundEntity = await GetById(id, context, mandatoryPredicateFactory.GetMandatoryPredicates());

			entityDto = _mapper.Map<TEditEntityDto>(foundEntity);
	
			await correctors.CorrectEditEntityDtoAsync(correctorContext, entityDto, foundEntity);

			return (entityDto, foundEntity);
		}

		public async Task Delete(string id, DbContext context, IResourceMandatoryPredicateFactory<TEntity, TKey> mandatoryPredicateFactory)
		{
			var foundEntity = await GetById(id, context, mandatoryPredicateFactory.GetMandatoryPredicates());
			var idRaw = GetIdRaw(id);

			if (foundEntity == null)
			{
				var predicate = LinqKit.PredicateBuilder.New<TEntity>(true);

				var getByIdSpec = new GetByIdSpecification<TEntity,TKey>((TKey)idRaw);


				predicate.And(getByIdSpec.IsSatisfiedBy());
				predicate.And(_onlyTenantEntitiesSpecification.IsSatisfiedBy());

				foundEntity = await context.Set<TEntity>().AsExpandable().FirstOrDefaultAsync(predicate);

				if (foundEntity == null)
				{
					throw new EntityNotFoundException();
				}
			}

			foundEntity.IsDeleted = true;

			context.Set<TEntity>().Update(foundEntity);
			await context.SaveChangesAsync();
		}
	}
}
