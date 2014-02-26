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
	static class  DataListUpdater
	{
		private static TwitterWorker _tw = new TwitterWorker();
	 
		public static   void StartDataLoading(string parameterName, TagListViewAdapter adapter, Activity activity)
		{
			Button button;
			if (activity != null) {
				button = activity.FindViewById<Button> (Resource.Id.MoreTwittsButton);
				button.Enabled = false;
			}
 
				System.Threading.Tasks.Task<RootObject> repositoriesTask = _tw.StartRequest(parameterName);
				repositoriesTask.ContinueWith(task => DataLoaded(task, adapter,activity));
 
				}

		private static void DataLoaded(System.Threading.Tasks.Task<RootObject> tr,TagListViewAdapter adapter, Activity activity)
		{   
	
			activity.RunOnUiThread (() => {
				Button button = activity.FindViewById<Button> (Resource.Id.MoreTwittsButton);		
				button.Enabled  = true;

				if(	tr.Exception !=null){  Toast.MakeText(
						Application.Context,tr.Exception.InnerException.Message, ToastLength.Long).Show();  
						return;
				}
			    adapter.ChangeItemList (tr.Result.statuses);
	
				 }
			);
		}  
	}
}

