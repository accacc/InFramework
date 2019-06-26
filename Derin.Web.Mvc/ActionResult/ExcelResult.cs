using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Derin.Core.Mvc.ActionResults
{
    public class ExcelResult : DocumentResult
    {

        public ExcelResult(string viewName, object model, string filename)
            : base(viewName, model, filename)
        {

        }

        public override void ExecuteResult(ControllerContext context)
        {
            base.ExecuteResult(context);
        }

        protected override void WriteToFile(object content)
        {
            HttpContext context = HttpContext.Current;
            context.Response.ContentEncoding = Encoding.UTF8;
            context.Response.Clear();
            context.Response.HeaderEncoding = Encoding.UTF8;
            context.Response.AddHeader("content-disposition", "attachment;filename=\"" + base.filename + "\"");
            context.Response.Charset = "";
            context.Response.ContentType = "application/ms-excel";
            context.Response.Write((string)content);
            context.Response.End();
        }

        protected override object GetContent(string content)
        {
            return content;

        }
    }
}
