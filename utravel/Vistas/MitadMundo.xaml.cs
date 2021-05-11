using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Essentials;
using System.Threading.Tasks;
using utravel.Modelos;
using utravel.Servicios;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Acr.UserDialogs;
using System.Collections.ObjectModel;

namespace utravel.Vistas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MitadMundo : CarouselPage
    {
        double TIEMPO;
        string fechaconver;
        int personas;
        int carros;
        int buses;
        int fuzet;
        int cocat;
        int lightt;
        int aguat;
        double fuzep;
        double cocap;
        double lightp;
        double aguap;
        double gasp;
        double distanciaF;
        double tiempoF;
        double TiempoMedio;
        System.DateTime Fecha;
        string Hora;
        int puntos;
        int A;
        int B;
        int C;
        int D;
        int E;
        int F;
        int G;
        int H;
        int I;
        int J;
        int K;
        int L;
        int M;
        int N;
        int O;
        Location mitadmundo = new Location(0.0971424, -78.3619518);
        Location teleferico = new Location(0.191264, -78.519396);
        Location panecillo = new Location(-0.228905, -78.518792);
        Location zoologico = new Location(-0.071145, -78.356270);
        Location cdl = new Location(-0.220410, -78.527996);
        Location mda = new Location(-0.2175062, -78.5210745);
        Location maqd = new Location(-0.202850, -78.492492);
        Location mind = new Location(-0.054556, -78.772934);
        Location maquid = new Location(0.0549045, -78.6817723);
        Location quilod = new Location(-0.867083, -78.916022);
        Location cotod = new Location(-0.7755869, -79.1405797);
        Location pulud = new Location(0.0253033, -78.4853774);
        Location banod = new Location(-1.398275, -78.423051);
        Location otad = new Location(0.230039, -78.262448);
        Location papad = new Location(-0.370550, -78.148301);
        IUserDialogs Dialogs = UserDialogs.Instance;
        Repositorio repo = new Repositorio();
        public MitadMundo(Calculos calc)

        {

            InitializeComponent();
            RutaFinal.ItemsSource = Lineas;
            personas = calc.personas;
            carros = calc.carros;
            buses = calc.buses;
            Fecha = calc.date;
            Hora = calc.time;
            

            //distancia = miles;

        }
        protected override void OnAppearing() 
        {
            var hor = System.DateTime.Parse(Hora);
            if (Fecha.DayOfWeek.ToString() == "Monday")
            {
                Children.Remove(T);
                Children.Remove(zoo);
                Children.Remove(ma);
            }
            if (hor.Hour >= 17 )
            {
                Children.Remove(T);
                Children.Remove(MM);
                Children.Remove(cl);
                Children.Remove(maq);
                Children.Remove(zoo);
                Children.Remove(pulu);
                Children.Remove(coto);
                Children.Remove(ota);
            }
            if (hor.Hour + TiempoMedio >= 17)
            {
                Children.Remove(T);
                Children.Remove(MM);
                Children.Remove(cl);
                Children.Remove(maq);
                Children.Remove(zoo);
                Children.Remove(pulu);
                Children.Remove(coto);
                Children.Remove(ota);
            }
            if (hor.Hour >= 18)
            {
                Children.Remove(ma);
            }
            if (hor.Hour + TiempoMedio >= 18) 
            {
                Children.Remove(ma);
            }

        }
        public async void Check_Clicked(object sender, EventArgs e)
        {
            string result = await DisplayPromptAsync("Tiempo", "¿Cuanto tiempo desea pasar en el punto?", initialValue: "1", maxLength: 1, keyboard: Keyboard.Numeric);
            if (result == null)
            {
                CurrentPage = MM;
            }
            else
            {
                var location = await Geolocation.GetLastKnownLocationAsync();
                Repositorio repo = new Repositorio();
                Consulta consulta = new Consulta();
                consulta.origen = "" + location.Latitude.ToString() + ", " + location.Longitude.ToString() + "";
                consulta.destino = "Manuel Cordova Galarza Km. 13, 5 SN, Quito, Ecuador";
                //consulta.destino = "Golden Gate Bridge";
                var distanciaGoogle = repo.getDistancia(consulta);
                distanciaF = distanciaGoogle.rows[0].elements[0].distance.value;
                tiempoF = distanciaGoogle.rows[0].elements[0].duration.value;
                puntos = puntos + 1;
                A = 1;
                Lineas lin = new Lineas();
                lin.Producto = t1.Text;
                lin.Tiempo = distanciaGoogle.rows[0].elements[0].duration.value.ToString();
                lin.Kilometros = distanciaGoogle.rows[0].elements[0].distance.value.ToString();
                lin.TiempoPunto = Double.Parse(result).ToString();
                TIEMPO = TIEMPO + Double.Parse(result);
                Lineas lineas = await repo.PostLineas(lin);
                Dialogs.ShowLoading("Buena Elección");
                await Task.Delay(2000);
                Dialogs.HideLoading();
                TiempoMedio = ((buses * puntos) + (tiempoF / 3600));
                Lineas.Add(lin);                
                if (TiempoMedio >= 8)
                {
                    Dialogs.ShowLoading("Has logrado el limite de tiempo diario");
                    await Task.Delay(2000);
                    Dialogs.HideLoading();
                    CurrentPage = gaseosas;
                    Children.Remove(MM);
                    Children.Remove(T);
                    Children.Remove(pan);
                    Children.Remove(zoo);
                    Children.Remove(cl);
                    Children.Remove(ma);
                    Children.Remove(maq);
                    Children.Remove(maqui);
                    Children.Remove(pulu);
                    Children.Remove(min);
                    Children.Remove(coto);
                    Children.Remove(bano);
                    Children.Remove(ota);
                    Children.Remove(papa);
                    Children.Remove(quilo);
                }
                else
                {
                    CurrentPage = T;
                    Children.Remove(MM);
                }
            }
            
            


        }
        

        public async void Check1_Clicked(object sender, EventArgs e)
        {
            string result = await DisplayPromptAsync("Tiempo", "¿Cuanto tiempo desea pasar en el punto?", initialValue: "1", maxLength: 1, keyboard: Keyboard.Numeric);
            if (result == null)
            {
                CurrentPage = T;
            }
            else
            {
                var location = await Geolocation.GetLastKnownLocationAsync();
                double km;
                double hr;
                if (A == 1)
                {
                    Repositorio repo = new Repositorio();
                    Consulta consulta = new Consulta();
                    consulta.origen = "Manuel Cordova Galarza Km. 13, 5 SN, Quito, Ecuador";
                    consulta.destino = "Teleférico Quito, Quito, Ecuador";
                    var distanciaGoogle = repo.getDistancia(consulta);
                    double ruta = distanciaGoogle.rows[0].elements[0].distance.value;
                    double tiempo = distanciaGoogle.rows[0].elements[0].duration.value + Double.Parse(result);
                    km = ruta;
                    hr = tiempo;
                    distanciaF = distanciaF + ruta;
                    tiempoF = tiempoF + tiempo;
                }
                else
                {
                    Repositorio repo = new Repositorio();
                    Consulta consulta = new Consulta();
                    consulta.origen = "" + location.Latitude.ToString() + ", " + location.Longitude.ToString() + "";
                    consulta.destino = "Teleférico Quito, Quito, Ecuador";
                    var distanciaGoogle = repo.getDistancia(consulta);
                    double ruta = distanciaGoogle.rows[0].elements[0].distance.value;
                    double tiempo = distanciaGoogle.rows[0].elements[0].duration.value;
                    km = ruta;
                    hr = tiempo;
                    distanciaF = ruta;
                    tiempoF = tiempo;
                }
                puntos = puntos + 1;
                B = 1;
                Lineas lin = new Lineas();
                lin.Producto = t2.Text;
                lin.Tiempo = hr.ToString();
                lin.Kilometros = km.ToString();
                lin.TiempoPunto = result;                
                Lineas lineas = await repo.PostLineas(lin);
                Dialogs.ShowLoading("Buena Elección");
                await Task.Delay(2000);
                Dialogs.HideLoading();
                TiempoMedio = ((buses * puntos) + (tiempoF / 3600));
                Lineas.Add(lin);
                TIEMPO = TIEMPO + Double.Parse(result);
                if (TiempoMedio >= 8)
                {
                    Dialogs.ShowLoading("Has logrado el limite de tiempo diario");
                    await Task.Delay(2000);
                    Dialogs.HideLoading();
                    CurrentPage = gaseosas;
                    Children.Remove(MM);
                    Children.Remove(T);
                    Children.Remove(pan);
                    Children.Remove(zoo);
                    Children.Remove(cl);
                    Children.Remove(ma);
                    Children.Remove(maq);
                    Children.Remove(maqui);
                    Children.Remove(pulu);
                    Children.Remove(min);
                    Children.Remove(coto);
                    Children.Remove(bano);
                    Children.Remove(ota);
                    Children.Remove(papa);
                    Children.Remove(quilo);
                }
                else
                {
                    CurrentPage = pan;
                    Children.Remove(T);
                }
            }
            
        }
        public async void Check2_Clicked(object sender, EventArgs e)
        {
            string result = await DisplayPromptAsync("Tiempo", "¿Cuanto tiempo desea pasar en el punto?", initialValue: "1", maxLength: 1, keyboard: Keyboard.Numeric);
            if (result == null)
            {
                CurrentPage = pan;
            }
            else
            {
                var location = await Geolocation.GetLastKnownLocationAsync();
                double km;
                double hr;
                if (B == 1)
                {
                    Repositorio repo = new Repositorio();
                    Consulta consulta = new Consulta();
                    consulta.origen = "Teleférico Quito, Quito, Ecuador";
                    consulta.destino = "Panecillo, Quito, Ecuador";
                    var distanciaGoogle = repo.getDistancia(consulta);
                    double ruta = distanciaGoogle.rows[0].elements[0].distance.value;
                    double tiempo = distanciaGoogle.rows[0].elements[0].duration.value + Double.Parse(result);
                    km = ruta;
                    hr = tiempo;
                    distanciaF = distanciaF + ruta;
                    tiempoF = tiempoF + tiempo;
                }
                else
                {
                    if (A == 1)
                    {
                        Repositorio repo = new Repositorio();
                        Consulta consulta = new Consulta();
                        consulta.origen = "Manuel Cordova Galarza Km. 13, 5 SN, Quito, Ecuador";
                        consulta.destino = "Panecillo, Quito, Ecuador";
                        var distanciaGoogle = repo.getDistancia(consulta);
                        double ruta = distanciaGoogle.rows[0].elements[0].distance.value;
                        double tiempo = distanciaGoogle.rows[0].elements[0].duration.value + Double.Parse(result);
                        km = ruta;
                        hr = tiempo;
                        distanciaF = distanciaF + ruta;
                        tiempoF = tiempoF + tiempo;
                    }
                    else
                    {
                        Repositorio repo = new Repositorio();
                        Consulta consulta = new Consulta();
                        consulta.origen = "" + location.Latitude.ToString() + ", " + location.Longitude.ToString() + "";
                        consulta.destino = "Panecillo, Quito, Ecuador";
                        var distanciaGoogle = repo.getDistancia(consulta);
                        double ruta = distanciaGoogle.rows[0].elements[0].distance.value;
                        double tiempo = distanciaGoogle.rows[0].elements[0].duration.value;
                        km = ruta;
                        hr = tiempo;
                        distanciaF = ruta;
                        tiempoF = tiempo;
                    }
                }
                puntos = puntos + 1;
                C = 1;
                Lineas lin = new Lineas();
                lin.Producto = t3.Text;
                lin.Tiempo = hr.ToString();
                lin.Kilometros = km.ToString();
                lin.TiempoPunto = result;
                
                Lineas lineas = await repo.PostLineas(lin);
                Dialogs.ShowLoading("Buena Elección");
                await Task.Delay(2000);
                Dialogs.HideLoading();
                TiempoMedio = ((buses * puntos) + (tiempoF / 3600));
                Lineas.Add(lin);
                TIEMPO = TIEMPO + Double.Parse(result);
                if (TiempoMedio >= 8)
                {
                    Dialogs.ShowLoading("Has logrado el limite de tiempo diario");
                    await Task.Delay(2000);
                    Dialogs.HideLoading();
                    CurrentPage = gaseosas;
                    Children.Remove(MM);
                    Children.Remove(T);
                    Children.Remove(pan);
                    Children.Remove(zoo);
                    Children.Remove(cl);
                    Children.Remove(ma);
                    Children.Remove(maq);
                    Children.Remove(maqui);
                    Children.Remove(pulu);
                    Children.Remove(min);
                    Children.Remove(coto);
                    Children.Remove(bano);
                    Children.Remove(ota);
                    Children.Remove(papa);
                    Children.Remove(quilo);
                }
                else
                {
                    CurrentPage = zoo;
                    Children.Remove(pan);
                }
            }
            
        }
        public async void Check3_Clicked(object sender, EventArgs e)
        {
            string result = await DisplayPromptAsync("Tiempo", "¿Cuanto tiempo desea pasar en el punto?", initialValue: "1", maxLength: 1, keyboard: Keyboard.Numeric);
            if (result == null)
            {
                CurrentPage = zoo;
            }
            else
            {
                var location = await Geolocation.GetLastKnownLocationAsync();
                double km;
                double hr;
                if (C == 1)
                {
                    Repositorio repo = new Repositorio();
                    Consulta consulta = new Consulta();
                    consulta.origen = "Panecillo, Quito, Ecuador";
                    consulta.destino = "Zoológico de Quito en Guayllabamba, Urb, Huertos Familiares, Quito 170209, Ecuador";
                    var distanciaGoogle = repo.getDistancia(consulta);
                    double ruta = distanciaGoogle.rows[0].elements[0].distance.value;
                    double tiempo = distanciaGoogle.rows[0].elements[0].duration.value + Double.Parse(result);
                    km = ruta;
                    hr = tiempo;
                    distanciaF = distanciaF + ruta;
                    tiempoF = tiempoF + tiempo;
                }
                else
                {
                    if (B == 1)
                    {
                        Repositorio repo = new Repositorio();
                        Consulta consulta = new Consulta();
                        consulta.origen = "Teleférico Quito, Quito, Ecuador";
                        consulta.destino = "Zoológico de Quito en Guayllabamba, Urb, Huertos Familiares, Quito 170209, Ecuador";
                        var distanciaGoogle = repo.getDistancia(consulta);
                        double ruta = distanciaGoogle.rows[0].elements[0].distance.value;
                        double tiempo = distanciaGoogle.rows[0].elements[0].duration.value + Double.Parse(result);
                        km = ruta;
                        hr = tiempo;
                        distanciaF = distanciaF + ruta;
                        tiempoF = tiempoF + tiempo;
                    }
                    else
                    {
                        if (A == 1)
                        {
                            Repositorio repo = new Repositorio();
                            Consulta consulta = new Consulta();
                            consulta.origen = "Manuel Cordova Galarza Km. 13, 5 SN, Quito, Ecuador";
                            consulta.destino = "Zoológico de Quito en Guayllabamba, Urb, Huertos Familiares, Quito 170209, Ecuador";
                            var distanciaGoogle = repo.getDistancia(consulta);
                            double ruta = distanciaGoogle.rows[0].elements[0].distance.value;
                            double tiempo = distanciaGoogle.rows[0].elements[0].duration.value + Double.Parse(result);
                            km = ruta;
                            hr = tiempo;
                            distanciaF = distanciaF + ruta;
                            tiempoF = tiempoF + tiempo;
                        }
                        else
                        {
                            Repositorio repo = new Repositorio();
                            Consulta consulta = new Consulta();
                            consulta.origen = "" + location.Latitude.ToString() + ", " + location.Longitude.ToString() + "";
                            consulta.destino = "Zoológico de Quito en Guayllabamba, Urb, Huertos Familiares, Quito 170209, Ecuador";
                            var distanciaGoogle = repo.getDistancia(consulta);
                            double ruta = distanciaGoogle.rows[0].elements[0].distance.value;
                            double tiempo = distanciaGoogle.rows[0].elements[0].duration.value;
                            km = ruta;
                            hr = tiempo;
                            distanciaF = ruta;
                            tiempoF = tiempo;
                        }
                    }
                }
                puntos = puntos + 1;
                D = 1;
                Lineas lin = new Lineas();
                lin.Producto = t4.Text;
                lin.Tiempo = hr.ToString();
                lin.Kilometros = km.ToString();
                lin.TiempoPunto = result;
                
                Lineas lineas = await repo.PostLineas(lin);
                Dialogs.ShowLoading("Buena Elección");
                await Task.Delay(2000);
                Dialogs.HideLoading();
                TiempoMedio = ((buses * puntos) + (tiempoF / 3600));
                Lineas.Add(lin);
                TIEMPO = TIEMPO + Double.Parse(result);
                if (TiempoMedio >= 8)
                {
                    Dialogs.ShowLoading("Has logrado el limite de tiempo diario");
                    await Task.Delay(2000);
                    Dialogs.HideLoading();
                    CurrentPage = gaseosas;
                    Children.Remove(MM);
                    Children.Remove(T);
                    Children.Remove(pan);
                    Children.Remove(zoo);
                    Children.Remove(cl);
                    Children.Remove(ma);
                    Children.Remove(maq);
                    Children.Remove(maqui);
                    Children.Remove(pulu);
                    Children.Remove(min);
                    Children.Remove(coto);
                    Children.Remove(bano);
                    Children.Remove(ota);
                    Children.Remove(papa);
                    Children.Remove(quilo);
                }
                else
                {
                    CurrentPage = cl;
                    Children.Remove(zoo);
                }
            }
        }
        public async void Check4_Clicked(object sender, EventArgs e)
        {
            string result = await DisplayPromptAsync("Tiempo", "¿Cuanto tiempo desea pasar en el punto?", initialValue: "1", maxLength: 1, keyboard: Keyboard.Numeric);
            if (result == null)
            {
                CurrentPage = cl;
            }
            else
            {
                var location = await Geolocation.GetLastKnownLocationAsync();
                double km;
                double hr;
                if (D == 1)
                {
                    Repositorio repo = new Repositorio();
                    Consulta consulta = new Consulta();
                    consulta.origen = "Zoológico de Quito en Guayllabamba, Urb, Huertos Familiares, Quito 170209, Ecuador";
                    consulta.destino = "Quito 170110, Ecuador";
                    var distanciaGoogle = repo.getDistancia(consulta);
                    double ruta = distanciaGoogle.rows[0].elements[0].distance.value;
                    double tiempo = distanciaGoogle.rows[0].elements[0].duration.value + Double.Parse(result);
                    km = ruta;
                    hr = tiempo;
                    distanciaF = distanciaF + ruta;
                    tiempoF = tiempoF + tiempo;
                }
                else
                {
                    if (C == 1)
                    {
                        Repositorio repo = new Repositorio();
                        Consulta consulta = new Consulta();
                        consulta.origen = "Panecillo, Quito, Ecuador";
                        consulta.destino = "Quito 170110, Ecuador";
                        var distanciaGoogle = repo.getDistancia(consulta);
                        double ruta = distanciaGoogle.rows[0].elements[0].distance.value;
                        double tiempo = distanciaGoogle.rows[0].elements[0].duration.value + Double.Parse(result);
                        km = ruta;
                        hr = tiempo;
                        distanciaF = distanciaF + ruta;
                        tiempoF = tiempoF + tiempo;
                    }
                    else
                    {
                        if (B == 1)
                        {
                            Repositorio repo = new Repositorio();
                            Consulta consulta = new Consulta();
                            consulta.origen = "Teleférico Quito, Quito, Ecuador";
                            consulta.destino = "Quito 170110, Ecuador";
                            var distanciaGoogle = repo.getDistancia(consulta);
                            double ruta = distanciaGoogle.rows[0].elements[0].distance.value;
                            double tiempo = distanciaGoogle.rows[0].elements[0].duration.value + Double.Parse(result);
                            km = ruta;
                            hr = tiempo;
                            distanciaF = distanciaF + ruta;
                            tiempoF = tiempoF + tiempo;
                        }
                        else
                        {
                            if (A == 1)
                            {
                                Repositorio repo = new Repositorio();
                                Consulta consulta = new Consulta();
                                consulta.origen = "Manuel Cordova Galarza Km. 13, 5 SN, Quito, Ecuador";
                                consulta.destino = "Quito 170110, Ecuador";
                                var distanciaGoogle = repo.getDistancia(consulta);
                                double ruta = distanciaGoogle.rows[0].elements[0].distance.value;
                                double tiempo = distanciaGoogle.rows[0].elements[0].duration.value + Double.Parse(result);
                                km = ruta;
                                hr = tiempo;
                                distanciaF = distanciaF + ruta;
                                tiempoF = tiempoF + tiempo;
                            }
                            else
                            {
                                Repositorio repo = new Repositorio();
                                Consulta consulta = new Consulta();
                                consulta.origen = "" + location.Latitude.ToString() + ", " + location.Longitude.ToString() + "";
                                consulta.destino = "Quito 170110, Ecuador";
                                var distanciaGoogle = repo.getDistancia(consulta);
                                double ruta = distanciaGoogle.rows[0].elements[0].distance.value;
                                double tiempo = distanciaGoogle.rows[0].elements[0].duration.value + Double.Parse(result);
                                km = ruta;
                                hr = tiempo;
                                distanciaF = ruta;
                                tiempoF = tiempo;
                            }
                        }
                    }

                }
                puntos = puntos + 1;
                E = 1;
                Lineas lin = new Lineas();
                lin.Producto = t5.Text;
                lin.Tiempo = hr.ToString();
                lin.Kilometros = km.ToString();
                lin.TiempoPunto = result;
                
                Lineas lineas = await repo.PostLineas(lin);
                Dialogs.ShowLoading("Buena Elección");
                await Task.Delay(2000);
                Dialogs.HideLoading();
                TiempoMedio = ((buses * puntos) + (tiempoF / 3600));
                Lineas.Add(lin);
                TIEMPO = TIEMPO + Double.Parse(result);
                if (TiempoMedio >= 8)
                {
                    Dialogs.ShowLoading("Has logrado el limite de tiempo diario");
                    await Task.Delay(2000);
                    Dialogs.HideLoading();
                    CurrentPage = gaseosas;
                    Children.Remove(MM);
                    Children.Remove(T);
                    Children.Remove(pan);
                    Children.Remove(zoo);
                    Children.Remove(cl);
                    Children.Remove(ma);
                    Children.Remove(maq);
                    Children.Remove(maqui);
                    Children.Remove(pulu);
                    Children.Remove(min);
                    Children.Remove(coto);
                    Children.Remove(bano);
                    Children.Remove(ota);
                    Children.Remove(papa);
                    Children.Remove(quilo);
                }
                else
                {
                    CurrentPage = ma;
                    Children.Remove(cl);
                }
            }
            
        }
        public async void Check5_Clicked(object sender, EventArgs e)
        {
            string result = await DisplayPromptAsync("Tiempo", "¿Cuanto tiempo desea pasar en el punto?", initialValue: "1", maxLength: 1, keyboard: Keyboard.Numeric);
            if (result == null)
            {
                CurrentPage = ma;
            }
            else
            {
                var location = await Geolocation.GetLastKnownLocationAsync();
                double km;
                double hr;
                if (E == 1)
                {
                    Repositorio repo = new Repositorio();
                    Consulta consulta = new Consulta();
                    consulta.origen = "Quito 170110, Ecuador";
                    consulta.destino = "El Placer, Quito 170130, Ecuador";
                    var distanciaGoogle = repo.getDistancia(consulta);
                    double ruta = distanciaGoogle.rows[0].elements[0].distance.value;
                    double tiempo = distanciaGoogle.rows[0].elements[0].duration.value + Double.Parse(result);
                    km = ruta;
                    hr = tiempo;
                    distanciaF = distanciaF + ruta;
                    tiempoF = tiempoF + tiempo;
                }
                else
                {
                    if (D == 1)
                    {
                        Repositorio repo = new Repositorio();
                        Consulta consulta = new Consulta();
                        consulta.origen = "Zoológico de Quito en Guayllabamba, Urb, Huertos Familiares, Quito 170209, Ecuador";
                        consulta.destino = "El Placer, Quito 170130, Ecuador";
                        var distanciaGoogle = repo.getDistancia(consulta);
                        double ruta = distanciaGoogle.rows[0].elements[0].distance.value;
                        double tiempo = distanciaGoogle.rows[0].elements[0].duration.value + Double.Parse(result);
                        km = ruta;
                        hr = tiempo;
                        distanciaF = distanciaF + ruta;
                        tiempoF = tiempoF + tiempo;
                    }
                    else
                    {
                        if (C == 1)
                        {
                            Repositorio repo = new Repositorio();
                            Consulta consulta = new Consulta();
                            consulta.origen = "Panecillo, Quito, Ecuador";
                            consulta.destino = "El Placer, Quito 170130, Ecuador";
                            var distanciaGoogle = repo.getDistancia(consulta);
                            double ruta = distanciaGoogle.rows[0].elements[0].distance.value;
                            double tiempo = distanciaGoogle.rows[0].elements[0].duration.value + Double.Parse(result);
                            km = ruta;
                            hr = tiempo;
                            distanciaF = distanciaF + ruta;
                            tiempoF = tiempoF + tiempo;
                        }
                        else
                        {
                            if (B == 1)
                            {
                                Repositorio repo = new Repositorio();
                                Consulta consulta = new Consulta();
                                consulta.origen = "Teleférico Quito, Quito, Ecuador";
                                consulta.destino = "El Placer, Quito 170130, Ecuador";
                                var distanciaGoogle = repo.getDistancia(consulta);
                                double ruta = distanciaGoogle.rows[0].elements[0].distance.value;
                                double tiempo = distanciaGoogle.rows[0].elements[0].duration.value + Double.Parse(result);
                                km = ruta;
                                hr = tiempo;
                                distanciaF = distanciaF + ruta;
                                tiempoF = tiempoF + tiempo;
                            }
                            else
                            {
                                if (A == 1)
                                {
                                    Repositorio repo = new Repositorio();
                                    Consulta consulta = new Consulta();
                                    consulta.origen = "Manuel Cordova Galarza Km. 13, 5 SN, Quito, Ecuador";
                                    consulta.destino = "El Placer, Quito 170130, Ecuador";
                                    var distanciaGoogle = repo.getDistancia(consulta);
                                    double ruta = distanciaGoogle.rows[0].elements[0].distance.value;
                                    double tiempo = distanciaGoogle.rows[0].elements[0].duration.value + Double.Parse(result);
                                    km = ruta;
                                    hr = tiempo;
                                    distanciaF = distanciaF + ruta;
                                    tiempoF = tiempoF + tiempo;
                                }
                                else
                                {
                                    Repositorio repo = new Repositorio();
                                    Consulta consulta = new Consulta();
                                    consulta.origen = "" + location.Latitude.ToString() + ", " + location.Longitude.ToString() + "";
                                    consulta.destino = "El Placer, Quito 170130, Ecuador";
                                    var distanciaGoogle = repo.getDistancia(consulta);
                                    double ruta = distanciaGoogle.rows[0].elements[0].distance.value;
                                    double tiempo = distanciaGoogle.rows[0].elements[0].duration.value + Double.Parse(result);
                                    km = ruta;
                                    hr = tiempo;
                                    distanciaF = ruta;
                                    tiempoF = tiempo;
                                }
                            }
                        }

                    }

                }
                puntos = puntos + 1;
                F = 1;
                Lineas lin = new Lineas();
                lin.Producto = t6.Text;
                lin.Tiempo = hr.ToString();
                lin.Kilometros = km.ToString();
                lin.TiempoPunto = result;

                Lineas lineas = await repo.PostLineas(lin);
                Dialogs.ShowLoading("Buena Elección");
                await Task.Delay(2000);
                Dialogs.HideLoading();
                TiempoMedio = ((buses * puntos) + (tiempoF / 3600));
                Lineas.Add(lin);
                TIEMPO = TIEMPO + Double.Parse(result);
                if (TiempoMedio >= 8)
                {
                    Dialogs.ShowLoading("Has logrado el limite de tiempo diario");
                    await Task.Delay(2000);
                    Dialogs.HideLoading();
                    CurrentPage = gaseosas;
                    Children.Remove(MM);
                    Children.Remove(T);
                    Children.Remove(pan);
                    Children.Remove(zoo);
                    Children.Remove(cl);
                    Children.Remove(ma);
                    Children.Remove(maq);
                    Children.Remove(maqui);
                    Children.Remove(pulu);
                    Children.Remove(min);
                    Children.Remove(coto);
                    Children.Remove(bano);
                    Children.Remove(ota);
                    Children.Remove(papa);
                    Children.Remove(quilo);
                }
                else
                {
                    CurrentPage = maq;
                    Children.Remove(ma);
                }
            }
        }
        public async void Check6_Clicked(object sender, EventArgs e)
        {
            string result = await DisplayPromptAsync("Tiempo", "¿Cuanto tiempo desea pasar en el punto?", initialValue: "1", maxLength: 1, keyboard: Keyboard.Numeric);
            if (result == null)
            {
                CurrentPage = maq;
            }
            else
            {
                var location = await Geolocation.GetLastKnownLocationAsync();
                double km;
                double hr;
                if (F == 1)
                {
                    Repositorio repo = new Repositorio();
                    Consulta consulta = new Consulta();
                    consulta.origen = "El Placer, Quito 170130, Ecuador";
                    consulta.destino = "Jorge Washington 611, Quito 170143, Ecuador";
                    var distanciaGoogle = repo.getDistancia(consulta);
                    double ruta = distanciaGoogle.rows[0].elements[0].distance.value;
                    double tiempo = distanciaGoogle.rows[0].elements[0].duration.value + Double.Parse(result);
                    km = ruta;
                    hr = tiempo;
                    distanciaF = distanciaF + ruta;
                    tiempoF = tiempoF + tiempo;
                }
                else
                {
                    if (E == 1)
                    {
                        Repositorio repo = new Repositorio();
                        Consulta consulta = new Consulta();
                        consulta.origen = "Quito 170110, Ecuador";
                        consulta.destino = "Jorge Washington 611, Quito 170143, Ecuador";
                        var distanciaGoogle = repo.getDistancia(consulta);
                        double ruta = distanciaGoogle.rows[0].elements[0].distance.value;
                        double tiempo = distanciaGoogle.rows[0].elements[0].duration.value + Double.Parse(result);
                        km = ruta;
                        hr = tiempo;
                        distanciaF = distanciaF + ruta;
                        tiempoF = tiempoF + tiempo;
                    }
                    else
                    {
                        if (D == 1)
                        {
                            Repositorio repo = new Repositorio();
                            Consulta consulta = new Consulta();
                            consulta.origen = "Zoológico de Quito en Guayllabamba, Urb, Huertos Familiares, Quito 170209, Ecuador";
                            consulta.destino = "Jorge Washington 611, Quito 170143, Ecuador";
                            var distanciaGoogle = repo.getDistancia(consulta);
                            double ruta = distanciaGoogle.rows[0].elements[0].distance.value;
                            double tiempo = distanciaGoogle.rows[0].elements[0].duration.value + Double.Parse(result);
                            km = ruta;
                            hr = tiempo;
                            distanciaF = distanciaF + ruta;
                            tiempoF = tiempoF + tiempo;
                        }
                        else
                        {
                            if (C == 1)
                            {
                                Repositorio repo = new Repositorio();
                                Consulta consulta = new Consulta();
                                consulta.origen = "Panecillo, Quito, Ecuador";
                                consulta.destino = "Jorge Washington 611, Quito 170143, Ecuador";
                                var distanciaGoogle = repo.getDistancia(consulta);
                                double ruta = distanciaGoogle.rows[0].elements[0].distance.value;
                                double tiempo = distanciaGoogle.rows[0].elements[0].duration.value + Double.Parse(result);
                                km = ruta;
                                hr = tiempo;
                                distanciaF = distanciaF + ruta;
                                tiempoF = tiempoF + tiempo;
                            }
                            else
                            {
                                if (B == 1)
                                {
                                    Repositorio repo = new Repositorio();
                                    Consulta consulta = new Consulta();
                                    consulta.origen = "Teleférico Quito, Quito, Ecuador";
                                    consulta.destino = "Jorge Washington 611, Quito 170143, Ecuador";
                                    var distanciaGoogle = repo.getDistancia(consulta);
                                    double ruta = distanciaGoogle.rows[0].elements[0].distance.value;
                                    double tiempo = distanciaGoogle.rows[0].elements[0].duration.value + Double.Parse(result);
                                    km = ruta;
                                    hr = tiempo;
                                    distanciaF = distanciaF + ruta;
                                    tiempoF = tiempoF + tiempo;
                                }
                                else
                                {
                                    if (A == 1)
                                    {
                                        Repositorio repo = new Repositorio();
                                        Consulta consulta = new Consulta();
                                        consulta.origen = "Manuel Cordova Galarza Km. 13, 5 SN, Quito, Ecuador";
                                        consulta.destino = "Jorge Washington 611, Quito 170143, Ecuador";
                                        var distanciaGoogle = repo.getDistancia(consulta);
                                        double ruta = distanciaGoogle.rows[0].elements[0].distance.value;
                                        double tiempo = distanciaGoogle.rows[0].elements[0].duration.value + Double.Parse(result);
                                        km = ruta;
                                        hr = tiempo;
                                        distanciaF = distanciaF + ruta;
                                        tiempoF = tiempoF + tiempo;
                                    }
                                    else
                                    {
                                        Repositorio repo = new Repositorio();
                                        Consulta consulta = new Consulta();
                                        consulta.origen = "" + location.Latitude.ToString() + ", " + location.Longitude.ToString() + "";
                                        consulta.destino = "Jorge Washington 611, Quito 170143, Ecuador";
                                        var distanciaGoogle = repo.getDistancia(consulta);
                                        double ruta = distanciaGoogle.rows[0].elements[0].distance.value;
                                        double tiempo = distanciaGoogle.rows[0].elements[0].duration.value + Double.Parse(result);
                                        km = ruta;
                                        hr = tiempo;
                                        distanciaF = ruta;
                                        tiempoF = tiempo;
                                    }
                                }
                            }

                        }

                    }
                }
                puntos = puntos + 1;
                G = 1;
                Lineas lin = new Lineas();
                lin.Producto = t7.Text;
                lin.Tiempo = hr.ToString();
                lin.Kilometros = km.ToString();
                lin.TiempoPunto = result;

                Lineas lineas = await repo.PostLineas(lin);
                Dialogs.ShowLoading("Buena Elección");
                await Task.Delay(2000);
                Dialogs.HideLoading();
                TiempoMedio = ((buses * puntos) + (tiempoF / 3600));
                Lineas.Add(lin);
                TIEMPO = TIEMPO + Double.Parse(result);
                if (TiempoMedio >= 8)
                {
                    Dialogs.ShowLoading("Has logrado el limite de tiempo diario");
                    await Task.Delay(2000);
                    Dialogs.HideLoading();
                    CurrentPage = gaseosas;
                    Children.Remove(MM);
                    Children.Remove(T);
                    Children.Remove(pan);
                    Children.Remove(zoo);
                    Children.Remove(cl);
                    Children.Remove(ma);
                    Children.Remove(maq);
                    Children.Remove(maqui);
                    Children.Remove(pulu);
                    Children.Remove(min);
                    Children.Remove(coto);
                    Children.Remove(bano);
                    Children.Remove(ota);
                    Children.Remove(papa);
                    Children.Remove(quilo);
                }
                else
                {
                    CurrentPage = min;
                    Children.Remove(maq);
                }
            }
            
        }
        public async void Check7_Clicked(object sender, EventArgs e)
        {
            string result = await DisplayPromptAsync("Tiempo", "¿Cuanto tiempo desea pasar en el punto?", initialValue: "1", maxLength: 1, keyboard: Keyboard.Numeric);
            if (result == null)
            {
                CurrentPage = min;
            }
            else
            {
                var location = await Geolocation.GetLastKnownLocationAsync();
                double km;
                double hr;
                if (G == 1)
                {
                    Repositorio repo = new Repositorio();
                    Consulta consulta = new Consulta();
                    consulta.origen = "Jorge Washington 611, Quito 170143, Ecuador";
                    consulta.destino = "Vía a Mindo, Ecuador";
                    var distanciaGoogle = repo.getDistancia(consulta);
                    double ruta = distanciaGoogle.rows[0].elements[0].distance.value;
                    double tiempo = distanciaGoogle.rows[0].elements[0].duration.value + Double.Parse(result);
                    km = ruta;
                    hr = tiempo;
                    distanciaF = distanciaF + ruta;
                    tiempoF = tiempoF + tiempo;
                }
                else
                {
                    if (F == 1)
                    {
                        Repositorio repo = new Repositorio();
                        Consulta consulta = new Consulta();
                        consulta.origen = "El Placer, Quito 170130, Ecuador";
                        consulta.destino = "Vía a Mindo, Ecuador";
                        var distanciaGoogle = repo.getDistancia(consulta);
                        double ruta = distanciaGoogle.rows[0].elements[0].distance.value;
                        double tiempo = distanciaGoogle.rows[0].elements[0].duration.value + Double.Parse(result);
                        km = ruta;
                        hr = tiempo;
                        distanciaF = distanciaF + ruta;
                        tiempoF = tiempoF + tiempo;
                    }
                    else
                    {
                        if (E == 1)
                        {
                            Repositorio repo = new Repositorio();
                            Consulta consulta = new Consulta();
                            consulta.origen = "Quito 170110, Ecuador";
                            consulta.destino = "Vía a Mindo, Ecuador";
                            var distanciaGoogle = repo.getDistancia(consulta);
                            double ruta = distanciaGoogle.rows[0].elements[0].distance.value;
                            double tiempo = distanciaGoogle.rows[0].elements[0].duration.value + Double.Parse(result);
                            km = ruta;
                            hr = tiempo;
                            distanciaF = distanciaF + ruta;
                            tiempoF = tiempoF + tiempo;
                        }
                        else
                        {
                            if (D == 1)
                            {
                                Repositorio repo = new Repositorio();
                                Consulta consulta = new Consulta();
                                consulta.origen = "Zoológico de Quito en Guayllabamba, Urb, Huertos Familiares, Quito 170209, Ecuador";
                                consulta.destino = "Vía a Mindo, Ecuador";
                                var distanciaGoogle = repo.getDistancia(consulta);
                                double ruta = distanciaGoogle.rows[0].elements[0].distance.value;
                                double tiempo = distanciaGoogle.rows[0].elements[0].duration.value + Double.Parse(result);
                                km = ruta;
                                hr = tiempo;
                                distanciaF = distanciaF + ruta;
                                tiempoF = tiempoF + tiempo;
                            }
                            else
                            {
                                if (C == 1)
                                {
                                    Repositorio repo = new Repositorio();
                                    Consulta consulta = new Consulta();
                                    consulta.origen = "Panecillo, Quito, Ecuador";
                                    consulta.destino = "Vía a Mindo, Ecuador";
                                    var distanciaGoogle = repo.getDistancia(consulta);
                                    double ruta = distanciaGoogle.rows[0].elements[0].distance.value;
                                    double tiempo = distanciaGoogle.rows[0].elements[0].duration.value + Double.Parse(result);
                                    km = ruta;
                                    hr = tiempo;
                                    distanciaF = distanciaF + ruta;
                                    tiempoF = tiempoF + tiempo;
                                }
                                else
                                {
                                    if (B == 1)
                                    {
                                        Repositorio repo = new Repositorio();
                                        Consulta consulta = new Consulta();
                                        consulta.origen = "Teleférico Quito, Quito, Ecuador";
                                        consulta.destino = "Vía a Mindo, Ecuador";
                                        var distanciaGoogle = repo.getDistancia(consulta);
                                        double ruta = distanciaGoogle.rows[0].elements[0].distance.value;
                                        double tiempo = distanciaGoogle.rows[0].elements[0].duration.value + Double.Parse(result);
                                        km = ruta;
                                        hr = tiempo;
                                        distanciaF = distanciaF + ruta;
                                        tiempoF = tiempoF + tiempo;
                                    }
                                    else
                                    {
                                        if (A == 1)
                                        {
                                            Repositorio repo = new Repositorio();
                                            Consulta consulta = new Consulta();
                                            consulta.origen = "Manuel Cordova Galarza Km. 13, 5 SN, Quito, Ecuador";
                                            consulta.destino = "Vía a Mindo, Ecuador";
                                            var distanciaGoogle = repo.getDistancia(consulta);
                                            double ruta = distanciaGoogle.rows[0].elements[0].distance.value;
                                            double tiempo = distanciaGoogle.rows[0].elements[0].duration.value + Double.Parse(result);
                                            km = ruta;
                                            hr = tiempo;
                                            distanciaF = distanciaF + ruta;
                                            tiempoF = tiempoF + tiempo;
                                        }
                                        else
                                        {
                                            Repositorio repo = new Repositorio();
                                            Consulta consulta = new Consulta();
                                            consulta.origen = "" + location.Latitude.ToString() + ", " + location.Longitude.ToString() + "";
                                            consulta.destino = "Vía a Mindo, Ecuador";
                                            var distanciaGoogle = repo.getDistancia(consulta);
                                            double ruta = distanciaGoogle.rows[0].elements[0].distance.value;
                                            double tiempo = distanciaGoogle.rows[0].elements[0].duration.value + Double.Parse(result);
                                            km = ruta;
                                            hr = tiempo;
                                            distanciaF = ruta;
                                            tiempoF = tiempo;
                                        }
                                    }
                                }

                            }

                        }
                    }

                }
                puntos = puntos + 1;
                H = 1;
                Lineas lin = new Lineas();
                lin.Producto = t8.Text;
                lin.Tiempo = hr.ToString();
                lin.Kilometros = km.ToString();
                lin.TiempoPunto = result;

                Lineas lineas = await repo.PostLineas(lin);
                Dialogs.ShowLoading("Buena Elección");
                await Task.Delay(2000);
                Dialogs.HideLoading();
                TiempoMedio = ((buses * puntos) + (tiempoF / 3600));
                Lineas.Add(lin);
                TIEMPO = TIEMPO + Double.Parse(result);
                if (TiempoMedio >= 8)
                {
                    Dialogs.ShowLoading("Has logrado el limite de tiempo diario");
                    await Task.Delay(2000);
                    Dialogs.HideLoading();
                    CurrentPage = gaseosas;
                    Children.Remove(MM);
                    Children.Remove(T);
                    Children.Remove(pan);
                    Children.Remove(zoo);
                    Children.Remove(cl);
                    Children.Remove(ma);
                    Children.Remove(maq);
                    Children.Remove(maqui);
                    Children.Remove(pulu);
                    Children.Remove(min);
                    Children.Remove(coto);
                    Children.Remove(bano);
                    Children.Remove(ota);
                    Children.Remove(papa);
                    Children.Remove(quilo);
                }
                else
                {
                    CurrentPage = maqui;
                    Children.Remove(min);
                }

            }
        
        }
        public async void Check8_Clicked(object sender, EventArgs e)
        {
            string result = await DisplayPromptAsync("Tiempo", "¿Cuanto tiempo desea pasar en el punto?", initialValue: "1", maxLength: 1, keyboard: Keyboard.Numeric);
            if (result == null)
            {
                CurrentPage = maqui;
            }
            else
            {
                var location = await Geolocation.GetLastKnownLocationAsync();
                double km;
                double hr;
                if (H == 1)
                {
                    Repositorio repo = new Repositorio();
                    Consulta consulta = new Consulta();
                    consulta.origen = "Vía a Mindo, Ecuador";
                    consulta.destino = "Maquipucuna Reserve, Vía a Nanegal, Quito 170168, Ecuador";
                    var distanciaGoogle = repo.getDistancia(consulta);
                    double ruta = distanciaGoogle.rows[0].elements[0].distance.value;
                    double tiempo = distanciaGoogle.rows[0].elements[0].duration.value + Double.Parse(result);
                    km = ruta;
                    hr = tiempo;
                    distanciaF = distanciaF + ruta;
                    tiempoF = tiempoF + tiempo;
                }
                else
                {
                    if (G == 1)
                    {
                        Repositorio repo = new Repositorio();
                        Consulta consulta = new Consulta();
                        consulta.origen = "Jorge Washington 611, Quito 170143, Ecuador";
                        consulta.destino = "Maquipucuna Reserve, Vía a Nanegal, Quito 170168, Ecuador";
                        var distanciaGoogle = repo.getDistancia(consulta);
                        double ruta = distanciaGoogle.rows[0].elements[0].distance.value;
                        double tiempo = distanciaGoogle.rows[0].elements[0].duration.value + Double.Parse(result);
                        km = ruta;
                        hr = tiempo;
                        distanciaF = distanciaF + ruta;
                        tiempoF = tiempoF + tiempo;
                    }
                    else
                    {
                        if (F == 1)
                        {
                            Repositorio repo = new Repositorio();
                            Consulta consulta = new Consulta();
                            consulta.origen = "El Placer, Quito 170130, Ecuador";
                            consulta.destino = "Maquipucuna Reserve, Vía a Nanegal, Quito 170168, Ecuador";
                            var distanciaGoogle = repo.getDistancia(consulta);
                            double ruta = distanciaGoogle.rows[0].elements[0].distance.value;
                            double tiempo = distanciaGoogle.rows[0].elements[0].duration.value + Double.Parse(result);
                            km = ruta;
                            hr = tiempo;
                            distanciaF = distanciaF + ruta;
                            tiempoF = tiempoF + tiempo;
                        }
                        else
                        {
                            if (E == 1)
                            {
                                Repositorio repo = new Repositorio();
                                Consulta consulta = new Consulta();
                                consulta.origen = "Quito 170110, Ecuador";
                                consulta.destino = "Maquipucuna Reserve, Vía a Nanegal, Quito 170168, Ecuador";
                                var distanciaGoogle = repo.getDistancia(consulta);
                                double ruta = distanciaGoogle.rows[0].elements[0].distance.value;
                                double tiempo = distanciaGoogle.rows[0].elements[0].duration.value + Double.Parse(result);
                                km = ruta;
                                hr = tiempo;
                                distanciaF = distanciaF + ruta;
                                tiempoF = tiempoF + tiempo;
                            }
                            else
                            {
                                if (D == 1)
                                {
                                    Repositorio repo = new Repositorio();
                                    Consulta consulta = new Consulta();
                                    consulta.origen = "Zoológico de Quito en Guayllabamba, Urb, Huertos Familiares, Quito 170209, Ecuador";
                                    consulta.destino = "Maquipucuna Reserve, Vía a Nanegal, Quito 170168, Ecuador";
                                    var distanciaGoogle = repo.getDistancia(consulta);
                                    double ruta = distanciaGoogle.rows[0].elements[0].distance.value;
                                    double tiempo = distanciaGoogle.rows[0].elements[0].duration.value + Double.Parse(result);
                                    km = ruta;
                                    hr = tiempo;
                                    distanciaF = distanciaF + ruta;
                                    tiempoF = tiempoF + tiempo;
                                }
                                else
                                {
                                    if (C == 1)
                                    {
                                        Repositorio repo = new Repositorio();
                                        Consulta consulta = new Consulta();
                                        consulta.origen = "Panecillo, Quito, Ecuador";
                                        consulta.destino = "Maquipucuna Reserve, Vía a Nanegal, Quito 170168, Ecuador";
                                        var distanciaGoogle = repo.getDistancia(consulta);
                                        double ruta = distanciaGoogle.rows[0].elements[0].distance.value;
                                        double tiempo = distanciaGoogle.rows[0].elements[0].duration.value + Double.Parse(result);
                                        km = ruta;
                                        hr = tiempo;
                                        distanciaF = distanciaF + ruta;
                                        tiempoF = tiempoF + tiempo;
                                    }
                                    else
                                    {
                                        if (B == 1)
                                        {
                                            Repositorio repo = new Repositorio();
                                            Consulta consulta = new Consulta();
                                            consulta.origen = "Teleférico Quito, Quito, Ecuador";
                                            consulta.destino = "Maquipucuna Reserve, Vía a Nanegal, Quito 170168, Ecuador";
                                            var distanciaGoogle = repo.getDistancia(consulta);
                                            double ruta = distanciaGoogle.rows[0].elements[0].distance.value;
                                            double tiempo = distanciaGoogle.rows[0].elements[0].duration.value + Double.Parse(result);
                                            km = ruta;
                                            hr = tiempo;
                                            distanciaF = distanciaF + ruta;
                                            tiempoF = tiempoF + tiempo;
                                        }
                                        else
                                        {
                                            if (A == 1)
                                            {
                                                Repositorio repo = new Repositorio();
                                                Consulta consulta = new Consulta();
                                                consulta.origen = "Manuel Cordova Galarza Km. 13, 5 SN, Quito, Ecuador";
                                                consulta.destino = "Maquipucuna Reserve, Vía a Nanegal, Quito 170168, Ecuador";
                                                var distanciaGoogle = repo.getDistancia(consulta);
                                                double ruta = distanciaGoogle.rows[0].elements[0].distance.value;
                                                double tiempo = distanciaGoogle.rows[0].elements[0].duration.value + Double.Parse(result);
                                                km = ruta;
                                                hr = tiempo;
                                                distanciaF = distanciaF + ruta;
                                                tiempoF = tiempoF + tiempo;
                                            }
                                            else
                                            {
                                                Repositorio repo = new Repositorio();
                                                Consulta consulta = new Consulta();
                                                consulta.origen = "" + location.Latitude.ToString() + ", " + location.Longitude.ToString() + "";
                                                consulta.destino = "Maquipucuna Reserve, Vía a Nanegal, Quito 170168, Ecuador";
                                                var distanciaGoogle = repo.getDistancia(consulta);
                                                double ruta = distanciaGoogle.rows[0].elements[0].distance.value;
                                                double tiempo = distanciaGoogle.rows[0].elements[0].duration.value + Double.Parse(result);
                                                km = ruta;
                                                hr = tiempo;
                                                distanciaF = ruta;
                                                tiempoF = tiempo;
                                            }
                                        }
                                    }

                                }

                            }
                        }

                    }
                }
                puntos = puntos + 1;
                I = 1;
                Lineas lin = new Lineas();
                lin.Producto = t9.Text;
                lin.Tiempo = hr.ToString();
                lin.Kilometros = km.ToString();
                lin.TiempoPunto = result;

                Lineas lineas = await repo.PostLineas(lin);
                Dialogs.ShowLoading("Buena Elección");
                await Task.Delay(2000);
                Dialogs.HideLoading();
                TiempoMedio = ((buses * puntos) + (tiempoF / 3600));
                Lineas.Add(lin);
                TIEMPO = TIEMPO + Double.Parse(result);
                if (TiempoMedio >= 8)
                {
                    Dialogs.ShowLoading("Has logrado el limite de tiempo diario");
                    await Task.Delay(2000);
                    Dialogs.HideLoading();
                    CurrentPage = gaseosas;
                    Children.Remove(MM);
                    Children.Remove(T);
                    Children.Remove(pan);
                    Children.Remove(zoo);
                    Children.Remove(cl);
                    Children.Remove(ma);
                    Children.Remove(maq);
                    Children.Remove(maqui);
                    Children.Remove(pulu);
                    Children.Remove(min);
                    Children.Remove(coto);
                    Children.Remove(bano);
                    Children.Remove(ota);
                    Children.Remove(papa);
                    Children.Remove(quilo);
                }
                else
                {
                    CurrentPage = quilo;
                    Children.Remove(maqui);
                }
            }
            
        }
        public async void Check9_Clicked(object sender, EventArgs e)
        {
            string result = await DisplayPromptAsync("Tiempo", "¿Cuanto tiempo desea pasar en el punto?", initialValue: "1", maxLength: 1, keyboard: Keyboard.Numeric);
            if (result == null)
            {
                CurrentPage = quilo;
            }
            else
            {
                var location = await Geolocation.GetLastKnownLocationAsync();
                double km;
                double hr;
                if (I == 1)
                {
                    Repositorio repo = new Repositorio();
                    Consulta consulta = new Consulta();
                    consulta.origen = "Maquipucuna Reserve, Vía a Nanegal, Quito 170168, Ecuador";
                    consulta.destino = "Quilotoa, Provincia de Cotopaxi,Ecuador, Ecuador";
                    var distanciaGoogle = repo.getDistancia(consulta);
                    double ruta = distanciaGoogle.rows[0].elements[0].distance.value;
                    double tiempo = distanciaGoogle.rows[0].elements[0].duration.value + Double.Parse(result);
                    km = ruta;
                    hr = tiempo;
                    distanciaF = distanciaF + ruta;
                    tiempoF = tiempoF + tiempo;
                }
                else
                {
                    if (H == 1)
                    {
                        Repositorio repo = new Repositorio();
                        Consulta consulta = new Consulta();
                        consulta.origen = "Vía a Mindo, Ecuador";
                        consulta.destino = "Quilotoa, Provincia de Cotopaxi,Ecuador, Ecuador";
                        var distanciaGoogle = repo.getDistancia(consulta);
                        double ruta = distanciaGoogle.rows[0].elements[0].distance.value;
                        double tiempo = distanciaGoogle.rows[0].elements[0].duration.value + Double.Parse(result);
                        km = ruta;
                        hr = tiempo;
                        distanciaF = distanciaF + ruta;
                        tiempoF = tiempoF + tiempo;
                    }
                    else
                    {
                        if (G == 1)
                        {
                            Repositorio repo = new Repositorio();
                            Consulta consulta = new Consulta();
                            consulta.origen = "Jorge Washington 611, Quito 170143, Ecuador";
                            consulta.destino = "Quilotoa, Provincia de Cotopaxi,Ecuador, Ecuador";
                            var distanciaGoogle = repo.getDistancia(consulta);
                            double ruta = distanciaGoogle.rows[0].elements[0].distance.value;
                            double tiempo = distanciaGoogle.rows[0].elements[0].duration.value + Double.Parse(result);
                            km = ruta;
                            hr = tiempo;
                            distanciaF = distanciaF + ruta;
                            tiempoF = tiempoF + tiempo;
                        }
                        else
                        {
                            if (F == 1)
                            {
                                Repositorio repo = new Repositorio();
                                Consulta consulta = new Consulta();
                                consulta.origen = "El Placer, Quito 170130, Ecuador";
                                consulta.destino = "Quilotoa, Provincia de Cotopaxi,Ecuador, Ecuador";
                                var distanciaGoogle = repo.getDistancia(consulta);
                                double ruta = distanciaGoogle.rows[0].elements[0].distance.value;
                                double tiempo = distanciaGoogle.rows[0].elements[0].duration.value + Double.Parse(result);
                                km = ruta;
                                hr = tiempo;
                                distanciaF = distanciaF + ruta;
                                tiempoF = tiempoF + tiempo;
                            }
                            else
                            {
                                if (E == 1)
                                {
                                    Repositorio repo = new Repositorio();
                                    Consulta consulta = new Consulta();
                                    consulta.origen = "Quito 170110, Ecuador";
                                    consulta.destino = "Quilotoa, Provincia de Cotopaxi,Ecuador, Ecuador";
                                    var distanciaGoogle = repo.getDistancia(consulta);
                                    double ruta = distanciaGoogle.rows[0].elements[0].distance.value;
                                    double tiempo = distanciaGoogle.rows[0].elements[0].duration.value + Double.Parse(result);
                                    km = ruta;
                                    hr = tiempo;
                                    distanciaF = distanciaF + ruta;
                                    tiempoF = tiempoF + tiempo;
                                }
                                else
                                {
                                    if (D == 1)
                                    {
                                        Repositorio repo = new Repositorio();
                                        Consulta consulta = new Consulta();
                                        consulta.origen = "Zoológico de Quito en Guayllabamba, Urb, Huertos Familiares, Quito 170209, Ecuador";
                                        consulta.destino = "Quilotoa, Provincia de Cotopaxi,Ecuador, Ecuador";
                                        var distanciaGoogle = repo.getDistancia(consulta);
                                        double ruta = distanciaGoogle.rows[0].elements[0].distance.value;
                                        double tiempo = distanciaGoogle.rows[0].elements[0].duration.value + Double.Parse(result);
                                        km = ruta;
                                        hr = tiempo;
                                        distanciaF = distanciaF + ruta;
                                        tiempoF = tiempoF + tiempo;
                                    }
                                    else
                                    {
                                        if (C == 1)
                                        {
                                            Repositorio repo = new Repositorio();
                                            Consulta consulta = new Consulta();
                                            consulta.origen = "Panecillo, Quito, Ecuador";
                                            consulta.destino = "Quilotoa, Provincia de Cotopaxi,Ecuador, Ecuador";
                                            var distanciaGoogle = repo.getDistancia(consulta);
                                            double ruta = distanciaGoogle.rows[0].elements[0].distance.value;
                                            double tiempo = distanciaGoogle.rows[0].elements[0].duration.value + Double.Parse(result);
                                            km = ruta;
                                            hr = tiempo;
                                            distanciaF = distanciaF + ruta;
                                            tiempoF = tiempoF + tiempo;
                                        }
                                        else
                                        {
                                            if (B == 1)
                                            {
                                                Repositorio repo = new Repositorio();
                                                Consulta consulta = new Consulta();
                                                consulta.origen = "Teleférico Quito, Quito, Ecuador";
                                                consulta.destino = "Quilotoa, Provincia de Cotopaxi,Ecuador, Ecuador";
                                                var distanciaGoogle = repo.getDistancia(consulta);
                                                double ruta = distanciaGoogle.rows[0].elements[0].distance.value;
                                                double tiempo = distanciaGoogle.rows[0].elements[0].duration.value + Double.Parse(result);
                                                km = ruta;
                                                hr = tiempo;
                                                distanciaF = distanciaF + ruta;
                                                tiempoF = tiempoF + tiempo;
                                            }
                                            else
                                            {
                                                if (A == 1)
                                                {
                                                    Repositorio repo = new Repositorio();
                                                    Consulta consulta = new Consulta();
                                                    consulta.origen = "Manuel Cordova Galarza Km. 13, 5 SN, Quito, Ecuador";
                                                    consulta.destino = "Quilotoa, Provincia de Cotopaxi,Ecuador, Ecuador";
                                                    var distanciaGoogle = repo.getDistancia(consulta);
                                                    double ruta = distanciaGoogle.rows[0].elements[0].distance.value;
                                                    double tiempo = distanciaGoogle.rows[0].elements[0].duration.value + Double.Parse(result);
                                                    km = ruta;
                                                    hr = tiempo;
                                                    distanciaF = distanciaF + ruta;
                                                    tiempoF = tiempoF + tiempo;
                                                }
                                                else
                                                {
                                                    Repositorio repo = new Repositorio();
                                                    Consulta consulta = new Consulta();
                                                    consulta.origen = "" + location.Latitude.ToString() + ", " + location.Longitude.ToString() + "";
                                                    consulta.destino = "Quilotoa, Provincia de Cotopaxi,Ecuador, Ecuador";
                                                    var distanciaGoogle = repo.getDistancia(consulta);
                                                    double ruta = distanciaGoogle.rows[0].elements[0].distance.value;
                                                    double tiempo = distanciaGoogle.rows[0].elements[0].duration.value + Double.Parse(result);
                                                    km = ruta;
                                                    hr = tiempo;
                                                    distanciaF = ruta;
                                                    tiempoF = tiempo;
                                                }
                                            }
                                        }

                                    }

                                }
                            }

                        }
                    }
                }
                puntos = puntos + 1;
                J = 1;
                Lineas lin = new Lineas();
                lin.Producto = t10.Text;
                lin.Tiempo = hr.ToString();
                lin.Kilometros = km.ToString();
                lin.TiempoPunto = result;

                Lineas lineas = await repo.PostLineas(lin);
                Dialogs.ShowLoading("Buena Elección");
                await Task.Delay(2000);
                Dialogs.HideLoading();
                TiempoMedio = ((buses * puntos) + (tiempoF / 3600));
                Lineas.Add(lin);
                TIEMPO = TIEMPO + Double.Parse(result);
                if (TiempoMedio >= 8)
                {
                    Dialogs.ShowLoading("Has logrado el limite de tiempo diario");
                    await Task.Delay(2000);
                    Dialogs.HideLoading();
                    CurrentPage = gaseosas;
                    Children.Remove(MM);
                    Children.Remove(T);
                    Children.Remove(pan);
                    Children.Remove(zoo);
                    Children.Remove(cl);
                    Children.Remove(ma);
                    Children.Remove(maq);
                    Children.Remove(maqui);
                    Children.Remove(pulu);
                    Children.Remove(min);
                    Children.Remove(coto);
                    Children.Remove(bano);
                    Children.Remove(ota);
                    Children.Remove(papa);
                    Children.Remove(quilo);
                }
                else
                {
                    CurrentPage = coto;
                    Children.Remove(quilo);
                }
            }
        }
        public async void Check10_Clicked(object sender, EventArgs e)
        {
            string result = await DisplayPromptAsync("Tiempo", "¿Cuanto tiempo desea pasar en el punto?", initialValue: "1", maxLength: 1, keyboard: Keyboard.Numeric);
            if (result == null)
            {
                CurrentPage = coto;
            }
            else
            {
                var location = await Geolocation.GetLastKnownLocationAsync();
                double km;
                double hr;
                if (J == 1)
                {
                    Repositorio repo = new Repositorio();
                    Consulta consulta = new Consulta();
                    consulta.origen = "Quilotoa, Provincia de Cotopaxi,Ecuador, Ecuador";
                    consulta.destino = "Hacienda Ilitío de Plaza, Ecuador";
                    var distanciaGoogle = repo.getDistancia(consulta);
                    double ruta = distanciaGoogle.rows[0].elements[0].distance.value;
                    double tiempo = distanciaGoogle.rows[0].elements[0].duration.value + Double.Parse(result);
                    km = ruta;
                    hr = tiempo;
                    distanciaF = distanciaF + ruta;
                    tiempoF = tiempoF + tiempo;
                }
                else
                {
                    if (I == 1)
                    {
                        Repositorio repo = new Repositorio();
                        Consulta consulta = new Consulta();
                        consulta.origen = "Maquipucuna Reserve, Vía a Nanegal, Quito 170168, Ecuador";
                        consulta.destino = "Hacienda Ilitío de Plaza, Ecuador";
                        var distanciaGoogle = repo.getDistancia(consulta);
                        double ruta = distanciaGoogle.rows[0].elements[0].distance.value;
                        double tiempo = distanciaGoogle.rows[0].elements[0].duration.value + Double.Parse(result);
                        km = ruta;
                        hr = tiempo;
                        distanciaF = distanciaF + ruta;
                        tiempoF = tiempoF + tiempo;
                    }
                    else
                    {
                        if (H == 1)
                        {
                            Repositorio repo = new Repositorio();
                            Consulta consulta = new Consulta();
                            consulta.origen = "Vía a Mindo, Ecuador";
                            consulta.destino = "Hacienda Ilitío de Plaza, Ecuador";
                            var distanciaGoogle = repo.getDistancia(consulta);
                            double ruta = distanciaGoogle.rows[0].elements[0].distance.value;
                            double tiempo = distanciaGoogle.rows[0].elements[0].duration.value + Double.Parse(result);
                            km = ruta;
                            hr = tiempo;
                            distanciaF = distanciaF + ruta;
                            tiempoF = tiempoF + tiempo;
                        }
                        else
                        {
                            if (G == 1)
                            {
                                Repositorio repo = new Repositorio();
                                Consulta consulta = new Consulta();
                                consulta.origen = "Jorge Washington 611, Quito 170143, Ecuador";
                                consulta.destino = "Hacienda Ilitío de Plaza, Ecuador";
                                var distanciaGoogle = repo.getDistancia(consulta);
                                double ruta = distanciaGoogle.rows[0].elements[0].distance.value;
                                double tiempo = distanciaGoogle.rows[0].elements[0].duration.value + Double.Parse(result);
                                km = ruta;
                                hr = tiempo;
                                distanciaF = distanciaF + ruta;
                                tiempoF = tiempoF + tiempo;
                            }
                            else
                            {
                                if (F == 1)
                                {
                                    Repositorio repo = new Repositorio();
                                    Consulta consulta = new Consulta();
                                    consulta.origen = "El Placer, Quito 170130, Ecuador";
                                    consulta.destino = "Hacienda Ilitío de Plaza, Ecuador";
                                    var distanciaGoogle = repo.getDistancia(consulta);
                                    double ruta = distanciaGoogle.rows[0].elements[0].distance.value;
                                    double tiempo = distanciaGoogle.rows[0].elements[0].duration.value + Double.Parse(result);
                                    km = ruta;
                                    hr = tiempo;
                                    distanciaF = distanciaF + ruta;
                                    tiempoF = tiempoF + tiempo;
                                }
                                else
                                {
                                    if (E == 1)
                                    {
                                        Repositorio repo = new Repositorio();
                                        Consulta consulta = new Consulta();
                                        consulta.origen = "Quito 170110, Ecuador";
                                        consulta.destino = "Hacienda Ilitío de Plaza, Ecuador";
                                        var distanciaGoogle = repo.getDistancia(consulta);
                                        double ruta = distanciaGoogle.rows[0].elements[0].distance.value;
                                        double tiempo = distanciaGoogle.rows[0].elements[0].duration.value + Double.Parse(result);
                                        km = ruta;
                                        hr = tiempo;
                                        distanciaF = distanciaF + ruta;
                                        tiempoF = tiempoF + tiempo;
                                    }
                                    else
                                    {
                                        if (D == 1)
                                        {
                                            Repositorio repo = new Repositorio();
                                            Consulta consulta = new Consulta();
                                            consulta.origen = "Zoológico de Quito en Guayllabamba, Urb, Huertos Familiares, Quito 170209, Ecuador";
                                            consulta.destino = "Hacienda Ilitío de Plaza, Ecuador";
                                            var distanciaGoogle = repo.getDistancia(consulta);
                                            double ruta = distanciaGoogle.rows[0].elements[0].distance.value;
                                            double tiempo = distanciaGoogle.rows[0].elements[0].duration.value + Double.Parse(result);
                                            km = ruta;
                                            hr = tiempo;
                                            distanciaF = distanciaF + ruta;
                                            tiempoF = tiempoF + tiempo;
                                        }
                                        else
                                        {
                                            if (C == 1)
                                            {
                                                Repositorio repo = new Repositorio();
                                                Consulta consulta = new Consulta();
                                                consulta.origen = "Panecillo, Quito, Ecuador";
                                                consulta.destino = "Hacienda Ilitío de Plaza, Ecuador";
                                                var distanciaGoogle = repo.getDistancia(consulta);
                                                double ruta = distanciaGoogle.rows[0].elements[0].distance.value;
                                                double tiempo = distanciaGoogle.rows[0].elements[0].duration.value + Double.Parse(result);
                                                km = ruta;
                                                hr = tiempo;
                                                distanciaF = distanciaF + ruta;
                                                tiempoF = tiempoF + tiempo;
                                            }
                                            else
                                            {
                                                if (B == 1)
                                                {
                                                    Repositorio repo = new Repositorio();
                                                    Consulta consulta = new Consulta();
                                                    consulta.origen = "Teleférico Quito, Quito, Ecuador";
                                                    consulta.destino = "Hacienda Ilitío de Plaza, Ecuador";
                                                    var distanciaGoogle = repo.getDistancia(consulta);
                                                    double ruta = distanciaGoogle.rows[0].elements[0].distance.value;
                                                    double tiempo = distanciaGoogle.rows[0].elements[0].duration.value + Double.Parse(result);
                                                    km = ruta;
                                                    hr = tiempo;
                                                    distanciaF = distanciaF + ruta;
                                                    tiempoF = tiempoF + tiempo;
                                                }
                                                else
                                                {
                                                    if (A == 1)
                                                    {
                                                        Repositorio repo = new Repositorio();
                                                        Consulta consulta = new Consulta();
                                                        consulta.origen = "Manuel Cordova Galarza Km. 13, 5 SN, Quito, Ecuador";
                                                        consulta.destino = "Hacienda Ilitío de Plaza, Ecuador";
                                                        var distanciaGoogle = repo.getDistancia(consulta);
                                                        double ruta = distanciaGoogle.rows[0].elements[0].distance.value;
                                                        double tiempo = distanciaGoogle.rows[0].elements[0].duration.value + Double.Parse(result);
                                                        km = ruta;
                                                        hr = tiempo;
                                                        distanciaF = distanciaF + ruta;
                                                        tiempoF = tiempoF + tiempo;
                                                    }
                                                    else
                                                    {
                                                        Repositorio repo = new Repositorio();
                                                        Consulta consulta = new Consulta();
                                                        consulta.origen = "" + location.Latitude.ToString() + ", " + location.Longitude.ToString() + "";
                                                        consulta.destino = "Hacienda Ilitío de Plaza, Ecuador";
                                                        var distanciaGoogle = repo.getDistancia(consulta);
                                                        double ruta = distanciaGoogle.rows[0].elements[0].distance.value;
                                                        double tiempo = distanciaGoogle.rows[0].elements[0].duration.value + Double.Parse(result);
                                                        km = ruta;
                                                        hr = tiempo;
                                                        distanciaF = ruta;
                                                        tiempoF = tiempo;
                                                    }
                                                }
                                            }

                                        }

                                    }
                                }

                            }
                        }
                    }
                }
                puntos = puntos + 1;
                K = 1;
                Lineas lin = new Lineas();
                lin.Producto = t11.Text;
                lin.Tiempo = hr.ToString();
                lin.Kilometros = km.ToString();
                lin.TiempoPunto = result;

                Lineas lineas = await repo.PostLineas(lin);
                Dialogs.ShowLoading("Buena Elección");
                await Task.Delay(2000);
                Dialogs.HideLoading();
                TiempoMedio = ((buses * puntos) + (tiempoF / 3600));
                Lineas.Add(lin);
                TIEMPO = TIEMPO + Double.Parse(result);
                if (TiempoMedio >= 8)
                {
                    Dialogs.ShowLoading("Has logrado el limite de tiempo diario");
                    await Task.Delay(2000);
                    Dialogs.HideLoading();
                    CurrentPage = gaseosas;
                    Children.Remove(MM);
                    Children.Remove(T);
                    Children.Remove(pan);
                    Children.Remove(zoo);
                    Children.Remove(cl);
                    Children.Remove(ma);
                    Children.Remove(maq);
                    Children.Remove(maqui);
                    Children.Remove(pulu);
                    Children.Remove(min);
                    Children.Remove(coto);
                    Children.Remove(bano);
                    Children.Remove(ota);
                    Children.Remove(papa);
                    Children.Remove(quilo);
                }
                else
                {
                    CurrentPage = pulu;
                    Children.Remove(coto);
                }
            }
        }
        public async void Check11_Clicked(object sender, EventArgs e)
        {
            string result = await DisplayPromptAsync("Tiempo", "¿Cuanto tiempo desea pasar en el punto?", initialValue: "1", maxLength: 1, keyboard: Keyboard.Numeric);
            if (result == null)
            {
                CurrentPage = pulu;
            }
            else
            {
                var location = await Geolocation.GetLastKnownLocationAsync();
                double km;
                double hr;
                if (K == 1)
                {
                    Repositorio repo = new Repositorio();
                    Consulta consulta = new Consulta();
                    consulta.origen = "Hacienda Ilitío de Plaza, Ecuador";
                    consulta.destino = "Pululahua, Quito, Ecuador";
                    var distanciaGoogle = repo.getDistancia(consulta);
                    double ruta = distanciaGoogle.rows[0].elements[0].distance.value;
                    double tiempo = distanciaGoogle.rows[0].elements[0].duration.value + Double.Parse(result);
                    km = ruta;
                    hr = tiempo;
                    distanciaF = distanciaF + ruta;
                    tiempoF = tiempoF + tiempo;
                }
                else
                {
                    if (J == 1)
                    {
                        Repositorio repo = new Repositorio();
                        Consulta consulta = new Consulta();
                        consulta.origen = "Quilotoa, Provincia de Cotopaxi,Ecuador, Ecuador";
                        consulta.destino = "Pululahua, Quito, Ecuador";
                        var distanciaGoogle = repo.getDistancia(consulta);
                        double ruta = distanciaGoogle.rows[0].elements[0].distance.value;
                        double tiempo = distanciaGoogle.rows[0].elements[0].duration.value + Double.Parse(result);
                        km = ruta;
                        hr = tiempo;
                        distanciaF = distanciaF + ruta;
                        tiempoF = tiempoF + tiempo;
                    }
                    else
                    {
                        if (I == 1)
                        {
                            Repositorio repo = new Repositorio();
                            Consulta consulta = new Consulta();
                            consulta.origen = "Maquipucuna Reserve, Vía a Nanegal, Quito 170168, Ecuador";
                            consulta.destino = "Pululahua, Quito, Ecuador";
                            var distanciaGoogle = repo.getDistancia(consulta);
                            double ruta = distanciaGoogle.rows[0].elements[0].distance.value;
                            double tiempo = distanciaGoogle.rows[0].elements[0].duration.value + Double.Parse(result);
                            km = ruta;
                            hr = tiempo;
                            distanciaF = distanciaF + ruta;
                            tiempoF = tiempoF + tiempo;
                        }
                        else
                        {
                            if (H == 1)
                            {
                                Repositorio repo = new Repositorio();
                                Consulta consulta = new Consulta();
                                consulta.origen = "Vía a Mindo, Ecuador";
                                consulta.destino = "Pululahua, Quito, Ecuador";
                                var distanciaGoogle = repo.getDistancia(consulta);
                                double ruta = distanciaGoogle.rows[0].elements[0].distance.value;
                                double tiempo = distanciaGoogle.rows[0].elements[0].duration.value + Double.Parse(result);
                                km = ruta;
                                hr = tiempo;
                                distanciaF = distanciaF + ruta;
                                tiempoF = tiempoF + tiempo;
                            }
                            else
                            {
                                if (G == 1)
                                {
                                    Repositorio repo = new Repositorio();
                                    Consulta consulta = new Consulta();
                                    consulta.origen = "Jorge Washington 611, Quito 170143, Ecuador";
                                    consulta.destino = "Pululahua, Quito, Ecuador";
                                    var distanciaGoogle = repo.getDistancia(consulta);
                                    double ruta = distanciaGoogle.rows[0].elements[0].distance.value;
                                    double tiempo = distanciaGoogle.rows[0].elements[0].duration.value + Double.Parse(result);
                                    km = ruta;
                                    hr = tiempo;
                                    distanciaF = distanciaF + ruta;
                                    tiempoF = tiempoF + tiempo;
                                }
                                else
                                {
                                    if (F == 1)
                                    {
                                        Repositorio repo = new Repositorio();
                                        Consulta consulta = new Consulta();
                                        consulta.origen = "El Placer, Quito 170130, Ecuador";
                                        consulta.destino = "Pululahua, Quito, Ecuador";
                                        var distanciaGoogle = repo.getDistancia(consulta);
                                        double ruta = distanciaGoogle.rows[0].elements[0].distance.value;
                                        double tiempo = distanciaGoogle.rows[0].elements[0].duration.value + Double.Parse(result);
                                        km = ruta;
                                        hr = tiempo;
                                        distanciaF = distanciaF + ruta;
                                        tiempoF = tiempoF + tiempo;
                                    }
                                    else
                                    {
                                        if (E == 1)
                                        {
                                            Repositorio repo = new Repositorio();
                                            Consulta consulta = new Consulta();
                                            consulta.origen = "Quito 170110, Ecuador";
                                            consulta.destino = "Pululahua, Quito, Ecuador";
                                            var distanciaGoogle = repo.getDistancia(consulta);
                                            double ruta = distanciaGoogle.rows[0].elements[0].distance.value;
                                            double tiempo = distanciaGoogle.rows[0].elements[0].duration.value + Double.Parse(result);
                                            km = ruta;
                                            hr = tiempo;
                                            distanciaF = distanciaF + ruta;
                                            tiempoF = tiempoF + tiempo;
                                        }
                                        else
                                        {
                                            if (D == 1)
                                            {
                                                Repositorio repo = new Repositorio();
                                                Consulta consulta = new Consulta();
                                                consulta.origen = "Zoológico de Quito en Guayllabamba, Urb, Huertos Familiares, Quito 170209, Ecuador";
                                                consulta.destino = "Pululahua, Quito, Ecuador";
                                                var distanciaGoogle = repo.getDistancia(consulta);
                                                double ruta = distanciaGoogle.rows[0].elements[0].distance.value;
                                                double tiempo = distanciaGoogle.rows[0].elements[0].duration.value + Double.Parse(result);
                                                km = ruta;
                                                hr = tiempo;
                                                distanciaF = distanciaF + ruta;
                                                tiempoF = tiempoF + tiempo;
                                            }
                                            else
                                            {
                                                if (C == 1)
                                                {
                                                    Repositorio repo = new Repositorio();
                                                    Consulta consulta = new Consulta();
                                                    consulta.origen = "Panecillo, Quito, Ecuador";
                                                    consulta.destino = "Pululahua, Quito, Ecuador";
                                                    var distanciaGoogle = repo.getDistancia(consulta);
                                                    double ruta = distanciaGoogle.rows[0].elements[0].distance.value;
                                                    double tiempo = distanciaGoogle.rows[0].elements[0].duration.value + Double.Parse(result);
                                                    km = ruta;
                                                    hr = tiempo;
                                                    distanciaF = distanciaF + ruta;
                                                    tiempoF = tiempoF + tiempo;
                                                }
                                                else
                                                {
                                                    if (B == 1)
                                                    {
                                                        Repositorio repo = new Repositorio();
                                                        Consulta consulta = new Consulta();
                                                        consulta.origen = "Teleférico Quito, Quito, Ecuador";
                                                        consulta.destino = "Pululahua, Quito, Ecuador";
                                                        var distanciaGoogle = repo.getDistancia(consulta);
                                                        double ruta = distanciaGoogle.rows[0].elements[0].distance.value;
                                                        double tiempo = distanciaGoogle.rows[0].elements[0].duration.value + Double.Parse(result);
                                                        km = ruta;
                                                        hr = tiempo;
                                                        distanciaF = distanciaF + ruta;
                                                        tiempoF = tiempoF + tiempo;
                                                    }
                                                    else
                                                    {
                                                        if (A == 1)
                                                        {
                                                            Repositorio repo = new Repositorio();
                                                            Consulta consulta = new Consulta();
                                                            consulta.origen = "Manuel Cordova Galarza Km. 13, 5 SN, Quito, Ecuador";
                                                            consulta.destino = "Pululahua, Quito, Ecuador";
                                                            var distanciaGoogle = repo.getDistancia(consulta);
                                                            double ruta = distanciaGoogle.rows[0].elements[0].distance.value;
                                                            double tiempo = distanciaGoogle.rows[0].elements[0].duration.value + Double.Parse(result);
                                                            km = ruta;
                                                            hr = tiempo;
                                                            distanciaF = distanciaF + ruta;
                                                            tiempoF = tiempoF + tiempo;
                                                        }
                                                        else
                                                        {
                                                            Repositorio repo = new Repositorio();
                                                            Consulta consulta = new Consulta();
                                                            consulta.origen = "" + location.Latitude.ToString() + ", " + location.Longitude.ToString() + "";
                                                            consulta.destino = "Pululahua, Quito, Ecuador";
                                                            var distanciaGoogle = repo.getDistancia(consulta);
                                                            double ruta = distanciaGoogle.rows[0].elements[0].distance.value;
                                                            double tiempo = distanciaGoogle.rows[0].elements[0].duration.value + Double.Parse(result);
                                                            km = ruta;
                                                            hr = tiempo;
                                                            distanciaF = ruta;
                                                            tiempoF = tiempo;
                                                        }
                                                    }
                                                }

                                            }

                                        }
                                    }

                                }
                            }
                        }
                    }

                }
                puntos = puntos + 1;
                L = 1;
                Lineas lin = new Lineas();
                lin.Producto = t12.Text;
                lin.Tiempo = hr.ToString();
                lin.Kilometros = km.ToString();
                lin.TiempoPunto = result;
                Lineas lineas = await repo.PostLineas(lin);
                Dialogs.ShowLoading("Buena Elección");
                await Task.Delay(2000);
                Dialogs.HideLoading();
                TiempoMedio = ((buses * puntos) + (tiempoF / 3600));
                Lineas.Add(lin);
                TIEMPO = TIEMPO + Double.Parse(result);
                if (TiempoMedio >= 8)
                {
                    Dialogs.ShowLoading("Has logrado el limite de tiempo diario");
                    await Task.Delay(2000);
                    Dialogs.HideLoading();
                    CurrentPage = gaseosas;
                    Children.Remove(MM);
                    Children.Remove(T);
                    Children.Remove(pan);
                    Children.Remove(zoo);
                    Children.Remove(cl);
                    Children.Remove(ma);
                    Children.Remove(maq);
                    Children.Remove(maqui);
                    Children.Remove(pulu);
                    Children.Remove(min);
                    Children.Remove(coto);
                    Children.Remove(bano);
                    Children.Remove(ota);
                    Children.Remove(papa);
                    Children.Remove(quilo);
                }
                else
                {
                    CurrentPage = bano;
                    Children.Remove(pulu);
                }
            }
        }
        public async void Check12_Clicked(object sender, EventArgs e)
        {
            string result = await DisplayPromptAsync("Tiempo", "¿Cuanto tiempo desea pasar en el punto?", initialValue: "1", maxLength: 1, keyboard: Keyboard.Numeric);
            if (result == null)
            {
                CurrentPage = bano;
            }
            else
            {
                var location = await Geolocation.GetLastKnownLocationAsync();
                double km;
                double hr;
                if (L == 1)
                {
                    Repositorio repo = new Repositorio();
                    Consulta consulta = new Consulta();
                    consulta.origen = "Pululahua, Quito, Ecuador";
                    consulta.destino = "Baños Quito 170130 Ecuador";
                    var distanciaGoogle = repo.getDistancia(consulta);
                    double ruta = distanciaGoogle.rows[0].elements[0].distance.value;
                    double tiempo = distanciaGoogle.rows[0].elements[0].duration.value + Double.Parse(result);
                    km = ruta;
                    hr = tiempo;
                    distanciaF = distanciaF + ruta;
                    tiempoF = tiempoF + tiempo;
                }
                else
                {
                    if (K == 1)
                    {
                        Repositorio repo = new Repositorio();
                        Consulta consulta = new Consulta();
                        consulta.origen = "Hacienda Ilitío de Plaza, Ecuador";
                        consulta.destino = "Baños Quito 170130 Ecuador";
                        var distanciaGoogle = repo.getDistancia(consulta);
                        double ruta = distanciaGoogle.rows[0].elements[0].distance.value;
                        double tiempo = distanciaGoogle.rows[0].elements[0].duration.value + Double.Parse(result);
                        km = ruta;
                        hr = tiempo;
                        distanciaF = distanciaF + ruta;
                        tiempoF = tiempoF + tiempo;
                    }
                    else
                    {
                        if (J == 1)
                        {
                            Repositorio repo = new Repositorio();
                            Consulta consulta = new Consulta();
                            consulta.origen = "Quilotoa, Provincia de Cotopaxi,Ecuador, Ecuador";
                            consulta.destino = "Baños Quito 170130 Ecuador";
                            var distanciaGoogle = repo.getDistancia(consulta);
                            double ruta = distanciaGoogle.rows[0].elements[0].distance.value;
                            double tiempo = distanciaGoogle.rows[0].elements[0].duration.value + Double.Parse(result);
                            km = ruta;
                            hr = tiempo;
                            distanciaF = distanciaF + ruta;
                            tiempoF = tiempoF + tiempo;
                        }
                        else
                        {
                            if (I == 1)
                            {
                                Repositorio repo = new Repositorio();
                                Consulta consulta = new Consulta();
                                consulta.origen = "Maquipucuna Reserve, Vía a Nanegal, Quito 170168, Ecuador";
                                consulta.destino = "Baños Quito 170130 Ecuador";
                                var distanciaGoogle = repo.getDistancia(consulta);
                                double ruta = distanciaGoogle.rows[0].elements[0].distance.value;
                                double tiempo = distanciaGoogle.rows[0].elements[0].duration.value + Double.Parse(result);
                                km = ruta;
                                hr = tiempo;
                                distanciaF = distanciaF + ruta;
                                tiempoF = tiempoF + tiempo;
                            }
                            else
                            {
                                if (H == 1)
                                {
                                    Repositorio repo = new Repositorio();
                                    Consulta consulta = new Consulta();
                                    consulta.origen = "Vía a Mindo, Ecuador";
                                    consulta.destino = "Baños Quito 170130 Ecuador";
                                    var distanciaGoogle = repo.getDistancia(consulta);
                                    double ruta = distanciaGoogle.rows[0].elements[0].distance.value;
                                    double tiempo = distanciaGoogle.rows[0].elements[0].duration.value + Double.Parse(result);
                                    km = ruta;
                                    hr = tiempo;
                                    distanciaF = distanciaF + ruta;
                                    tiempoF = tiempoF + tiempo;
                                }
                                else
                                {
                                    if (G == 1)
                                    {
                                        Repositorio repo = new Repositorio();
                                        Consulta consulta = new Consulta();
                                        consulta.origen = "Jorge Washington 611, Quito 170143, Ecuador";
                                        consulta.destino = "Baños Quito 170130 Ecuador";
                                        var distanciaGoogle = repo.getDistancia(consulta);
                                        double ruta = distanciaGoogle.rows[0].elements[0].distance.value;
                                        double tiempo = distanciaGoogle.rows[0].elements[0].duration.value + Double.Parse(result);
                                        km = ruta;
                                        hr = tiempo;
                                        distanciaF = distanciaF + ruta;
                                        tiempoF = tiempoF + tiempo;
                                    }
                                    else
                                    {
                                        if (F == 1)
                                        {
                                            Repositorio repo = new Repositorio();
                                            Consulta consulta = new Consulta();
                                            consulta.origen = "El Placer, Quito 170130, Ecuador";
                                            consulta.destino = "Baños Quito 170130 Ecuador";
                                            var distanciaGoogle = repo.getDistancia(consulta);
                                            double ruta = distanciaGoogle.rows[0].elements[0].distance.value;
                                            double tiempo = distanciaGoogle.rows[0].elements[0].duration.value + Double.Parse(result);
                                            km = ruta;
                                            hr = tiempo;
                                            distanciaF = distanciaF + ruta;
                                            tiempoF = tiempoF + tiempo;
                                        }
                                        else
                                        {
                                            if (E == 1)
                                            {
                                                Repositorio repo = new Repositorio();
                                                Consulta consulta = new Consulta();
                                                consulta.origen = "Quito 170110, Ecuador";
                                                consulta.destino = "Baños Quito 170130 Ecuador";
                                                var distanciaGoogle = repo.getDistancia(consulta);
                                                double ruta = distanciaGoogle.rows[0].elements[0].distance.value;
                                                double tiempo = distanciaGoogle.rows[0].elements[0].duration.value + Double.Parse(result);
                                                km = ruta;
                                                hr = tiempo;
                                                distanciaF = distanciaF + ruta;
                                                tiempoF = tiempoF + tiempo;
                                            }
                                            else
                                            {
                                                if (D == 1)
                                                {
                                                    Repositorio repo = new Repositorio();
                                                    Consulta consulta = new Consulta();
                                                    consulta.origen = "Zoológico de Quito en Guayllabamba, Urb, Huertos Familiares, Quito 170209, Ecuador";
                                                    consulta.destino = "Baños Quito 170130 Ecuador";
                                                    var distanciaGoogle = repo.getDistancia(consulta);
                                                    double ruta = distanciaGoogle.rows[0].elements[0].distance.value;
                                                    double tiempo = distanciaGoogle.rows[0].elements[0].duration.value + Double.Parse(result);
                                                    km = ruta;
                                                    hr = tiempo;
                                                    distanciaF = distanciaF + ruta;
                                                    tiempoF = tiempoF + tiempo;
                                                }
                                                else
                                                {
                                                    if (C == 1)
                                                    {
                                                        Repositorio repo = new Repositorio();
                                                        Consulta consulta = new Consulta();
                                                        consulta.origen = "Panecillo, Quito, Ecuador";
                                                        consulta.destino = "Baños Quito 170130 Ecuador";
                                                        var distanciaGoogle = repo.getDistancia(consulta);
                                                        double ruta = distanciaGoogle.rows[0].elements[0].distance.value;
                                                        double tiempo = distanciaGoogle.rows[0].elements[0].duration.value + Double.Parse(result);
                                                        km = ruta;
                                                        hr = tiempo;
                                                        distanciaF = distanciaF + ruta;
                                                        tiempoF = tiempoF + tiempo;
                                                    }
                                                    else
                                                    {
                                                        if (B == 1)
                                                        {
                                                            Repositorio repo = new Repositorio();
                                                            Consulta consulta = new Consulta();
                                                            consulta.origen = "Teleférico Quito, Quito, Ecuador";
                                                            consulta.destino = "Baños Quito 170130 Ecuador";
                                                            var distanciaGoogle = repo.getDistancia(consulta);
                                                            double ruta = distanciaGoogle.rows[0].elements[0].distance.value;
                                                            double tiempo = distanciaGoogle.rows[0].elements[0].duration.value + Double.Parse(result);
                                                            km = ruta;
                                                            hr = tiempo;
                                                            distanciaF = distanciaF + ruta;
                                                            tiempoF = tiempoF + tiempo;
                                                        }
                                                        else
                                                        {
                                                            if (A == 1)
                                                            {
                                                                Repositorio repo = new Repositorio();
                                                                Consulta consulta = new Consulta();
                                                                consulta.origen = "Manuel Cordova Galarza Km. 13, 5 SN, Quito, Ecuador";
                                                                consulta.destino = "Baños Quito 170130 Ecuador";
                                                                var distanciaGoogle = repo.getDistancia(consulta);
                                                                double ruta = distanciaGoogle.rows[0].elements[0].distance.value;
                                                                double tiempo = distanciaGoogle.rows[0].elements[0].duration.value + Double.Parse(result);
                                                                km = ruta;
                                                                hr = tiempo;
                                                                distanciaF = distanciaF + ruta;
                                                                tiempoF = tiempoF + tiempo;
                                                            }
                                                            else
                                                            {
                                                                Repositorio repo = new Repositorio();
                                                                Consulta consulta = new Consulta();
                                                                consulta.origen = "" + location.Latitude.ToString() + ", " + location.Longitude.ToString() + "";
                                                                consulta.destino = "Baños Quito 170130 Ecuador";
                                                                var distanciaGoogle = repo.getDistancia(consulta);
                                                                double ruta = distanciaGoogle.rows[0].elements[0].distance.value;
                                                                double tiempo = distanciaGoogle.rows[0].elements[0].duration.value + Double.Parse(result);
                                                                km = ruta;
                                                                hr = tiempo;
                                                                distanciaF = ruta;
                                                                tiempoF = tiempo;
                                                            }
                                                        }
                                                    }

                                                }

                                            }
                                        }

                                    }
                                }
                            }
                        }

                    }
                }
                puntos = puntos + 1;
                M = 1;
                Lineas lin = new Lineas();
                lin.Producto = t13.Text;
                lin.Tiempo = hr.ToString();
                lin.Kilometros = km.ToString();
                lin.TiempoPunto = result;
                Lineas lineas = await repo.PostLineas(lin);
                Dialogs.ShowLoading("Buena Elección");
                await Task.Delay(2000);
                Dialogs.HideLoading();
                TiempoMedio = ((buses * puntos) + (tiempoF / 3600));
                Lineas.Add(lin);
                TIEMPO = TIEMPO + Double.Parse(result);
                if (TiempoMedio >= 8)
                {
                    Dialogs.ShowLoading("Has logrado el limite de tiempo diario");
                    await Task.Delay(2000);
                    Dialogs.HideLoading();
                    CurrentPage = gaseosas;
                    Children.Remove(MM);
                    Children.Remove(T);
                    Children.Remove(pan);
                    Children.Remove(zoo);
                    Children.Remove(cl);
                    Children.Remove(ma);
                    Children.Remove(maq);
                    Children.Remove(maqui);
                    Children.Remove(pulu);
                    Children.Remove(min);
                    Children.Remove(coto);
                    Children.Remove(bano);
                    Children.Remove(ota);
                    Children.Remove(papa);
                    Children.Remove(quilo);
                }
                else
                {
                    CurrentPage = ota;
                    Children.Remove(bano);
                }
            }
            
        }
        public async void Check13_Clicked(object sender, EventArgs e)
        {
            string result = await DisplayPromptAsync("Tiempo", "¿Cuanto tiempo desea pasar en el punto?", initialValue: "1", maxLength: 1, keyboard: Keyboard.Numeric);
            if (result == null)
            {
                CurrentPage = ota;
            }
            else
            {
                var location = await Geolocation.GetLastKnownLocationAsync();
                double km;
                double hr;
                if (M == 1)
                {
                    Repositorio repo = new Repositorio();
                    Consulta consulta = new Consulta();
                    consulta.origen = "Baños Quito 170130 Ecuador";
                    consulta.destino = "Otavalo 100401, Ecuador";
                    var distanciaGoogle = repo.getDistancia(consulta);
                    double ruta = distanciaGoogle.rows[0].elements[0].distance.value;
                    double tiempo = distanciaGoogle.rows[0].elements[0].duration.value + Double.Parse(result);
                    km = ruta;
                    hr = tiempo;
                    distanciaF = distanciaF + ruta;
                    tiempoF = tiempoF + tiempo;
                }
                else
                {
                    if (L == 1)
                    {
                        Repositorio repo = new Repositorio();
                        Consulta consulta = new Consulta();
                        consulta.origen = "Pululahua, Quito, Ecuador";
                        consulta.destino = "Otavalo 100401, Ecuador";
                        var distanciaGoogle = repo.getDistancia(consulta);
                        double ruta = distanciaGoogle.rows[0].elements[0].distance.value;
                        double tiempo = distanciaGoogle.rows[0].elements[0].duration.value + Double.Parse(result);
                        km = ruta;
                        hr = tiempo;
                        distanciaF = distanciaF + ruta;
                        tiempoF = tiempoF + tiempo;
                    }
                    else
                    {
                        if (K == 1)
                        {
                            Repositorio repo = new Repositorio();
                            Consulta consulta = new Consulta();
                            consulta.origen = "Hacienda Ilitío de Plaza, Ecuador";
                            consulta.destino = "Otavalo 100401, Ecuador";
                            var distanciaGoogle = repo.getDistancia(consulta);
                            double ruta = distanciaGoogle.rows[0].elements[0].distance.value;
                            double tiempo = distanciaGoogle.rows[0].elements[0].duration.value + Double.Parse(result);
                            km = ruta;
                            hr = tiempo;
                            distanciaF = distanciaF + ruta;
                            tiempoF = tiempoF + tiempo;
                        }
                        else
                        {
                            if (J == 1)
                            {
                                Repositorio repo = new Repositorio();
                                Consulta consulta = new Consulta();
                                consulta.origen = "Quilotoa, Provincia de Cotopaxi,Ecuador, Ecuador";
                                consulta.destino = "Otavalo 100401, Ecuador";
                                var distanciaGoogle = repo.getDistancia(consulta);
                                double ruta = distanciaGoogle.rows[0].elements[0].distance.value;
                                double tiempo = distanciaGoogle.rows[0].elements[0].duration.value + Double.Parse(result);
                                km = ruta;
                                hr = tiempo;
                                distanciaF = distanciaF + ruta;
                                tiempoF = tiempoF + tiempo;
                            }
                            else
                            {
                                if (I == 1)
                                {
                                    Repositorio repo = new Repositorio();
                                    Consulta consulta = new Consulta();
                                    consulta.origen = "Maquipucuna Reserve, Vía a Nanegal, Quito 170168, Ecuador";
                                    consulta.destino = "Otavalo 100401, Ecuador";
                                    var distanciaGoogle = repo.getDistancia(consulta);
                                    double ruta = distanciaGoogle.rows[0].elements[0].distance.value;
                                    double tiempo = distanciaGoogle.rows[0].elements[0].duration.value + Double.Parse(result);
                                    km = ruta;
                                    hr = tiempo;
                                    distanciaF = distanciaF + ruta;
                                    tiempoF = tiempoF + tiempo;
                                }
                                else
                                {
                                    if (H == 1)
                                    {
                                        Repositorio repo = new Repositorio();
                                        Consulta consulta = new Consulta();
                                        consulta.origen = "Vía a Mindo, Ecuador";
                                        consulta.destino = "Otavalo 100401, Ecuador";
                                        var distanciaGoogle = repo.getDistancia(consulta);
                                        double ruta = distanciaGoogle.rows[0].elements[0].distance.value;
                                        double tiempo = distanciaGoogle.rows[0].elements[0].duration.value + Double.Parse(result);
                                        km = ruta;
                                        hr = tiempo;
                                        distanciaF = distanciaF + ruta;
                                        tiempoF = tiempoF + tiempo;
                                    }
                                    else
                                    {
                                        if (G == 1)
                                        {
                                            Repositorio repo = new Repositorio();
                                            Consulta consulta = new Consulta();
                                            consulta.origen = "Jorge Washington 611, Quito 170143, Ecuador";
                                            consulta.destino = "Otavalo 100401, Ecuador";
                                            var distanciaGoogle = repo.getDistancia(consulta);
                                            double ruta = distanciaGoogle.rows[0].elements[0].distance.value;
                                            double tiempo = distanciaGoogle.rows[0].elements[0].duration.value + Double.Parse(result);
                                            km = ruta;
                                            hr = tiempo;
                                            distanciaF = distanciaF + ruta;
                                            tiempoF = tiempoF + tiempo;
                                        }
                                        else
                                        {
                                            if (F == 1)
                                            {
                                                Repositorio repo = new Repositorio();
                                                Consulta consulta = new Consulta();
                                                consulta.origen = "El Placer, Quito 170130, Ecuador";
                                                consulta.destino = "Otavalo 100401, Ecuador";
                                                var distanciaGoogle = repo.getDistancia(consulta);
                                                double ruta = distanciaGoogle.rows[0].elements[0].distance.value;
                                                double tiempo = distanciaGoogle.rows[0].elements[0].duration.value + Double.Parse(result);
                                                km = ruta;
                                                hr = tiempo;
                                                distanciaF = distanciaF + ruta;
                                                tiempoF = tiempoF + tiempo;
                                            }
                                            else
                                            {
                                                if (E == 1)
                                                {
                                                    Repositorio repo = new Repositorio();
                                                    Consulta consulta = new Consulta();
                                                    consulta.origen = "Quito 170110, Ecuador";
                                                    consulta.destino = "Otavalo 100401, Ecuador";
                                                    var distanciaGoogle = repo.getDistancia(consulta);
                                                    double ruta = distanciaGoogle.rows[0].elements[0].distance.value;
                                                    double tiempo = distanciaGoogle.rows[0].elements[0].duration.value + Double.Parse(result);
                                                    km = ruta;
                                                    hr = tiempo;
                                                    distanciaF = distanciaF + ruta;
                                                    tiempoF = tiempoF + tiempo;
                                                }
                                                else
                                                {
                                                    if (D == 1)
                                                    {
                                                        Repositorio repo = new Repositorio();
                                                        Consulta consulta = new Consulta();
                                                        consulta.origen = "Zoológico de Quito en Guayllabamba, Urb, Huertos Familiares, Quito 170209, Ecuador";
                                                        consulta.destino = "Otavalo 100401, Ecuador";
                                                        var distanciaGoogle = repo.getDistancia(consulta);
                                                        double ruta = distanciaGoogle.rows[0].elements[0].distance.value;
                                                        double tiempo = distanciaGoogle.rows[0].elements[0].duration.value + Double.Parse(result);
                                                        km = ruta;
                                                        hr = tiempo;
                                                        distanciaF = distanciaF + ruta;
                                                        tiempoF = tiempoF + tiempo;
                                                    }
                                                    else
                                                    {
                                                        if (C == 1)
                                                        {
                                                            Repositorio repo = new Repositorio();
                                                            Consulta consulta = new Consulta();
                                                            consulta.origen = "Panecillo, Quito, Ecuador";
                                                            consulta.destino = "Otavalo 100401, Ecuador";
                                                            var distanciaGoogle = repo.getDistancia(consulta);
                                                            double ruta = distanciaGoogle.rows[0].elements[0].distance.value;
                                                            double tiempo = distanciaGoogle.rows[0].elements[0].duration.value + Double.Parse(result);
                                                            km = ruta;
                                                            hr = tiempo;
                                                            distanciaF = distanciaF + ruta;
                                                            tiempoF = tiempoF + tiempo;
                                                        }
                                                        else
                                                        {
                                                            if (B == 1)
                                                            {
                                                                Repositorio repo = new Repositorio();
                                                                Consulta consulta = new Consulta();
                                                                consulta.origen = "Teleférico Quito, Quito, Ecuador";
                                                                consulta.destino = "Otavalo 100401, Ecuador";
                                                                var distanciaGoogle = repo.getDistancia(consulta);
                                                                double ruta = distanciaGoogle.rows[0].elements[0].distance.value;
                                                                double tiempo = distanciaGoogle.rows[0].elements[0].duration.value + Double.Parse(result);
                                                                km = ruta;
                                                                hr = tiempo;
                                                                distanciaF = distanciaF + ruta;
                                                                tiempoF = tiempoF + tiempo;
                                                            }
                                                            else
                                                            {
                                                                if (A == 1)
                                                                {
                                                                    Repositorio repo = new Repositorio();
                                                                    Consulta consulta = new Consulta();
                                                                    consulta.origen = "Manuel Cordova Galarza Km. 13, 5 SN, Quito, Ecuador";
                                                                    consulta.destino = "Otavalo 100401, Ecuador";
                                                                    var distanciaGoogle = repo.getDistancia(consulta);
                                                                    double ruta = distanciaGoogle.rows[0].elements[0].distance.value;
                                                                    double tiempo = distanciaGoogle.rows[0].elements[0].duration.value + Double.Parse(result);
                                                                    km = ruta;
                                                                    hr = tiempo;
                                                                    distanciaF = distanciaF + ruta;
                                                                    tiempoF = tiempoF + tiempo;
                                                                }
                                                                else
                                                                {
                                                                    Repositorio repo = new Repositorio();
                                                                    Consulta consulta = new Consulta();
                                                                    consulta.origen = "" + location.Latitude.ToString() + ", " + location.Longitude.ToString() + "";
                                                                    consulta.destino = "Otavalo 100401, Ecuador";
                                                                    var distanciaGoogle = repo.getDistancia(consulta);
                                                                    double ruta = distanciaGoogle.rows[0].elements[0].distance.value;
                                                                    double tiempo = distanciaGoogle.rows[0].elements[0].duration.value + Double.Parse(result);
                                                                    km = ruta;
                                                                    hr = tiempo;
                                                                    distanciaF = ruta;
                                                                    tiempoF = tiempo;
                                                                }
                                                            }
                                                        }

                                                    }

                                                }
                                            }

                                        }
                                    }
                                }
                            }

                        }
                    }
                }
                puntos = puntos + 1;
                N = 1;
                Lineas lin = new Lineas();
                lin.Producto = t14.Text;
                lin.Tiempo = hr.ToString();
                lin.Kilometros = km.ToString();
                lin.TiempoPunto = result;
                Lineas lineas = await repo.PostLineas(lin);
                Dialogs.ShowLoading("Buena Elección");
                await Task.Delay(2000);
                Dialogs.HideLoading();
                TiempoMedio = ((buses * puntos) + (tiempoF / 3600));
                Lineas.Add(lin);
                TIEMPO = TIEMPO + Double.Parse(result);
                if (TiempoMedio >= 8)
                {
                    Dialogs.ShowLoading("Has logrado el limite de tiempo diario");
                    await Task.Delay(2000);
                    Dialogs.HideLoading();
                    CurrentPage = gaseosas;
                    Children.Remove(MM);
                    Children.Remove(T);
                    Children.Remove(pan);
                    Children.Remove(zoo);
                    Children.Remove(cl);
                    Children.Remove(ma);
                    Children.Remove(maq);
                    Children.Remove(maqui);
                    Children.Remove(pulu);
                    Children.Remove(min);
                    Children.Remove(coto);
                    Children.Remove(bano);
                    Children.Remove(ota);
                    Children.Remove(papa);
                    Children.Remove(quilo);
                }
                else
                {
                    CurrentPage = papa;
                    Children.Remove(ota);
                }
            }
        }
        public async void Check14_Clicked(object sender, EventArgs e)
        {
            string result = await DisplayPromptAsync("Tiempo", "¿Cuanto tiempo desea pasar en el punto?", initialValue: "1", maxLength: 1, keyboard: Keyboard.Numeric);
            if (result == null)
            {
                CurrentPage = papa;
            }
            else
            {
                var location = await Geolocation.GetLastKnownLocationAsync();
                double km;
                double hr;
                if (N == 1)
                {
                    Repositorio repo = new Repositorio();
                    Consulta consulta = new Consulta();
                    consulta.origen = "Otavalo 100401, Ecuador";
                    consulta.destino = "km. 65 via Quito - Baeza, Papallacta, Ecuador";
                    var distanciaGoogle = repo.getDistancia(consulta);
                    double ruta = distanciaGoogle.rows[0].elements[0].distance.value;
                    double tiempo = distanciaGoogle.rows[0].elements[0].duration.value + Double.Parse(result);
                    km = ruta;
                    hr = tiempo;
                    distanciaF = distanciaF + ruta;
                    tiempoF = tiempoF + tiempo;
                }
                else
                {
                    if (M == 1)
                    {
                        Repositorio repo = new Repositorio();
                        Consulta consulta = new Consulta();
                        consulta.origen = "Baños Quito 170130 Ecuador";
                        consulta.destino = "km. 65 via Quito - Baeza, Papallacta, Ecuador";
                        var distanciaGoogle = repo.getDistancia(consulta);
                        double ruta = distanciaGoogle.rows[0].elements[0].distance.value;
                        double tiempo = distanciaGoogle.rows[0].elements[0].duration.value + Double.Parse(result);
                        km = ruta;
                        hr = tiempo;
                        distanciaF = distanciaF + ruta;
                        tiempoF = tiempoF + tiempo;
                    }
                    else
                    {
                        if (L == 1)
                        {
                            Repositorio repo = new Repositorio();
                            Consulta consulta = new Consulta();
                            consulta.origen = "Pululahua, Quito, Ecuador";
                            consulta.destino = "km. 65 via Quito - Baeza, Papallacta, Ecuador";
                            var distanciaGoogle = repo.getDistancia(consulta);
                            double ruta = distanciaGoogle.rows[0].elements[0].distance.value;
                            double tiempo = distanciaGoogle.rows[0].elements[0].duration.value + Double.Parse(result);
                            km = ruta;
                            hr = tiempo;
                            distanciaF = distanciaF + ruta;
                            tiempoF = tiempoF + tiempo;
                        }
                        else
                        {
                            if (K == 1)
                            {
                                Repositorio repo = new Repositorio();
                                Consulta consulta = new Consulta();
                                consulta.origen = "Hacienda Ilitío de Plaza, Ecuador";
                                consulta.destino = "km. 65 via Quito - Baeza, Papallacta, Ecuador";
                                var distanciaGoogle = repo.getDistancia(consulta);
                                double ruta = distanciaGoogle.rows[0].elements[0].distance.value;
                                double tiempo = distanciaGoogle.rows[0].elements[0].duration.value + Double.Parse(result);
                                km = ruta;
                                hr = tiempo;
                                distanciaF = distanciaF + ruta;
                                tiempoF = tiempoF + tiempo;
                            }
                            else
                            {
                                if (J == 1)
                                {
                                    Repositorio repo = new Repositorio();
                                    Consulta consulta = new Consulta();
                                    consulta.origen = "Quilotoa, Provincia de Cotopaxi,Ecuador, Ecuador";
                                    consulta.destino = "km. 65 via Quito - Baeza, Papallacta, Ecuador";
                                    var distanciaGoogle = repo.getDistancia(consulta);
                                    double ruta = distanciaGoogle.rows[0].elements[0].distance.value;
                                    double tiempo = distanciaGoogle.rows[0].elements[0].duration.value + Double.Parse(result);
                                    km = ruta;
                                    hr = tiempo;
                                    distanciaF = distanciaF + ruta;
                                    tiempoF = tiempoF + tiempo;
                                }
                                else
                                {
                                    if (I == 1)
                                    {
                                        Repositorio repo = new Repositorio();
                                        Consulta consulta = new Consulta();
                                        consulta.origen = "Maquipucuna Reserve, Vía a Nanegal, Quito 170168, Ecuador";
                                        consulta.destino = "km. 65 via Quito - Baeza, Papallacta, Ecuador";
                                        var distanciaGoogle = repo.getDistancia(consulta);
                                        double ruta = distanciaGoogle.rows[0].elements[0].distance.value;
                                        double tiempo = distanciaGoogle.rows[0].elements[0].duration.value + Double.Parse(result);
                                        km = ruta;
                                        hr = tiempo;
                                        distanciaF = distanciaF + ruta;
                                        tiempoF = tiempoF + tiempo;
                                    }
                                    else
                                    {
                                        if (H == 1)
                                        {
                                            Repositorio repo = new Repositorio();
                                            Consulta consulta = new Consulta();
                                            consulta.origen = "Vía a Mindo, Ecuador";
                                            consulta.destino = "km. 65 via Quito - Baeza, Papallacta, Ecuador";
                                            var distanciaGoogle = repo.getDistancia(consulta);
                                            double ruta = distanciaGoogle.rows[0].elements[0].distance.value;
                                            double tiempo = distanciaGoogle.rows[0].elements[0].duration.value + Double.Parse(result);
                                            km = ruta;
                                            hr = tiempo;
                                            distanciaF = distanciaF + ruta;
                                            tiempoF = tiempoF + tiempo;
                                        }
                                        else
                                        {
                                            if (G == 1)
                                            {
                                                Repositorio repo = new Repositorio();
                                                Consulta consulta = new Consulta();
                                                consulta.origen = "Jorge Washington 611, Quito 170143, Ecuador";
                                                consulta.destino = "km. 65 via Quito - Baeza, Papallacta, Ecuador";
                                                var distanciaGoogle = repo.getDistancia(consulta);
                                                double ruta = distanciaGoogle.rows[0].elements[0].distance.value;
                                                double tiempo = distanciaGoogle.rows[0].elements[0].duration.value + Double.Parse(result);
                                                km = ruta;
                                                hr = tiempo;
                                                distanciaF = distanciaF + ruta;
                                                tiempoF = tiempoF + tiempo;
                                            }
                                            else
                                            {
                                                if (F == 1)
                                                {
                                                    Repositorio repo = new Repositorio();
                                                    Consulta consulta = new Consulta();
                                                    consulta.origen = "El Placer, Quito 170130, Ecuador";
                                                    consulta.destino = "km. 65 via Quito - Baeza, Papallacta, Ecuador";
                                                    var distanciaGoogle = repo.getDistancia(consulta);
                                                    double ruta = distanciaGoogle.rows[0].elements[0].distance.value;
                                                    double tiempo = distanciaGoogle.rows[0].elements[0].duration.value + Double.Parse(result);
                                                    km = ruta;
                                                    hr = tiempo;
                                                    distanciaF = distanciaF + ruta;
                                                    tiempoF = tiempoF + tiempo;
                                                }
                                                else
                                                {
                                                    if (E == 1)
                                                    {
                                                        Repositorio repo = new Repositorio();
                                                        Consulta consulta = new Consulta();
                                                        consulta.origen = "Quito 170110, Ecuador";
                                                        consulta.destino = "km. 65 via Quito - Baeza, Papallacta, Ecuador";
                                                        var distanciaGoogle = repo.getDistancia(consulta);
                                                        double ruta = distanciaGoogle.rows[0].elements[0].distance.value;
                                                        double tiempo = distanciaGoogle.rows[0].elements[0].duration.value + Double.Parse(result);
                                                        km = ruta;
                                                        hr = tiempo;
                                                        distanciaF = distanciaF + ruta;
                                                        tiempoF = tiempoF + tiempo;
                                                    }
                                                    else
                                                    {
                                                        if (D == 1)
                                                        {
                                                            Repositorio repo = new Repositorio();
                                                            Consulta consulta = new Consulta();
                                                            consulta.origen = "Zoológico de Quito en Guayllabamba, Urb, Huertos Familiares, Quito 170209, Ecuador";
                                                            consulta.destino = "km. 65 via Quito - Baeza, Papallacta, Ecuador";
                                                            var distanciaGoogle = repo.getDistancia(consulta);
                                                            double ruta = distanciaGoogle.rows[0].elements[0].distance.value;
                                                            double tiempo = distanciaGoogle.rows[0].elements[0].duration.value + Double.Parse(result);
                                                            km = ruta;
                                                            hr = tiempo;
                                                            distanciaF = distanciaF + ruta;
                                                            tiempoF = tiempoF + tiempo;
                                                        }
                                                        else
                                                        {
                                                            if (C == 1)
                                                            {
                                                                Repositorio repo = new Repositorio();
                                                                Consulta consulta = new Consulta();
                                                                consulta.origen = "Panecillo, Quito, Ecuador";
                                                                consulta.destino = "km. 65 via Quito - Baeza, Papallacta, Ecuador";
                                                                var distanciaGoogle = repo.getDistancia(consulta);
                                                                double ruta = distanciaGoogle.rows[0].elements[0].distance.value;
                                                                double tiempo = distanciaGoogle.rows[0].elements[0].duration.value + Double.Parse(result);
                                                                km = ruta;
                                                                hr = tiempo;
                                                                distanciaF = distanciaF + ruta;
                                                                tiempoF = tiempoF + tiempo;
                                                            }
                                                            else
                                                            {
                                                                if (B == 1)
                                                                {
                                                                    Repositorio repo = new Repositorio();
                                                                    Consulta consulta = new Consulta();
                                                                    consulta.origen = "Teleférico Quito, Quito, Ecuador";
                                                                    consulta.destino = "km. 65 via Quito - Baeza, Papallacta, Ecuador";
                                                                    var distanciaGoogle = repo.getDistancia(consulta);
                                                                    double ruta = distanciaGoogle.rows[0].elements[0].distance.value;
                                                                    double tiempo = distanciaGoogle.rows[0].elements[0].duration.value + Double.Parse(result);
                                                                    km = ruta;
                                                                    hr = tiempo;
                                                                    distanciaF = distanciaF + ruta;
                                                                    tiempoF = tiempoF + tiempo;
                                                                }
                                                                else
                                                                {
                                                                    if (A == 1)
                                                                    {
                                                                        Repositorio repo = new Repositorio();
                                                                        Consulta consulta = new Consulta();
                                                                        consulta.origen = "Manuel Cordova Galarza Km. 13, 5 SN, Quito, Ecuador";
                                                                        consulta.destino = "km. 65 via Quito - Baeza, Papallacta, Ecuador";
                                                                        var distanciaGoogle = repo.getDistancia(consulta);
                                                                        double ruta = distanciaGoogle.rows[0].elements[0].distance.value;
                                                                        double tiempo = distanciaGoogle.rows[0].elements[0].duration.value + Double.Parse(result);
                                                                        km = ruta;
                                                                        hr = tiempo;
                                                                        distanciaF = distanciaF + ruta;
                                                                        tiempoF = tiempoF + tiempo;
                                                                    }
                                                                    else
                                                                    {
                                                                        Repositorio repo = new Repositorio();
                                                                        Consulta consulta = new Consulta();
                                                                        consulta.origen = "" + location.Latitude.ToString() + ", " + location.Longitude.ToString() + "";
                                                                        consulta.destino = "km. 65 via Quito - Baeza, Papallacta, Ecuador";
                                                                        var distanciaGoogle = repo.getDistancia(consulta);
                                                                        double ruta = distanciaGoogle.rows[0].elements[0].distance.value;
                                                                        double tiempo = distanciaGoogle.rows[0].elements[0].duration.value + Double.Parse(result);
                                                                        km = ruta;
                                                                        hr = tiempo;
                                                                        distanciaF = ruta;
                                                                        tiempoF = tiempo;
                                                                    }
                                                                }
                                                            }

                                                        }

                                                    }
                                                }

                                            }
                                        }
                                    }
                                }

                            }
                        }
                    }
                }
                O = 1;
                puntos = puntos + 1;
                Lineas lin = new Lineas();
                lin.Producto = t15.Text;
                lin.Tiempo = hr.ToString();
                lin.Kilometros = km.ToString();
                lin.TiempoPunto = result;
                Lineas lineas = await repo.PostLineas(lin);
                Dialogs.ShowLoading("Buena Elección");
                await Task.Delay(2000);
                Dialogs.HideLoading();
                TiempoMedio = ((buses * puntos) + (tiempoF / 3600));
                Lineas.Add(lin);
                TIEMPO = TIEMPO + Double.Parse(result);
                if (TiempoMedio >= 8)
                {
                    Dialogs.ShowLoading("Has logrado el limite de tiempo diario");
                    await Task.Delay(2000);
                    Dialogs.HideLoading();
                    CurrentPage = gaseosas;
                    Children.Remove(MM);
                    Children.Remove(T);
                    Children.Remove(pan);
                    Children.Remove(zoo);
                    Children.Remove(cl);
                    Children.Remove(ma);
                    Children.Remove(maq);
                    Children.Remove(maqui);
                    Children.Remove(pulu);
                    Children.Remove(min);
                    Children.Remove(coto);
                    Children.Remove(bano);
                    Children.Remove(ota);
                    Children.Remove(papa);
                    Children.Remove(quilo);
                }
                else
                {
                    CurrentPage = CH;
                    Children.Remove(papa);
                }
            }
            

        }
        public async void Check15_Clicked(object sender, EventArgs e)
        {
            string result = await DisplayPromptAsync("Tiempo", "¿Cuanto tiempo desea pasar en el punto?", initialValue: "1", maxLength: 1, keyboard: Keyboard.Numeric);
            if (result == null)
            {
                CurrentPage = CH;
            }
            else
            {
                var location = await Geolocation.GetLastKnownLocationAsync();
                double km;
                double hr;
                if (O == 1)
                {
                    Repositorio repo = new Repositorio();
                    Consulta consulta = new Consulta();
                    consulta.origen = "km. 65 via Quito - Baeza, Papallacta, Ecuador";
                    consulta.destino = "Centro Historico, Quito, Ecuador";
                    var distanciaGoogle = repo.getDistancia(consulta);
                    double ruta = distanciaGoogle.rows[0].elements[0].distance.value;
                    double tiempo = distanciaGoogle.rows[0].elements[0].duration.value + Double.Parse(result);
                    km = ruta;
                    hr = tiempo;
                    distanciaF = distanciaF + ruta;
                    tiempoF = tiempoF + tiempo;
                }
                else
                {
                    if (N == 1)
                    {
                        Repositorio repo = new Repositorio();
                        Consulta consulta = new Consulta();
                        consulta.origen = "Otavalo 100401, Ecuador";
                        consulta.destino = "Centro Historico, Quito, Ecuador";
                        var distanciaGoogle = repo.getDistancia(consulta);
                        double ruta = distanciaGoogle.rows[0].elements[0].distance.value;
                        double tiempo = distanciaGoogle.rows[0].elements[0].duration.value + Double.Parse(result);
                        km = ruta;
                        hr = tiempo;
                        distanciaF = distanciaF + ruta;
                        tiempoF = tiempoF + tiempo;
                    }
                    else
                    {
                        if (M == 1)
                        {
                            Repositorio repo = new Repositorio();
                            Consulta consulta = new Consulta();
                            consulta.origen = "Baños Quito 170130 Ecuador";
                            consulta.destino = "Centro Historico, Quito, Ecuador";
                            var distanciaGoogle = repo.getDistancia(consulta);
                            double ruta = distanciaGoogle.rows[0].elements[0].distance.value;
                            double tiempo = distanciaGoogle.rows[0].elements[0].duration.value + Double.Parse(result);
                            km = ruta;
                            hr = tiempo;
                            distanciaF = distanciaF + ruta;
                            tiempoF = tiempoF + tiempo;
                        }
                        else
                        {
                            if (L == 1)
                            {
                                Repositorio repo = new Repositorio();
                                Consulta consulta = new Consulta();
                                consulta.origen = "Pululahua, Quito, Ecuador";
                                consulta.destino = "Centro Historico, Quito, Ecuador";
                                var distanciaGoogle = repo.getDistancia(consulta);
                                double ruta = distanciaGoogle.rows[0].elements[0].distance.value;
                                double tiempo = distanciaGoogle.rows[0].elements[0].duration.value + Double.Parse(result);
                                km = ruta;
                                hr = tiempo;
                                distanciaF = distanciaF + ruta;
                                tiempoF = tiempoF + tiempo;
                            }
                            else
                            {
                                if (K == 1)
                                {
                                    Repositorio repo = new Repositorio();
                                    Consulta consulta = new Consulta();
                                    consulta.origen = "Hacienda Ilitío de Plaza, Ecuador";
                                    consulta.destino = "Centro Historico, Quito, Ecuador";
                                    var distanciaGoogle = repo.getDistancia(consulta);
                                    double ruta = distanciaGoogle.rows[0].elements[0].distance.value;
                                    double tiempo = distanciaGoogle.rows[0].elements[0].duration.value + Double.Parse(result);
                                    km = ruta;
                                    hr = tiempo;
                                    distanciaF = distanciaF + ruta;
                                    tiempoF = tiempoF + tiempo;
                                }
                                else
                                {
                                    if (J == 1)
                                    {
                                        Repositorio repo = new Repositorio();
                                        Consulta consulta = new Consulta();
                                        consulta.origen = "Quilotoa, Provincia de Cotopaxi,Ecuador, Ecuador";
                                        consulta.destino = "Centro Historico, Quito, Ecuador";
                                        var distanciaGoogle = repo.getDistancia(consulta);
                                        double ruta = distanciaGoogle.rows[0].elements[0].distance.value;
                                        double tiempo = distanciaGoogle.rows[0].elements[0].duration.value + Double.Parse(result);
                                        km = ruta;
                                        hr = tiempo;
                                        distanciaF = distanciaF + ruta;
                                        tiempoF = tiempoF + tiempo;
                                    }
                                    else
                                    {
                                        if (I == 1)
                                        {
                                            Repositorio repo = new Repositorio();
                                            Consulta consulta = new Consulta();
                                            consulta.origen = "Maquipucuna Reserve, Vía a Nanegal, Quito 170168, Ecuador";
                                            consulta.destino = "Centro Historico, Quito, Ecuador";
                                            var distanciaGoogle = repo.getDistancia(consulta);
                                            double ruta = distanciaGoogle.rows[0].elements[0].distance.value;
                                            double tiempo = distanciaGoogle.rows[0].elements[0].duration.value + Double.Parse(result);
                                            km = ruta;
                                            hr = tiempo;
                                            distanciaF = distanciaF + ruta;
                                            tiempoF = tiempoF + tiempo;
                                        }
                                        else
                                        {
                                            if (H == 1)
                                            {
                                                Repositorio repo = new Repositorio();
                                                Consulta consulta = new Consulta();
                                                consulta.origen = "Vía a Mindo, Ecuador";
                                                consulta.destino = "Centro Historico, Quito, Ecuador";
                                                var distanciaGoogle = repo.getDistancia(consulta);
                                                double ruta = distanciaGoogle.rows[0].elements[0].distance.value;
                                                double tiempo = distanciaGoogle.rows[0].elements[0].duration.value + Double.Parse(result);
                                                km = ruta;
                                                hr = tiempo;
                                                distanciaF = distanciaF + ruta;
                                                tiempoF = tiempoF + tiempo;
                                            }
                                            else
                                            {
                                                if (G == 1)
                                                {
                                                    Repositorio repo = new Repositorio();
                                                    Consulta consulta = new Consulta();
                                                    consulta.origen = "Jorge Washington 611, Quito 170143, Ecuador";
                                                    consulta.destino = "Centro Historico, Quito, Ecuador";
                                                    var distanciaGoogle = repo.getDistancia(consulta);
                                                    double ruta = distanciaGoogle.rows[0].elements[0].distance.value;
                                                    double tiempo = distanciaGoogle.rows[0].elements[0].duration.value + Double.Parse(result);
                                                    km = ruta;
                                                    hr = tiempo;
                                                    distanciaF = distanciaF + ruta;
                                                    tiempoF = tiempoF + tiempo;
                                                }
                                                else
                                                {
                                                    if (F == 1)
                                                    {
                                                        Repositorio repo = new Repositorio();
                                                        Consulta consulta = new Consulta();
                                                        consulta.origen = "El Placer, Quito 170130, Ecuador";
                                                        consulta.destino = "Centro Historico, Quito, Ecuador";
                                                        var distanciaGoogle = repo.getDistancia(consulta);
                                                        double ruta = distanciaGoogle.rows[0].elements[0].distance.value;
                                                        double tiempo = distanciaGoogle.rows[0].elements[0].duration.value + Double.Parse(result);
                                                        km = ruta;
                                                        hr = tiempo;
                                                        distanciaF = distanciaF + ruta;
                                                        tiempoF = tiempoF + tiempo;
                                                    }
                                                    else
                                                    {
                                                        if (E == 1)
                                                        {
                                                            Repositorio repo = new Repositorio();
                                                            Consulta consulta = new Consulta();
                                                            consulta.origen = "Quito 170110, Ecuador";
                                                            consulta.destino = "Centro Historico, Quito, Ecuador";
                                                            var distanciaGoogle = repo.getDistancia(consulta);
                                                            double ruta = distanciaGoogle.rows[0].elements[0].distance.value;
                                                            double tiempo = distanciaGoogle.rows[0].elements[0].duration.value + Double.Parse(result);
                                                            km = ruta;
                                                            hr = tiempo;
                                                            distanciaF = distanciaF + ruta;
                                                            tiempoF = tiempoF + tiempo;
                                                        }
                                                        else
                                                        {
                                                            if (D == 1)
                                                            {
                                                                Repositorio repo = new Repositorio();
                                                                Consulta consulta = new Consulta();
                                                                consulta.origen = "Zoológico de Quito en Guayllabamba, Urb, Huertos Familiares, Quito 170209, Ecuador";
                                                                consulta.destino = "Centro Historico, Quito, Ecuador";
                                                                var distanciaGoogle = repo.getDistancia(consulta);
                                                                double ruta = distanciaGoogle.rows[0].elements[0].distance.value;
                                                                double tiempo = distanciaGoogle.rows[0].elements[0].duration.value + Double.Parse(result);
                                                                km = ruta;
                                                                hr = tiempo;
                                                                distanciaF = distanciaF + ruta;
                                                                tiempoF = tiempoF + tiempo;
                                                            }
                                                            else
                                                            {
                                                                if (C == 1)
                                                                {
                                                                    Repositorio repo = new Repositorio();
                                                                    Consulta consulta = new Consulta();
                                                                    consulta.origen = "Panecillo, Quito, Ecuador";
                                                                    consulta.destino = "Centro Historico, Quito, Ecuador";
                                                                    var distanciaGoogle = repo.getDistancia(consulta);
                                                                    double ruta = distanciaGoogle.rows[0].elements[0].distance.value;
                                                                    double tiempo = distanciaGoogle.rows[0].elements[0].duration.value + Double.Parse(result);
                                                                    km = ruta;
                                                                    hr = tiempo;
                                                                    distanciaF = distanciaF + ruta;
                                                                    tiempoF = tiempoF + tiempo;
                                                                }
                                                                else
                                                                {
                                                                    if (B == 1)
                                                                    {
                                                                        Repositorio repo = new Repositorio();
                                                                        Consulta consulta = new Consulta();
                                                                        consulta.origen = "Teleférico Quito, Quito, Ecuador";
                                                                        consulta.destino = "Centro Historico, Quito, Ecuador";
                                                                        var distanciaGoogle = repo.getDistancia(consulta);
                                                                        double ruta = distanciaGoogle.rows[0].elements[0].distance.value;
                                                                        double tiempo = distanciaGoogle.rows[0].elements[0].duration.value + Double.Parse(result);
                                                                        km = ruta;
                                                                        hr = tiempo;
                                                                        distanciaF = distanciaF + ruta;
                                                                        tiempoF = tiempoF + tiempo;
                                                                    }
                                                                    else
                                                                    {
                                                                        if (A == 1)
                                                                        {
                                                                            Repositorio repo = new Repositorio();
                                                                            Consulta consulta = new Consulta();
                                                                            consulta.origen = "Manuel Cordova Galarza Km. 13, 5 SN, Quito, Ecuador";
                                                                            consulta.destino = "Centro Historico, Quito, Ecuador";
                                                                            var distanciaGoogle = repo.getDistancia(consulta);
                                                                            double ruta = distanciaGoogle.rows[0].elements[0].distance.value;
                                                                            double tiempo = distanciaGoogle.rows[0].elements[0].duration.value + Double.Parse(result);
                                                                            km = ruta;
                                                                            hr = tiempo;
                                                                            distanciaF = distanciaF + ruta;
                                                                            tiempoF = tiempoF + tiempo;
                                                                        }
                                                                        else
                                                                        {
                                                                            Repositorio repo = new Repositorio();
                                                                            Consulta consulta = new Consulta();
                                                                            consulta.origen = "" + location.Latitude.ToString() + ", " + location.Longitude.ToString() + "";
                                                                            consulta.destino = "Centro Historico, Quito, Ecuador";
                                                                            var distanciaGoogle = repo.getDistancia(consulta);
                                                                            double ruta = distanciaGoogle.rows[0].elements[0].distance.value;
                                                                            double tiempo = distanciaGoogle.rows[0].elements[0].duration.value + Double.Parse(result);
                                                                            km = ruta;
                                                                            hr = tiempo;
                                                                            distanciaF = ruta;
                                                                            tiempoF = tiempo;
                                                                        }
                                                                    }
                                                                }

                                                            }

                                                        }
                                                    }

                                                }
                                            }
                                        }
                                    }

                                }
                            }
                        }
                    }
                }
                puntos = puntos + 1;
                C = 1;
                Lineas lin = new Lineas();
                lin.Producto = t16.Text;
                lin.Tiempo = hr.ToString();
                lin.Kilometros = km.ToString();
                lin.TiempoPunto = result;
                Lineas lineas = await repo.PostLineas(lin);
                Dialogs.ShowLoading("Buena Elección");
                await Task.Delay(2000);
                Dialogs.HideLoading();
                TiempoMedio = ((buses * puntos) + (tiempoF / 3600));
                Lineas.Add(lin);
                TIEMPO = TIEMPO + Double.Parse(result);
                if (TiempoMedio >= 8)
                {
                    Dialogs.ShowLoading("Has logrado el limite de tiempo diario");
                    await Task.Delay(2000);
                    Dialogs.HideLoading();
                    CurrentPage = gaseosas;
                    Children.Remove(MM);
                    Children.Remove(T);
                    Children.Remove(pan);
                    Children.Remove(zoo);
                    Children.Remove(cl);
                    Children.Remove(ma);
                    Children.Remove(maq);
                    Children.Remove(maqui);
                    Children.Remove(pulu);
                    Children.Remove(min);
                    Children.Remove(coto);
                    Children.Remove(bano);
                    Children.Remove(ota);
                    Children.Remove(papa);
                    Children.Remove(quilo);
                }
                else
                {
                    CurrentPage = gaseosas;
                    Children.Remove(CH);
                }
            }
        }
        public void Siguiente_Clicked(object sender, EventArgs e)
        {
            Gaseosas gas = new Gaseosas();
            if (Agua.Text == null)
            {
                Agua.Text = 0.ToString();
            }
            else 
            {
                aguat = Int32.Parse(Agua.Text);
            }
            if (Te.Text == null) 
            {
                Te.Text = 0.ToString();
            }
            else
            {
                fuzet = Int32.Parse(Te.Text);
            }
            if (Normal.Text == null)
            {
                Normal.Text = 0.ToString();
            }
            else
            {
                cocat = Int32.Parse(Normal.Text);
            }
            if (Light.Text == null)
            {
                Light.Text = 0.ToString();
            }
            else
            {
                lightt = Int32.Parse(Light.Text);
            }
            fuzep = fuzet * 1.9;
            aguap = aguat * 0.9;
            cocap = cocat * 1.9;
            lightp = lightt * 0.9;
            gasp = fuzep + aguap + cocap + lightp;
            CurrentPage = musica;
            Children.Remove(gaseosas);







        }

        public async void info1_Clicked(object sender, EventArgs e)
        {
            
            InfoMMPage myHomePage = new InfoMMPage();
            NavigationPage.SetHasNavigationBar(myHomePage, true);
            await Navigation.PushModalAsync(myHomePage);
        }
        public async void info2_Clicked(object sender, EventArgs e)
        {

            infoTPage myHomePage = new infoTPage();
            NavigationPage.SetHasNavigationBar(myHomePage, true);
            await Navigation.PushModalAsync(myHomePage);
        }
        public async void info3_Clicked(object sender, EventArgs e)
        {

            infoPanePage myHomePage = new infoPanePage();
            NavigationPage.SetHasNavigationBar(myHomePage, true);
            await Navigation.PushModalAsync(myHomePage);
        }
        public async void info4_Clicked(object sender, EventArgs e)
        {

            infozooPage myHomePage = new infozooPage();
            NavigationPage.SetHasNavigationBar(myHomePage, true);
            await Navigation.PushModalAsync(myHomePage);
        }
        public async void info5_Clicked(object sender, EventArgs e)
        {

            infocimaPage myHomePage = new infocimaPage();
            NavigationPage.SetHasNavigationBar(myHomePage, true);
            await Navigation.PushModalAsync(myHomePage);
        }
        public async void info6_Clicked(object sender, EventArgs e)
        {

            infomuseoPage myHomePage = new infomuseoPage();
            NavigationPage.SetHasNavigationBar(myHomePage, true);
            await Navigation.PushModalAsync(myHomePage);
        }
        public async void info7_Clicked(object sender, EventArgs e)
        {

            infoma1 myHomePage = new infoma1();
            NavigationPage.SetHasNavigationBar(myHomePage, true);
            await Navigation.PushModalAsync(myHomePage);
        }
        public async void info8_Clicked(object sender, EventArgs e)
        {

            infomindo myHomePage = new infomindo();
            NavigationPage.SetHasNavigationBar(myHomePage, true);
            await Navigation.PushModalAsync(myHomePage);
        }
        public async void info9_Clicked(object sender, EventArgs e)
        {

            infomaqui myHomePage = new infomaqui();
            NavigationPage.SetHasNavigationBar(myHomePage, true);
            await Navigation.PushModalAsync(myHomePage);
        }
        public async void info10_Clicked(object sender, EventArgs e)
        {

            infoquilo myHomePage = new infoquilo();
            NavigationPage.SetHasNavigationBar(myHomePage, true);
            await Navigation.PushModalAsync(myHomePage);
        }
        public async void info11_Clicked(object sender, EventArgs e)
        {

            infocoto myHomePage = new infocoto();
            NavigationPage.SetHasNavigationBar(myHomePage, true);
            await Navigation.PushModalAsync(myHomePage);
        }
        public async void info12_Clicked(object sender, EventArgs e)
        {

            infopulu myHomePage = new infopulu();
            NavigationPage.SetHasNavigationBar(myHomePage, true);
            await Navigation.PushModalAsync(myHomePage);
        }
        public async void info13_Clicked(object sender, EventArgs e)
        {

            infobanos myHomePage = new infobanos();
            NavigationPage.SetHasNavigationBar(myHomePage, true);
            await Navigation.PushModalAsync(myHomePage);
        }
        public async void info14_Clicked(object sender, EventArgs e)
        {

            infootavalos myHomePage = new infootavalos();
            NavigationPage.SetHasNavigationBar(myHomePage, true);
            await Navigation.PushModalAsync(myHomePage);
        }
        public async void info15_Clicked(object sender, EventArgs e)
        {

            infopapa myHomePage = new infopapa();
            NavigationPage.SetHasNavigationBar(myHomePage, true);
            await Navigation.PushModalAsync(myHomePage);
        }
        public async void info16_Clicked(object sender, EventArgs e)
        {

            InfoCH myHomePage = new InfoCH();
            NavigationPage.SetHasNavigationBar(myHomePage, true);
            await Navigation.PushModalAsync(myHomePage);
        }
        public void X_Clicked(object sender, EventArgs e)
        {
            CurrentPage = T;
            Children.Remove(MM);
        }
        public void X1_Clicked(object sender, EventArgs e)
        {
            CurrentPage = pan;
            Children.Remove(T);
        }
        public void X2_Clicked(object sender, EventArgs e)
        {
            CurrentPage = zoo;
            Children.Remove(pan);
        }
        public void X3_Clicked(object sender, EventArgs e)
        {
            CurrentPage = cl;
            Children.Remove(zoo);
        }
        public void X4_Clicked(object sender, EventArgs e)
        {
            CurrentPage = ma;
            Children.Remove(cl);
        }
        public void X5_Clicked(object sender, EventArgs e)
        {
            CurrentPage = maq;
            Children.Remove(ma);
        }
        public void X6_Clicked(object sender, EventArgs e)
        {
            CurrentPage = min;
            Children.Remove(maq);
        }
        public void X7_Clicked(object sender, EventArgs e)
        {
            CurrentPage = maqui;
            Children.Remove(min);
        }
        public void X8_Clicked(object sender, EventArgs e)
        {
            CurrentPage = quilo;
            Children.Remove(maqui);
        }
        public void X9_Clicked(object sender, EventArgs e)
        {
            CurrentPage = coto;
            Children.Remove(quilo);
        }
        public void X10_Clicked(object sender, EventArgs e)
        {
            CurrentPage = pulu;
            Children.Remove(coto);
        }
        public void X11_Clicked(object sender, EventArgs e)
        {
            CurrentPage = bano;
            Children.Remove(pulu);
        }
        public void X12_Clicked(object sender, EventArgs e)
        {
            CurrentPage = ota;
            Children.Remove(bano);
        }
        public void X13_Clicked(object sender, EventArgs e)
        {
            CurrentPage = papa;
            Children.Remove(ota);
        }
        public void X14_Clicked(object sender, EventArgs e)
        {
            CurrentPage = CH;
            Children.Remove(papa);
        }
        public void X15_Clicked(object sender, EventArgs e)
        {
            CurrentPage = gaseosas;
            Children.Remove(CH);
        }
        public async void Carrito_Clicked(object sender, EventArgs e)
        {

            Calculos calculos = new Calculos();
            calculos.personas = personas;
            calculos.buses = Int32.Parse(TIEMPO.ToString());
            calculos.carros = carros;
            calculos.distancia = distanciaF / 1000;
            calculos.puntos = puntos;
            tiempoF = (TIEMPO + tiempoF) / 3600;
            calculos.monto = ((distanciaF * 0.36) + (tiempoF * 0.08) + gasp)/1000;
            calculos.tiempo = tiempoF.ToString("N2");
            calculos.gaseosas = aguat + cocat + lightt + fuzet;
            Dialogs.ShowLoading("Calculando Ruta");
            await Task.Delay(2000);
            Dialogs.HideLoading();
            CarritoPage myHomePage = new CarritoPage(calculos);
            NavigationPage.SetHasNavigationBar(myHomePage, true);
            await Navigation.PushModalAsync(myHomePage);

        }
        public async void CarritoDOS_Clicked(object sender, EventArgs e)
        {
            Calculos calculos = new Calculos();
            calculos.personas = personas;
            calculos.buses = Int32.Parse(TIEMPO.ToString());
            calculos.carros = carros;
            calculos.distancia = distanciaF/1000;
            calculos.puntos = puntos;
            tiempoF = (TIEMPO + tiempoF)/3600;
            calculos.monto = ((distanciaF * 0.36) + (tiempoF * 0.08) + gasp)/1000;
            calculos.tiempo = tiempoF.ToString("N2");
            calculos.gaseosas = aguat + cocat + lightt + fuzet;
            Dialogs.ShowLoading("Calculando Ruta");
            await Task.Delay(2000);
            Dialogs.HideLoading();
            CarritoPage myHomePage = new CarritoPage(calculos);
            NavigationPage.SetHasNavigationBar(myHomePage, true);
            await Navigation.PushModalAsync(myHomePage);
            Children.Remove(musica);
        }
        public async void Usuario_Clicked(object sender, EventArgs e)
        {
            IUserDialogs Dialogs = UserDialogs.Instance;
            
            UsuarioRequest userrecibo = new UsuarioRequest();
            userrecibo.id = await SecureStorage.GetAsync("id");
            userrecibo.Nombre = await SecureStorage.GetAsync("nombre");
            userrecibo.Email = await SecureStorage.GetAsync("email");            
            fechaconver = await SecureStorage.GetAsync("fecha");
            userrecibo.FechaRegistro = DateTime.Parse(fechaconver);
            userrecibo.Telefono = await SecureStorage.GetAsync("telefono");                       
            Dialogs.ShowLoading("Cargando Informacion de Usuario");
            await Task.Delay(2000);
            Dialogs.HideLoading();
            UsuarioPage myHomePage = new UsuarioPage(userrecibo);
            NavigationPage.SetHasNavigationBar(myHomePage, true);
            await Navigation.PushModalAsync(myHomePage);
        }
        private ObservableCollection<Lineas> _linea;
        public ObservableCollection<Lineas> Lineas
        {
            get
            {
                return _linea ?? (_linea = new ObservableCollection<Lineas>());
            }
        }
        public async void Cancelar_Clicked(object sender, EventArgs e)
        {
            MapPage myHomePage = new MapPage();
            NavigationPage.SetHasNavigationBar(myHomePage, false);
            await Navigation.PushModalAsync(myHomePage);
        }
        public void Ruta_Clicked(object sender, EventArgs e)
        {
            CurrentPage = Ruta;
        }

    }
}