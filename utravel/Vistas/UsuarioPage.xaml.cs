using Acr.UserDialogs;
using System;
using utravel.Servicios;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using utravel.Modelos;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace utravel.Vistas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UsuarioPage : ContentPage
    {
        public UsuarioPage(UsuarioRequest usuariore)
        {
            
            InitializeComponent();
                                    
            nombre.Text = usuariore.Nombre;
            apellido.Text = usuariore.Telefono;
            fecha.Text = usuariore.FechaRegistro.ToString();
            origen.Text = "App";
            email.Text = usuariore.Email;
            id.Text = usuariore.id;   
            usuario.Text = usuariore.Email;


        }
        public async void Siguiente_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
        public async void Politicas_Clicked(object sender, EventArgs e)
        {
            PoliticaPage myHomePage = new PoliticaPage();
            NavigationPage.SetHasNavigationBar(myHomePage, false);
            await Navigation.PushModalAsync(myHomePage);
        }
        public async void Reco_Clicked(object sender, EventArgs e)
        {
            RecomendacionesPage myHomePage = new RecomendacionesPage();
            NavigationPage.SetHasNavigationBar(myHomePage, false);
            await Navigation.PushModalAsync(myHomePage);
        }
    }
}