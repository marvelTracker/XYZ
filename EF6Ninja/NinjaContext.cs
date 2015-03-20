using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityFrameworkNinja.Model;
using EF6Ninja.Model;

namespace EntityFrameworkNinja
{
    public class NinjaContext:DbContext
    {
        public NinjaContext()
            : base("name = NinjaContextConnectionString")
        {
            
        }


        public DbSet<Ninja> Ninjas { get; set; }

        public DbSet<LaptopMetaData> LaptopMetaDataList { get; set; } 
    }
}
