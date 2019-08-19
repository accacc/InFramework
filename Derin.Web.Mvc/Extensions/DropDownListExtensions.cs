using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Text;
using System.Globalization;
using System.Text.RegularExpressions;

namespace Derin.Core.Mvc.Extensions
{
    public static class DropDownListExtensions
    {
        public static MvcHtmlString DropDownListMonth(this HtmlHelper helper, string id, int? selectedValue = null, bool hasSelectAll = false)
        {
            if (!selectedValue.HasValue && !hasSelectAll) selectedValue = DateTime.Now.Month;

            List<SelectListItem> items = new List<SelectListItem>();

            for (int i = 0; i < DateTimeFormatInfo
                .CurrentInfo
                .MonthNames.Length - 1; i++)
            {
                var item = new SelectListItem();
                item.Text = DateTimeFormatInfo.CurrentInfo.MonthNames[i];
                item.Value = (i + 1).ToString();
                item.Selected = (selectedValue.ToString() == item.Value);
                items.Add(item);
            }

            if (hasSelectAll)
                items.Insert(0, new SelectListItem { Value = "", Text = "All", Selected = true });

            var result = helper.DropDownList(id, items, new { @class = "select2_category form-control" }).ToHtmlString();
            return new MvcHtmlString(result);
        }

        public static MvcHtmlString DropDownListIcons(this HtmlHelper helper, string id, string selectedValue = null)
        {
            if (String.IsNullOrWhiteSpace(selectedValue)) selectedValue = Icons.Icon.icons.First();

            List<SelectListItem> items = new List<SelectListItem>();

            foreach (var icon in Icons.Icon.icons)

            {
                var item = new SelectListItem();
                item.Text = MvcHtmlString.Create(@"<i class=""fa fas-"">" + icon + "</i>").ToHtmlString();
                item.Value = icon;
                item.Selected = (selectedValue.ToString() == item.Value);
                items.Add(item);
            }

            var result = helper.DropDownList(id, items, new { @class = "select2_category form-control" }).ToHtmlString();
            return new MvcHtmlString(result);
        }


        public static MvcHtmlString DropDownListYear(this HtmlHelper helper, string id, int? selectedValue = null, bool hasSelectAll = false)
        {
            if (!selectedValue.HasValue && !hasSelectAll) selectedValue = DateTime.Now.Year;

            List<SelectListItem> items = new List<SelectListItem>();

            for (int i = 2010; i < 2051; i++)
            {
                items.Add(new SelectListItem
                {
                    Text = i.ToString(),
                    Value = i.ToString(),
                    Selected = (selectedValue.ToString() == i.ToString())
                });
            }

            if (hasSelectAll)
                items.Insert(0, new SelectListItem { Value = "", Text = "All", Selected = true });

            var result = helper.DropDownList(id, items, new { @class = "select2_category form-control" }).ToHtmlString();
            return new MvcHtmlString(result);
        }

      
        

        public static List<SelectListItem> GetSelectListFromEnum<TEnum>(Type resourceType, TEnum selectedValue)
        {
            bool isEnum =IF.Core.Enum.EnumHelper.IsEnum<TEnum>();

            if (!isEnum)
                throw new ArgumentException("TEnum must be an enumerated type");

            var enums = IF.Core.Enum.EnumHelper.GetEnumValues<TEnum>(resourceType);

            List<SelectListItem> items = new List<SelectListItem>();

            foreach (var @enum in enums)
            {
                var item = new SelectListItem();

                item.Text = @enum.Value;
                item.Value = System.Convert.ToUInt64(@enum.Key).ToString();
                item.Selected = String.IsNullOrWhiteSpace(selectedValue.ToString()) ? false : (@enum.Key.ToString().Equals(selectedValue.ToString()));
                items.Add(item);
            }
            return items;
        }
    }
}
