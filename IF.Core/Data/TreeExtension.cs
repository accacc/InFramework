using System.Collections.Generic;
using System.Linq;

namespace IF.Core.Data
{
    public static class TreeExtension
    {

        public static List<T> ToTree<T>(this IEnumerable<T> list, List<T> parents = null,int level=0) where T : TreeDto<T>
        {
            level++;

            if (parents == null)
            {
                parents = list.OrderBy(p => p.SortOrder).Where(a => !a.ParentId.HasValue).ToList();
            }

            for (int i = 0; i < parents.Count; i++)
            {
                parents[i].Level = level;

                if (parents[i].ParentId.HasValue)
                {

                    parents[i].Parent = list.Where(l => l.Id == parents[i].ParentId.Value).SingleOrDefault();
                }                

                var childs = list.OrderBy(p => p.SortOrder).Where(l => l.ParentId == parents[i].Id).ToList();

                if (childs != null)
                {
                    parents[i].Childs = new List<T>(childs);
                    ToTree(list, childs,level);
                }
            }

            return parents;
        }

        public static List<T> ToTreeSiblings<T>(this IEnumerable<T> list, int Id) where T : TreeDto<T>
        {
            var sibling = list.Where(c => c.Id == Id).SingleOrDefault();

            List<T> parents = list.Where(c => c.ParentId == sibling.ParentId).ToList();

            var tree = ToTree(list, parents);

            return tree;

        }

        public static List<T> ToSelectedTree<T>(this IEnumerable<T> list, List<int> selecteds, List<T> parents) where T : TreeDto<T>
        {

            List<T> newList = new List<T>();

            if (parents == null)
            {
                parents = list.Where(a => a.ParentId == null).ToList();
            }

            foreach (var cat in parents)
            {
                var childs = list.Where(l => l.ParentId == cat.Id).ToList();

                if (childs != null)
                {
                    cat.Childs = new List<T>(childs);

                    if (selecteds != null)
                    {

                        cat.Selected = selecteds.Contains(cat.Id);

                        if (cat.Selected)
                        {
                            int? parentId = cat.ParentId;

                            while (parentId.HasValue)
                            {
                                var parentAction = list.SingleOrDefault(a => a.Id == parentId);

                                parentAction.Selected = parentAction != null;

                                parentId = parentAction.ParentId;

                            }
                        }

                    }

                    newList.Add(cat);

                    ToSelectedTree(list, selecteds, childs);
                }
            }

            return parents;
        }

        public static List<T> ToParentPath<T>(this T item, IEnumerable<T> all) where T : TreeDto<T>
        {
            var list = new List<T>();

            if (item != null)
            {


                while (item != null)
                {
                    var parent = all.Where(a => a.Id == item.ParentId).SingleOrDefault();

                    if (parent != null)
                    {
                        list.Add(parent);
                    }
                    
                    item = parent;

                }

                list.Reverse();
                list.Add(item);
            }

            return list;
        }
    }

}
