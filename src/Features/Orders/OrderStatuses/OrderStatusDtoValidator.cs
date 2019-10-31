using System;
using System.Collections.Generic;
using System.Text;
using Common.Features;
using Entities.Dto;
using FluentValidation;

namespace Orders.OrderStatuses
{
	public class OrderStatusDtoValidator : BaseEntityDtoValidator<OrderStatusDto>
	{
		public OrderStatusDtoValidator()
		{
			RuleFor(i => i.Title).NotEmpty();
			RuleFor(i => i.OrderStatusKindId).IsInEnum();
		}
	}
}
