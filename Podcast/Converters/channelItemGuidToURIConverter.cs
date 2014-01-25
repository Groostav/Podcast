using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;
using Podcast.Common;

namespace Podcast.Converters
{
    public class channelItemGuidToURIConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is PodcastRssFeedXmlSurrogate.rssChannelItemGuid)
            {
                var guid = value as PodcastRssFeedXmlSurrogate.rssChannelItemGuid;
                return new Uri(guid.Value);
            }
            else
            {
                return value;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}