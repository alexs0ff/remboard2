using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Common.Features.BaseEntity;

namespace Common.Features
{
    public interface IEntityCorrector<TEntity>
        where TEntity : BaseEntityGuidKey
    {
        Task CorrectBefore(TEntity entity);

        Task CorrectAfter(TEntity entity);
    }
}
