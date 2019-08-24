using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Remboard.Infrastructure
{
    public class EfGlobalListener : IObserver<DiagnosticListener>
    {
        private readonly DbInterceptor _dbInterceptor = new DbInterceptor();

        public void OnCompleted()
        {
        }

        public void OnError(Exception error)
        {
        }

        public void OnNext(DiagnosticListener listener)
        {
            if (listener.Name == DbLoggerCategory.Name)
            {
                listener.Subscribe(_dbInterceptor);
            }
        }
    }
}
