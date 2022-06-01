using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Extentions
{
    public static class StringExtentions
    {
        public static string Mask (this string value)
        {
            var builder = new StringBuilder();
            var length = value.Length;


            if (value.Contains('/'))
            {
                builder.Append("*/");
                length = 4;
            }
            else if (length > 4)
            {
                builder.Append(value[..2]);
                length -= 2;
            }
           

            builder.Append(new string('*', length));

            return builder.ToString();
        }
    }
}
