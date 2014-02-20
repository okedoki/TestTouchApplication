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
				return this.ListView.Adapter as TagListViewAdapter;
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
					queryName =  Arguments.GetString ("tag");
			
			 this.ListAdapter = new TagListViewAdapter();

			this.ListView.ItemClick += (sender, e) => {

				Intent intent = new Intent(this.Activity, typeof(DetailInfoActivity));
				Bundle b = new Bundle();
				b.PutString("TweetInfo", 
				            TweetInfoXMLSerializer.Serialize(new TweetInfo{
					NameText = TabListViewAdapter[e.Position].user.screen_name, 
					DicriptionText = TabListViewAdapter[e.Position].text, 
					AvatarUrl =  TabListViewAdapter[e.Position].user.profile_image_url,
					TweetDateTime = TabListViewAdapter[e.Position].created_at
				}));
				intent.PutExtras(b); 
				StartActivity(intent);
			};
			base.OnCreate(savedInstanceState);
		}
 

		public override void OnStart()
		{
			DataListUpdater.StartDataLoading (queryName, this.TabListViewAdapter, this.Activity);

			base.OnStart();
		}

}
}
	

