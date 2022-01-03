using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyWork.Model
{
    [Table("tree.my_hierarchy")]
    public class Hierarchy : IEntity<int>
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [ForeignKey("Children")]
        public int? ParentId { get; set; }
        public string Title { get; set; }
        public virtual Hierarchy Parent { get; set; }
        public virtual ICollection<Hierarchy> Children { get; set; }  
        public int HierachyType { get; set; }
        public string HierachyItem { get; set; }
        public string LinkHierachyItem { get; set; }

        public DateTime CreatedDateTime { get; set; }
        public string CreatedBy { get; set; }
    }

    public class HierarchyItem : IEqualityComparer<HierarchyItem>
    {
        public int Id { get; set; }
        public string ItemName { get; set; }
        public string DataType { get; set; }
        public string ItemValue { get; set; }

        public bool Equals(HierarchyItem x, HierarchyItem y)
        {
            if(x == null && y == null)
            {
                return true;
            }else if(x == null || y == null)
            {
                return false;
            }
            return (x.Id == y.Id &&
                    x.ItemName == y.ItemName &&
                    x.DataType == y.DataType &&
                    x.ItemValue == y.ItemValue);
            //throw new NotImplementedException();
        }

        public int GetHashCode(HierarchyItem obj)
        {
            throw new NotImplementedException();
        }
    }
}
