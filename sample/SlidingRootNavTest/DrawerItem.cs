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
    public abstract class BaseDrawerItem
    {

        protected bool isChecked;

        public BaseDrawerItem SetChecked(bool isChecked)
        {
            this.isChecked = isChecked;

            return this;
        }

        public abstract object CreateViewHolder(ViewGroup parent);

        public abstract void BindViewHolder(object holder);

        public bool IsChecked => isChecked;

        public bool IsSelectable => true;

    }

    public abstract class DrawerItem<T> : BaseDrawerItem where T : DrawerAdapterViewHolder
    {
        

    }
}