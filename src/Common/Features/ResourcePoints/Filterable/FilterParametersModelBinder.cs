using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Logging;

namespace Common.Features.ResourcePoints.Filterable
{
	public class FilterParametersModelBinder : IModelBinder
	{
		/// <summary>Attempts to bind a model.</summary>
		/// <param name="bindingContext">The <see cref="T:Microsoft.AspNetCore.Mvc.ModelBinding.ModelBindingContext" />.</param>
		/// <returns>
		/// <para>
		/// A <see cref="T:System.Threading.Tasks.Task" /> which will complete when the model binding process completes.
		/// </para>
		/// <para>
		/// If model binding was successful, the <see cref="P:Microsoft.AspNetCore.Mvc.ModelBinding.ModelBindingContext.Result" /> should have
		/// <see cref="P:Microsoft.AspNetCore.Mvc.ModelBinding.ModelBindingResult.IsModelSet" /> set to <c>true</c>.
		/// </para>
		/// <para>
		/// A model binder that completes successfully should set <see cref="P:Microsoft.AspNetCore.Mvc.ModelBinding.ModelBindingContext.Result" /> to
		/// a value returned from <see cref="M:Microsoft.AspNetCore.Mvc.ModelBinding.ModelBindingResult.Success(System.Object)" />.
		/// </para>
		/// </returns>
		public Task BindModelAsync(ModelBindingContext bindingContext)
		{
			if (bindingContext == null)
			{
				throw new ArgumentNullException(nameof(bindingContext));
			}

			if (bindingContext.ModelType != typeof(FilterParameters))
			{
				return Task.CompletedTask;
			}


			// Try to fetch the value of the argument by name
			var queryParameters = bindingContext.HttpContext.Request.Query;

			if (!queryParameters.Any())
			{
				var par = new FilterParameters();
				bindingContext.Result = ModelBindingResult.Success(par);
				return Task.CompletedTask;
			}

			FilterParameters model = ParseModel(queryParameters);

			if (model == null)
			{
				return Task.CompletedTask;
			}

			bindingContext.Result = ModelBindingResult.Success(model);

			return Task.CompletedTask;
		}

		private FilterParameters ParseModel(IQueryCollection queryParameters)
		{
			var columnNames = queryParameters[FilterColumnName];
			var groups = columnNames.GroupBy(s => s).Select(
				s => new {Name = s.Key, Count = s.Count()});

			var columns = groups.ToDictionary(arg => arg.Name, arg1 => arg1.Count);

			var filter = new FilterParameters();

			foreach (var name in columns.Keys)
			{
				var expectedCount = columns[name];
				var valueParameter = string.Format(FilterColumnValue, name);
				var values = new string[expectedCount];

				if (queryParameters.ContainsKey(valueParameter))
				{
					var parameterValues = queryParameters[valueParameter];
					values = parameterValues;
				}

				var operatorParameter = string.Format(FilterParameterOperator, name);
				var comparisonOperators = ParseEnum(queryParameters, operatorParameter, expectedCount, FilterComparisonOperators.None);
				var logicalParameter = string.Format(FilterParameterNext, name);
				var logicalOperators = ParseEnum(queryParameters, logicalParameter, expectedCount, FilterLogicalOperators.None);

				for (int i = 0; i < expectedCount; i++)
				{
					if (!string.IsNullOrWhiteSpace(values[i]) && comparisonOperators[i] != FilterComparisonOperators.None)
					{
						var filterStatement = new FilterStatement();
						filterStatement.ParameterName = name;
						filterStatement.ParameterValue = values[i];
						filterStatement.ComparisonOperator = comparisonOperators[i];
						filterStatement.LogicalOperator = logicalOperators[i];
						filter.Statements.Add(filterStatement);
					}
				}

				
			}

			if (queryParameters.ContainsKey(PageSize))
			{
				if (int.TryParse(queryParameters[PageSize], NumberStyles.None, CultureInfo.InvariantCulture,
					out var res))
				{
					filter.PageSize = res;
				}
			}

			filter.PageSize = ParseInt(queryParameters, PageSize, -1);
			filter.CurrentPage = ParseInt(queryParameters, CurrentPage, -1);

			if (queryParameters.ContainsKey(OrderBy))
			{
				filter.OrderKind = ParseEnum(queryParameters, OrderKind,1, Filterable.OrderKind.Asc).First();
				filter.OrderBy = queryParameters[OrderBy];
			}

			return filter;
		}

		private int ParseInt(IQueryCollection queryParameters, string parameterName, int defaultValue)
		{
			var result = defaultValue;
			if (queryParameters.ContainsKey(parameterName))
			{
				if (int.TryParse(queryParameters[parameterName], NumberStyles.None, CultureInfo.InvariantCulture,
					out var res))
				{
					result = res;
				}
			}

			return result;
		}

		private List<TEnum> ParseEnum<TEnum>(IQueryCollection queryParameters, string parameterName, int expectedCount,
			TEnum defaultValue)
			where TEnum : Enum
		{
			List<TEnum> result = new List<TEnum>();

			if (queryParameters.ContainsKey(parameterName))
			{
				var comparisonEnumRaw = queryParameters[parameterName];
				for (int i = 0; i < expectedCount; i++)
				{
					if ((comparisonEnumRaw.Count>i) && Enum.TryParse(typeof(TEnum), comparisonEnumRaw[i], true, out var co))
					{
						result.Add((TEnum) co);
					}
					else
					{
						result.Add(defaultValue);
					}
				}


			}
			else
			{
				result.AddRange(Enumerable.Repeat(defaultValue, expectedCount));
			}

			return result;
		}


		private const string FilterColumnName = "filtercolumnname";

		private const string FilterColumnValue = "filtercolumn{0}value";

		private const string FilterParameterOperator = "filtercolumn{0}operator";

		private const string FilterParameterNext = "filtercolumn{0}logic";

		private const string PageSize = "pagesize";

		private const string CurrentPage = "page";

		private const string OrderBy = "OrderBy";

		private const string OrderKind = "OrderKind";
	}
}
