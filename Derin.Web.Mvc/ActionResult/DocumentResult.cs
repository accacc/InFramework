using System.IO;
using System.Text;
using System.Web.Mvc;

namespace Derin.Core.Mvc.ActionResults
{
    public abstract class DocumentResult : PartialViewResult
    {
        protected string filename;

        public DocumentResult(string viewName, object model, string filename)
        {
            base.ViewName = viewName;
            base.ViewData.Model = model;
            this.filename = filename;
        }

        protected abstract object GetContent(string content);
        protected abstract void  WriteToFile(object content);

        public override void ExecuteResult(ControllerContext context)
        {
            StringBuilder builder = new StringBuilder();
            StringWriter writer = new StringWriter(builder);

            ViewEngineResult result = null;

            if (View == null)
            {
                result = base.FindView(context);
                base.View = result.View;
            }

            ViewContext viewContext = new ViewContext(context, View, ViewData, TempData, writer);

            base.View.Render(viewContext, writer);
            var content = this.GetContent(builder.ToString());
            this.WriteToFile(content);

            if (result != null)
                result.ViewEngine.ReleaseView(context, View);

        }
    }
}
