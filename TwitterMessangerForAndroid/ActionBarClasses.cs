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
	public class ActionBarAdapter:  Android.Support.V4.App.FragmentStatePagerAdapter, Xamarin.ActionbarSherlockBinding.App.ActionBar.ITabListener, Android.Support.V4.View.ViewPager.IOnPageChangeListener
	{
		private readonly Context _context;
		private readonly List<string> _tabs = new List<string>();
		private readonly Xamarin.ActionbarSherlockBinding.App.ActionBar _bar;
		private readonly Android.Support.V4.View.ViewPager _viewPager;


		public ActionBarAdapter (Android.Support.V4.App.FragmentActivity activity, Xamarin.ActionbarSherlockBinding.App.ActionBar bar, Android.Support.V4.View.ViewPager viewPager): base(activity.SupportFragmentManager)
		{
			_context = activity;
			_bar = bar;
			_viewPager = viewPager;
		 	_viewPager.Adapter = this;
			_viewPager.SetOnPageChangeListener(this);
		

		}

		public void AddTab (string Name,bool selected = false)
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
	   public void SetCurentTab(int number)
		{
			_bar.SetSelectedNavigationItem (number);
		}
		public override Android.Support.V4.App.Fragment GetItem(int position)
		{
			MainActivity ma = _context as MainActivity;
			var args = new Bundle();
			args.PutString("tag", _tabs[position]);

	//		Android.Support.V4.App.Fragment fragment;
	//		if ((fragment = ma.SupportFragmentManager.FindFragmentByTag (_tabs [position])) == null)
		//		 ma.SupportFragmentManager.BeginTransaction ().Add (fragment = new TweetTabFragment(), _tabs [position]).Commit();
		
			Android.Support.V4.App.Fragment fragment = Android.Support.V4.App.Fragment.Instantiate(_context, Java.Lang.Class.FromType(typeof(TweetTabFragment)).Name, args);


			fragmentDiсtionary[_tabs [position]]= fragment as TweetTabFragment;
 			
			return fragment;
		}
		static Dictionary <string ,TweetTabFragment>  fragmentDiсtionary = new  Dictionary<string ,TweetTabFragment>();

		public override void SetPrimaryItem (ViewGroup container, int position, Java.Lang.Object @object)
		{
			var fragment = @object as TweetTabFragment;
			fragmentDiсtionary [_tabs [position]] = fragment;
			base.SetPrimaryItem (container, position, @object);
		}

		public void NextQuery()
		{
			TweetTabFragment _currentFragment = fragmentDiсtionary [tag];
			if (_currentFragment == null)
				return;
			_currentFragment.DownloadNext ();
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

			TweetTabFragment _currentFragment = fragmentDiсtionary [tag];

		}

		#endregion


		string tag;
		public void OnTabSelected(Xamarin.ActionbarSherlockBinding.App.ActionBar.Tab p0, Android.Support.V4.App.FragmentTransaction p1)
		{	      
			tag = p0.Tag.ToString ();

			var index = _tabs.IndexOf (tag);
			if (index != -1) {
				_viewPager.SetCurrentItem (index, true);
				(_context as Activity).Title = tag;
			}
		}

		public void OnTabReselected (Xamarin.ActionbarSherlockBinding.App.ActionBar.Tab p0, Android.Support.V4.App.FragmentTransaction p1)
		{           
		}



		public void OnTabUnselected (Xamarin.ActionbarSherlockBinding.App.ActionBar.Tab p0, Android.Support.V4.App.FragmentTransaction p1)
		{
		}

	}
	
}

