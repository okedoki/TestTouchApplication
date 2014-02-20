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
		private class ViewHolder:Java.Lang.Object {
			public TextView nameTextView;
			public TextView dicriptionTextView;
			public ImageView userImageView;
		}
		private ViewHolder holder;

		public override View GetView (int position, View convertView, ViewGroup parent)
		{
			holder = new ViewHolder ();

			if (convertView == null) {
				convertView = LayoutInflater.FromContext (parent.Context).Inflate (Resource.Layout.ListElementLayout, null);
				holder.nameTextView = convertView.FindViewById<TextView> (Resource.Id.username);	
				holder.dicriptionTextView = convertView.FindViewById<TextView> (Resource.Id.discription);
				holder.userImageView = convertView.FindViewById<ImageView> (Resource.Id.useravatar);
				holder.nameTextView.Clickable = false;
				convertView.Tag = holder;
			} else {
				 holder = convertView.Tag as ViewHolder;
			}



			holder.nameTextView.Text = _status [position].user.screen_name;

			string disctiption = _status [position].text;
			holder.dicriptionTextView.Text = disctiption.Substring(0, Math.Min(30, disctiption.Length));
			holder.userImageView.SetImageBitmap(_status [position].user.profileImage);

			return convertView;
		}


		public void ChangeItemList(List<Status> newItemList)
			{
			if (this._status.Count == 0)
				this._status = newItemList;
			else
				this._status.AddRange (newItemList);
			this.NotifyDataSetChanged();
		}
			 

	}
}

