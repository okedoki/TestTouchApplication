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
			string id = b.GetString("key");

			 Toast.MakeText(this, id, ToastLength.Long).Show();
			// Create your application here
		}
	}
}

