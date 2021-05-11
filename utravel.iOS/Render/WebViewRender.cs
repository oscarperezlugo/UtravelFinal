using Foundation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UIKit;
using utravel.iOS.Render;
using utravel.Vistas;
using Xamarin.Forms;

[assembly: ExportRenderer(typeof(WebViewer), typeof(WebViewRender))]
namespace utravel.iOS.Render
{
    using System.Threading.Tasks;
    using UIKit;
    using Xamarin.Forms.Platform.iOS;

    public class WebViewRender : WkWebViewRenderer
    {
        protected override void OnElementChanged(VisualElementChangedEventArgs e)
        {
            base.OnElementChanged(e);

            var webView = e.NewElement as WebViewer;
            if (webView != null)
                webView.EvaluateJavascript = (js) =>
                {
                    return Task.FromResult(webView.EvaluateJavascript(js).Result);
                };
        }
    }
}