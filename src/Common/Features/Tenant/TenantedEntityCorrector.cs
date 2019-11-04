using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Common.Features.BaseEntity;
using Entities;

namespace Common.Features.Tenant
{
	public class TenantedEntityCorrector<TCreateEntityDto, TEditEntityDto, TKey> : IEntityCorrector<BaseEntity<TKey>, TCreateEntityDto, TEditEntityDto, TKey>
		where TCreateEntityDto : class
		where TEditEntityDto : class
		where TKey : struct
	{
		private readonly ITenantInfoProvider _tenantInfoProvider;

		public TenantedEntityCorrector(ITenantInfoProvider tenantInfoProvider)
		{
			_tenantInfoProvider = tenantInfoProvider;
		}

		public Task CorrectEntityAsync(EntityCorrectorContext context, BaseEntity<TKey> entity, TCreateEntityDto receivedEntityDto)
		{
			((ITenantedEntity) entity).TenantId = _tenantInfoProvider.GetCurrentTenantId() ?? Guid.Empty;
			return Task.CompletedTask;
		}

		public Task CorrectEntityAsync(EntityCorrectorContext context, BaseEntity<TKey> entity, TEditEntityDto receivedEntityDto)
		{
			((ITenantedEntity)entity).TenantId = _tenantInfoProvider.GetCurrentTenantId() ?? Guid.Empty;
			return Task.CompletedTask;
		}

		public Task CorrectEntityDtoAsync(EntityCorrectorContext context, TEditEntityDto entityDto, BaseEntity<TKey> entity)
		{
			return Task.CompletedTask;
		}
	}
}
