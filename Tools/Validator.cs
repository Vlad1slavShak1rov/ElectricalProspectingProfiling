using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectricalProspectingProfiling.Tools
{
    public static class Validator
    {
        public static bool IsNullOrEmptyOrWhiteSpace(params string[] strings)
        {
            return strings.Any(st=>string.IsNullOrEmpty(st) || string.IsNullOrWhiteSpace(st));
        }
    }
}
