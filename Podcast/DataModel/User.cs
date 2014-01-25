using System.Collections.Generic;
using System.Xml.Serialization;
using Podcast.Common;

namespace Podcast.DataModel
{
    public class User
    {
        public static readonly XmlSerializer Serializer = new XmlSerializer(typeof(User));

        public User()
        {
            Subscriptions = new List<PodcastRssFeedXmlSurrogate.rss>();
        }

        public List<PodcastRssFeedXmlSurrogate.rss> Subscriptions { get; set; } 
    }
}