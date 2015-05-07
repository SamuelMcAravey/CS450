using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DecisionTreeClassifier
{
    /// <summary>
    /// Defines an adapter that must be implemented in order to use the LinqToTree
    /// extension methods
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ITree<T>
    {
        /// <summary>
        /// Obtains all the children of the Item.
        /// </summary>
        /// <returns></returns>
        IEnumerable<ITree<T>> Children();

        /// <summary>
        /// The parent of the Item.
        /// </summary>
        ITree<T> Parent { get; }

        /// <summary>
        /// The item being adapted.
        /// </summary>
        T Data { get; }
    }
}
