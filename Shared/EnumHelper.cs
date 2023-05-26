using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace diarioAlimentar.Shared
{
    public static class EnumHelper
    {
        public static string GetEnumDisplay(this Enum e)
        {
            return .GetType().GetMember(e.ToString())
                   .First()
                   .GetCustomAttribute<DisplayAttribute>()
                   .Name;
        }
    }
}
