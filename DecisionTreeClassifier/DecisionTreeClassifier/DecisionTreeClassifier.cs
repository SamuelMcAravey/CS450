using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;

namespace DecisionTreeClassifier
{
    public class ID3DecisionTreeClassifier<TItem, TClass> : Classifier<TItem, TItem, TClass> where TItem : IClassified<TClass>, IClassifiable
    {
        //private Tree 
        protected override TClass Classify(TItem input)
        {
        }

        public override void Train(IClassifiedDataset<TItem, TClass> trainingDataset)
        {
        }
    }
}
