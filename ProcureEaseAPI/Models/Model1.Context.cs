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
        public virtual DbSet<AdvertCategory> AdvertCategories { get; set; }
        public virtual DbSet<AdvertCategoryNumber> AdvertCategoryNumbers { get; set; }
        public virtual DbSet<AdvertisedItem> AdvertisedItems { get; set; }
        public virtual DbSet<AdvertLotNumber> AdvertLotNumbers { get; set; }
        public virtual DbSet<Advert> Adverts { get; set; }
        public virtual DbSet<AdvertStatu> AdvertStatus { get; set; }
        public virtual DbSet<ApprovedItem> ApprovedItems { get; set; }
        public virtual DbSet<AspNetRole> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUserRole> AspNetUserRoles { get; set; }
        public virtual DbSet<AspNetUser> AspNetUsers { get; set; }
        public virtual DbSet<BudgetYear> BudgetYears { get; set; }
        public virtual DbSet<Department> Departments { get; set; }
        public virtual DbSet<Item> Items { get; set; }
        public virtual DbSet<OrganizationSetting> OrganizationSettings { get; set; }
        public virtual DbSet<ProcurementMethod> ProcurementMethods { get; set; }
        public virtual DbSet<Procurement> Procurements { get; set; }
        public virtual DbSet<ProcurementStatu> ProcurementStatus { get; set; }
        public virtual DbSet<ProjectCategory> ProjectCategories { get; set; }
        public virtual DbSet<SourceOfFund> SourceOfFunds { get; set; }
        public virtual DbSet<TelephoneNumber> TelephoneNumbers { get; set; }
        public virtual DbSet<UserProfile> UserProfiles { get; set; }
    }
}
