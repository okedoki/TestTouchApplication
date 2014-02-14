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
	public class TagListViewAdapter : BaseAdapter<Status>
	{
		List<Status> _status;
		 
		public TagListViewAdapter () : base()
		{

			this._status = new List<Status> ();
		}

		public override long GetItemId (int position)
		{
			return position;
		}

		public override Status this [int position] {  
			get { return _status [position]; }
		}

		public override Int32 Count {
 			get { return _status.Count; }
		}
	
		public override View GetView (int position, View convertView, ViewGroup parent)
		{
			//			TextView view = new TextView (context);
			//			view.Text = items [position];
			//		
			//		
		//	parent.Context;
//			var	view = LayoutInflater.FromContext(parent.Context).Inflate (Android.Resource.Layout.SimpleListItem1, null);
//			view.Clickable = false;
//			view.Focusable = false; 

			TextView textView = new TextView(parent.Context);
			textView.SetMinHeight(parent.Height / 5);
		//	TextView textView = view.FindViewById<TextView> (Android.Resource.Id.Text1);
		//	textView.Clickable = false;
		//	textView.Focusable = false; 


			textView.Text = _status [position].text;
			textView.SetBackgroundColor (Android.Graphics.Color.BlueViolet); 
			//			view.FindViewById<TextView>(Android.Resource.Id.Text1).Text = items[position];

			return textView;
		}

		public void ChangeItemList(List<Status> newItemList)
			{
       		this._status = newItemList;
			this.NotifyDataSetChanged();
		}
			 

	}
}

