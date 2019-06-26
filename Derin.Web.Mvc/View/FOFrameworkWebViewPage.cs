using IF.Core.Configuration;
using System.Web.Mvc;

namespace Derin.Core.Mvc.View
{
    public abstract class IFWebViewPage<TModel> : WebViewPage<TModel>
    {
        
        public IAppSettings AppSettings;

        public IFWebViewPage()
        {
        }
        

        public string LayoutPath { get { return AppSettings.LayoutPath; } }

        public string PageLayoutPath { get { return AppSettings.PageLayoutPath; } }

        public string DialogFormLayout { get { return AppSettings.DialogFormLayout; } }

        
        public string EmptyLayoutPath { get { return AppSettings.EmptyLayoutPath; } }

        public string GridLayoutPath { get { return AppSettings.GridLayoutPath; } }

        public string MenuPath { get { return AppSettings.MenuPath; } }




    }
}
