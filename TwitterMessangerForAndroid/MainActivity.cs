using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using RestSharp;
using Xamarin.ActionbarSherlockBinding.App;
 
 

namespace TwitterMessangerForAndroid
{
	[Activity (Label = "TwitterMessangerForAndroid", MainLauncher = false)]
	public class MainActivity : SherlockFragmentActivity
	{
		private Android.Support.V4.View.ViewPager pager;
		private ActionBarAdapter _actionBarWorker;
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			SetTheme (Resource.Style.ActionBarTheme);
			SetContentView (Resource.Layout.Main);
 
 
			SupportActionBar.SetIcon (Android.Resource.Color.Transparent);

				 
			Xamarin.ActionbarSherlockBinding.App.ActionBar actionBar = this.SupportActionBar;

			actionBar.NavigationMode = (int)ActionBarNavigationMode.Tabs;
			 
			 pager = FindViewById<Android.Support.V4.View.ViewPager> (Resource.Id.tweetpager);
			pager.CurrentItem = 1;
			_actionBarWorker = new ActionBarAdapter(this, actionBar, pager);

		
			_actionBarWorker.AddTab ("#putin");
			_actionBarWorker.AddTab ("#sochi");
			_actionBarWorker.AddTab ("#love");
		

			Button b =  FindViewById<Button> (Resource.Id.MoreTwittsButton);
			b.Click += moreTwittsClick;
		
	
		}
		public void moreTwittsClick(object sender, EventArgs e)
		{
			_actionBarWorker.NextQuery ();
		}

		public override bool OnCreateOptionsMenu (Xamarin.ActionbarSherlockBinding.Views.IMenu menu)
		{
		    SupportMenuInflater.Inflate (Resource.Menu.menu, menu);
			return true;
		}

	    public override bool OnOptionsItemSelected (Xamarin.ActionbarSherlockBinding.Views.IMenuItem item)
		{
			StartActivity (typeof(AboutActivity));
			return base.OnOptionsItemSelected (item);
		}
 
	
		protected override void OnSaveInstanceState (Bundle outState)
		{
			base.OnSaveInstanceState (outState);
			outState.PutInt ("PagerPage", pager.CurrentItem);

		}
		protected override void OnRestoreInstanceState (Bundle savedInstanceState)
		{
			base.OnRestoreInstanceState (savedInstanceState);
			int currentPage = savedInstanceState.GetInt ("PagerPage", 0);
			if (currentPage < 0 && currentPage >= _actionBarWorker.Count)
				return;
			pager.CurrentItem = currentPage;
		}

	}
}
