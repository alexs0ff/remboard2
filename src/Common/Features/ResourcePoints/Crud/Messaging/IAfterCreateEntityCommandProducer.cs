using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Common.Features.ResourcePoints.Crud.Messaging
{
	public interface IAfterCreateEntityCommandProducer<TCreateEntityDto>
		where TCreateEntityDto : class
	{
		Task Send(TCreateEntityDto entityDto);
	}
}
