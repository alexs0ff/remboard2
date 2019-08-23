using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Common.Extensions
{
    public static class IndexExtensions
    {
        public static IndexBuilder<TEntity> AddUniqueWithoutDeleted<TEntity>(this IndexBuilder<TEntity> builder)
        {
            return builder.IsUnique().HasFilter("IsDeleted = 0");
        }
    }
}
