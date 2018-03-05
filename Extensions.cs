using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using HiveOsAutomation.ApiClients.HiveOs;

namespace HiveOsAutomation
{
    public static class MethodExnteions
    {
        public static  KeyValuePair<string, string> GetDisplayNameAndGroupName(this Enum value)
        {
            var type = value.GetType();
            var name = Enum.GetName(type, value);

            var attr = type
                .GetField(name)
                .GetCustomAttributes(false)
                .OfType<DisplayAttribute>()
                .SingleOrDefault();

            if (attr != null)
            {
                return new KeyValuePair<string, string>(attr.Name, attr.GroupName);
            }

            throw new Exception();
        }

        public static eMinerSoftware GetMinerSoftware(this string value)
        {
            var values = 
                Enum.GetValues(typeof(eMinerSoftware))
                .Cast<eMinerSoftware>()
                .Select(a => new {
                    value = a,
                    strValue = a.GetDisplayNameAndGroupName().Key
                });

            return 
                values.Where(a => a.strValue == value)
                .Select(a => a.value)
                .SingleOrDefault();
        }
    }
}