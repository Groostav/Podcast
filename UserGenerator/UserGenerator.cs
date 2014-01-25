using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Windows.Storage;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using Podcast.Common;
using Podcast.DataModel;

namespace UserGenerator
{
    [TestClass]
    public sealed class UserGenerator
    {
        [TestMethod]
        public async static void create_user_with_security_now_sub()
        {
            var appData = ApplicationData.Current.LocalFolder;
            var outFile = await appData.CreateFileAsync("Subscriptions.xml");
            var outStream = await outFile.OpenAsync(FileAccessMode.ReadWrite);

            var inFile = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Assets/sn.xml"));
            var inStream = await inFile.OpenAsync(FileAccessMode.Read);
            var rssFeed = (PodcastSurrogate.rss)PodcastSurrogate.rss.Serializer.Deserialize(inStream.AsStream());

            var user = new User();
            user.Subscriptions.Add(rssFeed);

            User.Serializer.Serialize(outStream.AsStream(), user);

        }
    }
}
