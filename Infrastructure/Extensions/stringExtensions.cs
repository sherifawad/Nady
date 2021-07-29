using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Infrastructure.Extensions
{
    public static class stringExtensions
    {
        private static readonly Regex sWhitespace = new Regex(@"\s+");
        public static string RemoveWhiteSpace(this string input)
        {
            return sWhitespace.Replace(input, "");
        }
    }
}
