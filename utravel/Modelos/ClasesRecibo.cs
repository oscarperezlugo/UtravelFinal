using System;
using System.Collections.Generic;
using System.Text;

namespace utravel.Modelos
{
    public class ClasesRecibo
    {
        public string Message { get; set; }
        public string Nombre { get; set; }
        public string message { get; set; }
        public string id { get; set; }
        public string IDUser { get; set; }
        public string Token { get; set; }
        public string idorden { get; set; }        
        public string Email { get; set; }
        public string User { get; set; }
        public string Telefono { get; set; }
        public string FechaRegistro { get; set; }
        public string Pais { get; set; }
        public string nombre { get; set; }
        public string email { get; set; }
        public string fecha { get; set; }
        public string telefono { get; set; }
    }
    public class CabeceraActualizacion
    {
        public int IDorden { get; set; }
        public Nullable<int> CantidadPuntos { get; set; }
        public Nullable<double> MontoTotal { get; set; }
        public Nullable<double> TiempoFinal { get; set; }
        public Nullable<double> TotalKilometros { get; set; }
    }
    public class CreacionCabecera
    {
        public System.DateTime FechaViaje { get; set; }
        public int IDUser { get; set; }
        public Nullable<int> Carros { get; set; }
        public Nullable<int> Personas { get; set; }
        public string HoraViaje { get; set; }
    }
    public class Lineas
    {
        public Nullable<int> IDorden { get; set; }
        public string Producto { get; set; }
        public string Tiempo { get; set; }
        public string Kilometros { get; set; }
        public int Row { get; set; }
        public string TiempoPunto { get; set; }
    }
    public class Login
    {
        public string User { get; set; }
        public string Pass { get; set; }
    }
    public class UsuarioRequest
    {
        public string id { get; set; }
        public string Nombre { get; set; }
        public string Email { get; set; }
        public string User { get; set; }
        public string Telefono { get; set; }
        public string Pais { get; set; }
        public string Pass { get; set; }
        public System.DateTime FechaRegistro { get; set; }
    }
    public partial class CabeceraPreCompra
    {
        public int IDorden { get; set; }
        public System.DateTime FechaCreacion { get; set; }
        public System.DateTime FechaViaje { get; set; }
        public int IDUser { get; set; }
        public Nullable<int> Carros { get; set; }
        public Nullable<int> Personas { get; set; }
        public string HoraViaje { get; set; }
        public Nullable<int> CantidadPuntos { get; set; }
        public Nullable<double> MontoTotal { get; set; }
        public Nullable<double> TiempoPunto { get; set; }
        
        public string UbicacionCliente { get; set; }
    }
    public partial class LineasPreCompra
    {
        public Nullable<int> IDorden { get; set; }
        public int IDproducto { get; set; }
        public Nullable<int> Cantidad { get; set; }
        public int ID { get; set; }
    }
    public partial class Usuario
    {
        public int IDUser { get; set; }
        public string Nombre { get; set; }
        public string Email { get; set; }
        public string User { get; set; }
        public string Telefono { get; set; }
        public System.DateTime FechaRegistro { get; set; }
        public string Pais { get; set; }
        public string Pass { get; set; }
    }
    public class Distance
    {
        public string text { get; set; }
        public int value { get; set; }
    }

    public class Duration
    {
        public string text { get; set; }
        public int value { get; set; }
    }

    public class Element
    {
        public Distance distance { get; set; }
        public Duration duration { get; set; }
        public string status { get; set; }
    }

    public class Row
    {
        public List<Element> elements { get; set; }
    }

    public class DistanciaGoogle
    {
        public List<string> destination_addresses { get; set; }
        public List<string> origin_addresses { get; set; }
        public List<Row> rows { get; set; }
        public string status { get; set; }
    }
    public class Consulta 
    {
        public string origen { get; set; }
        public string destino { get; set; }
    }
    public class Pasarela
    {
        public string Monto { get; set; }
        
    }
    public class Url
    {
        public string Direc { get; set; }

    }
}
