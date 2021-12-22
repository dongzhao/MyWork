﻿using MyWork.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyWork.Model
{
    [Table("my_role")]
    public class Role : IEntity<int>
    {
        public Role()
        {
            Users = new HashSet<User>();
            Permissions = new HashSet<Permission>();
        }

        [Key]
        public int Id { get; set; }
        [ModelAttribute(Searchable = true)]
        public string ShortName { get; set; }
        [ModelAttribute(Searchable = true)]
        public string Description { get; set; }

        public virtual ICollection<User> Users { get; set; }
        public virtual ICollection<Permission> Permissions { get; set; }
    }
}