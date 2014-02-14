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
	public class ActionBarWorker: Java.Lang.Object, Xamarin.ActionbarSherlockBinding.App.ActionBar.ITabListener
	{
		private Xamarin.ActionbarSherlockBinding.App.ActionBar _actionBar;
		private View _view;

		public ActionBarWorker (Xamarin.ActionbarSherlockBinding.App.ActionBar actionBar, View view)
		{
			_actionBar = actionBar;
			_view = view;
		}

		public void AddTab (string Name)
		{
			Xamarin.ActionbarSherlockBinding.App.ActionBar.Tab tab = this._actionBar.NewTab();

			tab.SetText (Name);

			tab.SetTabListener (this);
			this._actionBar.AddTab (tab);

		}

		public void OnTabSelected(Xamarin.ActionbarSherlockBinding.App.ActionBar.Tab p0, Android.Support.V4.App.FragmentTransaction p1)
		{
			var sampleTextView =
				_view.FindViewById<TextView> (Resource.Id.SimpleTextView);      


			MainActivity  calledActivity= _view.Context as MainActivity;

			string parameterName = p0.Text;
			calledActivity.Title = parameterName;
			sampleTextView.Text = parameterName; 

			calledActivity.StartDataLoading (parameterName);
		
	


			}

		public void OnTabReselected (Xamarin.ActionbarSherlockBinding.App.ActionBar.Tab p0, Android.Support.V4.App.FragmentTransaction p1)
		{

		 
			var sampleTextView =
						 _view.FindViewById<TextView> (Resource.Id.SimpleTextView);      
			(_view.Context as Activity).Title = p0.Text;
			sampleTextView.Text = p0.Text + "Double"; 

		}



		public void OnTabUnselected (Xamarin.ActionbarSherlockBinding.App.ActionBar.Tab p0, Android.Support.V4.App.FragmentTransaction p1)
		{
			//throw new NotImplementedException ();
		}

	}

//	internal class TagsTableFragment: Android.Support.V4.App.Fragment //TableFragment + Singleton 
//	{
//		public override View OnCreateView (LayoutInflater inflater,
//		                                   ViewGroup container, Bundle savedInstanceState)
//		{
//			base.OnCreateView (inflater, container, savedInstanceState);
//
//			var view = inflater.Inflate (
//				Resource.Layout.Main, container, false);
//
//			var sampleTextView =
//				view.FindViewById<TextView> (Resource.Id.SimpleTextView);            
//			sampleTextView.Text = "sample fragment text";
//
//			return view;
//		}
//
//		private  int _layoutId = Resource.Layout.Fragment;
//
//		public int LayoutId{
//			get { return _layoutId; }
//		}
//
//		private TagsTableFragment ()
//		{
//		}
//
//		private sealed class SingletonCreator
//		{
//			private static readonly TagsTableFragment instance = new TagsTableFragment ();
//
//			public static TagsTableFragment Instance { get { return instance; } }
//		}
//
//		public static TagsTableFragment Instance {
//			get { return SingletonCreator.Instance; }
//		}
//	}

}

