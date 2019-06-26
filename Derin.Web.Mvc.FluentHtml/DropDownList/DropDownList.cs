using IF.Web.Mvc.FluentHtml.Base;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace IF.Web.Mvc.FluentHtml.DropDownList
{
    public class DropDownList:HtmlFormElement
    {


        public bool allowMultiple { get; set; }
        public IEnumerable<SelectListItem> selectList { get; set; }

        public string optionLabel { get; set; }
        public DropDownList(HtmlHelper html,string name, IEnumerable<SelectListItem> selectList, bool allowMultiple,ModelMetadata metaData):base(html,name,metaData)
            
        {
            this.MetaData = metaData;
            this.Name = Name;
            this.selectList = selectList;
        }
        public override void Build()
        {
            base.Build();

            string fullName = htmlHelper.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(Name);
            
            if (String.IsNullOrEmpty(fullName))
            {
                throw new ArgumentException("name");
            }

            bool usedViewData = false;

            
            if (selectList == null)
            {
                selectList = htmlHelper.GetSelectData(Name);
                usedViewData = true;
            }

            object defaultValue = (allowMultiple) ? htmlHelper.GetModelStateValue(fullName, typeof(string[])) : htmlHelper.GetModelStateValue(fullName, typeof(string));

            if (defaultValue == null && !String.IsNullOrEmpty(Name))
            {
                if (!usedViewData)
                {
                    defaultValue = htmlHelper.ViewData.Eval(Name);
                }
                else if (MetaData != null)
                {
                    defaultValue = MetaData.Model;
                }
            }

            if (defaultValue != null)
            {
                selectList = GetSelectListWithDefaultValue(selectList, defaultValue, allowMultiple);
            }

            // Convert each ListItem to an <option> tag and wrap them with <optgroup> if requested.
            StringBuilder listItemBuilder = BuildItems(optionLabel, selectList);



            this.Builder.InnerHtml = listItemBuilder.ToString();

            foreach (var att in this.HtmlAttributes)
            {
                this.Builder.MergeAttribute(att.Key, att.Value.ToString(), true);
            }

            

            this.Builder.MergeAttribute("name", fullName, true /* replaceExisting */);

            if (String.IsNullOrWhiteSpace(Id))
            {
                this.Builder.MergeAttribute("id", fullName, true /* replaceExisting */);
            }

            this.Builder.GenerateId(fullName);
            
            if (allowMultiple)
            {
                this.Builder.MergeAttribute("multiple", "multiple");
            }

            ModelState modelState;

            if (htmlHelper.ViewData.ModelState.TryGetValue(fullName, out modelState))
            {
                if (modelState.Errors.Count > 0)
                {
                    this.Builder.AddCssClass(HtmlHelper.ValidationInputCssClassName);
                }
            }

            this.Builder.MergeAttributes(htmlHelper.GetUnobtrusiveValidationAttributes(Name, MetaData));

        }


        private static StringBuilder BuildItems(string optionLabel, IEnumerable<SelectListItem> selectList)
        {
            StringBuilder listItemBuilder = new StringBuilder();

            // Make optionLabel the first item that gets rendered.
            if (optionLabel != null)
            {
                listItemBuilder.AppendLine(ListItemToOption(new SelectListItem()
                {
                    Text = optionLabel,
                    Value = String.Empty,
                    Selected = false
                }));
            }

            // Group items in the SelectList if requested.
            // Treat each item with Group == null as a member of a unique group
            // so they are added according to the original order.
            IEnumerable<IGrouping<int, SelectListItem>> groupedSelectList = selectList.GroupBy<SelectListItem, int>(
                i => (i.Group == null) ? i.GetHashCode() : i.Group.GetHashCode());
            foreach (IGrouping<int, SelectListItem> group in groupedSelectList)
            {
                SelectListGroup optGroup = group.First().Group;

                // Wrap if requested.
                TagBuilder groupBuilder = null;
                if (optGroup != null)
                {
                    groupBuilder = new TagBuilder("optgroup");
                    if (optGroup.Name != null)
                    {
                        groupBuilder.MergeAttribute("label", optGroup.Name);
                    }
                    if (optGroup.Disabled)
                    {
                        groupBuilder.MergeAttribute("disabled", "disabled");
                    }
                    listItemBuilder.AppendLine(groupBuilder.ToString(TagRenderMode.StartTag));
                }

                foreach (SelectListItem item in group)
                {
                    listItemBuilder.AppendLine(ListItemToOption(item));
                }

                if (optGroup != null)
                {
                    listItemBuilder.AppendLine(groupBuilder.ToString(TagRenderMode.EndTag));
                }
            }

            return listItemBuilder;
        }

        internal static string ListItemToOption(SelectListItem item)
        {
            TagBuilder builder = new TagBuilder("option")
            {
                InnerHtml = HttpUtility.HtmlEncode(item.Text)
            };
            if (item.Value != null)
            {
                builder.Attributes["value"] = item.Value;
            }
            if (item.Selected)
            {
                builder.Attributes["selected"] = "selected";
            }
            if (item.Disabled)
            {
                builder.Attributes["disabled"] = "disabled";
            }
            return builder.ToString(TagRenderMode.Normal);
        }

        private static IEnumerable<SelectListItem> GetSelectListWithDefaultValue(IEnumerable<SelectListItem> selectList, object defaultValue, bool allowMultiple)
        {
            IEnumerable defaultValues;

            if (allowMultiple)
            {
                defaultValues = defaultValue as IEnumerable;
                if (defaultValues == null || defaultValues is string)
                {
                    throw new InvalidOperationException(
                        String.Format(
                            CultureInfo.CurrentCulture,
                            "MvcResources.HtmlHelper_SelectExpressionNotEnumerable",
                            "expression"));
                }
            }
            else
            {
                defaultValues = new[] { defaultValue };
            }

            IEnumerable<string> values = from object value in defaultValues
                                         select System.Convert.ToString(value, CultureInfo.CurrentCulture);

            IEnumerable<string> enumValues = from System.Enum value in defaultValues.OfType<System.Enum>()
                                             select value.ToString("d");

            values = values.Concat(enumValues);

            HashSet<string> selectedValues = new HashSet<string>(values, StringComparer.OrdinalIgnoreCase);
            List<SelectListItem> newSelectList = new List<SelectListItem>();

            foreach (SelectListItem item in selectList)
            {
                item.Selected = (item.Value != null) ? selectedValues.Contains(item.Value) : selectedValues.Contains(item.Text);
                newSelectList.Add(item);
            }
            return newSelectList;
        }

        public override string GetTag()
        {
            return "select";
        }

        public override MvcHtmlString CreateHtml()
        {
            return MvcHtmlString.Create(this.Builder.ToString(TagRenderMode.Normal));
        }
    }
}
