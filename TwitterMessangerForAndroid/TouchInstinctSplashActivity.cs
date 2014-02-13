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
			 
			if (Autorize ())
				StartActivity (typeof(MainActivity));
			 	else   Toast.MakeText(this, "Problem with authirize", ToastLength.Long);

			// Create your application here
		}
		private  bool Autorize()
		{
			string api = "owHHftNHM2wJkmrurIuMeQ";
			string s = "UU4a8c3C9OJiiNQxrlQfoXQWnWnd4zW5XeSzjcCMLsQ";
			var baseUrl = "http://api.twitter.com/";

			var client = new RestClient(baseUrl);                     
			var request = new RestRequest("/oauth2/token", Method.POST);

			var concat = api + ":" +s;
			string encodeTo64 = concat.Base64Encode();
			 

			request.AddHeader("Authorization", "Basic " + encodeTo64);
			request.AddHeader("Content-Type", "application/x-www-form-urlencoded;charset=UTF-8");
			request.AddBody("grant_type=client_credentials");

			IRestResponse restResponse = client.Execute(request);


			return true;
		}
	
	}
	static class StringExtention
	{
		public static string Base64Encode(this string plainText)
		{
			var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
			return System.Convert.ToBase64String(plainTextBytes);
		}
	}
		
}

