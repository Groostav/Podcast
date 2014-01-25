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
            Subscriptions = new List<PodcastSurrogate.rss>();
        }

        public List<PodcastSurrogate.rss> Subscriptions { get; set; } 
    }
}