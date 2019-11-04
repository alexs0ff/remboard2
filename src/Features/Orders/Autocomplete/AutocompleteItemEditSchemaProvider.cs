using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Common.Features.ResourcePoints.Schema;
using Entities;
using Entities.Dto;

namespace Orders.Autocomplete
{
	public class AutocompleteItemEditSchemaProvider: IEntityFormSchemaProvider<AutocompleteItemDto, AutocompleteItemDto>
	{
		private static readonly EntityEditFormModel FormModel = new EntityEditFormModel
		{
			DisplayedLayoutIds = new[] {"mainGroup"},
			EntityEdit = new EntityEdit
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
						"mainGroup", new FormLayout
						{
							Rows = new[]
							{
								new FormLayoutRow{Content = new FormLayoutHiddenItems
								{
									Items = new []{ "id", "autocompleteKindTitle" }
								}}, 
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
													Source = SelectBoxItemsSourceExtensions
														.SourceFromEnum<AutocompleteKind, AutocompleteKinds>()
												}
											},
										}
									}
								},
							}
						}
					}
				}
			}
		};

		public Task<EntityEditFormModel> GetCreateModelAsync(EntityEditSchemaProviderContext context)
		{
			return Task.FromResult(FormModel);
		}

		public Task<EntityEditFormModel> GetEditModelAsync(EntityEditSchemaProviderContext context)
		{
			return Task.FromResult(FormModel);
		}
	}
}
