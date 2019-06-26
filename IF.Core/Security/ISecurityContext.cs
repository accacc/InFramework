using IF.Core.Mvc;
using System.Collections.Generic;

namespace IF.Core.Security
{
    public interface ISecurityContext
    {
        IEnumerable<PermissionDto> Permissions();
        IEnumerable<PermissionMapDto> PermissionMaps();

        IEnumerable<PermissionMapDto> PermissionMapsTree(int[] filter = null);

        IEnumerable<PermissionDto> PermissionTree(int[] filter = null);

        PermissionMapDto CurrentAction();

        string ControllerName { get; }
        string ActionName { get; }

        IList<PermissionMapDto> CurrentActionChilds(ActionWidgetType widgetType);

    }
}
