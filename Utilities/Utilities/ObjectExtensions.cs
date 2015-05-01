using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities
{
    public static class ObjectExtensions
    {
        public static bool IsNumber(this object value)
        {
            return value is double
                    || value is byte
                    || value is short
                    || value is ushort
                    || value is int
                    || value is uint
                    || value is long
                    || value is ulong
                    || value is float
                    || value is sbyte
                    || value is decimal;
        }
    }
}
