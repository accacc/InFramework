using Derin.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Derin.Navigation
{
    public class PermissionDto : TreeDto<PermissionDto>
    {
        public int? ParentId { get; set; } // ParentId


        public string Name { get; set; } // Name

        public string Text { get; set; } // Name

        public string RouteParameter { get; set; }


        public string Description { get; set; } // Description

        public string ResourceKey { get; set; } // ResourceKey


        public string ParentPermissionName { get; set; }
        public bool? IsPermissionEnabled { get; set; }

        public string ControllerName { get; set; }

        public string ActionName { get; set; }
        public string RouteParameters { get; set; }

        public string IconName { get; set; }

        public bool IsAdmin { get; set; }

        public string Method { get; set; }


    }

    public class PermissionMapDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Text { get; set; }

        public string Title { get; set; }
        public string ImageName { get; set; }
        public int ParentId { get; set; }
        public int PermissionId { get; set; }
        public string ActionName { get; set; }
        public string ControllerName { get; set; }
        public int SortOrder { get; set; }
        public string RouteParameter { get; set; }

        public byte WidgetRenderType { get; set; }

        public string PermissionCode { get; set; }

        public byte WidgetType { get; set; }

        public string IconName { get; set; }

        public bool IsWorkflowPermission { get; set; }
        public byte Type { get; set; }
        public bool? IsPermissionEnabled { get; set; }

        public bool IsActive { get; set; }

        public bool IsAdmin { get; set; }

        public string Method { get; set; }

        //public int Level { get; set; }
        public List<PermissionMapDto> Childs { get; set; }

        //public static IEnumerable<PermissionMapDto> GetAvailableItems(IEnumerable<PermissionMapDto> AllItems, int[] availablePermissionIds)
        //{

        //    if (availablePermissionIds == null) { return AllItems; }

        //    var result = AllItems.Where(m => availablePermissionIds.Contains(m.PermissionId) || (m.Type == (int)ActionWidgetType.Text || m.ParentId == 0)).ToList();

        //    return result;
        //}

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
                        while (parentId.Value != 0)
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

        public static List<PermissionMapDto> GetAvailableItems(IEnumerable<PermissionMapDto> AllItems, int[] availableActionsIds)
        {
            var siteActionMapList = new List<PermissionMapDto>();


            foreach (var action in AllItems)
            {
                if (availableActionsIds.Contains(action.PermissionId))
                {
                    siteActionMapList.Add(new PermissionMapDto()
                    {
                        PermissionId = action.PermissionId,
                        ActionName = action.ActionName,
                        ControllerName = action.ControllerName,
                        Id = action.Id,
                        ImageName = action.ImageName,
                        Name = action.Name,
                        //Parent = action.Parent,
                        ParentId = action.ParentId,
                        SortOrder = action.SortOrder,
                        Type = action.Type,
                        WidgetType = action.WidgetType,


                    });

                    int? parentId = action.ParentId;

                    if (parentId.HasValue && !siteActionMapList.Any(m => m.Id == parentId.Value) && !siteActionMapList.Any(m => m.Id == action.Id))
                    {
                        while (parentId.HasValue)
                        {
                            var parentAction = AllItems.SingleOrDefault(a => a.Id == parentId);

                            if (parentAction != null)
                            {
                                siteActionMapList.Add(new PermissionMapDto()
                                {
                                    PermissionId = parentAction.PermissionId,
                                    ActionName = parentAction.ActionName,
                                    ControllerName = parentAction.ControllerName,
                                    Id = parentAction.Id,
                                    ImageName = parentAction.ImageName,
                                    Name = parentAction.Name,
                                    //Parent = parentAction.Parent,
                                    ParentId = parentAction.ParentId,
                                    SortOrder = parentAction.SortOrder,
                                    Type = action.Type,
                                    WidgetType = action.WidgetType,

                                });

                                parentId = parentAction.ParentId;

                            }

                        }

                    }
                }
            }


            return siteActionMapList;
        }

        public static List<PermissionMapDto> ListToTree(IEnumerable<PermissionMapDto> list, List<PermissionMapDto> parents)
        {
            if (parents == null)
            {
                parents = list.Where(a => a.ParentId == 0).OrderBy(p => p.SortOrder).ToList();
            }

            for (int i = 0; i < parents.Count; i++)
            {
                var childs = list.Where(l => l.ParentId == parents[i].Id).OrderBy(p => p.SortOrder).ToList();

                if (childs != null)
                {
                    parents[i].Childs = new List<PermissionMapDto>(childs);
                    ListToTree(list, childs);
                }
            }

            return parents;
        }

        public static List<PermissionMapDto> GetActionPath(PermissionMapDto currentAction, IEnumerable<PermissionMapDto> allActions)
        {
            var actionList = new List<PermissionMapDto>();

            if (currentAction != null)
            {

                var action = currentAction;

                while (action != null)
                {
                    var parent = allActions.Where(a => a.Id == action.ParentId).SingleOrDefault();

                    if (parent != null)
                    {
                        actionList.Add(parent);
                    }
                    action = parent;

                }

                actionList.Reverse();
                actionList.Add(currentAction);
            }

            return actionList;
        }



    }
}
