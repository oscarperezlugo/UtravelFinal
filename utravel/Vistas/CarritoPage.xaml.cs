using Acr.UserDialogs;
using System;
using utravel.Modelos;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Threading.Tasks;
using utravel.Servicios;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;
using System.Web;

namespace utravel.Vistas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CarritoPage : ContentPage
    {
        
        public CarritoPage(Calculos calc)
        {
            InitializeComponent();


            Personas.Text = calc.personas.ToString();
            Carros.Text = calc.carros.ToString();
            Buses.Text = calc.buses.ToString();
            Distancia.Text = calc.distancia.ToString("N2");
            Puntos.Text = calc.puntos.ToString();
            Monto.Text = calc.monto.ToString("N2");
            Gaseosas.Text = calc.gaseosas.ToString();
            Tiempo.Text = calc.tiempo;



        }
        public async void Siguiente_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();

        }
        public async void Pagar_Clicked(object sender, EventArgs e)
        {            
                       
            IUserDialogs Dialogs = UserDialogs.Instance;            
            Repositorio repositorio = new Repositorio();
            CabeceraActualizacion cabeceraActualizacion = new CabeceraActualizacion();
            int puntos = Int16.Parse(Puntos.Text);
            cabeceraActualizacion.CantidadPuntos = puntos;
            string idorden = await SecureStorage.GetAsync("idorden");
            cabeceraActualizacion.IDorden = Int16.Parse(idorden);
            double monto = Double.Parse(Monto.Text);
            cabeceraActualizacion.MontoTotal = monto;
            cabeceraActualizacion.TiempoFinal = Double.Parse(Tiempo.Text);
            cabeceraActualizacion.TotalKilometros = Double.Parse(Distancia.Text);
            ClasesRecibo finalorden = repositorio.OrderUpdate(cabeceraActualizacion).Result;
            Dialogs.ShowLoading(finalorden.message.ToString());
            await Task.Delay(2000);
            Dialogs.HideLoading();
            Pasarela pasarela = new Pasarela();
            pasarela.Monto = monto.ToString();
            Double MontoPre = Math.Round(monto, 2);
            Double montofin = MontoPre * 100;
            Double sinimpu = (monto * 1.12) * 100;
            Guid oper = System.Guid.NewGuid();
            
            Url url = new Url();
            url.Direc = "https://utravel.somee.com/Pago?monto=" + montofin.ToString() + "&impu=" + montofin.ToString() + "&guid=" + oper.ToString() + "";
            await Browser.OpenAsync(url.Direc, BrowserLaunchMode.External);
            MapPage myHomePage = new MapPage();
            NavigationPage.SetHasNavigationBar(myHomePage, false);
            await Navigation.PushModalAsync(myHomePage);
            //openbrowser(url);
            
        }
        private void openbrowser (Url url)
        {
            Browser.OpenAsync(url.Direc , BrowserLaunchMode.SystemPreferred);
            
        }
        public async void Cancelar_Clicked(object sender, EventArgs e)
        {
            MapPage myHomePage = new MapPage();
            NavigationPage.SetHasNavigationBar(myHomePage, false);
            await Navigation.PushModalAsync(myHomePage);
        }
    }
}