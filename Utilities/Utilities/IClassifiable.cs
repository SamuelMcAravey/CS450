using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities
{
    public interface IClassifiable
    {
        IEnumerable<Tuple<string, object>> Values { get; }
        IReadOnlyDictionary<string, object> ValueDictionary { get; }
        void SetValue(string property, object value);
    }
}
