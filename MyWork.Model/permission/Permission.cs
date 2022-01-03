using MyWork.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyWork.Model
{
    [Table("dbo.my_permission")]
    public class Permission : IEntity<int>, ICodeGen
    {
        public Permission()
        {
            //Roles = new HashSet<Role>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [ModelAttribute(Searchable = true)]
        public string ShortName { get; set; }
        [ModelAttribute(Searchable = true)]
        public string Description { get; set; }
        public virtual ICollection<Role> Roles { get; set; } = new HashSet<Role>();
    }
}
