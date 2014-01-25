using System.Collections.Generic;

namespace Podcast.Common
{
    public class User
    {
        public User()
        {
            Subscriptions = new List<PodcastSurrogate.rss>();
        }

        public List<PodcastSurrogate.rss> Subscriptions { get; set; } 
    }
}