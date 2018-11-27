using Plugin.Geolocator;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MyLatLongApp
{
    public partial class MainPage : ContentPage
    {
        Stopwatch timer;
        public Task Initialization { get; private set; }
        public MainPage()
        {
            InitializeComponent();
            timer = new Stopwatch();
            Initialization = InitializeAsync();
        }

        private async Task InitializeAsync()
        {
            // Asynchronously initialize this instance.
            await GetLocation();
        }
        private async void Button_Clicked(object sender, EventArgs e)
        {
            await GetLocation();
        }

        private async Task GetLocation()
        {
            timer.Reset();
            timer.Start();

            var locator = CrossGeolocator.Current;
            locator.DesiredAccuracy = 100;

            var position2 = await locator.GetLastKnownLocationAsync();
            var position = await locator.GetPositionAsync(TimeSpan.FromSeconds(2), null, true);
            OldLat.Text = Lat.Text;
            OldLng.Text = Lng.Text;
            Lat.Text = position.Latitude.ToString();
            Lng.Text = position.Longitude.ToString();
            var addresses = await locator.GetAddressesForPositionAsync(position);
            Addr.Text = addresses.FirstOrDefault().Locality.ToString();
            timer.Stop();

            ExTime.Text = timer.Elapsed.TotalSeconds.ToString();
        }
    }
}
