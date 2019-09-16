using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Common.Extensions
{
    public static class SystemHelpers
    {
        public static TValue GetInstanceField<TInstance,TValue>(TInstance instance, string fieldName)
        {
            BindingFlags bindFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic
                                     | BindingFlags.Static;
            FieldInfo field = typeof(TInstance).GetField(fieldName, bindFlags);
            if (field == null)
            {
                return default(TValue);
            }
            return (TValue)field.GetValue(instance);
        }
    }
}
