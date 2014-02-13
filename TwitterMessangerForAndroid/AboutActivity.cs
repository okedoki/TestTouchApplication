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
	[Activity (Label = "AboutActivity")]			
	public class AboutActivity : Activity
	{
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			SetContentView (Resource.Layout.About);
			// Create your application here
		}
		[Java.Interop.Export("linkbuttonclick")]
		public void linkbuttonclick(View view) {

			switch (view.Id) {
			case(Resource.Id.mailtouchinbutton):
				var email = new Intent (Android.Content.Intent.ActionSend);
				email.PutExtra (Android.Content.Intent.ExtraEmail, 
				                new string[]{"hello@touchin.ru"} );
				email.PutExtra (Android.Content.Intent.ExtraSubject, "Testing topic");
				email.PutExtra (Android.Content.Intent.ExtraText, 
				                "Hello from Xamarin.Android");
				email.SetType ("message/rfc822");
				StartActivity(email);
				break;
			case(Resource.Id.phonetouchinbutton):
				Toast.MakeText(this,"phonetouchinbutton", ToastLength.Long).Show();
				Intent phone = new Intent(Intent.ActionDial, 
				                          Android.Net.Uri.Parse(string.Format("tel:{0}", "+7 931 002-89-90")));
				StartActivity(phone);
				break;
		 
			}
		

			// Do something in response to button click
		}


	}
}

