using Microsoft.AspNetCore.Mvc.Razor;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Encodings.Web;

namespace IF.Web.Mvc.FluentHtml.Extension
{
    public static class TextWriterExtensions
    {
        public static void WriteContent<T>(this StringWriter writer, Func<T, object> action, HtmlEncoder htmlEncoder, T dataItem = null, bool htmlEncode = false)
        where T : class
        {
            object obj = action(dataItem);
            HelperResult helperResult = obj as HelperResult;
            if (helperResult != null)
            {
                helperResult.WriteTo(writer, htmlEncoder);
                return;
            }
            if (obj != null)
            {
                if (htmlEncode)
                {
                    writer.Write(htmlEncoder.Encode(obj.ToString()));
                    return;
                }
                writer.Write(obj.ToString());
            }
        }
    }
}
