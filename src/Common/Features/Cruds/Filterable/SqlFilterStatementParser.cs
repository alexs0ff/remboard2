using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.Extensions;
using Common.Features.BaseEntity;
using Common.Features.Tenant;

namespace Common.Features.Cruds.Filterable
{
    public sealed class SqlFilterStatementParser<TEntity, TFilterableEntity>
        where TEntity : BaseEntityGuidKey
        where TFilterableEntity : class
    {
        private readonly ITenantInfoProvider _tenantInfoProvider;

        /// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
        public SqlFilterStatementParser(ITenantInfoProvider tenantInfoProvider)
        {
            _tenantInfoProvider = tenantInfoProvider;
        }

        public (string where, object[] parameters) Parse(IList<FilterStatement> statements, EntitySqlFilterOperationParameters operationParameters)
        {
            var parameterList = new List<object>();

            var whereBuilder = new StringBuilder();
            whereBuilder.Append(" WHERE (");
            AddIsDeleted(parameterList, whereBuilder,operationParameters);
            TryAddTenant(parameterList, whereBuilder,operationParameters);

            whereBuilder.Append(" )");

            if (statements.Any())
            {
                whereBuilder.Append(" AND ( ");

                var isFirst = true;
                foreach (var filterStatement in statements)
                {
                    var added = AddStatement(isFirst, filterStatement, parameterList, whereBuilder, operationParameters);

                    if (added)
                    {
                        isFirst = false;
                    }
                }

                whereBuilder.Append(" ) ");
            }


            return (whereBuilder.ToString(), parameterList.ToArray());

        }

        private bool AddStatement(bool isFirst,FilterStatement filterStatement, List<object> parameterList, StringBuilder whereBuilder, EntitySqlFilterOperationParameters operationParameters)
        {
            var currentIndex = parameterList.Count;

            var value = FilterTypeCorrector.ChangeType<TFilterableEntity>(filterStatement.ParameterName, filterStatement.ParameterValue);

            if (value == null)
            {
                return false;
            }

            parameterList.Add(value);

            if (!isFirst)
            {
                if (filterStatement.LogicalOperator == FilterLogicalOperators.Or)
                {
                    whereBuilder.Append(" OR ");
                }
                else
                {
                    whereBuilder.Append(" AND ");
                }
            }

            var columnName = CreateColumnName(filterStatement.ParameterName, operationParameters);

            CreateLogicalOperation(whereBuilder,columnName, currentIndex, filterStatement.ComparisonOperator);

            return true;
        }

        private void CreateLogicalOperation(StringBuilder whereBuilder, string columnName, in int currentIndex, FilterComparisonOperators comparisonOperator)
        {
            switch (comparisonOperator)
            {
                case FilterComparisonOperators.Equals:
                    whereBuilder.Append(columnName+"={"+currentIndex+"} ");
                    break;
                case FilterComparisonOperators.Contains:
                    whereBuilder.Append("CHARINDEX({"+currentIndex+"},"+ columnName + ") > 0 ");
                    break;
                case FilterComparisonOperators.LessThan:
                    whereBuilder.Append(columnName + "< {" + currentIndex + "} ");
                    break;
                case FilterComparisonOperators.GreaterThan:
                    whereBuilder.Append(columnName + "> {" + currentIndex + "} ");
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(comparisonOperator), comparisonOperator, null);
            }
        }

        private void TryAddTenant(List<object> parameterList, StringBuilder whereBuilder, EntitySqlFilterOperationParameters operationParameters)
        {
            if (!typeof(TEntity).HasImplementation<ITenantedEntity>())
            {
                return;
            }

            Guid tenantId = _tenantInfoProvider.GetCurrentTenantId() ?? Guid.Empty;

            if (tenantId == Guid.Empty)
            {
                throw new Exception("TenantId is required");
            }

            parameterList.Add(tenantId);

            whereBuilder.Append($"AND {CreateColumnName("TenantId", operationParameters)} = {{1}} ");
        }

        private void AddIsDeleted(List<object> parameterList, StringBuilder whereBuilder, EntitySqlFilterOperationParameters operationParameters)
        {
            parameterList.Add(false);

            whereBuilder.Append($"{CreateColumnName("IsDeleted",operationParameters)} = {{0}} ");
        }

        private string CreateColumnName(string parameterName, EntitySqlFilterOperationParameters operationParameters)
        {
            if (string.IsNullOrWhiteSpace(operationParameters.AliasName))
            {
                return parameterName;
            }

            return operationParameters.AliasName + "." + parameterName;
        }
    }
}
