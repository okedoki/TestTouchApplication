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
using Xamarin.ActionbarSherlockBinding.App;

namespace TwitterMessangerForAndroid
{
	[Activity (Label = "DetailInfoActivity",Theme = "@style/Theme.Sherlock")]			
	public class DetailInfoActivity : SherlockActivity
	{
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
	 
			Bundle b = Intent.Extras;
			List<TweetInfo> tweetInfoList = TweetInfoSerializer.Deserialize(b.GetString("TweetInfo"));
			TweetInfo tweetInfo = tweetInfoList [0];
			SetContentView (Resource.Layout.DatailInfoLayout);
			SupportActionBar.SetDisplayHomeAsUpEnabled (true);
			SupportActionBar.SetDisplayShowHomeEnabled (false);
			this.Title = "Твиты";
		 
	       
			DateTime dt = DateTime.ParseExact(tweetInfo.TweetDateTime,
			                                  "ddd MMM dd HH:mm:ss zzz yyyy",
			                                  System.Globalization.CultureInfo.InvariantCulture,
			                                  System.Globalization.DateTimeStyles.AdjustToUniversal);

			this.FindViewById<TextView> (Resource.Id.detaildatetextview).Text = dt.ToString ("d", new System.Globalization.CultureInfo ("ru-Ru"));
		 	this.FindViewById<TextView> (Resource.Id.detailusername).Text = tweetInfo.NameText;
			this.FindViewById<TextView> (Resource.Id.detailtextview).Text = tweetInfo.DescriptionText;
			this.FindViewById<TextView> (Resource.Id.detailsiteinfo).Text = tweetInfo.TweetAddress;

			ImageView avatarImageView = this.FindViewById<ImageView> (Resource.Id.detailuseravatar);
		
	 		Com.Nostra13.Universalimageloader.Core.ImageLoader.Instance.DisplayImage (tweetInfo.AvatarUrl, avatarImageView);
		}
 
		public override bool OnOptionsItemSelected (Xamarin.ActionbarSherlockBinding.Views.IMenuItem p0)
		{
			if (p0.ItemId == Android.Resource.Id.Home) {
				OnBackPressed ();
			}
			return base.OnOptionsItemSelected (p0);
		}
	}
}

