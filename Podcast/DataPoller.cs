using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Windows.ApplicationModel;
using Windows.Storage;
using Windows.Storage.Search;
using Windows.Web.Http;
using Podcast.Common;
using Windows.Storage.Streams;
using Windows.Storage;

namespace Podcast
{
    public static class DataPoller
    {
        private static readonly XmlSerializer serializer = new XmlSerializer(typeof(User));

        public async static void refresh()
        {
            var user = await GetUser();

            refreshFeeds(user);

//            var file = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Assets/twig.xml"));
//            var stream = await file.OpenAsync(FileAccessMode.Read);
//
//            var surrogate = (PodcastSurrogate.rss) serializer.Deserialize(stream.AsStream());
        }

        private static void refreshFeeds(User user)
        {
            foreach (PodcastSurrogate.rss rssFeed in user.Subscriptions)
            {
                HttpClient client = new HttpClient();
                var response = client.GetAsync(new Uri(rssFeed.channel.link));
            }
        }

        private static async Task<User> GetUser()
        {
            var appData = ApplicationData.Current.LocalFolder;
            var availableFiles = await appData.GetFilesAsync(CommonFileQuery.DefaultQuery);

            if (!availableFiles.Any(file => file.Name.Equals("Subscriptions.xml")))
            {
                return new User();
            }

            var settingsFile = await appData.GetFileAsync("Subscriptions.xml");
            using (var stream = await settingsFile.OpenAsync(FileAccessMode.Read))
            {
                return (User) serializer.Deserialize(stream.AsStream());
            }
        }
    }
}

