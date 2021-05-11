using Plugin.Geolocator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Acr.UserDialogs;
using utravel.Modelos;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;


namespace utravel.Vistas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MapPage : ContentPage
    {
        IUserDialogs Dialogs = UserDialogs.Instance;
        double A;
        double B;        
        public MapPage()
        {

            InitializeComponent();




        }
        protected async override void OnAppearing()
        {



            var request = new GeolocationRequest(GeolocationAccuracy.Medium);
            var location = await Geolocation.GetLastKnownLocationAsync();
            Position position = new Position(location.Latitude, location.Longitude);
            if (location == null)
            {
                A = 0;
                B = 0;
            }
            else
            {
                A = position.Longitude;
                B = position.Latitude;
            }            
            MapSpan mapSpan = MapSpan.FromCenterAndRadius(position, Xamarin.Forms.Maps.Distance.FromKilometers(1));
            map.MoveToRegion(mapSpan);
            var reLocate = new Button { Text = "Re-center" };


            Pin pin = new Pin
            {
                Position = new Position(B, A),
                Type = PinType.Place,
                Label = "Tu Ubicación",
                Address = "A donde inicia la aventura"
            };
            map.Pins.Add(pin);


        }

        async void OnMarkerClickedAsync(object sender, PinClickedEventArgs e)
        {
            e.HideInfoWindow = true;
            string pinName = ((Pin)sender).Label;
            await DisplayAlert("Pin Clicked", $"{pinName} was clicked.", "Ok");
        }

        async void OnInfoWindowClickedAsync(object sender, PinClickedEventArgs e)
        {
            string pinName = ((Pin)sender).Label;
            await DisplayAlert("Info Window Clicked", $"The info window was clicked for {pinName}.", "Ok");
        }
        public async void Map_Clicked(object sender, EventArgs e)
        {
            CabeceraPreCompra cabeceraPreCompra = new CabeceraPreCompra();            
            var location = await Geolocation.GetLastKnownLocationAsync();
            string Ubic = "" + location.Latitude.ToString() + ", " + location.Longitude.ToString() + "";
            cabeceraPreCompra.UbicacionCliente = Ubic;
            FechaPage myHomePage = new FechaPage(cabeceraPreCompra);
            NavigationPage.SetHasNavigationBar(myHomePage, false);
            await Navigation.PushModalAsync(myHomePage);

        }
    }
}