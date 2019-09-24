using IF.Core.Data;
using System.Collections.Generic;
using System.Linq;

namespace IF.Core.Security
{



    public class PermissionDto : TreeDto<PermissionDto>
    {
        public string ActionName { get; set; }
        public string ControllerName { get; set; }
        public string Info { get; set; }

        
        public string RouteParameter { get; set; }

        public bool IsActive { get; set; }
        public string Name { get; set; }

        public string IconName { get; set; }

        public string ResourcesKey { get; set; }
        public bool Delegation { get; set; }

        public bool IsWorkflowPermission { get; set; }
        public string PermissionCode { get; set; }

        public string ApplicationCode { get; set; }

        public string DescriptionTR { get; set; }

        public string DescriptionEN { get; set; }

        public string WindowType { get; set; }


        public string AuthorizationCode { get; set; }

        

    }

    public class PermissionMapDto : TreeDto<PermissionMapDto>
    {
        public string Name { get; set; }

        public string Text { get; set; }
        public string Title { get; set; }
        public string ImageName { get; set; }
        public int PermissionId { get; set; }
        public string ActionName { get; set; }
        public string ControllerName { get; set; }
        public string RouteParameter { get; set; }

        public string PermissionCode { get; set; }

        public string ResourcesKey { get; set; }

        public bool IsActive { get; set; }

        public int WidgetType { get; set; }

        public string IconName { get; set; }

        public bool IsWorkflowPermission { get; set; }
        public int Type { get; set; }

        public byte WidgetRenderType { get; set; }

        public static void AddParentPermissionsIds(List<int> checkedPermissionMapIds, IEnumerable<PermissionMapDto> allPermissions)
        {
            for (int i = 0; i < checkedPermissionMapIds.Count; i++)
            {
                var permissionMapId = checkedPermissionMapIds[i];



                var permissionMap = allPermissions.Where(p => p.Id == permissionMapId).SingleOrDefault();

                if (permissionMap != null)
                {
                    int? parentId = permissionMap.ParentId;

                    if (parentId.HasValue)
                    {
                        while (parentId.HasValue)
                        {
                            var parentPermission = allPermissions.SingleOrDefault(a => a.Id == parentId);

                            if (parentPermission != null)
                            {
                                if (checkedPermissionMapIds.IndexOf(parentPermission.Id) == -1)
                                {
                                    checkedPermissionMapIds.Add(parentPermission.Id);
                                }

                                parentId = parentPermission.ParentId;
                            }
                        }
                    }
                }
            }




        }

    }

}
