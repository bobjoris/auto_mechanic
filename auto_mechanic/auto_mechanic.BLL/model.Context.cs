﻿//------------------------------------------------------------------------------
// <auto-generated>
//    Ce code a été généré à partir d'un modèle.
//
//    Des modifications manuelles apportées à ce fichier peuvent conduire à un comportement inattendu de votre application.
//    Les modifications manuelles apportées à ce fichier sont remplacées si le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace auto_mechanic.BLL
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class AutomechanicsEntities : DbContext
    {
        public AutomechanicsEntities()
            : base("name=AutomechanicsEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<Mechanic> Mechanic { get; set; }
        public DbSet<Franchise> Franchise { get; set; }
        public DbSet<Service> Service { get; set; }
        public DbSet<Mechanic_Service> Mechanic_Service { get; set; }
        public DbSet<Car> Car { get; set; }
        public DbSet<Brand> Brand { get; set; }
        public DbSet<ServiceBook> ServiceBook { get; set; }
        public DbSet<Holiday> Holiday { get; set; }
    }
}