using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;
using utravel.Droid.Render;
using utravel.Vistas;
using Xamarin.Forms.Platform.Android;
using System.Threading;
using Android.Webkit;
using System.Threading.Tasks;

[assembly: ExportRenderer(typeof(WebViewer), typeof(WebViewRender))]
namespace utravel.Droid.Render
{
    using Android.OS;
    using System;
    using Xamarin.Forms.Platform.Android;
    using Android.Content;
    using Android.Webkit;
    using System.Threading;
    using System.Threading.Tasks;

    public class WebViewRender : WebViewRenderer
    {
        public WebViewRender(Context context) : base(context)
        {
            AutoPackage = false;
        }
        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.WebView> e)
        {
            
            base.OnElementChanged(e);

            var webView = e.NewElement as WebViewer;
            if (webView != null)
                webView.EvaluateJavascript = async (js) =>
                {
                    var reset = new ManualResetEvent(false);
                    var response = string.Empty;
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        Control?.EvaluateJavascript(js, new JavascriptCallback((r) => { response = r; reset.Set(); }));
                    });
                    await Task.Run(() => { reset.WaitOne(); });
                    return response;
                };
        }
    }

    internal class JavascriptCallback : Java.Lang.Object, IValueCallback
    {
        public JavascriptCallback(Action<string> callback)
        {
            _callback = callback;
        }

        private Action<string> _callback;
        public void OnReceiveValue(Java.Lang.Object value)
        {
            _callback?.Invoke(Convert.ToString(value));
        }
    }

}