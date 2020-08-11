using Akan.Exceptions;

using System;
using System.Text.RegularExpressions;
using System.Text;

namespace Akan.Util
{
    public class EmoteConverter
    {
        private EmoteConverter()
        {
            throw new IllegalAccessException("Don't instantiate");
        }

        public static string ConvertToRegionalIndicators(string message)
        {
            char[] chars = message.ToLower()
                .Replace("ö", "oe").Replace("ü", "ue").Replace("ä", "ae").Replace("ß", "ss")
                .ToCharArray();
            StringBuilder output = new StringBuilder();
            foreach (char c in chars)
            {
                if (Regex.Match(Char.ToString(c), "[a-z]").Success)
                {
                    output.Append($":regional_indicator_{c}:");
                } else if (c == '!')
                {
                    output.Append(":exclamation:");
                } else if (c == '?')
                {
                    output.Append(":question:");
                } else if (c == ' ')
                {
                    output.Append("\n");
                } else
                {
                    output.Append(c);
                }

            }

            return output.ToString();

        }
    }
}