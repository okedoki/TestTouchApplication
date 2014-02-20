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
using Com.Nostra13.Universalimageloader.Core;

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
		
			if (convertView == null) {
				convertView = LayoutInflater.FromContext (parent.Context).Inflate (Resource.Layout.ListElementLayout, null);
			}
			TextView nameTextView = convertView.FindViewById<TextView> (Resource.Id.username);	
			TextView descriptionTextView = convertView.FindViewById<TextView> (Resource.Id.discription);

			ImageView userImageView = convertView.FindViewById<ImageView> (Resource.Id.useravatar);
	
			ImageLoader.Instance.CancelDisplayTask(userImageView);	
			ImageLoader.Instance.DisplayImage(_status [position].user.profile_image_url, userImageView);
	 
	


		

			nameTextView.Clickable = false;
			nameTextView.Text = _status [position].user.screen_name;

			string description = _status [position].text;
			descriptionTextView.Text = description.Substring(0, Math.Min(30, description.Length));
 

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


