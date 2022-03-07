using Fluent;
using Prism.Regions;
using System.Collections.Specialized;

namespace Issues_Honeys.Core.Prism
{
    public class RibbonGroupBoxRegionAdapter : RegionAdapterBase<RibbonGroupBox>
    {
        public RibbonGroupBoxRegionAdapter(IRegionBehaviorFactory regionBehaviorFactory) : base(regionBehaviorFactory)
        {

        }

        protected override void Adapt(IRegion region, RibbonGroupBox regionTarget)
        {
            region.Views.CollectionChanged += (s, e) =>
            {
                if (e.Action == NotifyCollectionChangedAction.Add)
                {
                    foreach (Button element in e.NewItems)
                    {
                        regionTarget.Items.Add(element);
                    }
                }

                if (e.Action == NotifyCollectionChangedAction.Remove)
                {
                    foreach (Button element in e.OldItems)
                    {
                        regionTarget.Items.Remove(element);
                    }
                }
            };
        }

        protected override IRegion CreateRegion()
        {
            return new SingleActiveRegion();
        }
    }
}
