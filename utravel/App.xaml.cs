using System;
using Xamarin.Forms;
using utravel.Vistas;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;

namespace utravel
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new LoginPage();
        }

        protected override void OnStart()
        {
            SecureStorage.RemoveAll();
        }

        protected override void OnSleep()
        {
            SecureStorage.RemoveAll();
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
