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
			TweetInfo tweetInfo = 	TweetInfoXMLSerializer.Deserialize(b.GetString("TweetInfo"));

			SetContentView (Resource.Layout.DatailInfoLayout);

		
	       
			DateTime dt = DateTime.ParseExact(tweetInfo.TweetDateTime,
			                                  "ddd MMM dd HH:mm:ss zzz yyyy",
			                                  System.Globalization.CultureInfo.InvariantCulture,
			                                  System.Globalization.DateTimeStyles.AdjustToUniversal);

			this.FindViewById<TextView> (Resource.Id.detaildatetextview).Text = dt.ToString ("d", new System.Globalization.CultureInfo ("ru-Ru"));
		 	this.FindViewById<TextView> (Resource.Id.detailusername).Text = tweetInfo.NameText;
			this.FindViewById<TextView> (Resource.Id.detailtextview).Text = tweetInfo.DicriptionText;


			ImageView avatarImageView = this.FindViewById<ImageView> (Resource.Id.detailuseravatar);
		
	 		Com.Nostra13.Universalimageloader.Core.ImageLoader.Instance.DisplayImage (tweetInfo.AvatarUrl, avatarImageView);
		}
	}
}

