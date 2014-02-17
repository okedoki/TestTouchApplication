using System;
using System.Runtime.Serialization;
using Android.Widget;
using System.IO;

namespace TwitterMessangerForAndroid
{
//	class TweetInfo:Java.Lang.Object, Java.IO.ISerializable
//	{
//		public string nameText{ get; set;}
//		public string dicriptionText{ get; set;}
//		//public Android.Graphics.Bitmap userImageView;
//
//		[Java.Interop.Export ("readObject", Throws = new [] {
//			typeof (Java.IO.IOException),
//			typeof (Java.Lang.ClassNotFoundException)})]
//		private void ReadTweetInfo (Java.IO.ObjectInputStream source)
//		{
//			nameText = ReadNullableString (source);
//			dicriptionText = ReadNullableString (source);
//		}
//
//		[Java.Interop.Export ("writeObject", Throws = new [] {
//			typeof (Java.IO.IOException),
//			typeof (Java.Lang.ClassNotFoundException)})]
//		private void WriteTweetInfo (Java.IO.ObjectOutputStream destination)
//		{
//			WriteNullableString (destination, nameText);
//			WriteNullableString (destination, dicriptionText);
//		}
//		static void WriteNullableString (Java.IO.ObjectOutputStream dest, string value)
//		{
//			dest.WriteBoolean (value != null);
//			if (value != null)
//				dest.WriteUTF (value);
//		}
//
//		static string ReadNullableString (Java.IO.ObjectInputStream source)
//		{
//			if (source.ReadBoolean ())
//				return source.ReadUTF ();
//			return null;
//		}
//	}
	[Serializable]
		public class TweetInfo
		{
			public string nameText{ get; set;}
			public string dicriptionText{ get; set;}
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
					return new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter().Deserialize(ms) as TweetInfo;
				}
		 
			}

		}

}

