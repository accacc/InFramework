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
            Button button = new Button(this.Form.htmlHelper, this.Form);
            Form.Buttons.Add(button);
            return new ButtonBuilder(button);
        }
    }
}
