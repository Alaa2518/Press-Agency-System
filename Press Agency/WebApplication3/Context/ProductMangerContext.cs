using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

using System.Data.Entity.ModelConfiguration.Conventions;
using WebApplication3.Models;

namespace WebApplication3.Context
{
    public class ProductMangerContext:DbContext
    {
        public ProductMangerContext() : base("DefaultConnection")
        {

        }


        
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<Person> People { get; set; }
        public DbSet<Article> articles { get; set; }
        public DbSet<Photo> photos { get; set; }
        public DbSet<Questions> questions { get; set; }
        public DbSet<Saving> saving { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}