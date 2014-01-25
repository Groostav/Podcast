
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Section Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234229
using Newtonsoft.Json;
using Podcast.Common;
using Podcast.DataModel;
using RestSharp.Portable;

namespace Podcast
{
    /// <summary>
    /// A page that displays an overview of a single group, including a preview of the items
    /// within the group.
    /// </summary>
    public sealed partial class SearchPage : Page
    {
        private NavigationHelper navigationHelper;
        private ObservableDictionary defaultViewModel = new ObservableDictionary();

        private readonly HttpClient client = new HttpClient() { BaseAddress = new Uri("https://itunes.apple.com"), };



        public NavigationHelper NavigationHelper
        {
            get { return this.navigationHelper; }
        }

        public ObservableDictionary DefaultViewModel
        {
            get { return this.defaultViewModel; }
        }

        public SearchPage()
        {
            this.InitializeComponent();
            this.navigationHelper = new NavigationHelper(this);
            this.navigationHelper.LoadState += navigationHelper_LoadState;
        }

        private async void navigationHelper_LoadState(object sender, LoadStateEventArgs e)
        {
            var instigatingEvent = (KeyRoutedEventArgs)e.NavigationParameter;

            var key = (int)instigatingEvent.Key;

            if (key >= 0x41 && key <= 0x5A) //ascii printable
            {
                searchBox.QueryText += instigatingEvent.Key;
            }
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            navigationHelper.OnNavigatedTo(e);
            searchBox.Focus(FocusState.Keyboard);
            await Dispatcher.RunIdleAsync((x) => searchBox.Focus(FocusState.Programmatic));
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            navigationHelper.OnNavigatedFrom(e);
        }

        private async void SearchBoxEventsSuggestionsRequested(SearchBox box, SearchBoxSuggestionsRequestedEventArgs e)
        {
            if ( ! string.IsNullOrEmpty(e.QueryText))
            {
                var suggestions = (await pollItunesWithNewQuery(box.QueryText)).ToList();

                DefaultViewModel["Items"] = suggestions;
            }
        }



        private async Task<IEnumerable<ItunesPodcastDescriptorJsonSurrogate.Result>> pollItunesWithNewQuery(string queryText)
        {
            queryText = queryText.Replace(' ', '+');
            //sanitize the remaning chars
            //inject it into the search term

            var response = await client.GetAsync("/search?term=" + queryText + "&entity=podcast");
            var stream = await response.Content.ReadAsStreamAsync();
            var str = new StreamReader(stream).ReadToEnd();
            var converted = JsonConvert.DeserializeObject<ItunesPodcastDescriptorJsonSurrogate>(str);
            return converted.results.ToList();
        }

        /// <summary>
        /// Called when query submitted in SearchBox
        /// </summary>
        /// <param name="sender">The Xaml SearchBox</param>
        /// <param name="e">Event when user submits query</param>
        private void SearchBoxEventsQuerySubmitted(SearchBox box, SearchBoxQuerySubmittedEventArgs e)
        {
            var queryText = e.QueryText;
            if (!string.IsNullOrEmpty(queryText))
            {

            }
        }

        private void UIElement_OnTapped(object sender, TappedRoutedEventArgs e)
        {
             
        }
    }

}