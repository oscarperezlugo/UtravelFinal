using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using utravel.Modelos;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace utravel.Vistas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Pago : ContentPage
    {
        string MONTO;
        public Pago()
        {
            InitializeComponent();
            MONTO = "100";
            Pasarela pasarela1 = new Pasarela();
            pasarela1.Monto = MONTO;
            Double montofin = (Double.Parse(pasarela1.Monto)) * 100;
            Double sinimpu = (Double.Parse(pasarela1.Monto) * 1.12) * 100;
            Guid oper = System.Guid.NewGuid();
            string url = "https://utravel.somee.com/Pago?monto=" + HttpUtility.UrlEncode(sinimpu.ToString()) + "&impu=" + HttpUtility.UrlEncode(montofin.ToString()) + "&guid=" + HttpUtility.UrlEncode(oper.ToString());
            Pasarela.Source = url;

        }
        protected void OnAppearing()
        {
            
        }
    }
}