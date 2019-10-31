using System;
using System.Collections.Generic;
using System.Text;
using Common.Features;
using Entities.Dto;
using FluentValidation;

namespace Orders.OrderTypes
{
	public class OrderTypeDtoValidator : BaseEntityDtoValidator<OrderTypeDto>
	{
		public OrderTypeDtoValidator()
		{
			RuleFor(i => i.Title).NotEmpty();
		}
	}
}
