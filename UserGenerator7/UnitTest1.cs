using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using Podcast.Common;
using Podcast.DataModel;

namespace UserGenerator7
{
    [TestClass]
    public sealed class UserGenerator
    {
        [TestMethod]
        public void create_user_with_security_now_sub()
        {
            getTask();
        }

        private static async void getTask()
        {
            var outFile = await DownloadsFolder.CreateFileAsync("Subscriptions.xml");
            using (var outStream = await outFile.OpenAsync(FileAccessMode.ReadWrite))
            {
                var inFile = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Assets/sn.xml"));
                var inStream = await inFile.OpenAsync(FileAccessMode.Read);
                var rssFeed = (PodcastSurrogate.rss)PodcastSurrogate.rss.Serializer.Deserialize(inStream.AsStream());

                var user = new User();
                user.Subscriptions.Add(rssFeed);

                User.Serializer.Serialize(outStream.AsStream(), user);    
            }
        }
    }
}
