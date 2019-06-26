using Derin.Core.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Derin.Core.Security
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
