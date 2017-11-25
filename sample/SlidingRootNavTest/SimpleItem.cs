using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using SlidingRootNavTest;

namespace SlidingRootNavLibTest
{
    public class SimpleItem : BaseDrawerItem
    {

        private Color selectedItemIconTint;
        private Color selectedItemTextTint;

        private Color normalItemIconTint;
        private Color normalItemTextTint;

        private Drawable icon;
        private String title;

        public SimpleItem(Drawable icon, String title)
        {
            this.icon = icon;
            this.title = title;
        }

        public override object CreateViewHolder(ViewGroup parent)
        {
            LayoutInflater inflater = LayoutInflater.From(parent.Context);

            View v = inflater.Inflate(Resource.Layout.item_option, parent, false);

            return new SimpleItemViewHolder(v);
        }
        
        public override void BindViewHolder(object holder)
        {
            var viewHolder = holder as SimpleItemViewHolder;

            viewHolder.title.Text = title;
            viewHolder.icon.SetImageDrawable(icon);

            viewHolder.title.SetTextColor(isChecked ? selectedItemTextTint : normalItemTextTint);
            viewHolder.icon.SetColorFilter(isChecked ? selectedItemIconTint : normalItemIconTint);
        }

        public SimpleItem WithSelectedIconTint(Color selectedItemIconTint)
        {
            this.selectedItemIconTint = selectedItemIconTint;

            return this;
        }

        public SimpleItem WithSelectedTextTint(Color selectedItemTextTint)
        {
            this.selectedItemTextTint = selectedItemTextTint;

            return this;
        }

        public SimpleItem WithIconTint(Color normalItemIconTint)
        {
            this.normalItemIconTint = normalItemIconTint;

            return this;
        }

        public SimpleItem WithTextTint(Color normalItemTextTint)
        {
            this.normalItemTextTint = normalItemTextTint;

            return this;
        }
        
    }

    public class SimpleItemViewHolder : DrawerAdapterViewHolder
    {

        public ImageView icon;
        public TextView title;

        public SimpleItemViewHolder(View itemView) : base(itemView)
        {
            icon = itemView.FindViewById<ImageView>(Resource.Id.icon);
            title = itemView.FindViewById<TextView>(Resource.Id.title);
        }
    }
}