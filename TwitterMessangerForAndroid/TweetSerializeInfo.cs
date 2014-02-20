using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Android.Widget;
using System.IO;
using System.Xml;

namespace TwitterMessangerForAndroid
{
	
	[Serializable]
		public class TweetInfo
		{
			public string NameText{ get; set;}
	 		public string DicriptionText{ get; set;}
		    public string TweetDateTime{ get; set;}
		    public string AvatarUrl{ get; set;}
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

