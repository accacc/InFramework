using Derin.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Derin.Core.Security
{
    public interface ISecurityService : IBaseService
    {


        PermissionListResponse GetPermissionList(PermissionListRequest request);
        PermissionMapListResponse GetPermissionMapList(PermissionMapListRequest request);

        void AddPermissionMap(PermissionMapDto dto);
        void UpdatePermissionMap(PermissionMapUpdateCommand command);

        PermissionGetResponse GetPermissionGet(PermissionGetRequest request);

        void UpdatePermission(PermissionUpdateCommand permissionUpdateCommand);
        void AddPermission(PermissionAddCommand permissionAddCommand);
    }
}
