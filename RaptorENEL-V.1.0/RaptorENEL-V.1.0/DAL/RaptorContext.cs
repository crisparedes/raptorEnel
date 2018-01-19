using RaptorENEL_V._1._0.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace RaptorENEL_V._1._0.DAL
{
    public class RaptorContext: DbContext
    {

        public RaptorContext() : base(nameOrConnectionString: "DBRaptor") { }

       
        public DbSet<User> User { get; set; }
        public DbSet<Base> Base { get; set; }
        public DbSet<Municipio> Municipio { get; set; }
        public DbSet<Reportecandidato> Reportecandidato { get; set; }
        public DbSet<Candidato> Candidato { get; set; }



        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
      
        }

        
    }
}