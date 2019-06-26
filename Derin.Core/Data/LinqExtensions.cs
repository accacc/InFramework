using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Derin.Core.Data
{

    public class Order
    {
        /// <summary>
        /// Sorting column
        /// </summary>
        public string OrderBy { get; set; }

        /// <summary>
        /// Sorting direction
        /// </summary>
        public bool IsDescending { get; set; }

        /// <summary>
        /// Priority of the orders
        /// </summary>
        public int Priority { get; set; }
    }
    public static class LinqExtensions
    {
        /// <summary>
        /// Sort
        /// </summary>
        /// <param name="collection"></param>
        /// <param name="orders"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IQueryable<T> Sort<T>(this IQueryable<T> collection, List<Order> orders)
        {
            return collection.OrderBy(orders.OrderBy(i => i.Priority).Select(i => i.OrderBy + (i.IsDescending ? " descending" : "")).Aggregate((a, b) => a + ',' + b));
        }

        /// <summary>
        /// Sort
        /// </summary>
        /// <param name="collection"></param>
        /// <param name="sortBy"></param>
        /// <param name="reverse"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IQueryable<T> Sort<T>(this IQueryable<T> collection, string sortBy, bool? reverse)
        {
            return collection.OrderBy(sortBy + (reverse ?? false ? " descending" : ""));
        }

        /// <summary>
        /// Filter
        /// </summary>
        /// <param name="collection"></param>
        /// <param name="filterObject"></param>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TU"></typeparam>
        /// <returns></returns>
        public static IQueryable<T> Filter<T, TU>(this IQueryable<T> collection, TU filterObject) where TU : class
        {
            if (filterObject == null)
            {
                return collection;
            }

            PropertyInfo[] properties = filterObject.GetType().GetProperties();
            var queryList = new List<string>();
            var paramList = new List<dynamic>();
            var i = 0;

            foreach (var property in properties)
            {
                var value = property.GetValue(filterObject);
                if (value != null)
                {
                    Type t = Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType;
                    queryList.Add(t == typeof(string)
                        ? string.Format("{0}.Contains(@{1})", property.Name, i++)
                        : string.Format("{0} == @{1}", property.Name, i++));

                    dynamic safeValue = System.Convert.ChangeType(value, t);
                    paramList.Add(safeValue);
                }
            }

            if (queryList.Any())
            {
                var whereQuery = string.Join(" and ", queryList);
                return collection.Where(whereQuery, paramList.ToArray());
            }

            return collection;
        }
    }
}
