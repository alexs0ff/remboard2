using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace Common.Data
{
    public class RemboardContext: DbContext
    {
        public RemboardContext(DbContextOptions<RemboardContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
        }
    }
}
