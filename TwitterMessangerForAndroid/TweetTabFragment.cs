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
using Com.Nostra13.Universalimageloader.Core;

namespace TwitterMessangerForAndroid
{
	public class TweetTabFragment : Android.Support.V4.App.ListFragment
	{
	 
		private string queryName;
		public TagListViewAdapter TabListViewAdapter {
			get {
				return this.ListAdapter as TagListViewAdapter;
			}
		}
 
		private static ImageLoaderConfiguration imageLoaderConfiguration = null;
		private  ImageLoaderConfiguration setConfig()
		{
			const int memoryCacheSizeLimit = 2 * 1024 * 1024;

			var defaultDisplayImageOptions = new DisplayImageOptions.Builder()
				.CacheOnDisc(true)
					.CacheInMemory(true)
					.Build();

			return new ImageLoaderConfiguration.Builder((this.Activity))
			                                            .DiscCacheSize(10 * 1024 * 1024)
			                                            .MemoryCache(new Com.Nostra13.Universalimageloader.Cache.Memory.Impl.LruMemoryCache(memoryCacheSizeLimit))
			                                            .MemoryCacheSize(memoryCacheSizeLimit)
			                                            .DefaultDisplayImageOptions(defaultDisplayImageOptions)
			                                            .Build();
	    }


		public override void OnActivityCreated (Bundle savedInstanceState)
		{
			if (imageLoaderConfiguration == null) {
				imageLoaderConfiguration = this.setConfig ();
				ImageLoader.Instance.Init(imageLoaderConfiguration);
			}
			queryName = this.Arguments.GetString("tag");
			 
			TagListViewAdapter lAdapter = new TagListViewAdapter ();
			this.ListAdapter = lAdapter;
		    this.ListView.ItemClick += (sender, e) => {
				Intent intent = new Intent(this.Activity, typeof(DetailInfoActivity));
				Bundle b = new Bundle();
				List<TweetInfo> lw = new List<TweetInfo>{TabListViewAdapter[e.Position]};

				b.PutString("TweetInfo", TweetInfoSerializer.Serialize(lw));
				intent.PutExtras(b); 
				StartActivity(intent);
			};
			base.OnCreate(savedInstanceState);
		   
			this.Restore (savedInstanceState);

			if(this.ListAdapter.Count == 0) this.DownloadNext ();
		}


    
    public override void OnSaveInstanceState (Bundle outState)
		{
 
			base.OnSaveInstanceState (outState);
			List<TweetInfo> serializedTweet = new List<TweetInfo> ();
			if (ListAdapter == null)
				return;
			(this.ListAdapter as TagListViewAdapter).status.ForEach 
				(
					t => serializedTweet.Add (t)
				 );
			outState.PutString ("TweetInfo", TweetInfoSerializer.Serialize(serializedTweet));
	 

		}
		private void Restore (Bundle savedInstanceState)
		{
			if (savedInstanceState == null)
				return;

			List<Status> deserializeTweet = new List<Status>();
			List<TweetInfo> tInfo;
			tInfo = TweetInfoSerializer.Deserialize (savedInstanceState.GetString ("TweetInfo"));
			if (tInfo == null)
				return;
			tInfo.ForEach(t => deserializeTweet.Add(t));

			TagListViewAdapter listAdapter = new TagListViewAdapter ();
			this.ListAdapter = listAdapter;
			listAdapter.ChangeItemList (deserializeTweet);

		}
 
		public void DownloadNext()
		{
			DataListUpdater.StartDataLoading (queryName, this.TabListViewAdapter, this.Activity);
		}

}
}
	

