using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using compareIT.Data.Model;
using EF6Ninja.Model;

namespace compareIT.Data
{
    public class CompareITContext : DbContext
    {

        public CompareITContext()
            : base("name=CompareITContext")
        {
            
        }
        public DbSet<Computer> Computers { get; set; }

        public DbSet<LaptopMetaData> LaptopMetaDataList { get;set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }

}
