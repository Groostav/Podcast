using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Windows.Storage;
using Windows.Storage.Search;
using Podcast.Common;
using Podcast.DataModel;

namespace Podcast
{
    static class SubscriptionService
    {
        private static User _user;

        public async static Task<IEnumerable<PodcastRssFeedXmlSurrogate.rss>> GetSubscriptions()
        {
            var user = await GetUser();
            return user.Subscriptions;
        }

        public static async Task<User> GetUser()
        {
            if (_user != null)
            {
                return _user;
            }

            var appData = ApplicationData.Current.LocalFolder;
            var availableFiles = await appData.GetFilesAsync(CommonFileQuery.DefaultQuery);

            if ( ! availableFiles.Any(file => file.Name.Equals("Subscriptions.xml")))
            {
                _user = new User();
                return _user;
            }

            var settingsFile = await appData.GetFileAsync("Subscriptions.xml");
            using (var stream = await settingsFile.OpenAsync(FileAccessMode.Read))
            {
                _user = (User) User.Serializer.Deserialize(stream.AsStream());
                return _user;
            }
        }
    }
}