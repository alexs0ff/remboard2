using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.Data;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Remboard.Infrastructure
{
    public class DbInterceptor : IObserver<KeyValuePair<string, object>>
    {
        public void OnCompleted()
        {
        }

        public void OnError(Exception error)
        {
        }

        public void OnNext(KeyValuePair<string, object> value)
        {
            if (value.Key == RelationalEventId.CommandExecuting.Name)
            {
                
                
                var context = ((CommandEventData)value.Value).Context;
                if (context is RemboardContext)
                {
                    var command = ((CommandEventData)value.Value).Command;
                    var executeMethod = ((CommandEventData)value.Value).ExecuteMethod;

                    if (executeMethod == DbCommandMethod.ExecuteNonQuery)
                    {
                        //ResetConnection(command, masterConnectionString);
                    }
                    else if (executeMethod == DbCommandMethod.ExecuteScalar)
                    {
                       // ResetConnection(command, slaveConnectionString);
                    }
                    else if (executeMethod == DbCommandMethod.ExecuteReader)
                    {
                        //ResetConnection(command, slaveConnectionString);
                    }
                }

                
                // Do DbCommand manipulation here
            }
        }
    }
}
