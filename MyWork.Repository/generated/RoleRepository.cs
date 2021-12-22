
/**
The class generated by codegen 
*/
using MyWork.Model;
using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using SYstem.Threading.Tasks;

namespace MyWork.Repository
{
    public class RoleRepository : IRoleRepository
    {
        protected readonly MyWorkDbContext ctx;
        public RoleRepository(MyWorkDbContext context){
            this.ctx = context;
        }

        public void Delete(int id){
            var role = ctx.RoleSet.SingleOrDefault(e => e.Id == id);
            ctx.RoleSet.Remove(role);
            ctx.SaveChanges();
        }

        public IEnumerable<Role> GetAll()
        {
            return ctx.RoleSet

            .ToList();
        }

        public Role GetById(int id)
        {
            return ctx.RoleSet

            .SingleOrDefault(e => e.Id == id);
        }

        public int Create(Role role)
        {
            ctx.RoleSet.Add(role);
            ctx.SaveChanges();
            return role.Id;
        }

        public int Update(Role role)
        {
            ctx.RoleSet.Add(role);
            ctx.Entry(role).State = EntityState.Modified;
            ctx.SaveChanges();
            return role.Id;
        }
 
        public IEnumerable<Role> SearchByShortName(System.String shortname)
        {
            return ctx.RoleSet

            .Where(e => e.ShortName == shortname).ToList();            
        }                
 
        public IEnumerable<Role> SearchByDescription(System.String description)
        {
            return ctx.RoleSet

            .Where(e => e.Description == description).ToList();            
        }                
        
    }
}

