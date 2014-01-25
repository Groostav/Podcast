using System;
using Windows.Data.Html;
using Windows.UI.Xaml.Data;

namespace Podcast.Converters
{
    public sealed class HtmlToTextConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if(value is String)
            {
                return HtmlUtilities.ConvertToText(value.ToString());
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