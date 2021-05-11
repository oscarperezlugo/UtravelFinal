using System;
using System.Collections.Generic;
using System.Text;

namespace utravel.Modelos
{
    public class Gaseosas
    {
        public string light { get; set; }
        public string agua { get; set; }
        public string fuze { get; set; }
        public string normal { get; set; }
    }
    public class DatosOrden
    {
        public string customer_id { get; set; }
        public string date_created { get; set; }
        public string date_created_gmt { get; set; }
        public string net_total { get; set; }
        public string num_items_sold { get; set; }
        public string order_id { get; set; }
        public string parent_id { get; set; }
        public string returning_customer { get; set; }
        public string shipping_total { get; set; }
        public string status { get; set; }
        public string tax_total { get; set; }
        public string total_sales { get; set; }
    }
}
