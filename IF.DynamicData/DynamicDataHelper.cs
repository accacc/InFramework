using Newtonsoft.Json;
using System.Collections.Specialized;

namespace IF.DynamicData
{
    public static class DynamicDataHelper
    {
        public static T Parse<T>(NameValueCollection queryString) where T : DynamicDataRequest, new()
        {
            return new T
            {
                Take = queryString.GetQueryValue("take", (int?)null).Value,
                //Page = queryString.GetQueryValue("page", (int?)null),
                Skip = queryString.GetQueryValue("skip", (int?)null).Value,
                //PageSize = queryString.GetQueryValue("pageSize", (int?)null),

                FilterObjectWrapper = FilterHelper.Parse(queryString),
                //GroupObjects = GroupHelper.Parse(queryString),
                //AggregateObjects = AggregateHelper.Parse(queryString),
                SortObjects = SortHelper.Parse(queryString)
            };
        }

        public static DynamicDataRequest Parse(string jsonRequest)
        {
            var kendoJsonRequest = JsonConvert.DeserializeObject<DynamicData>(jsonRequest);

            return new DynamicDataRequest
            {
                Take = kendoJsonRequest.Take.Value,
                //Page = kendoJsonRequest.Page,
                //PageSize = kendoJsonRequest.PageSize,
                Skip = kendoJsonRequest.Skip.Value,
                //Logic = kendoJsonRequest.Logic,
                //GroupObjects = GroupHelper.Map(kendoJsonRequest.Groups),
                //AggregateObjects = AggregateHelper.Map(kendoJsonRequest.AggregateObjects),
                FilterObjectWrapper = FilterHelper.MapRootFilter(kendoJsonRequest.Filter),
                SortObjects = SortHelper.Map(kendoJsonRequest.Sort)
            };
        }
    }

}