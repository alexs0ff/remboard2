using System;
using System.Collections.Generic;
using System.Text;
using Common.Features;
using Entities.Dto;
using FluentValidation;

namespace Orders.Branches
{
	public class BranchDtoValidator : BaseEntityDtoValidator<BranchDto>
	{
		public BranchDtoValidator()
		{
			RuleFor(i => i.LegalName).NotEmpty();
			RuleFor(i => i.Title).NotEmpty();
		}
	}
}
