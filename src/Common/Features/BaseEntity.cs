using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Features
{
    public abstract class BaseEntity<TKey> 
        where TKey:struct
    {
        public TKey Id { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime? DateModified { get; set; }

        public byte[] RowVersion { get; set; }
    }
}
