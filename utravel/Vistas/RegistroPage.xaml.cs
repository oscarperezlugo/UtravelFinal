using Acr.UserDialogs;
using System;
using System.Threading.Tasks;
using utravel.Modelos;
using Xamarin.Forms;
using utravel.Servicios;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;

namespace utravel.Vistas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegistroPage : CarouselPage
    {
        string Message;
        public RegistroPage()
        {
            InitializeComponent();


        }
        public async void Registro_Clicked(object sender, EventArgs args)
        {
            IUserDialogs Dialogs = UserDialogs.Instance;

            if (contrasena.Text != null && email.Text != null && contrasena.Text.Equals(conficontrasena.Text))
            {


                UsuarioRequest usuario = new UsuarioRequest();
                ;
                
                usuario.Email = email.Text;                
                usuario.Nombre = ""+nombre.Text +" "+ apellido.Text+"";
                usuario.Telefono = telefono.Text;                
                usuario.Pass = contrasena.Text;
                



                Repositorio repository = new Repositorio();
                




                
                ClasesRecibo user = repository.postUserCreate(usuario).Result;
                if (user.Email != null)
                {
                    Dialogs.ShowLoading("Bienvenido " + user.Nombre + "");
                    await Task.Delay(2000);
                    
                    try
                    {
                        await SecureStorage.SetAsync("id", user.IDUser.ToString());
                        await SecureStorage.SetAsync("nombre", user.Nombre);
                        await SecureStorage.SetAsync("telefono", user.Telefono);
                        await SecureStorage.SetAsync("fecha", user.FechaRegistro);
                        await SecureStorage.SetAsync("email", user.Email);
                    }
                    catch (Exception ex)
                    {

                    }
                    
                    try
                    {
                        var location = await Geolocation.GetLastKnownLocationAsync();

                        if (location != null)
                        {
                            await SecureStorage.SetAsync("latitude", location.Latitude.ToString());
                            await SecureStorage.SetAsync("longitude", location.Longitude.ToString());
                        }
                    }
                    catch { }
                    Dialogs.HideLoading();
                    MapPage myHomePage = new MapPage();
                    NavigationPage.SetHasNavigationBar(myHomePage, false);
                    await Navigation.PushModalAsync(myHomePage);
                }
                else 
                {
                    Dialogs.ShowLoading("Intente de nuevo " + user.Message + "");
                    await Task.Delay(2000);
                    Dialogs.HideLoading();
                }

            }


            else
            {
                await DisplayAlert("Registrarse", "Verifique la Información", "Gracias");
            }

        }
        public void Siguiente_Clicked(object sender, EventArgs e)
        {
            CurrentPage = paso2;
            
        }
    }
}