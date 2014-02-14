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
using System.Threading;
using RestSharp;
using RestSharp.Authenticators;

namespace TwitterMessangerForAndroid
{
	[Activity (Label = "TouchInstinctSplashActivity", Theme = "@style/Theme.Splash", MainLauncher = true, NoHistory = true)]			
	public class TouchInstinctSplashActivity : Activity
	{
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

	    		Thread.Sleep (100);			 
				StartActivity (typeof(MainActivity));
		}


	
	}
}

