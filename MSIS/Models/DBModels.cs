using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MSIS.Models
{
    public class DbModels : DbContext
    {
        public DbModels()
           : base("DBContext")
        {
            Database.SetInitializer<DbModels>(null);
        }
        public DbSet<TransactionDM> TransactionDMs { get; set; }
    }
}