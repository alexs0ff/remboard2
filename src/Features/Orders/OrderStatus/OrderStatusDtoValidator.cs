using System;
using System.Collections.Generic;
using System.Text;
using Common.Features;
using FluentValidation;

namespace Orders.OrderStatus
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
