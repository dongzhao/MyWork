using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyWork.Model
{
    public class MyWorkDbContext : DbContext
    {
        public MyWorkDbContext() : base("name=MyWorkDbContext")
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MyTreeHierarchy>()
                .HasMany(p => p.Children)
                .WithOptional(c => c.Parent)
                .HasForeignKey(c => c.ParentId);

            modelBuilder.Entity<Role>()
                .HasMany(e => e.Users)
                .WithMany(e => e.Roles)
                .Map(m => m.ToTable("user_role").MapLeftKey("role_Id").MapRightKey("user_Id"));

            modelBuilder.Entity<Permission>()
                .HasMany(e => e.Roles)
                .WithMany(e => e.Permissions)
                .Map(m => m.ToTable("role_permission").MapLeftKey("permission_Id").MapRightKey("role_Id"));


            base.OnModelCreating(modelBuilder);
        }

        public virtual DbSet<MyTreeHierarchy> TreeHierarchySet { get; set; }

        public virtual DbSet<User> UserSet { get; set; }
        public virtual DbSet<Role> RoleSet { get; set; }
        public virtual DbSet<Permission> PermissionSet { get; set; }
        //public virtual Dbset<UserProfile> UserProfileSet { get; set; }
    }
}
