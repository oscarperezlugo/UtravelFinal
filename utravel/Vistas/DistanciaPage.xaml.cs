using Acr.UserDialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using utravel.Servicios;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace utravel.Vistas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DistanciaPage : ContentPage
    {
        IUserDialogs Dialogs = UserDialogs.Instance;
        Repositorio repo = new Repositorio();
        public DistanciaPage()
        {
            InitializeComponent();
        }
        protected override void OnAppearing()
        {
            //Repositorio repo = new Repositorio();
            //var distanciaGoogle = repo.getDistancia();

            Distancia.Text = DateTime.Now.DayOfWeek.ToString();/*distanciaGoogle.rows[0].elements[0].distance.text;*/
            Tiempo.Text = DateTime.Now.Hour.ToString();/*distanciaGoogle.rows[0].elements[0].duration.text;*/

        }
    }
}