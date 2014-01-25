using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238
using Podcast.Common;
using Podcast.DataModel;

namespace Podcast
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class BlankPage1 : Page
    {
        public BlankPage1()
        {
            this.InitializeComponent();
            DoStuff();
        }

        public static async void DoStuff()
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
            //hehehe
        }
    }
}
