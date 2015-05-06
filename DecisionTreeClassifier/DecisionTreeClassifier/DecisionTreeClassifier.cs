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
        protected override TClass Classify(TItem input)
        {
            throw new NotImplementedException();
        }

        public override void Train(IClassifiedDataset<TItem, TClass> trainingDataset)
        {
        }
    }
}
