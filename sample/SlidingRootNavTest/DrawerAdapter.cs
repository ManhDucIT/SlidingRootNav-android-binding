using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;

namespace SlidingRootNavLibTest
{
    public class DrawerAdapter : RecyclerView.Adapter
    {

        private IList<BaseDrawerItem> items;
        private Dictionary<BaseDrawerItem, int> viewTypes;
        private Dictionary<int, BaseDrawerItem> holderFactories;

        public event EventHandler<int> ItemSelected;

        public DrawerAdapter(IList<BaseDrawerItem> items)
        {
            this.items = items;
            this.viewTypes = new Dictionary<BaseDrawerItem, int>();
            this.holderFactories = new Dictionary<int, BaseDrawerItem>();

            ProcessViewTypes();
        }

        public override int ItemCount => items.Count;

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            DrawerAdapterViewHolder holder = (DrawerAdapterViewHolder) holderFactories[viewType]?.CreateViewHolder(parent);

            if (holder != null)
            {
                holder.Adapter = this;
            }

            return holder;
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            DrawerAdapterViewHolder viewHolder = holder as DrawerAdapterViewHolder;

            items[position]?.BindViewHolder(viewHolder);
        }

        public override int GetItemViewType(int position)
        {
            return viewTypes[items[position]];
        }

        private void ProcessViewTypes()
        {
            int type = 0;

            foreach (var item in items)
            {
                if (!viewTypes.ContainsKey(item))
                {
                    viewTypes.Add(item, type);

                    holderFactories.Add(type, item);

                    type++;
                }
            }
        }

        public void SetSelected(int position)
        {
            var newChecked = items[position];

            if (!newChecked.IsSelectable)
            {
                return;
            }

            for (int i = 0; i < items.Count; i++)
            {
                var item = items[i];

                if (item.IsChecked)
                {
                    item.SetChecked(false);

                    NotifyItemChanged(i);

                    break;
                }
            }

            newChecked.SetChecked(true);

            NotifyItemChanged(position);

            ItemSelected?.Invoke(this, position);
        }

    }

    public abstract class DrawerAdapterViewHolder : RecyclerView.ViewHolder
    {
        
        protected DrawerAdapterViewHolder(View itemView) : base(itemView)
        {
            itemView.Click += (s, e) =>
            {
                Adapter.SetSelected(AdapterPosition);
            };
        }

        public DrawerAdapter Adapter { get; set; }

    }
}