﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

using System.Data.Entity.ModelConfiguration.Conventions;

namespace WebApplication3.Context
{
    public class ProductMangerContext:DbContext
    {
        public ProductMangerContext() : base("ProductMangerContext")
        {

        }

       

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}