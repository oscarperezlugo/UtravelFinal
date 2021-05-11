using Acr.UserDialogs;
using System;
using System.Threading.Tasks;
using utravel.Modelos;
using utravel.Servicios;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace utravel.Vistas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {

        public LoginPage()
        {
            InitializeComponent();
        }
        public async void Login_Clicked(object sender, EventArgs e)
        {
            IUserDialogs Dialogs = UserDialogs.Instance;

            if (nombredeusuario.Text != null && contrasena.Text != null)
            {
                Login usuariologin = new Login();
                usuariologin.User = nombredeusuario.Text;
                usuariologin.Pass = contrasena.Text;
                Repositorio repositorio = new Repositorio();
                try
                {
                    ClasesRecibo userlogin = repositorio.ConnectUser(usuariologin).Result;
                    
                    try
                    {
                        await SecureStorage.SetAsync("id", userlogin.id.ToString());
                        await SecureStorage.SetAsync("nombre", userlogin.nombre);
                        await SecureStorage.SetAsync("telefono", userlogin.telefono);
                        await SecureStorage.SetAsync("fecha", userlogin.fecha);                        
                        await SecureStorage.SetAsync("email", userlogin.email);
                        Dialogs.ShowLoading("Bienvenido " + userlogin.nombre + "");
                    }
                    catch (Exception ex)
                    {

                    }
                    await Task.Delay(2000);
                    Dialogs.HideLoading();
                    if (userlogin.Token != "0")
                    {
                        try
                        {
                            var location = await Geolocation.GetLocationAsync();

                            if (location != null)
                            {
                                await SecureStorage.SetAsync("latitude", location.Latitude.ToString());
                                await SecureStorage.SetAsync("longitude", location.Longitude.ToString());
                                
                            }
                            else
                            {
                                location.Latitude = 0;
                                location.Longitude = 0;
                                await SecureStorage.SetAsync("latitude", location.Latitude.ToString());
                                await SecureStorage.SetAsync("longitude", location.Longitude.ToString());
                            }
                        }
                        catch { }


                        MapPage myHomePage = new MapPage();
                        NavigationPage.SetHasNavigationBar(myHomePage, false);
                        await Navigation.PushModalAsync(myHomePage);
                    }
                    else 
                    {
                        Dialogs.ShowLoading("Combinacion de usuario y contrasena incorrectos");
                        await Task.Delay(2000);
                        Dialogs.HideLoading();
                    }
                }
                catch { }               
            }
            
        }
        private async void Registrarse_Clicked(object sender, EventArgs e)
        {
            RegistroPage myHomePage = new RegistroPage();
            NavigationPage.SetHasNavigationBar(myHomePage, false);
            await Navigation.PushModalAsync(myHomePage);
        }
    }

}