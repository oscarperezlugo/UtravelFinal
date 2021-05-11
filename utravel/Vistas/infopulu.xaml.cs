using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace utravel.Vistas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class infopulu : ContentPage
    {
        public infopulu()
        {
            InitializeComponent();
        }
        public async void Siguiente_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();

        }
    }
}