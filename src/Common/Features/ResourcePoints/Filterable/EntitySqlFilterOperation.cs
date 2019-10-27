using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Common.Extensions;
using Common.Features.BaseEntity;
using LinqKit;
using Microsoft.EntityFrameworkCore;

namespace Common.Features.ResourcePoints.Filterable
{
    public class EntitySqlFilterOperation<TEntity, TFilterableEntity, TKey> :IEntityFilterOperation<TEntity, TFilterableEntity,TKey>
        where TEntity : BaseEntity<TKey>
        where TFilterableEntity:class
        where TKey : struct
	{
        private readonly EntitySqlFilterOperationParameters _parameters;

        private readonly SqlFilterStatementParser<TEntity, TFilterableEntity,TKey> _filterStatementParser;

        /// <summary>Initializes a new instance of the <see cref="T:Common.Features.Cruds.Filterable.EntitySqlFilterOperation" /> class.</summary>
        public EntitySqlFilterOperation(EntitySqlFilterOperationParameters parameters, SqlFilterStatementParser<TEntity, TFilterableEntity,TKey> filterStatementParser)
        {
            _parameters = parameters;
            _filterStatementParser = filterStatementParser;
            ShouldContains(_parameters.Sql, WhereClause);
            ShouldContains(_parameters.Sql, OrderByClause);
            ShouldContains(_parameters.Sql, PaggingClause);
        }

        public async Task<PagedResult<TFilterableEntity>> FilterAsync(DbContext context, IResourceMandatoryPredicateFactory<TEntity, TKey> mandatoryPredicateFactory, FilterParameters filterParameters)
        {
            var orderByClause = CreateOrderBy<TFilterableEntity>(filterParameters);
            var paggingClause = CreatePagging(filterParameters);
            var whereParameters = CreateWhere(filterParameters);

            var sqlCount = _parameters.Sql
                .Replace(OrderByClause, string.Empty, StringComparison.OrdinalIgnoreCase)
                .Replace(PaggingClause, string.Empty, StringComparison.OrdinalIgnoreCase)
                .Replace(WhereClause, whereParameters.sql, StringComparison.OrdinalIgnoreCase);

            var sqlResult = _parameters.Sql
                .Replace(OrderByClause, orderByClause, StringComparison.OrdinalIgnoreCase)
                .Replace(PaggingClause, paggingClause, StringComparison.OrdinalIgnoreCase)
                .Replace(WhereClause, whereParameters.sql, StringComparison.OrdinalIgnoreCase);

            var count = await context.Set<TFilterableEntity>().FromSqlRaw(sqlCount, whereParameters.parameters).LongCountAsync();

            var result = await context.Set<TFilterableEntity>().FromSqlRaw(sqlResult, whereParameters.parameters).ToArrayAsync();

            return new PagedResult<TFilterableEntity>(count,result);
        }

        private (string sql, object[] parameters) CreateWhere(FilterParameters filterParameters)
        {
            return _filterStatementParser.Parse(filterParameters.Statements,_parameters);
        }

        private string CreatePagging(FilterParameters filterParameters)
        {
            var pageData = PageDataExtensions.GetPageData(filterParameters.PageSize, filterParameters.CurrentPage);

            if (pageData.skip >= 0)
            {
                return $"OFFSET {pageData.skip} ROWS FETCH NEXT {pageData.take} ROWS ONLY";
            }

            return string.Empty;
        }

        private string CreateOrderBy<TFilterableEntity>(FilterParameters filterParameters)
        {
	        var orderColumn = _parameters.DefaultOrderColumn;

	        if (!string.IsNullOrWhiteSpace(filterParameters.OrderBy))
	        {
		        orderColumn = filterParameters.OrderBy;
	        }

	        if (!typeof(TFilterableEntity).PropertyExists(orderColumn)) //prevent sql injection
	        {
				throw new Exception($"The property {orderColumn} should exists in {typeof(TFilterableEntity).Name}");
	        }

            return $" ORDER BY {orderColumn} {filterParameters.OrderKind} ";
        }

        private void ShouldContains(string sql, string clause)
        {
            if (!sql.Contains(clause,StringComparison.OrdinalIgnoreCase))
            {
                throw new Exception("Sql should contains " + clause);
            }
        }

        private const string WhereClause = "{WhereClause}";

        private const string OrderByClause = "{OrderByClause}";

        private const string PaggingClause = "{PaggingClause}";
    }
}
