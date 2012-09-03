using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PresentationGenerator.Core.Utility
{
    public static class IdUtil
    {
        public static string CreateId(string type, string number)
        {
            return string.Format("{0}/{1}", type, number);
        }
    }
}
