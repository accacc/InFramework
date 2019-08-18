
using IF.Web.Mvc.FluentHtml.Button;

namespace IF.Core.Mvc.PageLayout.SubmitButton
{
    public class ButtonBuilder
    {

        internal IFButton button;

        public ButtonBuilder(IFButton button)
        {
            this.button = button;
        }

        public ButtonBuilder Id(string Id)
        {
            this.button.Id = Id;
            return this;
        }

        public ButtonBuilder UpdatedTargetId(string UpdatedTargetId)
        {
            this.button.UpdatedTargetId = UpdatedTargetId;
            return this;
        }

        public ButtonBuilder Class(string Class)
        {
            this.button.CssClass = Class;
            return this;
        }

        public ButtonBuilder TemplateName(string TemplateName)
        {
            this.button.TemplateName = TemplateName;
            return this;
        }


        public ButtonBuilder Text(string Text)
        {
            this.button.InnerText = Text;
            return this;
        }

        public ButtonBuilder ActionName(string ActionName)
        {
            this.button.ActionName = ActionName;
            return this;
        }

        public ButtonBuilder Hide()
        {
            this.button.Hide = true;
            return this;
        }




        public ButtonBuilder RedirectTo(string RedirectTo)
        {
            this.button.RedirectTo = RedirectTo;
            return this;
        }
    }
}
