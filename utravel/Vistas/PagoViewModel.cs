using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace utravel.Vistas
{
    public class PagoViewModel : BindableObject
    {
        private Func<string, Task<string>> _evaluateJavascript;
        public Func<string, Task<string>> EvaluateJavascript
        {
            get { return _evaluateJavascript; }
            set { _evaluateJavascript = value; }
        }

    }
}
