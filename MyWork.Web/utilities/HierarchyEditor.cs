using MyWork.Core;
using MyWork.Model;
using MyWork.Repository;
using MyWork.Web.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyWork.Web
{
    public class HierarchyEditor : IHierarchyEditor
    {
        private readonly IHierarchyRepository repository;

        public HierarchyEditor(IHierarchyRepository rs)
        {
            this.repository = rs;
        }

        public TreeNodeView PopulateTreeView(int hierarchyId, int activehierarchId)
        {
            var hierarchy = repository.GetById(hierarchyId);
            var rootNode = new TreeNodeView(hierarchy.Title)
            {
                //type = "",
                level = 0,
                itemType = TreeNodeEnum.ROOT.ToString(),
                //itemValue = 10,
                itemId = hierarchy.Id,
                colour = StringEnumUtility.GetStringValue(TreeNodeColourEnum.ROOT),
            };
            PopulateTreeView(hierarchy, rootNode, activehierarchId);
            return rootNode;
        }
        /// <summary>
        /// Recursive call to popuylate tree structure
        /// </summary>
        /// <param name="hierarchy"></param>
        /// <param name="treeNode"></param>
        /// <param name="activehierarchId"></param>
        private void PopulateTreeView(Hierarchy hierarchy, TreeNodeView treeNode, int activehierarchId)
        {
            foreach (var child in hierarchy.Children.OrderByDescending(c => c.CreatedDateTime).ToList())
            {
                var level = treeNode.level;
                var newNode = new TreeNodeView(child.Title)
                {
                    //type = "",
                    level = treeNode.level + 1, // important for calculating the depth of tree
                    itemType = ((TreeNodeEnum)child.HierarchyType).ToString(),
                    //itemValue = 10,
                    itemId = child.Id,
                    parentItemId = hierarchy.Id,
                    items = ToItemList(child.HierarchyItem),
                    link = ToItem(child.LinkHierarchyItem),
                    colour = StringEnumUtility.GetStringValue(TreeNodeColourEnum.ROOT),
                    activeItemId = child.Id != activehierarchId ? 0 : activehierarchId,
                };
                treeNode.AddChild(newNode);
                PopulateTreeView(child, newNode, activehierarchId);
            }
        }

        public List<TreeListView> GetAllTreeRoots()
        {
            var viewList = new List<TreeListView>();
            var list = repository.GetAllParents();
            foreach(var item in list)
            {
                viewList.Add(new TreeListView()
                {
                    Id = item.Id,
                    Title = item.Title,
                    CreateDate = item.CreatedDateTime,
                });
            }
            return viewList;
        }

        public int ReplicateTree(int rootHierarchId, string username)
        {
            var hierarchy = repository.GetById(rootHierarchId);
            if (hierarchy.ParentId.HasValue)
            {
                throw new Exception("Invalid tree root id!");
            }
            var rootHierarchy = Replicate(hierarchy, null, username);
            var rootHierarchyId = repository.Create(rootHierarchy);
            return rootHierarchyId;
        }
        /// <summary>
        /// Recursive call to replicate tree node
        /// </summary>
        /// <param name="from"></param>
        /// <param name="parent"></param>
        /// <param name="username"></param>
        /// <returns></returns>
        private Hierarchy Replicate(Hierarchy from, Hierarchy parent, string username)
        {
            var newcopy = new Hierarchy()
            {
                Title = from.Title,
                HierarchyType = from.HierarchyType,
                HierarchyItem = from.HierarchyItem,
                LinkHierarchyItem = from.LinkHierarchyItem,
                Parent = parent,
                CreatedDateTime = DateTime.Now,
                CreatedBy = username,
            };
            foreach (var child in from.Children)
            {
                var childCopy = Replicate(child, newcopy, username);
                newcopy.Children.Add(childCopy);
            }
            return newcopy;
        }

        public int CreateTreeNode(HierarchyChange change, string username)
        {
            if (!change.ParentId.HasValue) throw new Exception("Failed to create the node, parent node doesn't exist!");
            var hierarchy = repository.GetById(change.ParentId.Value);
            var newItem = new Hierarchy()
            {
                Title = change.Title,
                ParentId = change.ParentId,
                HierarchyType =  (int)Enum.Parse(typeof(TreeNodeEnum), change.ItemType),
                HierarchyItem = ToJson(change.Items),
                LinkHierarchyItem = ToJson(change.LinkItem),
                Parent = hierarchy,
                CreatedDateTime = DateTime.Now,
                CreatedBy = username,
            };
            var hierarchId = repository.Create(newItem);
            return hierarchId;
        }

        public bool UpdateTreeNode(HierarchyChange change, string username)
        {
            var hierarchy = repository.GetById(change.Id);
            if (hierarchy == null)
            {
                throw new Exception("Current node doesn't exist!");
            }
            bool changed = false;
            if (hierarchy.HierarchyType != (int)Enum.Parse(typeof(TreeNodeEnum), change.ItemType))
            {
                hierarchy.HierarchyType = (int)Enum.Parse(typeof(TreeNodeEnum), change.ItemType);
                changed = true;
            }

            var metadataList = ToItemList(hierarchy.HierarchyItem);
            if (HasMetadataChanged(metadataList, change.Items))
            {
                hierarchy.HierarchyItem = ToJson(change.Items);
                changed = true;
            }
            if (changed) repository.Update(hierarchy);
            return true;
        }

        public bool DeleteTreeNode(HierarchyItem change, string username)
        {
            var hierarchy = repository.GetById(change.Id);
            if (hierarchy == null)
            {
                throw new Exception("Current node doesn't exist!");
            }
            repository.Delete(change.Id);
            return true;
        }

        private bool HasMetadataChanged(ICollection<ItemMetadata> changeFrom, ICollection<ItemMetadata> ChangeTo)
        {
            if (changeFrom.SequenceEqual(ChangeTo, new ItemMetadataComparer()))
            {
                var changedList = changeFrom.Where(x => !ChangeTo.Any(y => new ItemMetadataComparer().Equals(x, y))).ToList();
                return changedList.Count() > 0;
            }
            return true;
        }

        private ItemMetadata ToItem(string jsonStr)
        {
            try
            {
                if (!string.IsNullOrEmpty(jsonStr))
                {
                    
                    return JsonConvert.DeserializeObject<ItemMetadata>(jsonStr);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.StackTrace);
            }
            return new ItemMetadata();

        }

        private List<ItemMetadata> ToItemList(string jsonStr)
        {
            try
            {
                if (!string.IsNullOrEmpty(jsonStr))
                {
                    return JsonConvert.DeserializeObject<List<ItemMetadata>>(jsonStr);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.StackTrace);
            }
            return new List<ItemMetadata>();

        }

        private string ToJson(List<ItemMetadata> items)
        {
            try
            {
                if (items!=null)
                {
                    return JsonConvert.SerializeObject(items);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.StackTrace);
            }
            return "";
        }

        private string ToJson(ItemMetadata item)
        {
            try
            {
                if (item != null)
                {
                    return JsonConvert.SerializeObject(item);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.StackTrace);
            }
            return "";
        }

        
    }

    public interface IHierarchyEditor
    {
        List<TreeListView> GetAllTreeRoots();
        TreeNodeView PopulateTreeView(int hierarchyId, int activehierarchId);
        int ReplicateTree(int rootHierarchId, string username);
        int CreateTreeNode(HierarchyChange change, string username);
        bool UpdateTreeNode(HierarchyChange change, string username);
        bool DeleteTreeNode(HierarchyItem change, string username);
    }
}