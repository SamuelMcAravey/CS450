using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DecisionTreeClassifier
{
    public class TreeNode : IEnumerable<TreeNode>
    {
        private readonly Dictionary<object, TreeNode> _children =
                                            new Dictionary<object, TreeNode>();

        public readonly object Attribute;
        public TreeNode Parent { get; private set; }

        public IEnumerable<object> BranchNames => this._children.Keys;

        public TreeNode(object attribute)
        {
            this.Attribute = attribute;
        }

        public TreeNode GetChild(object branchName)
        {
            return this._children[branchName];
        }

        public void Add(TreeNode item, object branchName)
        {
            item.Parent?._children.Remove(item.Attribute);
            item.Parent = this;
            this._children.Add(branchName, item);
        }

        public IEnumerator<TreeNode> GetEnumerator()
        {
            return this._children.Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public int Count => this._children.Count;

        public override string ToString()
        {
            return this.PrintPretty();
        }

        public static string BuildString(TreeNode tree)
        {
            var sb = new StringBuilder();

            BuildString(sb, tree, 0);

            return sb.ToString();
        }

        private static void BuildString(StringBuilder sb, TreeNode node, int depth)
        {
            var attribute = node.Attribute.ToString();
            sb.AppendLine(attribute.PadLeft(attribute.Length + depth, '-'));

            foreach (var child in node)
            {
                BuildString(sb, child, depth + 1);
            }
        }

        public string PrintPretty(string indent = "", bool last = true, string branchName = "")
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(indent);
            if (last)
            {
                sb.Append("└╴");
                indent += "  ";
            }
            else
            {
                sb.Append("├╴");
                indent += "│ ";
            }

            if (!string.IsNullOrWhiteSpace(branchName))
                sb.Append(branchName + ": ");

            sb.AppendLine(this.Attribute.ToString());

            var children = this._children.Values.ToList();
            var branchNames = this.BranchNames.ToList();
            for (int i = 0; i < children.Count; i++)
            {
                sb.Append(children[i].PrintPretty(indent, i == children.Count - 1, branchNames[i].ToString()));
            }

            return sb.ToString();
        }
    }
}
