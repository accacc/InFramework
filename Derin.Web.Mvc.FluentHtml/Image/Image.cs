using System;
using System.Web.Mvc;
using System.Web.Routing;

namespace IF.Web.Mvc.FluentHtml.Image
{
    internal class Image
    {
        internal static MvcHtmlString ImageByte(HtmlHelper helper, byte[] image,string noImgPath, object htmlAttributes = null)
        {
            TagBuilder builder = new TagBuilder("img");
            string img;
            if (image != null && image.Length > 0)
            {
                img = String.Format("data:image/jpg;base64,{0}", System.Convert.ToBase64String(image));
            }
            else
            {
                img = noImgPath;
            }
            
            builder.Attributes.Add("src", img);

            builder.MergeAttributes(new RouteValueDictionary(htmlAttributes));
            return MvcHtmlString.Create(builder.ToString(TagRenderMode.Normal));
        }
    }
}
