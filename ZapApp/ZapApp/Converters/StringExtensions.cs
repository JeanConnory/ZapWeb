using System;
using System.Collections.Generic;
using System.Text;

namespace System.Text
{
    public static class StringExtensions
    {
        public static string FirstCharToUpper(this string input)
        {
            switch (input)
            {
                case null: throw new ArgumentNullException(nameof(input));
                case "": return "Sem nome";
                default: return input[0].ToString().ToUpper() + input.Substring(1);
            }
        }
    }
}
