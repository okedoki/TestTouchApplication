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
			_client.Authenticator = RestSharp.Authenticators.OAuth1Authenticator.ForAccessToken ("owHHftNHM2wJkmrurIuMeQ", "UU4a8c3C9OJiiNQxrlQfoXQWnWnd4zW5XeSzjcCMLsQ", "595522277-pgAEtj8G5o9MjphKwcjHFrxRRYuYfrceXdB5ZBTf", "5udj2MFyfMUti8MOnURFksFHgYLYJtBn08oVPd72khrq4");
 
		}
		public Task<RootObject> RepositoriesAsyncRequest(string searchName)
		{	
			var request = new RestRequest("https://api.twitter.com/1.1/search/tweets.json");
			request.AddParameter ("q", searchName);
			request.AddParameter ("count", 5);
			var tcs=new TaskCompletionSource<RootObject>();
			_client.ExecuteAsync<RootObject>(request, responce => { 
				tcs.SetResult(responce.Data);
			});
			return tcs.Task;
		}
	}
}




public class User
{
	public int id { get; set; }
	public string id_str { get; set; }
	public string name { get; set; }
	public string screen_name { get; set; }
	public string location { get; set; }
	public string description { get; set; }
	public object url { get; set; }
	public string profile_image_url { get; set; }
	public string profile_image_url_https { get; set; }
}

public class UserMention
{
	public string screen_name { get; set; }
	public string name { get; set; }
	public int id { get; set; }
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
	public string created_at { get; set; }
	public long id { get; set; }
	public string id_str { get; set; }
	public string text { get; set; }
	public string source { get; set; }
	public User user { get; set; }
	public Entities2 entities { get; set; }
}


public class RootObject
{
	public List<Status> statuses { get; set; }
}