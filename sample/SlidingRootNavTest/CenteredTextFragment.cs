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
using SlidingRootNavTest;

namespace SlidingRootNavLibTest
{
    public class CenteredTextFragment : Fragment
    {

        private const string EXTRA_TEXT = "text";

        public static CenteredTextFragment createFor(String text)
        {
            CenteredTextFragment fragment = new CenteredTextFragment();

            Bundle args = new Bundle();
            args.PutString(EXTRA_TEXT, text);

            fragment.Arguments = args;

            return fragment;
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            return inflater.Inflate(Resource.Layout.fragment_text, container, false);
        }

        public override void OnViewCreated(View view, Bundle savedInstanceState)
        {
            string text = Arguments.GetString(EXTRA_TEXT);

            TextView textView = view.FindViewById<TextView>(Resource.Id.text);
            textView.Text = text;
            textView.Click += (s, e) =>
            {
                Toast.MakeText(Context, text, ToastLength.Short).Show();
            };
        }

    }
}