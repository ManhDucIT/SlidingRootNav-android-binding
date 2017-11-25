using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace SlidingRootNavLibTest
{
    public class SpaceItem : BaseDrawerItem
    {

        private int spaceDp;

        public SpaceItem(int spaceDp)
        {
            this.spaceDp = spaceDp;
        }


        public override object CreateViewHolder(ViewGroup parent)
        {
            View view = new View(parent.Context);

            int height = (int) (parent.Context.Resources.DisplayMetrics.Density * spaceDp);

            view.LayoutParameters = new ViewGroup.LayoutParams(ViewGroup.LayoutParams.MatchParent, height);

            return new SpaceItemViewHolder(view);
        }

        public override void BindViewHolder(object holder)
        {

        }
    }

    public class SpaceItemViewHolder : DrawerAdapterViewHolder
    {

        public SpaceItemViewHolder(View itemView) : base(itemView)
        {
        }

    }
}