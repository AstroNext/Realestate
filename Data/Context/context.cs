using System;
using System.Collections.Generic;
using System.Data.Entity;
using Model;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class context : DbContext
    {
        public context() : base("Context")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<context, Data.Configuration>("context"));
        }
        public virtual DbSet<Estate> Estates { get; set; }
        public virtual DbSet<EstateType> EstateTypes { get; set; }
        public virtual DbSet<SaleType> SaleTypes { get; set; }
        public virtual DbSet<Area> Areas { get; set; }
        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<Licence> Licences { get; set; }
        
    }
}
