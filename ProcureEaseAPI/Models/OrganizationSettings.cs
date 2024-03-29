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
    
    public partial class OrganizationSettings
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public OrganizationSettings()
        {
            this.TelephoneNumbers = new HashSet<TelephoneNumbers>();
            this.UserProfile = new HashSet<UserProfile>();
            this.Department = new HashSet<Department>();
            this.ProcurementMethodOrganizationSettings = new HashSet<ProcurementMethodOrganizationSettings>();
            this.ProjectCategoryOrganizationSettings = new HashSet<ProjectCategoryOrganizationSettings>();
            this.SourceOfFundsOrganizationSettings = new HashSet<SourceOfFundsOrganizationSettings>();
        }
    
        public System.Guid OrganizationID { get; set; }
        public string OrganizationNameInFull { get; set; }
        public string OrganizationNameAbbreviation { get; set; }
        public string OrganizationEmail { get; set; }
        public string Address { get; set; }
        public string Country { get; set; }
        public string State { get; set; }
        public string AboutOrganization { get; set; }
        public Nullable<System.DateTime> DateModified { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> DateCreated { get; set; }
        public string OrganizationLogoPath { get; set; }
        public Nullable<System.Guid> TenantID { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TelephoneNumbers> TelephoneNumbers { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UserProfile> UserProfile { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Department> Department { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProcurementMethodOrganizationSettings> ProcurementMethodOrganizationSettings { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProjectCategoryOrganizationSettings> ProjectCategoryOrganizationSettings { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SourceOfFundsOrganizationSettings> SourceOfFundsOrganizationSettings { get; set; }
        public virtual Catalog Catalog { get; set; }
    }
}
