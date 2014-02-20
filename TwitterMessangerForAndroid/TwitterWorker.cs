using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using RestSharp;
using RestSharp.Serializers;

namespace TwitterMessangerForAndroid
{
	class TwitterWorker
	{
		RestClient _client;
		

		public TwitterWorker()
		{
			_client= new RestClient(); 
			_client.Authenticator = RestSharp.Authenticators.OAuth1Authenticator.ForAccessToken ("8xFPkfa6Gxs9IzKhV8mhsw", "h0qWBTAamX9NjsFU0ynFkvKql4zpQMqia7FUgoTLZA", "595522277-pgAEtj8G5o9MjphKwcjHFrxRRYuYfrceXdB5ZBTf", "5udj2MFyfMUti8MOnURFksFHgYLYJtBn08oVPd72khrq4");
 
		}

		public Task<RootObject> StartRequest(string searchName)
		{

 
			var restRequest = new RestRequest("https://api.twitter.com/1.1/search/tweets.json") ;

			restRequest.AddParameter ("q", searchName);
		 	restRequest.AddParameter ("count", 5);

			return RepositoriesAsyncRequest (restRequest);
		}
		public Task<RootObject> StartRepeatedRequest(string searchName, long maxId)
		{

			var restRequest = new RestRequest("https://api.twitter.com/1.1/search/tweets.json");

			restRequest.AddParameter ("max_id", maxId);
			restRequest.AddParameter ("q", searchName);
			restRequest.AddParameter ("count", 5);

			return RepositoriesAsyncRequest (restRequest);	  
		}
	

		public Task<RootObject> RepositoriesAsyncRequest(RestRequest request)
		{	 
			var tcs=new TaskCompletionSource<RootObject>();
			_client.ExecuteAsync<RootObject>(request, responce => { 
				tcs.SetResult(responce.Data);
			});
			return tcs.Task;
		}
	}




public class User
{
	public string id { get; set; }
	public string id_str { get; set; }
	public string name { get; set; }
	public string screen_name { get; set; }
	public string location { get; set; }
	public string description { get; set; }
	public object url { get; set; }
	public string profile_image_url 
	{
			set{profileImage = DownloadUserAvatar (value);}
	}

	public Android.Graphics.Bitmap  profileImage { get; private set;}
	public string profile_image_url_https { get; set; }
	private Android.Graphics.Bitmap  DownloadUserAvatar(string bitmapUrl)
		{
			Java.Net.URL url = new Java.Net.URL (bitmapUrl);
			return Android.Graphics.BitmapFactory.DecodeStream (url .OpenConnection ().InputStream);
		}
}

public class UserMention
{
	public string screen_name { get; set; }
	public string name { get; set; }
	public string id { get; set; }
	public string id_str { get; set; }
	public List<int> indices { get; set; }
}

public class Entities2
{
	public List<object> hashtags { get; set; }
	public List<object> symbols { get; set; }
	public List<object> urls { get; set; }
	public List<UserMention> user_mentions { get; set; }
}

public class Status
{
	public long d;
	public string created_at { get; set; }
	public string id { get; set; }
	public string id_str { get; set; }
	public string text { get; set; }
	public string source { get; set; }
	public User user { get; set; }
	public Entities2 entities { get; set; }
}
	public class SearchMetadata
	{
		public double completed_in { get; set; }
		public long max_id { get; set; }
		public string next_results { get; set; }
		public string query { get; set; }
		public string refresh_url { get; set; }
		public int count { get; set; }
	}


public class RootObject
{
	public SearchMetadata search_metadata { get; set; }
	public List<Status> statuses { get; set; }
}
}