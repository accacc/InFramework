﻿using IF.Core.Data;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System.Linq;

namespace IF.Web.Mvc.Filters
{
    public static class KeepDataHelperTempData
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
