using IF.Core.Mvc;
using IF.Core.Mvc.PageLayout;
using IF.Core.Mvc.PageLayout.PageLayout;
using IF.Core.Security;
using IF.Web.Mvc.FluentHtml.Bootstrap;
using IF.Web.Mvc.FluentHtml.Bootstrap.Tab;
using IF.Web.Mvc.FluentHtml.Grid;
using IF.Web.Mvc.FluentHtml.HtmlForm;
using IF.Web.Mvc.FluentHtml.Link;
using IF.Web.Mvc.FluentHtml.Modal;
using System.ComponentModel;
using System.Web.Mvc;

namespace IF.Web.Mvc.FluentHtml
{
    public class HtmlElementFactory
    {
        [EditorBrowsable(EditorBrowsableState.Never)]
        protected HtmlHelper HtmlHelper
        {
            get;
            set;
        }

        ISecurityContext securityContext;

        public HtmlElementFactory(HtmlHelper helper)
        {
            this.HtmlHelper = helper;
            this.securityContext = DependencyResolver.Current.GetService<ISecurityContext>();
        }

        public PageLayoutFactory PageLayoutManager(HtmlHelper helper)
        {
            return new PageLayoutFactory(helper);
        }


        public ActionLinkSetBuilder GridHeaderButtons()
        {

            var currentAction = this.securityContext.CurrentAction();

            return GridHelper.GetGridHeaderButtonsBuilder(currentAction, this.HtmlHelper, this.HtmlHelper.ViewBag.CurrentGridViewId.ToString());
        }



        public ModalBuilder BootstrapModal()
        {
            return new ModalBuilder(HtmlHelper);
        }

        //public ActionLinkSpecialBuilder ContextActionLink(string Text, string ActionName, string ControllerName)
        //{
        //    string targetModalId = Guid.NewGuid().ToString();

        //    return new ActionLinkSpecialBuilder(new ActionLink(this.HtmlHelper, Text, ActionName, ControllerName))
        //    .AjaxOptions(ajax => ajax.UpdatedTargetId(targetModalId))
        //    .RouteValues(new { TargetModelId = targetModalId });

        //}

        //public ContextMenuBuilder ContextMenu(string Id)
        //{

        //    List<ActionLink> actions = new List<ActionLink>();


        //    var childActions = new List<PermissionMapDto>();


        //    if (childActions != null && childActions.Any())
        //    {
        //        foreach (var childAction in childActions)
        //        {
        //            string actionType = "OpenDialogFormOnGrid";

        //            if (childAction.Type == (int)ActionType.RedirectToAction)
        //            {
        //                actionType = "RedirectToAction";
        //            }
        //            else if (childAction.Type == (int)ActionType.UserDefinedAction)
        //            {
        //                actionType = "UserDefinedAction";
        //            }
        //            else if (childAction.Type == (int)ActionType.UserDefinedAction)
        //            {
        //                actionType = "ExportToDocument";
        //            }
        //            else if (childAction.Type == (int)ActionType.OpenDialogFormOnGridByFilter)
        //            {
        //                actionType = "OpenDialogFormOnGridByFilter";
        //            }
        //            else if (childAction.Type == (int)ActionType.OpenDialogFormOnPage)
        //            {
        //                actionType = "OpenDialogFormOnPage";
        //            }
        //            else if (childAction.Type == (int)ActionType.DirectActionOnButton)
        //            {
        //                actionType = "DirectActionOnButton";
        //            }

        //            var actionLink = new ActionLink(this.HtmlHelper, childAction.Name, childAction.ActionName, childAction.ControllerName);
        //            actionLink.HtmlAttributes.Add("actionTypeId", actionType);
        //            //actionLink.HtmlAttributes.Add("class", "btn btn-primary btn-sm margin-bottom-20");
        //            actionLink.HtmlAttributes.Add("dataDialogId", Guid.NewGuid().ToString());
        //            actionLink.HtmlAttributes.Add("dataDialogRouteValue", childAction.RouteParameter ?? String.Empty);
        //            actionLink.HtmlAttributes.Add("dataDialogTitle", childAction.Name);
        //            actionLink.HtmlAttributes.Add("PermissionMapId", childAction.Id);
        //            actionLink.HtmlAttributes.Add("id", childAction.ActionName);

        //            actions.Add(actionLink);


        //        }

        //    }


        //    return new ContextMenuBuilder(this.HtmlHelper, actions, Id);

        //}


        public BootstrapRow BootstrapRow()
        {
            return new BootstrapRow(this.HtmlHelper);
        }

        public BootstrapColumn BootstrapColumn()
        {
            return new BootstrapColumn(this.HtmlHelper);
        }



        public AjaxFormBuilder AjaxForm(object ModelId = null)
        {
            var builder = new AjaxFormBuilder(HtmlHelper, ModelId);

            builder.Id("CreateUpdateForm").CloseAfterSuccessSubmit(true);

            return builder;

        }

        public BootstrapAjaxFilterFormBuilder BootstrapAjaxFilterForm(int ModelId = 0)
        {
            return new BootstrapAjaxFilterFormBuilder(this.HtmlHelper, ModelId)
                .Id("GridFilterForm")
                ;
        }

        public BootstrapAjaxGridFormBuilder BootstrapAjaxGridForm(int ModelId = 0)
        {
            return new BootstrapAjaxGridFormBuilder(this.HtmlHelper, ModelId)
                .Id("GridForm")
                //.AjaxOptions(ajax => ajax.OnSuccess("GridFormOnSuccess"))
                ;
        }



        public HtmlFormBuilder FilterForm(int ModelId = 0)
        {
            return new HtmlFormBuilder(this.HtmlHelper, ModelId)
                .Id("GridFilterNormalForm")
                .Method("get")
                .DefaultSubmitButton(b => b.Text("Listele").Id("GridFilterNormalButton"))
                ;
        }



        //routeValuesByElementIds bu parametreye sayfadaki gonderilmek istenen degerleri barindiran inputlarin id leri gonderilmelidir.
        //Örnek: "Name,Surname,Age"
        //public ActionLinkSpecialBuilder ActionLinkDirectAction(string Text, string ActionName, string ControllerName, string routeValuesByElementIds = null)
        //{
        //    return new ActionLinkSpecialBuilder(new ActionLink(this.HtmlHelper, Text, ActionName, ControllerName))
        //    .ActionTypeId("DirectActionOnButton")
        //    .CssClass("btn btn-primary btn-sm margin-bottom-20")
        //    .RouteValues(routeValuesByElementIds)
        //    ;
        //}

        //public ActionLinkSpecialBuilder ActionLinkOpenDialogOnGrid(string Text, string ActionName, string ControllerName)
        //{
        //    return new ActionLinkSpecialBuilder(new ActionLink(this.HtmlHelper, Text, ActionName, ControllerName))
        //    .UpdatedTargetId(Guid.NewGuid().ToString())
        //    .CssClass("btn btn-primary btn-sm margin-bottom-20")
        //    .ActionTypeId("OpenDialogFormOnGrid")
        //    ;
        //}


        //public ActionLinkSpecialBuilder ActionLinkOpenDialog(string Text, string ActionName, string ControllerName)
        //{
        //    string targetModalId = Guid.NewGuid().ToString();

        //    return new ActionLinkSpecialBuilder(new ActionLink(this.HtmlHelper, Text, ActionName, ControllerName))
        //    //.ActionTypeId("OpenDialogFormOnPage")
        //    .UpdatedTargetId(targetModalId)
        //    .CssClass("btn btn-primary btn-sm margin-bottom-20")
        //    .RouteValues(new { TargetModelId = targetModalId });

        //}


        public ActionLinkBuilder ActionLink(string Text, string ActionName, string ControllerName)
        {
            return new ActionLinkBuilder(new ActionLink(this.HtmlHelper, Text, ActionName, ControllerName))
            ;
        }



        public AjaxLinkBuilder AjaxLink(string Text, string ActionName, string ControllerName)
        {
            var builder = new AjaxLinkBuilder(new ActionLink(this.HtmlHelper, Text, ActionName, ControllerName));
            builder.HtmlAttributes(new  { @class = "btn btn-primary" });
            return builder;
        }


        public BootstrapTabBuilder Tab()
        {
            return new BootstrapTabBuilder(new BootstrapTab(this.HtmlHelper));
        }


        public BootstrapTabBuilder TabSecure(string mainObjectTabName, string mainObjectViewName, object model, object routeValues)
        {

            var childActions = this.securityContext.CurrentActionChilds(ActionWidgetType.TabStripButton);

            BootstrapTabBuilder builder = TabHelper.TabSecure(this.HtmlHelper, mainObjectTabName, mainObjectViewName, model, routeValues, childActions);

            return builder;
        }



        public BootstrapTabBuilder TabSecure(object routeValues = null)
        {

            var childActions = this.securityContext.CurrentActionChilds(ActionWidgetType.TabStripButton);

            return TabHelper.TabSecure(this.HtmlHelper, routeValues, childActions);
        }


    }
}
