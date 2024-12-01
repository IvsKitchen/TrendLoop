using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrendLoop.Common
{
    public class StringHelper
    {
        public static string SplitCamelCase(string input)
        {
            return string.Join(" ", System.Text.RegularExpressions.Regex.Split(input, "(?<!^)(?=[A-Z])"));
        }
    }
}
