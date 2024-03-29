﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ProcureEaseAPI.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class ProcureEaseEntities : DbContext
    {
        public ProcureEaseEntities()
            : base("name=ProcureEaseEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<C__MigrationHistory> C__MigrationHistory { get; set; }
        public virtual DbSet<AdvertCategory> AdvertCategory { get; set; }
        public virtual DbSet<AdvertCategoryNumber> AdvertCategoryNumber { get; set; }
        public virtual DbSet<AdvertisedItems> AdvertisedItems { get; set; }
        public virtual DbSet<AdvertLotNumber> AdvertLotNumber { get; set; }
        public virtual DbSet<Adverts> Adverts { get; set; }
        public virtual DbSet<ApprovedItems> ApprovedItems { get; set; }
        public virtual DbSet<AspNetRoles> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserClaims> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogins> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUserRoles> AspNetUserRoles { get; set; }
        public virtual DbSet<AspNetUsers> AspNetUsers { get; set; }
        public virtual DbSet<BudgetYear> BudgetYear { get; set; }
        public virtual DbSet<ItemCode> ItemCode { get; set; }
        public virtual DbSet<Items> Items { get; set; }
        public virtual DbSet<OrganizationSettings> OrganizationSettings { get; set; }
        public virtual DbSet<ProcurementStatus> ProcurementStatus { get; set; }
        public virtual DbSet<TelephoneNumbers> TelephoneNumbers { get; set; }
        public virtual DbSet<UserProfile> UserProfile { get; set; }
        public virtual DbSet<Procurements> Procurements { get; set; }
        public virtual DbSet<Department> Department { get; set; }
        public virtual DbSet<ProcurementMethod> ProcurementMethod { get; set; }
        public virtual DbSet<ProjectCategory> ProjectCategory { get; set; }
        public virtual DbSet<SourceOfFunds> SourceOfFunds { get; set; }
        public virtual DbSet<ProcurementMethodOrganizationSettings> ProcurementMethodOrganizationSettings { get; set; }
        public virtual DbSet<ProjectCategoryOrganizationSettings> ProjectCategoryOrganizationSettings { get; set; }
        public virtual DbSet<SourceOfFundsOrganizationSettings> SourceOfFundsOrganizationSettings { get; set; }
        public virtual DbSet<AdvertStatus> AdvertStatus { get; set; }
        public virtual DbSet<Catalog> Catalog { get; set; }
        public virtual DbSet<RequestForDemo> RequestForDemo { get; set; }
        public virtual DbSet<ItemCodeCategory> ItemCodeCategory { get; set; }
    }
}
