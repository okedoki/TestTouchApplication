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
	public class TagListViewAdapter : BaseAdapter<Item>
	{
		List<Item> _items;
		Activity context;

		public TagListViewAdapter (Activity context, List<Item> items) : base()
		{
			this.context = context;
			this._items = items;
		}

		public TagListViewAdapter (Activity context) : base()
		{
			this.context = context;
			this._items = new List<Item> ();
		}

		public override long GetItemId (int position)
		{
			return position;
		}

		public override Item this [int position] {  
			get { return _items [position]; }
		}

		public override Int32 Count {
			get { return _items.Count; }
		}
	
		public override View GetView (int position, View convertView, ViewGroup parent)
		{
			//			TextView view = new TextView (context);
			//			view.Text = items [position];
			//		
			//		
			var	view = context.LayoutInflater.Inflate (Android.Resource.Layout.SimpleListItem1, null);
			view.Clickable = false;
			view.Focusable = false; 

			TextView textView = new TextView(context);
			textView.SetMinHeight(parent.Height / 5);
		//	TextView textView = view.FindViewById<TextView> (Android.Resource.Id.Text1);
		//	textView.Clickable = false;
		//	textView.Focusable = false; 


			textView.Text = _items [position].owner.login;
			textView.SetBackgroundColor (Android.Graphics.Color.BlueViolet); 
			//			view.FindViewById<TextView>(Android.Resource.Id.Text1).Text = items[position];

			return textView;
		}

		public void ChangeItemList(List<Item> newItemList)
			{
       		this._items = newItemList;
			this.NotifyDataSetChanged();
		}
			 

	}
}

