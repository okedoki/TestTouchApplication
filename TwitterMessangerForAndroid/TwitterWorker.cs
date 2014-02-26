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
using Com.Nostra13.Universalimageloader.Core;

 
namespace TwitterMessangerForAndroid
{
	class TwitterWorker
	{
		RestClient _client;
		 
		public TwitterWorker()
		{
			_client= new RestClient(); 
			_client.Authenticator = RestSharp.Authenticators.OAuth1Authenticator.ForAccessToken ("8xFPkfa6Gxs9IzKhV8mhsw", "h0qWBTAamX9NjsFU0ynFkvKql4zpQMqia7FUgoTLZA", "595522277-pgAEtj8G5o9MjphKwcjHFrxRRYuYfrceXdB5ZBTf", "5udj2MFyfMUti8MOnURFksFHgYLYJtBn08oVPd72khrq4");
			_client.Timeout = 150000;   
		}
	 
		private static System.Collections.Generic.Dictionary<string, ulong> maxIdDistionary = new Dictionary<string,ulong>();
	 
 
		public Task<RootObject> StartRequest(string searchName)
		{
            var restRequest = new RestRequest("https://api.twitter.com/1.1/search/tweets.json") ;

			restRequest.AddParameter ("q", searchName);
	    	restRequest.AddParameter ("count", 5);
			restRequest.AddParameter ("include_entities", 1);
	 
			if (maxIdDistionary.ContainsKey (searchName)) {
				ulong maxId = maxIdDistionary [searchName];
				restRequest.AddParameter ("max_id", maxId);
			}
			else {

				maxIdDistionary.Add (searchName, 0);
			}
 

			return RepositoriesAsyncRequest (restRequest);
		}

	

		public Task<RootObject> RepositoriesAsyncRequest(RestRequest request)
		{	 
			var tcs=new TaskCompletionSource<RootObject>();
			_client.ExecuteAsync<RootObject>(request, responce =>
			{ 
				if (responce != null && responce.ErrorException == null && responce.ResponseStatus == ResponseStatus.Completed)
				{
					tcs.SetResult(
						responce.Data ?? new RootObject());
					System.Collections.Specialized.NameValueCollection nextQueryParams = RestSharp.Contrib.HttpUtility.ParseQueryString(responce.Data.search_metadata.next_results);
				
					ulong maxId = Convert.ToUInt64(nextQueryParams["max_Id"]);
					string q = nextQueryParams["q"];
					maxIdDistionary[q] = maxId;
 
				}
				else
					tcs.TrySetException(new Exception("Проблема с соединением. Попробуйте пойзже.")); 
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
    public string profile_image_url { get; set; }

	public Android.Graphics.Bitmap  profileImage { get; set; }

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
		public ulong max_id { get; set; }
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