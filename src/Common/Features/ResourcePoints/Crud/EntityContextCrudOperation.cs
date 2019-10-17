using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Common.Features.BaseEntity;
using Common.Features.ResourcePoints.Filterable;
using LinqKit;
using Microsoft.EntityFrameworkCore;

namespace Common.Features.ResourcePoints.Crud
{
	public class EntityContextCrudOperation<TEntity, TEntityDto, TKey> : ICrudOperation<TEntity, TEntityDto, TKey>
		where TEntity : BaseEntity<TKey>
		where TKey : struct
		where TEntityDto : class
	{
		private readonly IMapper _mapper;

		/// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
		public EntityContextCrudOperation(IMapper mapper)
		{
			_mapper = mapper;
		}

		public async Task<TEntityDto> Get(string id, DbContext context, IResourceMandatoryPredicateFactory<TEntity, TKey> mandatoryPredicateFactory)
		{
			var entity = await GetById(id, context, mandatoryPredicateFactory);

			if (entity == null)
			{
				return null;
			}

			var entityDto = _mapper.Map<TEntityDto>(entity);

			return entityDto;
		}

		protected static async Task<TEntity> GetById(string id, DbContext context,IResourceMandatoryPredicateFactory<TEntity, TKey> mandatoryPredicateFactory)
		{
			var idRaw = FilterTypeCorrector.ChangeType<TEntity>(nameof(BaseEntity<TKey>.Id), id);

			if (idRaw == null)
			{
				throw new WrongIdValueException();
			}

			var getByIdSpec = new GetByIdSpecification<TEntity, TKey>((TKey) idRaw);

			var predicate = mandatoryPredicateFactory.GetMandatoryPredicates();
			predicate.And(getByIdSpec.IsSatisfiedBy());

			var entity = await context.Set<TEntity>().AsExpandable().FirstOrDefaultAsync(predicate);
			return entity;
		}
	}
}
