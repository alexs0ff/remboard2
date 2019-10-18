using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Common.Features.BaseEntity;

namespace Common.Features.Tenant
{
	public class TenantedEntityCorrector<TEntityDto, TKey> : IEntityCorrector<BaseEntity<TKey>, TEntityDto, TKey>
		where TEntityDto : class
		where TKey : struct
	{
		private readonly ITenantInfoProvider _tenantInfoProvider;

		public TenantedEntityCorrector(ITenantInfoProvider tenantInfoProvider)
		{
			_tenantInfoProvider = tenantInfoProvider;
		}

		public Task CorrectEntityAsync(BaseEntity<TKey> entity, TEntityDto receivedEntityDto)
		{
			((ITenantedEntity) entity).TenantId = _tenantInfoProvider.GetCurrentTenantId() ?? Guid.Empty;
			return Task.CompletedTask;
		}

		public Task CorrectEntityDtoAsync(TEntityDto entityDto, BaseEntity<TKey> entity)
		{
			return Task.CompletedTask;
			;
		}
	}
}
