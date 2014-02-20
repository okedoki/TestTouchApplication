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
	public class TweetTabFragment : Android.Support.V4.App.ListFragment
	{
	 
		private string queryName;
		public TagListViewAdapter TabListViewAdapter {
			get {
				return this.ListView.Adapter as TagListViewAdapter;
			}
		}

 
		public override void OnActivityCreated (Bundle savedInstanceState)
		{

			queryName =  Arguments.GetString ("tag");
			this.ListAdapter = new TagListViewAdapter();

			this.ListView.ItemClick += (sender, e) => {

				Intent intent = new Intent(this.Activity, typeof(DetailInfoActivity));
				Bundle b = new Bundle();
				b.PutString("TweetInfo", 
				            TweetInfoSerializer.Serialize(new TweetInfo{
					NameText = TabListViewAdapter[e.Position].user.screen_name, 
					DicriptionText = TabListViewAdapter[e.Position].text, 
					Avatar = TabListViewAdapter[e.Position].user.profileImage,
					TweetDateTime = TabListViewAdapter[e.Position].created_at
				}));
				intent.PutExtras(b); 
				StartActivity(intent);
			};
			base.OnCreate(savedInstanceState);;
		}
 

		public override void OnStart()
		{
			DataListUpdater.StartDataLoading (queryName, this.TabListViewAdapter, this.Activity);

			base.OnStart();
		}

}
}

