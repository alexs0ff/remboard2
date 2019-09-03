using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
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

        /// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
        public EntityContextFilterOperation(IMapper mapper)
        {
            _mapper = mapper;
        }

        public async Task<PagedResult<TFilterableEntity>> FilterAsync(DbContext context, ICrudPredicateFeature<TEntity> predicateFactory, FilterParameters filterParameters)
        {
            var predicate = predicateFactory.GetMandatoryPredicate();

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

            var count = await context.Set<TEntity>().AsExpandable().Where(predicate).LongCountAsync();

            var query = context.Set<TEntity>().AsExpandable().Where(predicate);

            var pageData = GetPageData(filterParameters.PageSize, filterParameters.CurrentPage);

            if (pageData.skip>=0)
            {
                query = query.Skip(pageData.skip).Take(pageData.take);
            }

            var entities = await _mapper.ProjectTo<TFilterableEntity>(query).ToArrayAsync();

            return new PagedResult<TFilterableEntity>(count,entities);
        }

        private ISpecification<TEntity> Create(FilterStatement filter)
        {
            var targetType = typeof(TEntity).GetPropertyType(filter.ParameterName);

            if (targetType == null)
            {
                return null;
            }

            object value = filter.ParameterValue;

            if (targetType== typeof(int))
            {
                if (!int.TryParse(filter.ParameterValue,NumberStyles.None, CultureInfo.InvariantCulture, out var res))
                {
                    return null;
                }
                value = res;
            }

            if (targetType == typeof(long))
            {
                if (!long.TryParse(filter.ParameterValue, NumberStyles.None, CultureInfo.InvariantCulture, out var res))
                {
                    return null;
                }
                value = res;
            }

            if (filter.СomparisonOperator == FilterСomparisonOperators.Equals)
            {
                return new EqualsSpecification<TEntity>(targetType,value,filter.ParameterName);
            }

            if (filter.СomparisonOperator == FilterСomparisonOperators.GreaterThan)
            {
                return new GreaterThanSpecification<TEntity>(targetType, value, filter.ParameterName);
            }

            if (filter.СomparisonOperator == FilterСomparisonOperators.LessThan)
            {
                return new LessThanSpecification<TEntity>(targetType, value, filter.ParameterName);
            }

            if (filter.СomparisonOperator == FilterСomparisonOperators.Contains)
            {
                return new ContainsSpecification<TEntity>(filter.ParameterValue, filter.ParameterName);
            }

            return null;
        }

        private (int skip, int take) GetPageData(int pageSize, int currentPage)
        {
            if (pageSize <=0 || currentPage <=0)
            {
                return (-1,-1);
            }

            var skip = pageSize * (currentPage - 1);

            return (skip, pageSize);
        }
    }
}
