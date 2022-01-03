using MyWork.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyWork.CommonService
{
    public class TreeNodeView
    {
        // d3.js node property
        private string _name;
        private string _type;
        private string _colour;
        private int _level;

        // item
        private int _itemId;
        private int _parentItemId;
        private string _itemType;
        private int _itemValue;

        private int _activeItemId;
        private int _changeItemId;

        private List<TreeNodeView> _children = new List<TreeNodeView>();
        private List<ItemMetadata> _items = new List<ItemMetadata>();
        private ItemMetadata _link = new ItemMetadata();

        public int changeItemId
        {
            get { return _changeItemId; }
            set { _changeItemId = value; }
        }

        public int activeItemId
        {
            get { return _activeItemId; }
            set { _activeItemId = value; }
        }

        public string colour
        {
            get { return _colour; }
            set { _colour = value; }
        }

        public string name
        {
            get { return _name; }
            set { _name = value; }
        }

        public string type
        {
            get { return _type; }
            set { _type = value; }
        }

        public int level
        {
            get { return _level; }
            set { _level = value; }
        }

        public int itemId
        {
            get { return _itemId; }
            set { _itemId = value; }
        }

        public int parentItemId
        {
            get { return _parentItemId; }
            set { _parentItemId = value; }
        }

        public string itemType
        {
            get { return _itemType; }
            set { _itemType = value; }
        }

        public List<TreeNodeView> children
        {
            get { return _children; }
            set { _children = value; }
        }

        public List<ItemMetadata> items
        {
            get { return _items; }
            set { _items = value; }
        }

        public ItemMetadata link
        {
            get { return _link; }
            set { _link = value; }
        }

        public TreeNodeView(string name)
        {
            this._name = name;
        }

        public void AddChild(TreeNodeView node)
        {
            _children.Add(node);
        }

    }

    public enum TreeNodeTypeEnum
    {
        ROOT = 0,
        CONDITION = 1,
        VALUE = 2,
    }

    public enum TreeNodeColourEnum
    {
        [StringEnumAttribute("LightPink")]
        ROOT = 0,
        [StringEnumAttribute("LightSkyBlue")]
        CONDITION = 1,
        [StringEnumAttribute("AliceBlue")]
        VALUE = 2,
        [StringEnumAttribute("lightgreen")]
        ACTIVE = 3,
        [StringEnumAttribute("YellowGreen")]
        CHANGE = 4,
    }

    public class ItemMetadata
    {
        public int Id { get; set; }
        public string ItemName { get; set; }
        public string ItemDataType { get; set; }
        public string ItemValue { get; set; }
    }

    public class ItemMetadataComparer : IEqualityComparer<ItemMetadata>
    {
        public bool Equals(ItemMetadata x, ItemMetadata y)
        {
            if (x == null && y == null)
            {
                return true;
            }
            else if (x == null || y == null)
            {
                return false;
            }

            return (x.Id == y.Id &&
                x.ItemName == y.ItemName &&
                x.ItemDataType == y.ItemDataType &&
                x.ItemValue.ToLower().Trim() == y.ItemValue.ToLower().Trim());

        }

        public int GetHashCode(ItemMetadata obj)
        {
            throw new NotImplementedException();
        }
    }

    public class HierarchyItem
    {
        public int Id { get; set; }
        public int? ParentId { get; set; }
        public string Title { get; set; }
        public string ItemType { get; set; }
        public List<ItemMetadata> Items { get; set; } = new List<ItemMetadata>();
        public ItemMetadata LinkItem { get; set; }
    }
}
