﻿using System;
using System.Collections.Generic;
using System.Text;
using Common.Features.ResourcePoints.Filterable.Schema;
using Common.Features.ResourcePoints.Schema;

namespace Orders.Autocomplete
{
	public class AutocompleteItemSchemaProvider: IEntitySchemaProvider<AutocompleteItemDto>
	{
		private static ServerDataGridModel _model = new ServerDataGridModel
		{
			EntitiesName = "autocompleteItems",
			Columns = new GridColumn[]
			{
				new GridColumn{Id = "title",Name = "Название",Options = new GridContentOptions{CanOrder = true,ValueKind = GridColumnContentKind.String}}, 
				new GridColumn{Id = "autocompleteKindTitle",Name = "Тип",Options = new GridContentOptions{CanOrder = true,ValueKind = GridColumnContentKind.String}}, 
			},
			PageSize = 50,
			Panel = new GridControlPanel { ShowAddButton = true},
			Filter = new GridFilter
			{
				Columns = new ControlBase[]
				{
					new TextBoxControl{Id = "title",Kind = TextBoxControlKind.Textbox,Label = "Название",ValueKind = ControlValueKind.String,Validators = new TextBoxControlValidators{Required = true}},  
					new SelectBoxControl{Id = "autocompleteKindId",Label = "Тип автодополнения",ValueKind = ControlValueKind.Number,Validators = new SelectBoxControlValidators{Required = true},Source = SelectBoxItemsSourceExtensions.SourceFromEnum<AutocompleteKind,AutocompleteKinds>()} 
				}
			}

		};
		public ServerDataGridModel GetModel(EntitySchemaProviderContext context)
		{
			return _model;
		}
	}
}
