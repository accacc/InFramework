using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Derin.Core.Mvc.HttpModule
{
    public class PageRenderTimeModule : IHttpModule
    {
        public void Dispose() { }

        public void Init(HttpApplication context)
        {
            context.PreRequestHandlerExecute += delegate (object sender, EventArgs e)
            {
                HttpContext requestContext = ((HttpApplication)sender).Context;
                Stopwatch timer = new Stopwatch();
                requestContext.Items["Timer"] = timer;
                timer.Start();
            };
            context.PostRequestHandlerExecute += delegate (object sender, EventArgs e)
            {
                HttpContext requestContext = ((HttpApplication)sender).Context;
                HttpResponse response = requestContext.Response;
                Stopwatch timer = (Stopwatch)requestContext.Items["Timer"];

                timer.Stop();

                if (requestContext.Response.ContentType == "text/html")
                {
                    double seconds = (double)timer.ElapsedTicks / Stopwatch.Frequency;
                    string resultTime = string.Format("{0:F4} sec ", seconds);

                    HttpCookie cookie = new HttpCookie("RenderTime");
                    cookie.Value = resultTime;
                    response.AppendCookie(cookie);


                }
            };
        }

    }
}
