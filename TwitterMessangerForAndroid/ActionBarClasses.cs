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

namespace TwitterMessangerForAndroid
{
	public class ActionBarWorker:  Android.Support.V4.App.FragmentPagerAdapter, Xamarin.ActionbarSherlockBinding.App.ActionBar.ITabListener, Android.Support.V4.View.ViewPager.IOnPageChangeListener
	{
		 
 
	 
		private readonly Context _context;
		private readonly List<string> _tabs = new List<string>();
		private readonly Xamarin.ActionbarSherlockBinding.App.ActionBar _bar;
		private readonly Android.Support.V4.View.ViewPager _viewPager;

		public ActionBarWorker (Android.Support.V4.App.FragmentActivity activity, Xamarin.ActionbarSherlockBinding.App.ActionBar bar, Android.Support.V4.View.ViewPager viewPager): base(activity.SupportFragmentManager)
		{
			_context = activity;
			_bar = bar;
			_viewPager = viewPager;
		 	_viewPager.Adapter = this;
			_viewPager.SetOnPageChangeListener(this);

		}

		public void AddTab (string Name)
		{
			Xamarin.ActionbarSherlockBinding.App.ActionBar.Tab tab = this._bar.NewTab();

			tab.SetText (Name);
			tab.SetTabListener (this);
			tab.SetTag (Name);
			this._tabs.Add(Name);
			this._bar.AddTab(tab);
			NotifyDataSetChanged();

		}
		public override int Count
		{
			get
			{
				return _tabs.Count;
			}
		}

		public override Android.Support.V4.App.Fragment GetItem(int position)
		{
			var args = new Bundle();
			args.PutString("tag", _tabs[position]);
			return Android.Support.V4.App.Fragment.Instantiate(_context, Java.Lang.Class.FromType(typeof(TweetTabFragment)).Name, args);
		}
		#region IOnPageChangeListener implementation

		public void OnPageScrollStateChanged (int state)
		{
		}

		public void OnPageScrolled (int position, float positionOffset, int positionOffsetPixels)
		{
		}

		public void OnPageSelected (int position)
		{
			_bar.SetSelectedNavigationItem(position);

		}

		#endregion

		public void OnTabSelected(Xamarin.ActionbarSherlockBinding.App.ActionBar.Tab p0, Android.Support.V4.App.FragmentTransaction p1)
		{
				      
			var tag = p0.Tag.ToString();
			var index = _tabs.IndexOf(tag);
			if (index != -1)
			{
				_viewPager.SetCurrentItem(index, true);
 
 

		

//				MainActivity  calledActivity= _view.Context as MainActivity;
//				string parameterName = p0.Text;
//				calledActivity.StartDataLoading(parameterName,0);
			}


		}

		public void OnTabReselected (Xamarin.ActionbarSherlockBinding.App.ActionBar.Tab p0, Android.Support.V4.App.FragmentTransaction p1)
		{
		           
		}



		public void OnTabUnselected (Xamarin.ActionbarSherlockBinding.App.ActionBar.Tab p0, Android.Support.V4.App.FragmentTransaction p1)
		{
			//throw new NotImplementedException ();
		}

	}
	
}

