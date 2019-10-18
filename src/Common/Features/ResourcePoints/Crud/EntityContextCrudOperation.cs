using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Common.Features.BaseEntity;
using Common.Features.ResourcePoints.Filterable;
using Common.Features.Tenant;
using LinqKit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Common.Features.ResourcePoints.Crud
{
	public class EntityContextCrudOperation<TEntity, TEntityDto, TKey> : ICrudOperation<TEntity, TEntityDto, TKey>
		where TEntity : BaseEntity<TKey>
		where TKey : struct
		where TEntityDto : class
	{
		private readonly IMapper _mapper;
		private readonly ILogger<EntityContextCrudOperation<TEntity, TEntityDto, TKey>> _logger;
		private readonly OnlyTenantEntitiesSpecification<TEntity, TKey> _onlyTenantEntitiesSpecification;

		/// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
		public EntityContextCrudOperation(IMapper mapper,ILogger<EntityContextCrudOperation<TEntity, TEntityDto, TKey>> logger, OnlyTenantEntitiesSpecification<TEntity,TKey> onlyTenantEntitiesSpecification)
		{
			_mapper = mapper;
			_logger = logger;
			_onlyTenantEntitiesSpecification = onlyTenantEntitiesSpecification;
		}

		public async Task<TEntityDto> Get(string id, DbContext context, IResourceMandatoryPredicateFactory<TEntity, TKey> mandatoryPredicateFactory)
		{
			var entity = await GetById(id, context, mandatoryPredicateFactory.GetMandatoryPredicates());

			if (entity == null)
			{
				return null;
			}

			var entityDto = _mapper.Map<TEntityDto>(entity);

			return entityDto;
		}

		protected static async Task<TEntity> GetById(string id, DbContext context,ExpressionStarter<TEntity> predicate)
		{
			var idRaw = GetIdRaw(id);

			var getByIdSpec = new GetByIdSpecification<TEntity, TKey>((TKey) idRaw);
			
			predicate.And(getByIdSpec.IsSatisfiedBy());

			var entity = await context.Set<TEntity>().AsExpandable().FirstOrDefaultAsync(predicate);
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

		public async Task<TEntityDto> Post(TEntityDto entityDto, DbContext context, List<IEntityCorrector<TEntity, TEntityDto, TKey>> correctors)
		{
			var entity = _mapper.Map<TEntity>(entityDto);

			_logger.LogInformation("Start add the new entity {entity} with id {id}", entity, entity.Id);

			foreach (var entityCorrector in correctors)
			{
				await entityCorrector.CorrectEntityAsync(entity, entityDto);
			}

			context.Set<TEntity>().Add(entity);
			await context.SaveChangesAsync();

			entityDto = _mapper.Map<TEntityDto>(entity);

			foreach (var entityCorrector in correctors)
			{
				await entityCorrector.CorrectEntityDtoAsync(entityDto, entity);
			}

			return entityDto;
		}

		public async Task<TEntityDto> Put(string id, TEntityDto entityDto, DbContext context, IResourceMandatoryPredicateFactory<TEntity, TKey> mandatoryPredicateFactory,List<IEntityCorrector<TEntity, TEntityDto, TKey>> correctors)
		{
			var foundEntity = await GetById(id, context, mandatoryPredicateFactory.GetMandatoryPredicates());

			if (foundEntity == null)
			{
				throw new EntityNotFoundException();
			}

			_mapper.Map(entityDto, foundEntity);

			foreach (var entityCorrector in correctors)
			{
				await entityCorrector.CorrectEntityAsync(foundEntity, entityDto);
			}


			context.Set<TEntity>().Update(foundEntity);
			await context.SaveChangesAsync();

			entityDto = _mapper.Map<TEntityDto>(foundEntity);

			foreach (var entityCorrector in correctors)
			{
				await entityCorrector.CorrectEntityDtoAsync(entityDto, foundEntity);
			}

			return entityDto;
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
