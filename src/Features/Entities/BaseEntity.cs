using System;

namespace Entities
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
