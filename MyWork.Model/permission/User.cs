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
    [Table("dbo.my_user")]
    public class User : IEntity<int>, ICodeGen
    {
        public User()
        {
            //Roles = new HashSet<Role>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        //[ModelAttribute(Searchable = true)]
        [ForeignKey("UserProfile")]
        public int UserProfileId { get; set; }
        [ModelAttribute(Searchable = true)]
        public string UserName { get; set; }
        public string Password { get; set; }
        [ModelAttribute(Searchable = true)]
        public string EmailAddress { get; set; }
        [ModelAttribute(EagerLoading = true)]
        public virtual UserProfile UserProfile { get; set; }

        public virtual ICollection<Role> Roles { get; set; } = new HashSet<Role>();
    }
}
