using IF.Core.Data;
using System.Linq;
using System.Web.Mvc;

namespace Derin.Core.Mvc.Filters
{
    public static class KeepDataHelperSession
    {
        public static T GetTempDataPosRequest<T>(TempDataDictionary TempData) where T : KeepData
        {
            T request = default(T);

            for (int i = 0; i < TempData.Count; i++)
            {
                if (typeof(T).IsAssignableFrom(TempData.ElementAt(i).Value.GetType()))
                {
                    request = TempData.ElementAt(i).Value as T;
                    break;
                }
            }

            return request;
        }
    }
}
