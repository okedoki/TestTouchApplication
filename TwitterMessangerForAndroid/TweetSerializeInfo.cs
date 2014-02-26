using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Android.Widget;
using System.IO;
using System.Xml;
using System.Collections.Generic;
using System.Linq;
namespace TwitterMessangerForAndroid
{
	
	[Serializable]
		public class TweetInfo
		{
			public string NameText{ get; set;}
	 		public string DescriptionText{ get; set;}
		    public string TweetDateTime{ get; set;}
		    public string AvatarUrl{ get; set;}
		    public string TweetAddress{ get; set;}

		public static implicit operator TweetInfo(Status s)
		{
			string url;
			try
			{
				url = s.user.url.ToString();
			}
			catch
			{
				url = "";
			}
				return new TweetInfo {
				NameText = s.user.screen_name,
				DescriptionText = s.text,
				AvatarUrl = s.user.profile_image_url,
				TweetDateTime = s.created_at,
				TweetAddress = url

			};
		}


		public static implicit operator Status(TweetInfo ti)
		{
			Status s =  new Status ();
			s.user = new User ();
			s.user.screen_name = ti.NameText;
			s.text = ti.DescriptionText;
			s.user.profile_image_url = ti.AvatarUrl;
			s.created_at = ti.TweetDateTime;
			s.entities = new Entities2();
			//s.entities.urls = new List<object>(){ti.TweetAddress};	
			return s;
		}	 
	    }

	    

	    public static class TweetInfoSerializer
		{

		  public static string Serialize(List<TweetInfo> info)
			{ 
			  IFormatter formatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
			  using (MemoryStream ms = new MemoryStream())
				{
					formatter.Serialize(ms, info);
					return Convert.ToBase64String(ms.ToArray());
				}
			}
		public static List<TweetInfo> Deserialize(string Base64TweetInfo)
			{
			  IFormatter formatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
			  using (MemoryStream ms = new MemoryStream(Convert.FromBase64String(Base64TweetInfo)))
				{
				return formatter.Deserialize(ms) as List<TweetInfo>;
				}
		 
			}
	}

	public static class TweetInfoXMLSerializer
	{
		public static string Serialize(TweetInfo info)
		{ 
			XmlSerializer serializer = new XmlSerializer (typeof(TweetInfo)); 

			
			using(StringWriter textWriter = new StringWriter()) {
				using(XmlWriter xmlWriter = XmlWriter.Create(textWriter)) {
					serializer.Serialize(xmlWriter, info);
				}
				return textWriter.ToString();
			}
		}
		public static TweetInfo Deserialize(string xmlString)
		{
			if(string.IsNullOrEmpty(xmlString)) return new TweetInfo();
			XmlSerializer serializer = new XmlSerializer (typeof(TweetInfo)); 
			using(StringReader textReader = new StringReader(xmlString)) {
				using(XmlReader xmlReader = XmlReader.Create(textReader)) {
					return (TweetInfo) serializer.Deserialize(xmlReader);
				}

		}

	}

}
}

