using System;
using System.Collections.Generic;
using Android.App;
using Android.Content.Res;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Support.V4.Content;
using Android.Support.V7.App;
using Android.Support.V7.Widget;
using Android.Views;
using Com.Yarolegovich.Slidingrootnav;
using SlidingRootNavLibTest;
using SlidingRootNavTest;

namespace SlidingRootNavLibTest
{
    [Activity(Label = "SlidingRootNavLibTest", MainLauncher = true, Theme = "@style/AppTheme")]
    public class MainActivity : AppCompatActivity
    {

        private const int POS_DASHBOARD = 0;
        private const int POS_ACCOUNT = 1;
        private const int POS_MESSAGES = 2;
        private const int POS_CART = 3;
        private const int POS_LOGOUT = 5;

        private string[] screenTitles;
        private Drawable[] screenIcons;

        private Toolbar toolbar;
        private ISlidingRootNav slidingRootNav;
        private RecyclerView recyclerView;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            
            SetContentView(Resource.Layout.activity_main);

            toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);

            SetupSlidingRootNav(savedInstanceState);
        }

        private void SetupSlidingRootNav(Bundle savedInstanceState)
        {
            slidingRootNav = new SlidingRootNavBuilder(this)
                .WithToolbarMenuToggle(toolbar)
                .WithMenuOpened(false)
                .WithContentClickableWhenMenuOpened(true)
                .WithSavedState(savedInstanceState)
                .WithMenuLayout(Resource.Layout.menu_left_drawer)
                .Inject();

            screenIcons = LoadScreenIcons();
            screenTitles = LoadScreenTitles();

            IList<BaseDrawerItem> list = new List<BaseDrawerItem>();

            var selectedItem = CreateItemFor(POS_DASHBOARD);
            selectedItem.SetChecked(true);

            list.Add(selectedItem);
            list.Add(CreateItemFor(POS_ACCOUNT));
            list.Add(CreateItemFor(POS_MESSAGES));
            list.Add(CreateItemFor(POS_CART));
            list.Add(new SpaceItem(48));
            list.Add(CreateItemFor(POS_LOGOUT));
            
            DrawerAdapter adapter = new DrawerAdapter(list);
            adapter.ItemSelected += Adapter_ItemSelected;

            recyclerView = FindViewById<RecyclerView>(Resource.Id.list);
            recyclerView.NestedScrollingEnabled = false;
            recyclerView.SetLayoutManager(new LinearLayoutManager(this));
            recyclerView.SetAdapter(adapter);

            adapter.SetSelected(POS_DASHBOARD);
        }

        private void Adapter_ItemSelected(object sender, int e)
        {
            if (e == POS_LOGOUT)
            {
                Finish();
            }

            slidingRootNav.CloseMenu();

            Fragment selectedScreen = CenteredTextFragment.createFor(screenTitles[e]);
            ShowFragment(selectedScreen);
        }

        private void ShowFragment(Fragment fragment)
        {
            FragmentManager.BeginTransaction()
                .Replace(Resource.Id.container, fragment)
                .Commit();
        }

        private BaseDrawerItem CreateItemFor(int position)
        {
            return new SimpleItem(screenIcons[position], screenTitles[position])
                .WithIconTint(Color(Resource.Color.textColorSecondary))
                .WithTextTint(Color(Resource.Color.textColorSecondary))
                .WithSelectedIconTint(Color(Resource.Color.colorAccent))
                .WithSelectedTextTint(Color(Resource.Color.colorAccent));
        }

        private string[] LoadScreenTitles()
        {
            return Resources.GetStringArray(Resource.Array.ld_activityScreenTitles);
        }

        private Drawable[] LoadScreenIcons()
        {
            TypedArray ta = Resources.ObtainTypedArray(Resource.Array.ld_activityScreenIcons);

            Drawable[] icons = new Drawable[ta.Length()];

            for (int i = 0; i < ta.Length(); i++)
            {
                int id = ta.GetResourceId(i, 0);

                if (id != 0)
                {
                    icons[i] = ContextCompat.GetDrawable(this, id);
                }
            }

            ta.Recycle();

            return icons;
        }

        private Color Color(int res)
        {
            var color = ContextCompat.GetColor(this, res);;
            
            return new Color(color);
        }

    }
}

