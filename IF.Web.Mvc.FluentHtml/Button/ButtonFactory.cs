using IF.Web.Mvc.FluentHtml.Button;
using IF.Web.Mvc.FluentHtml.HtmlForm;

namespace IF.Core.Mvc.PageLayout.SubmitButton
{
    public class ButtonFactory
    {
        public HtmlFormBase Form { get; set; }

        public ButtonFactory(HtmlFormBase form)
        {
            this.Form = form;

        }

        public ButtonBuilder Add()
        {
            IFButton button = new IFButton(this.Form.htmlHelper, this.Form);
            Form.Buttons.Add(button);
            return new ButtonBuilder(button);
        }
    }
}
