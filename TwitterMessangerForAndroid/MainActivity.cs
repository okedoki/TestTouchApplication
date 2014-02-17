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
	public class MainActivity : SherlockActivity
	{
		TwitterWorker _tw;
		ProgressDialog _progressDialog;
		TagListViewAdapter _hc;

 

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			SetTheme (Resource.Style.Theme_Sherlock);
			SetContentView (Resource.Layout.Main);

			_tw = new TwitterWorker ();
			_progressDialog = new ProgressDialog (this);

			ListView lw = FindViewById<ListView> (Resource.Id.TwitsListView);

			_hc = new TagListViewAdapter ();
			lw.Adapter = _hc;
			lw.ChoiceMode = ChoiceMode.None;

			lw.ItemClick += (sender, e) => {
				Intent intent = new Intent(this, typeof(DetailInfoActivity));
				Bundle b = new Bundle();
				b.PutString("Avatar", TweetInfoSerializer.Serialize(new TweetInfo{nameText = _hc[e.Position].user.screen_name, dicriptionText = _hc[e.Position].text})); //Your id
		    	intent.PutExtras(b); //Put your id to your next Intent
			    StartActivity(intent);
			};

			Xamarin.ActionbarSherlockBinding.App.ActionBar actionBar = this.SupportActionBar;
			actionBar.NavigationMode = (int)ActionBarNavigationMode.Tabs;
			ActionBarWorker actionBarWorker = new ActionBarWorker (this.SupportActionBar, this.FindViewById (Resource.Id.mainlayout));

			actionBarWorker.AddTab ("#putin");
			actionBarWorker.AddTab ("#sochi");
			actionBarWorker.AddTab ("#love");
   


		}
		public void StartDataLoading(string parameterName)
		{

			_progressDialog.SetTitle ("Ждём загрузку данных");
			_progressDialog.Show ();
 

			System.Threading.Tasks.Task<RootObject> repositoriesTask = _tw.RepositoriesAsyncRequest (parameterName);
			repositoriesTask.ContinueWith (DataLoaded);
		}

		private void DataLoaded(System.Threading.Tasks.Task<RootObject> tr)
		{   

			RunOnUiThread (() => {
				_hc.ChangeItemList (tr.Result.statuses);
				_progressDialog.Hide ();
			});
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
	}
}
