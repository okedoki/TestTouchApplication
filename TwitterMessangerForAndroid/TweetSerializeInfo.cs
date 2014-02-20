using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Android.Widget;
using System.IO;

namespace TwitterMessangerForAndroid
{
	
	[Serializable]
		public class TweetInfo
		{
			public string NameText{ get; set;}
	 		public string DicriptionText{ get; set;}
		    public string TweetDateTime{ get; set;}
		    private string codedAvatar;
	 
		public Android.Graphics.Bitmap Avatar {
			get {
				return UnCodeBitMap(codedAvatar);
			}
			set {
				CodeBitMap(value);
			}
		}
		 
		private void CodeBitMap(Android.Graphics.Bitmap value)
		    {
			using (MemoryStream ms = new MemoryStream())
			{
				value.Compress(Android.Graphics.Bitmap.CompressFormat.Webp,100, ms);
				codedAvatar = Convert.ToBase64String(ms.ToArray());
	         }
		    }

		private Android.Graphics.Bitmap UnCodeBitMap(string codedString)
		{
			using (MemoryStream ms = new MemoryStream(Convert.FromBase64String(codedString)))
			{
				return Android.Graphics.BitmapFactory.DecodeStream (ms);
			}
		}
	    }

	    

	    public static class TweetInfoSerializer
		{
		  public static string Serialize(TweetInfo info)
			{ 
			  IFormatter formatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
			  using (MemoryStream ms = new MemoryStream())
				{
					formatter.Serialize(ms, info);
					return Convert.ToBase64String(ms.ToArray());
				}
			}
			public static TweetInfo Deserialize(string Base64TweetInfo)
			{
			  IFormatter formatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
			  using (MemoryStream ms = new MemoryStream(Convert.FromBase64String(Base64TweetInfo)))
				{
				return formatter.Deserialize(ms) as TweetInfo;
				}
		 
			}

		}

}

