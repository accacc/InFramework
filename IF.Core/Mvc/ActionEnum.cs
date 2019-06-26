using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IF.Core.Mvc
{
    public enum ActionType : int
    {
        MainMenuItem = 1,
        SubMenuItem = 2,
        OpenDialogFormOnGrid = 3,
        RedirectToAction = 4,
        UserDefinedAction = 5,
        ExportToDocument = 6,
        OpenDialogFormOnGridByFilter = 8,
        DirectActionOnButton = 9,
        OpenDialogFormOnPage = 10,
        ContextActionLink = 11,
        DirectActionOnGrid = 12,
        GridSourceLink = 13

    }

    public enum ActionWidgetType : int
    {
        Text = 1,
        Link = 2,
        GridButton = 3,
        TabStripButton = 4,
        TreeMenuItem = 5,
        GridRowButton = 6,
        SheetRowButton = 7,
        SheetContextMenuButton = 8,
        FormButton = 9,
        SheetFormButton = 10,
        GridView = 11

    }

    public enum ActionWidgetRenderType : int
    {
        Icon = 1,
        Text = 2
    }
}
