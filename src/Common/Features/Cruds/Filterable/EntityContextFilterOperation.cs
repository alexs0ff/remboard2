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
using Common.Features.Cruds.Filterable.Specifications;
using Common.Features.Specifications;
using LinqKit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Common.Features.Cruds.Filterable
{
    public class EntityContextFilterOperation<TEntity, TFilterableEntity> :IEntityFilterOperation<TEntity, TFilterableEntity>
        where TEntity : BaseEntityGuidKey
    {
        private readonly IMapper _mapper;
        private readonly EntityContextFilterOperationParameters _parameters;

        /// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
        public EntityContextFilterOperation(IMapper mapper, EntityFilterOperationParameters parameters)
        {
            _mapper = mapper;
            _parameters = (EntityContextFilterOperationParameters)parameters;
        }

        public async Task<PagedResult<TFilterableEntity>> FilterAsync(DbContext context, ICrudPredicateFeature<TEntity> predicateFactory, FilterParameters filterParameters)
        {
            var mandatoryPredicate = predicateFactory.GetMandatoryPredicate();

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

            var query = context.Set<TEntity>().AsExpandable().Where(mandatoryPredicate).Where(predicate);

            
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

            var entities = await _mapper.ProjectTo<TFilterableEntity>(query).ToArrayAsync();

            return new PagedResult<TFilterableEntity>(count,entities);
        }


        private ISpecification<TEntity> Create(FilterStatement filter)
        {
	        var property = typeof(TEntity).GetProperty(filter.ParameterName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

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
