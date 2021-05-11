using Newtonsoft.Json;
using System;
using utravel.Vistas;
using System.Net.Http;
using System.Text;
using System.Diagnostics;
using System.Threading.Tasks;
using utravel.Modelos;
using Xamarin.Essentials;
using System.Net.Http.Headers;
using System.Web;

namespace utravel.Servicios
{
    
    public class Repositorio
        
    {                
        public async Task<ClasesRecibo> ConnectUser(Login userlogin)
        {
            Login logintest = new Login();
            logintest.User = userlogin.User;
            logintest.Pass = userlogin.Pass;          
            var jsonObj = JsonConvert.SerializeObject(logintest);
            using (HttpClient client = new HttpClient())
            {
                StringContent content = new StringContent(jsonObj.ToString(), Encoding.UTF8, "application/json");
                var request = new HttpRequestMessage()
                {
                    RequestUri = new Uri("https://utravel.somee.com/api/api/Login"),
                    Method = HttpMethod.Post,
                    Content = content
                };             
                var response = await client.SendAsync(request).ConfigureAwait(false);
                string dataResult = response.Content.ReadAsStringAsync().Result;
                ClasesRecibo result = JsonConvert.DeserializeObject<ClasesRecibo>(dataResult);
                return result;
            }
        }
        public async Task<ClasesRecibo> postUserCreate(UsuarioRequest usuario)
        {            
            UsuarioRequest logintest = new UsuarioRequest();
            logintest.Nombre = usuario.Nombre;            
            logintest.Pass = usuario.Pass;            
            logintest.Telefono = usuario.Telefono;
            logintest.Email = usuario.Email;
            logintest.FechaRegistro = DateTime.Now;
           
            var jsonObj = JsonConvert.SerializeObject(logintest);
            using (HttpClient client = new HttpClient())
            {
                                                
                StringContent content = new StringContent(jsonObj.ToString(), Encoding.UTF8, "application/json");                                
                var request = new HttpRequestMessage()                
                {                    
                    RequestUri = new Uri("https://utravel.somee.com/api/api/Usuarios"),
                    Method = HttpMethod.Post,
                    Content = content                    
                };                             
                var response = await client.SendAsync(request).ConfigureAwait(false);
                string dataResult = response.Content.ReadAsStringAsync().Result;
                ClasesRecibo result = JsonConvert.DeserializeObject<ClasesRecibo>(dataResult);
                return result;
            }
        }
        public async Task<CabeceraPreCompra> OrderCreate(Calculos calculos)
        {
            CabeceraPreCompra orden = new CabeceraPreCompra();
            var ID = await SecureStorage.GetAsync("id");
            orden.IDUser = Int32.Parse(ID);
            orden.Personas = calculos.personas;
            orden.Carros = calculos.carros;            
            orden.FechaViaje = calculos.date;
            orden.HoraViaje = calculos.time;
            orden.FechaCreacion = DateTime.Now;
            orden.UbicacionCliente = calculos.UbicacionCliente;
            orden.TiempoPunto = 1;
            var jsonObj = JsonConvert.SerializeObject(orden);
            using (HttpClient client = new HttpClient())
            {

                StringContent content = new StringContent(jsonObj.ToString(), Encoding.UTF8, "application/json");
                var request = new HttpRequestMessage()
                {
                    RequestUri = new Uri("https://utravel.somee.com/api/api/CabeceraPreCompras"),
                    Method = HttpMethod.Post,
                    Content = content
                };
                var response = await client.SendAsync(request).ConfigureAwait(false);
                string dataResult = response.Content.ReadAsStringAsync().Result;
                CabeceraPreCompra result = JsonConvert.DeserializeObject<CabeceraPreCompra>(dataResult);
                return result;
            }
        }
        public async Task<ClasesRecibo> OrderUpdate(CabeceraActualizacion cabeceraActualizacion)
        {
            CabeceraActualizacion act = new CabeceraActualizacion();

            act.CantidadPuntos = cabeceraActualizacion.CantidadPuntos;
            act.MontoTotal = cabeceraActualizacion.MontoTotal;
            act.IDorden = cabeceraActualizacion.IDorden;
            act.TiempoFinal = cabeceraActualizacion.TiempoFinal;
            act.TotalKilometros = cabeceraActualizacion.TotalKilometros;
            var jsonObj = JsonConvert.SerializeObject(act);
            using (HttpClient client = new HttpClient())
            {

                StringContent content = new StringContent(jsonObj.ToString(), Encoding.UTF8, "application/json");
                var request = new HttpRequestMessage()
                {
                    RequestUri = new Uri("https://utravel.somee.com/api/api/CabeceraActualizacion"),
                    Method = HttpMethod.Post,
                    Content = content
                };
                var response = await client.SendAsync(request).ConfigureAwait(false);
                string dataResult = response.Content.ReadAsStringAsync().Result;
                ClasesRecibo result = JsonConvert.DeserializeObject<ClasesRecibo>(dataResult);
                return result;
            }
        }
        public DistanciaGoogle getDistancia(Consulta consulta)
        {
            string origen = consulta.origen;
            string destino = consulta.destino;
            string llave = "AIzaSyB1rQTblrFX6uGxyAdnP5hYyI6QnZmk6fU";
            string url = "https://maps.googleapis.com/maps/api/distancematrix/json?units=imperial&origins=" +HttpUtility.UrlEncode(origen) + "&destinations=" + HttpUtility.UrlEncode(destino) + "&key=" + HttpUtility.UrlEncode(llave);
            try
            {
                DistanciaGoogle distanciaGoogle;
                var URLWebAPI = url;
                using (var Client = new System.Net.Http.HttpClient())
                {
                    var JSON = Client.GetStringAsync(URLWebAPI);
                    distanciaGoogle = Newtonsoft.Json.JsonConvert.DeserializeObject<DistanciaGoogle>(JSON.Result);
                }

                return distanciaGoogle;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<Lineas> PostLineas(Lineas lineas)
        {
            Lineas act = new Lineas();
            var id = await SecureStorage.GetAsync("idorden");
            act.IDorden = Int16.Parse(id);
            act.Producto = lineas.Producto;
            act.Tiempo = lineas.Tiempo;
            act.Kilometros = lineas.Kilometros;
            act.TiempoPunto = lineas.TiempoPunto;
            var jsonObj = JsonConvert.SerializeObject(act);
            using (HttpClient client = new HttpClient())
            {

                StringContent content = new StringContent(jsonObj.ToString(), Encoding.UTF8, "application/json");
                var request = new HttpRequestMessage()
                {
                    RequestUri = new Uri("https://utravel.somee.com/api/api/LineasPreCompras"),
                    Method = HttpMethod.Post,
                    Content = content
                };
                var response = await client.SendAsync(request).ConfigureAwait(false);
                string dataResult = response.Content.ReadAsStringAsync().Result;
                Lineas result = JsonConvert.DeserializeObject<Lineas>(dataResult);
                return result;
            }
        }





    }
}
