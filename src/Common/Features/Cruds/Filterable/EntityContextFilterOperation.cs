using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Common.Features.BaseEntity;
using LinqKit;
using Microsoft.EntityFrameworkCore;

namespace Common.Features.Cruds.Filterable
{
    public class EntityContextFilterOperation<TEntity, TFilterableEntity> :IEntityFilterOperation<TEntity, TFilterableEntity>
        where TEntity : BaseEntityGuidKey
    {
        private readonly IMapper _mapper;

        /// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
        public EntityContextFilterOperation(IMapper mapper)
        {
            _mapper = mapper;
        }


        public async Task<IEnumerable<TFilterableEntity>> FilterAsync(DbContext context, ICrudPredicateFeature<TEntity> predicateFactory, FilterParameters filterParameters)
        {
            var predicate = predicateFactory.GetMandatoryPredicate();

            var result = await context.Set<TEntity>().AsExpandable().Where(predicate).ToListAsync();
        }
    }
}
