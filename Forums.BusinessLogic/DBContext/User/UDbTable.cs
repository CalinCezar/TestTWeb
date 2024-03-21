using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using System.Threading.Tasks;

namespace Forums.BusinessLogic.DBContext.User
{
    public class UDbTable: DbContext
    {
        public UDbTable() : base("name=Forums") {}
        public virtual DbSet<Domain.Entities.User.DbModel.User> Users { get; set; }
    }
}
