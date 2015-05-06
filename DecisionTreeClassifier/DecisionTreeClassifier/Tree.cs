using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DecisionTreeClassifier
{
    public static class Tree
    {
        public static Tree<T> Create<T>(T data, params Tree<T>[] branches)
        {
            return new Tree<T>(data, branches);
        }
    }
    public class Tree<T> : ITree<T> // use null for Leaf
    {
        public T Data { get; private set; }
        public IList<Tree<T>> Branches { get; private set; }
        public Tree(T data, params Tree<T>[] branches)
        {
            this.Data = data;
            this.Parent = null;
            if (branches != null)
            {
                this.Branches = branches.Select(b =>
                                                {
                                                    b.Parent = this;
                                                    return b;
                                                }).ToList();
            }
            else
            {
                this.Branches = new List<Tree<T>>();
            }
        }

        public IEnumerable<ITree<T>> Children()
        {
            return this.Branches;
        }

        public ITree<T> Parent { get; private set; }

        public T Item
        {
            get { return this.Data; }
        }
    }
}
