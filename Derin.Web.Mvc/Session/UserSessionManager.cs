using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Derin.Core.Mvc.Session
{
    public static class UserSessionManager
    {

        private const string KEY = "CurrentUser";

        public static T CurrentGeneric<T>() where T : UserSessionContext
        {
            var user = HttpContext.Current.Session[KEY] as T;
            return user;
        }

        public static UserSessionContext Current
        {
            get
            {
                var user = HttpContext.Current.Session[KEY] as UserSessionContext;
                return user;
            }
        }


        private static void Set(object value)
        {
            if (value == null)
            {
                HttpContext.Current.Session.Remove(KEY);
            }
            else
            {
                HttpContext.Current.Session[KEY] = value;
            }
        }

        public static void Build<T>(Action<T> action) where T : UserSessionContext, new()
        {
            T container = new T();
            action(container);            
            Set(container);
        }

        public static void Dispose()
        {
            Set(null);

            //Prevent Session HiJacking
            //if (Request.Cookies["AuthToken"] != null)
            //{
            //    var c = new HttpCookie("AuthToken");
            //    c.Expires = DateTime.Now.AddDays(-1);
            //    Response.Cookies.Add(c);
            //}

            HttpContext.Current.Session.Clear();
            HttpContext.Current.Session.Abandon();
            HttpContext.Current.Session.RemoveAll();

            //if (Request.Cookies["ASP.NET_SessionId"] != null)
            //{
            //    Response.Cookies["ASP.NET_SessionId"].Value = string.Empty;
            //    Response.Cookies["ASP.NET_SessionId"].Expires = DateTime.Now.AddDays(-1);
            //}
        }


    }
}
