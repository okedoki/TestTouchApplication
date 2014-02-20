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
	[Activity (Label = "TwitterMessangerForAndroid", MainLauncher = true)]
	public class MainActivity : SherlockFragmentActivity
	{
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			SetTheme (Resource.Style.Theme_Sherlock);
			SetContentView (Resource.Layout.Main);



			Xamarin.ActionbarSherlockBinding.App.ActionBar actionBar = this.SupportActionBar;
			actionBar.NavigationMode = (int)ActionBarNavigationMode.Tabs;
			 
		 
			var actionBarWorker = new ActionBarWorker(this, actionBar, FindViewById<Android.Support.V4.View.ViewPager>(Resource.Id.tweetpager));


			actionBarWorker.AddTab ("#putin");
			actionBarWorker.AddTab ("#sochi");
			actionBarWorker.AddTab ("#love");
			actionBar.SelectTab (actionBar.GetTabAt (0));

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
		[Java.Interop.Export("moreTwittsClick")]
		public void moreTwittsClick(View v)
		{
	//		StartDataLoading (query, maxId);
		}
	}
}
