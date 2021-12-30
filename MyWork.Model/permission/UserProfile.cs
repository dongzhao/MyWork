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
    [Table("my_profile")]
    public class UserProfile : IEntity<int>
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [ModelAttribute(Searchable = true)]
        public string FirstName { get; set; }
        [ModelAttribute(Searchable = true)]
        public string LastName { get; set; }
        [ModelAttribute(Searchable = true)]
        public Boolean? Gender { get; set; } = null;
        [ModelAttribute(Searchable = true)]
        public DateTime? BirthDate { get; set; } = null;
        [ModelAttribute(Searchable = true)]
        public string Mobile { get; set; }
        [ModelAttribute(Searchable = true)]
        public string Address { get; set; }

    }
}
