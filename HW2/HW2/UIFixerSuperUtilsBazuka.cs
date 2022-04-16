using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HW2
{
    static class UIFixerSuperUtilsBazuka
    {
        public static string Shorten(string text, int max_length, int max_line_count)
        {
            string result = text;
            var lines = result.Split('\n');
            
            if (lines.Length > max_line_count)
            {
                result = string.Join("\n", lines.Take(max_line_count));

            }

            if (result.Length > max_length)
            {
                result = result.Remove(max_length);
                
            }

            if (result != text)
            {
                result += "...";
            }

            return result;
        }
    }
}
