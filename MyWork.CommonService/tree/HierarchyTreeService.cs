using MyWork.Core;
using MyWork.Model;
using MyWork.Repository;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyWork.CommonService.tree
{
    public class HierarchyTreeService : IHierarchyTreeService
    {
        private readonly IHierarchyRepository repository;

        public HierarchyTreeService(IHierarchyRepository rs)
        {
            this.repository = rs;
        }

        public List<Hierarchy> GetAllTreeRoots()
        {
            return repository.GetAllParents().ToList();
        }

        public int CreateHierarchyItem(HierarchyItem change, string username)
        {
            //var hierarchy = repository.GetById(change.ParentId);
            //if (hierarchy == null)
            //{
            //    throw new Exception("Failed to create the node, parent node doesn't exist!");
            //}
            //var newItem = new HierarchyTree()
            //{

            //};


            //treeNode.AddChild(newNode);
            return 0;
        }


        private ICollection<ItemMetadata> FindChangedMetadata(ICollection<ItemMetadata> changeFrom, ICollection<ItemMetadata> ChangeTo)
        {
            //if (changeFrom.SequenceEqual(ChangeTo, new ItemMetadataComparer()))
            //{
            //    var changedList = changeFrom.Where(x => !ChangeTo);
            //}
            return null;
        }


        public TreeNodeView PopulateTreeView(int hierarchyId, int activeHierarchId)
        {
            var hierarchy = repository.GetById(hierarchyId);
            var rootNode = new TreeNodeView(hierarchy.Title)
            {
                //type = "",
                level = 0,
                itemType = TreeNodeTypeEnum.ROOT.ToString(),
                //itemValue = 10,
                itemId = hierarchy.Id,
                colour = StringEnumUtility.GetStringValue(TreeNodeColourEnum.ROOT),
            };
            PopulateTreeView(hierarchy, rootNode, activeHierarchId);
            return rootNode;
        }

        public void PopulateTreeView(Hierarchy hierarchy, TreeNodeView treeNode, int activeHierarchId)
        {
            foreach (var child in hierarchy.Children.OrderByDescending(c => c.CreatedDateTime).ToList())
            {
                var level = treeNode.level;
                var newNode = new TreeNodeView(child.Title)
                {
                    //type = "",
                    level = treeNode.level + 1,
                    itemType = ((TreeNodeTypeEnum)child.HierarchyType).ToString(),
                    //itemValue = 10,
                    itemId = child.Id,
                    parentItemId = hierarchy.Id,
                    items = ToItemList(child.HierarchyItem),
                    link = ToItem(child.LinkHierarchyItem),
                    colour = StringEnumUtility.GetStringValue(TreeNodeColourEnum.ROOT),
                    activeItemId = child.Id != activeHierarchId ? 0 : activeHierarchId,
                };
                treeNode.AddChild(newNode);
                PopulateTreeView(child, newNode, activeHierarchId);
            }
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
    }

    public interface IHierarchyTreeService
    {
        int CreateHierarchyItem(HierarchyItem change, string username);
        TreeNodeView PopulateTreeView(int hierarchyId, int activeHierarchId);
    }
}
