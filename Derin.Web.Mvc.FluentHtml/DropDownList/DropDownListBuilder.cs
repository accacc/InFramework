using IF.Web.Mvc.FluentHtml.Base;
using System.Collections.Generic;
using System.Web.Mvc;

namespace IF.Web.Mvc.FluentHtml.DropDownList
{
    public class DropDownListBuilder : HtmlFormElementBuilder<DropDownList, DropDownListBuilder>
    {

        public DropDownListBuilder(HtmlHelper html, string name, IEnumerable<SelectListItem> selectList, ModelMetadata metaData, bool allowMultiple = false)
            
        {
            this.HtmlElement = new DropDownList(html, name, selectList, allowMultiple, metaData);
        }


        public DropDownListBuilder allowMultiple(bool allowMultiple)
        {
            this.HtmlElement.allowMultiple = allowMultiple;
            return this;
        }

        public DropDownListBuilder selectList(IEnumerable<SelectListItem> selectList)
        {
            this.HtmlElement.selectList = selectList;
            return this;
        }

        public DropDownListBuilder optionLabel(string optionLabel)
        {
            this.HtmlElement.optionLabel = optionLabel;
            return this;
        }

    }
}
