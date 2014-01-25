using System;
using Windows.Web.Http;
using Podcast.Common;
using Podcast.DataModel;

namespace Podcast
{
    public static class DataPoller
    {
        private static User _user;

        public async static void refresh()
        {
            var user = await SubscriptionService.GetUser();

            refreshFeeds(user);

//            var file = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Assets/twig.xml"));
//            var stream = await file.OpenAsync(FileAccessMode.Read);
//
//            var surrogate = (PodcastSurrogate.rss) serializer.Deserialize(stream.AsStream());
        }

        private static void refreshFeeds(User user)
        {
            foreach (PodcastRssFeedXmlSurrogate.rss rssFeed in user.Subscriptions)
            {
                HttpClient client = new HttpClient();
                var response = client.GetAsync(new Uri(rssFeed.channel.link));
            }
        }
    }
}

