using Acr.UserDialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using utravel.Modelos;
using utravel.Servicios;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace utravel.Vistas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FechaPage : ContentPage
    {
        string ubicacion;
        public FechaPage(CabeceraPreCompra cabeceraPreCompra)
        {
            InitializeComponent();
            ubicacion = cabeceraPreCompra.UbicacionCliente;
        }
        public async void Siguiente_Clicked(object sender, EventArgs e)
        {
            Calculos calculos = new Calculos();
            calculos.date = DateX.Date;
            calculos.time = TimeX.Time.ToString();
            calculos.UbicacionCliente = ubicacion;
            PersonasPage myHomePage = new PersonasPage(calculos);
            NavigationPage.SetHasNavigationBar(myHomePage, true);
            await Navigation.PushModalAsync(myHomePage);
        }


    }
    
}
  
