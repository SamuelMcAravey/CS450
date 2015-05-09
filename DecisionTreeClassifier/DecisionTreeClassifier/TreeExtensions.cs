using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DecisionTreeClassifier
{
    //public static class TreeExtensions
    //{
    //    /// <summary>
    //    /// Returns a collection of descendant elements.
    //    /// </summary>
    //    public static IEnumerable<ITree<T>> Descendants<T>(this ITree<T> adapter)
    //    {
    //        foreach (var child in adapter.Children())
    //        {
    //            yield return child;

    //            foreach (var grandChild in child.Descendants())
    //            {
    //                yield return grandChild;
    //            }
    //        }
    //    }
        
    //    /// <summary>
    //    /// Returns a collection of descendant elements.
    //    /// </summary>
    //    public static IEnumerable<ITree<T>> Descendants<T, K>(this ITree<T> adapter)
    //    {
    //        return adapter.Descendants().Where(i => i.Data is K);
    //    }

    //    /// <summary>
    //    /// Returns a collection of ancestor elements.
    //    /// </summary>
    //    public static IEnumerable<ITree<T>> Ancestors<T>(this ITree<T> adapter)
    //    {
    //        var parent = adapter.Parent;
    //        while (parent != null)
    //        {
    //            yield return parent;
    //            parent = parent.Parent;
    //        }
    //    }

    //    /// <summary>
    //    /// Returns a collection of child elements.
    //    /// </summary>
    //    public static IEnumerable<ITree<T>> Elements<T>(this ITree<T> adapter)
    //    {
    //        foreach (var child in adapter.Children())
    //        {
    //            yield return child;
    //        }
    //    }
    //}
}
