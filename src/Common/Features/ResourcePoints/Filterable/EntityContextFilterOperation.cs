using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Common.Extensions;
using Common.Features.BaseEntity;
using Common.Features.ResourcePoints.Filterable.Specifications;
using Common.Features.Specifications;
using LinqKit;
using Entities;
using Microsoft.EntityFrameworkCore;

namespace Common.Features.ResourcePoints.Filterable
{
    public class EntityContextFilterOperation<TEntity, TFilterableEntity,TKey> :IEntityFilterOperation<TEntity, TFilterableEntity,TKey>
        where TEntity : BaseEntity<TKey>
		where TKey:struct

	{
        private readonly IMapper _mapper;
        private readonly EntityContextFilterOperationParameters _parameters;

        /// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
        public EntityContextFilterOperation(IMapper mapper, EntityFilterOperationParameters parameters)
        {
            _mapper = mapper;
            _parameters = (EntityContextFilterOperationParameters)parameters;
        }

        public async Task<PagedResult<TFilterableEntity>> FilterAsync(DbContext context, IResourceMandatoryPredicateFactory<TEntity,TKey> mandatoryPredicateFactory, FilterParameters filterParameters)
        {
            var mandatoryPredicate = mandatoryPredicateFactory.GetMandatoryPredicates();

            var predicate = LinqKit.PredicateBuilder.New<TEntity>(true);

            foreach (var filter in filterParameters.Statements)
            {
                ISpecification<TEntity> specification = Create(filter);

                if (specification==null)
                {
                    continue;
                }

                if (filter.LogicalOperator == FilterLogicalOperators.Or)
                {
                    predicate.Or(specification.IsSatisfiedBy());
                }
                else
                {
                    predicate.And(specification.IsSatisfiedBy());
                }
            }

            var count = await context.Set<TEntity>().AsExpandable().Where(mandatoryPredicate).Where(predicate).LongCountAsync();

            var entityQuery = context.Set<TEntity>().AsExpandable();


            if (_parameters.IncludeProperties!=null)
            {
	            foreach (var includedProperty in _parameters.IncludeProperties)
	            {
		            entityQuery = entityQuery.Include(includedProperty);
	            }
            }
            
			var query = entityQuery.Where(mandatoryPredicate).Where(predicate);

            if (!string.IsNullOrWhiteSpace(filterParameters.OrderBy))
            {
                var sortOrder = filterParameters.OrderKind == OrderKind.Asc? ListSortDirection.Ascending: ListSortDirection.Descending;

                var orderByColumn = filterParameters.OrderBy.ToLower();

                if (_parameters.SortFieldsMaping.ContainsKey(orderByColumn))
                {
                    orderByColumn = _parameters.SortFieldsMaping[orderByColumn];
                }

                query = query.OrderBy(orderByColumn, sortOrder);
            }

            var pageData = PageDataExtensions.GetPageData(filterParameters.PageSize, filterParameters.CurrentPage);

            if (pageData.skip >= 0)
            {
                query = query.Skip(pageData.skip).Take(pageData.take);
            }

            TFilterableEntity[] entities;

            if (_parameters.DirectProject)
            {
				entities = await _mapper.ProjectTo<TFilterableEntity>(query).ToArrayAsync();    
			}
            else
            {
				var queryEntities = await query.ToArrayAsync();

				entities = _mapper.Map<TFilterableEntity[]>(queryEntities);
			}

            return new PagedResult<TFilterableEntity>(count,entities);
        }


        private ISpecification<TEntity> Create(FilterStatement filter)
        {
	        var property = typeof(TEntity).GetPropertyInfoIgnoreCase(filter.ParameterName);

	        if (property == null)
	        {
		        return null;
	        }

			var targetType = property.PropertyType;

            object value = FilterTypeCorrector.ChangeType<TEntity>(filter.ParameterName, filter.ParameterValue);

            if (value == null)
            {
                return null;
            }

            if (filter.ComparisonOperator == FilterComparisonOperators.Equals)
            {
                return new EqualsSpecification<TEntity>(targetType,value,filter.ParameterName);
            }

            if (filter.ComparisonOperator == FilterComparisonOperators.GreaterThan)
            {
                return new GreaterThanSpecification<TEntity>(targetType, value, filter.ParameterName);
            }

            if (filter.ComparisonOperator == FilterComparisonOperators.LessThan)
            {
                return new LessThanSpecification<TEntity>(targetType, value, filter.ParameterName);
            }

            if (filter.ComparisonOperator == FilterComparisonOperators.Contains)
            {
                return new ContainsSpecification<TEntity>(filter.ParameterValue, filter.ParameterName);
            }

            return null;
        }

        
    }
}
