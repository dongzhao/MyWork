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
    [Table("my_user")]
    public class User : IEntity<int>
    {
        public User()
        {
            Roles = new HashSet<Role>();
        }

        [Key]
        public int Id { get; set; }
        //[ModelAttribute(Searchable = true)]
        //[ForeignKey("UserProfile")]
        //public Int32 UserProfileId { get; set; }
        [ModelAttribute(Searchable = true)]
        public string UserName { get; set; }
        public string Password { get; set; }
        [ModelAttribute(Searchable = true)]
        public string EmailAddress { get; set; }
        //[ModelAttribute(EagerLoading = true)]
        //public virtual UserProfile UserProfile { get; set; }

        public virtual ICollection<Role> Roles { get; set; }
    }
}
