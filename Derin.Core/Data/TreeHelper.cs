using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Derin.Core.Data
{
    public static class TreeHelper
    {
        public static IEnumerable<T> GetAvailableItems<T>(IEnumerable<T> AllItems, int[] availablePermissionIds) where T : TreeDto<T>
        {

            if (availablePermissionIds == null) { return AllItems; }

            var result = AllItems.Where(m => availablePermissionIds.Contains(m.Id)).ToList();

            return result;
        }

        public static List<T> ListToTree<T>(IEnumerable<T> list, List<T> parents) where T : TreeDto<T>
        {
            if (parents == null)
            {
                parents = list.OrderBy(p => p.SortOrder).Where(a => a.ParentId == null).ToList();
            }

            for (int i = 0; i < parents.Count; i++)
            {
                var childs = list.OrderBy(p => p.SortOrder).Where(l => l.ParentId == parents[i].Id).ToList();

                if (childs != null)
                {
                    parents[i].Childs = new List<T>(childs);
                    ListToTree(list, childs);
                }
            }

            return parents;
        }
    }
}
