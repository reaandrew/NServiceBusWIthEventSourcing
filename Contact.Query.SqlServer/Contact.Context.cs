﻿//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Contact.Query.SqlServer
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class TestDatabaseEntities : DbContext
    {
        public TestDatabaseEntities()
            : base("name=TestDatabaseEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<AccommodationLead> AccommodationLeads { get; set; }
        public DbSet<AccommodationSupplier> AccommodationSuppliers { get; set; }
        public DbSet<Authentication> Authentications { get; set; }
        public DbSet<User> Users { get; set; }
    }
}