using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using utravel.Servicios;
using Xamarin.Forms;
using Acr.UserDialogs;
using Xamarin.Forms.Xaml;
using utravel.Modelos;
using Xamarin.Essentials;

namespace utravel.Vistas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PersonasPage : ContentPage
    {
        IUserDialogs Dialogs = UserDialogs.Instance;
        System.DateTime Fecha;
        string Hora;
        string ubicacion;
        public PersonasPage(Calculos calculosrec)
        {
            InitializeComponent();
            Fecha = calculosrec.date;
            Hora = calculosrec.time;
            ubicacion = calculosrec.UbicacionCliente;
            Personas.SelectedIndex = 0;
            Carros.SelectedIndex = 0;
        }
        public async void Siguiente_Clicked(object sender, EventArgs e)
        {
            Repositorio repositorio = new Repositorio();
            Calculos calculos = new Calculos();
            if (Personas.SelectedItem.ToString() == null)
            {                
                calculos.personas = 1;
                calculos.carros = 1;
                Dialogs.ShowLoading("Por defecto se asumira una persona y un vehiculo"); 
                await Task.Delay(2000);
                Dialogs.HideLoading();
                calculos.time = Hora;
                calculos.buses = 1;
                calculos.date = Fecha;
                calculos.UbicacionCliente = ubicacion;
                CabeceraPreCompra orden = repositorio.OrderCreate(calculos).Result;
                Dialogs.ShowLoading("Inicia la aventura");
                await Task.Delay(2000);
                Dialogs.HideLoading();
                try
                {
                    await SecureStorage.SetAsync("idorden", orden.IDorden.ToString());
                }
                catch (Exception ex)
                {

                }
                var myHomePage = new MitadMundo(calculos);
                NavigationPage.SetHasNavigationBar(myHomePage, true);
                await Navigation.PushModalAsync(myHomePage);

            }
            else 
            {
                int person = Int16.Parse(Personas.SelectedItem.ToString());
                calculos.personas = person;
                int carr = Int16.Parse(Carros.SelectedItem.ToString());
                calculos.carros = carr;
                calculos.time = Hora;
                calculos.date = Fecha;
                //calculos.buses = Int16.Parse(Buses.Text);
                calculos.UbicacionCliente = ubicacion;
                CabeceraPreCompra orden = repositorio.OrderCreate(calculos).Result;
                Dialogs.ShowLoading("Inicia la aventura");
                await Task.Delay(2000);
                Dialogs.HideLoading();
                try
                {
                    await SecureStorage.SetAsync("idorden", orden.IDorden.ToString());
                }
                catch (Exception ex)
                {

                }
                var myHomePage = new MitadMundo(calculos);
                NavigationPage.SetHasNavigationBar(myHomePage, true);
                await Navigation.PushModalAsync(myHomePage);
            }
                  
            
        }
            
    }
        


    

}