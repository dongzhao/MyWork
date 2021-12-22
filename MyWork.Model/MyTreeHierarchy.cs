using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyWork.Model
{
    [Table("my_hierarchy_tree")]
    public class MyTreeHierarchy
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [ForeignKey("Children")]
        public int? ParentId { get; set; }
        public string Title { get; set; }
        public virtual MyTreeHierarchy Parent { get; set; }
        public virtual ICollection<MyTreeHierarchy> Children { get; set; }  
        public int HierachyType { get; set; }
        public string HierachyItem { get; set; }
    }

    public class MyHierarchyItem : IEqualityComparer<MyHierarchyItem>
    {
        public int Id { get; set; }
        public string ItemName { get; set; }
        public string DataType { get; set; }
        public string ItemValue { get; set; }

        public bool Equals(MyHierarchyItem x, MyHierarchyItem y)
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

        public int GetHashCode(MyHierarchyItem obj)
        {
            throw new NotImplementedException();
        }
    }
}
