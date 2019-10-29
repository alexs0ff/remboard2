using System;
using System.Collections.Generic;
using System.Text;
using Common.Features.ResourcePoints.Schema;

namespace Orders.Autocomplete
{
	public class AutocompleteItemEditSchemaProvider: IEntityEditSchemaProvider<AutocompleteItemDto>
	{
		private static readonly EntityEditModel _model = new EntityEditModel
		{
			EntitiesName = "autocompleteItems",
			Title = "Пункт автодополнения",
			RemoveDialog = new RemoveDialogOptions
			{
				ValueId = "title",
			},

			Layouts = new Dictionary<string, FormLayout>
			{
				{
					"MainGroup", new FormLayout
					{
						Rows = new[]
						{
							new FormLayoutRow
							{
								Content = new FormLayoutControls
								{
									Items = new[]
									{
										new FormLayoutItem
										{
											FlexExpression = FormItemFlexExpression.TwoItemsExpressions,
											Control = new TextBoxControl
											{
												Kind = TextBoxControlKind.Textbox,
												Id = "title",
												Label = "Название",
												Hint = "Название пункта автодополнения",
												ValueKind = ControlValueKind.String,
												Validators = new TextBoxControlValidators
												{
													Required = true
												}
											},
											
										},
										new FormLayoutItem
										{
											FlexExpression = FormItemFlexExpression.TwoItemsExpressions,
											Control = new SelectBoxControl
											{
												Kind = SelectBoxControlKind.Selectbox,
												Id = "autocompleteKindId",
												ValueKind = ControlValueKind.Number,
												Label = "Тип автодополнения",
												Hint = "Тип автодополнения",
												Validators = new SelectBoxControlValidators
												{
													Required = true,
												},
												Source = SelectBoxItemsSourceExtensions.SourceFromEnum<AutocompleteKind,AutocompleteKinds>()
											}
										}, 
									}
								}
							},
						}
					}
				}
			}
		};

		public EntityEditModel GetModel(EntityEditSchemaProviderContext context)
		{
			return _model;
		}
	}
}
