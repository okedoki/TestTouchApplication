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
			_client = new RestClient();
			_client.BaseUrl = "https://api.github.com";
		}
		public Task<RootObject> RepositoriesAsyncRequest(string searchName)
		{	
			var request = new RestRequest();
			request.Resource = "search/repositories";
			request.AddParameter ("q", searchName);
			var tcs=new TaskCompletionSource<RootObject>();
			_client.ExecuteAsync<RootObject>(request, responce => { 
				tcs.SetResult(responce.Data);
			});
			return tcs.Task;
		}
	}
}

public class Owner
{
	public string login { get; set; }
	public int id { get; set; }
	public string avatar_url { get; set; }
}

public class Item
{
	public int id { get; set; }
	public string name { get; set; }
	public string full_name { get; set; }
	public Owner owner { get; set; }
}

public class RootObject
{
	public int total_count { get; set; }
	public List<Item> items { get; set; }
}