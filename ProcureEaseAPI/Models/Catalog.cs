//------------------------------------------------------------------------------
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
    using System.Collections.Generic;
    
    public partial class Catalog
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Catalog()
        {
            this.AdvertCategory = new HashSet<AdvertCategory>();
            this.AdvertCategoryNumber = new HashSet<AdvertCategoryNumber>();
            this.AdvertisedItems = new HashSet<AdvertisedItems>();
            this.AdvertLotNumber = new HashSet<AdvertLotNumber>();
            this.Adverts = new HashSet<Adverts>();
            this.AdvertStatus = new HashSet<AdvertStatus>();
            this.ApprovedItems = new HashSet<ApprovedItems>();
            this.AspNetUsers = new HashSet<AspNetUsers>();
            this.BudgetYear = new HashSet<BudgetYear>();
            this.Department = new HashSet<Department>();
            this.ItemCode = new HashSet<ItemCode>();
            this.Items = new HashSet<Items>();
            this.OrganizationSettings1 = new HashSet<OrganizationSettings>();
            this.Procurements = new HashSet<Procurements>();
            this.ProcurementStatus = new HashSet<ProcurementStatus>();
            this.ProjectCategory = new HashSet<ProjectCategory>();
            this.ProjectCategory1 = new HashSet<ProjectCategory>();
            this.SourceOfFunds = new HashSet<SourceOfFunds>();
            this.TelephoneNumbers = new HashSet<TelephoneNumbers>();
            this.UserProfile = new HashSet<UserProfile>();
            this.ProcurementMethodOrganizationSettings = new HashSet<ProcurementMethodOrganizationSettings>();
            this.ProjectCategoryOrganizationSettings = new HashSet<ProjectCategoryOrganizationSettings>();
            this.SourceOfFundsOrganizationSettings = new HashSet<SourceOfFundsOrganizationSettings>();
            this.ProcurementMethod = new HashSet<ProcurementMethod>();
        }
    
        public System.Guid TenantID { get; set; }
        public Nullable<System.Guid> RequestID { get; set; }
        public string SubDomain { get; set; }
        public Nullable<System.Guid> OrganizationID { get; set; }
        public Nullable<System.DateTime> DateCreated { get; set; }
        public Nullable<System.DateTime> DateModified { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AdvertCategory> AdvertCategory { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AdvertCategoryNumber> AdvertCategoryNumber { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AdvertisedItems> AdvertisedItems { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AdvertLotNumber> AdvertLotNumber { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Adverts> Adverts { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AdvertStatus> AdvertStatus { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ApprovedItems> ApprovedItems { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AspNetUsers> AspNetUsers { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BudgetYear> BudgetYear { get; set; }
        public virtual OrganizationSettings OrganizationSettings { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Department> Department { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ItemCode> ItemCode { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Items> Items { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrganizationSettings> OrganizationSettings1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Procurements> Procurements { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProcurementStatus> ProcurementStatus { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProjectCategory> ProjectCategory { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProjectCategory> ProjectCategory1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SourceOfFunds> SourceOfFunds { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TelephoneNumbers> TelephoneNumbers { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UserProfile> UserProfile { get; set; }
        public virtual RequestForDemo RequestForDemo { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProcurementMethodOrganizationSettings> ProcurementMethodOrganizationSettings { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProjectCategoryOrganizationSettings> ProjectCategoryOrganizationSettings { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SourceOfFundsOrganizationSettings> SourceOfFundsOrganizationSettings { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProcurementMethod> ProcurementMethod { get; set; }
    }
}
