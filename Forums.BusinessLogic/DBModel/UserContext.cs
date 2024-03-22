using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Forums.Domain.Entities.User;

namespace Forums.BusinessLogic.DBModel
{
    public class UserContext : DbContext
    {
        public UserContext():
            base("name=Forums") // ConnectionString name define in FILE Web.config
        {
        }
        public virtual DbSet<UDbTable> Users { get; set; }
    }
}
