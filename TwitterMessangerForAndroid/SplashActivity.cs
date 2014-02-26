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
	[Activity (MainLauncher = true, Theme = "@style/Theme.Sherlock.NoActionBar")]			
	public class SplashActivity : Activity
	{
		private const int SPLASH_DISPLAY_LENGHT = 1000;
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			SetContentView (Resource.Layout.SplashLayout);
 
		 
			new Handler().PostDelayed(()=>
			                          {
					Intent mainIntent = new Intent(this,typeof(MainActivity));
					this.StartActivity(mainIntent);
				    this.Finish();
			}, SPLASH_DISPLAY_LENGHT);
		}
	}
}

