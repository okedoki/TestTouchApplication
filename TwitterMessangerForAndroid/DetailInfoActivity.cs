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
	[Activity (Label = "DetailInfoActivity")]			
	public class DetailInfoActivity : Activity
	{
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			Bundle b = Intent.Extras;
			TweetInfo tweetInfo = TweetInfoSerializer.Deserialize(b.GetString("Avatar"));

			SetContentView (Resource.Layout.DatailInfoLayout);

			this.FindViewById<TextView> (Resource.Id.detailusername).Text = tweetInfo.nameText;
			this.FindViewById<TextView> (Resource.Id.detailtextview).Text = tweetInfo.dicriptionText;
			this.FindViewById<ImageView> (Resource.Id.detailuseravatar).SetImageBitmap(tweetInfo.Avatar);
			 //Toast.MakeText(this, id, ToastLength.Long).Show();
			// Create your application here
		}
	}
}

