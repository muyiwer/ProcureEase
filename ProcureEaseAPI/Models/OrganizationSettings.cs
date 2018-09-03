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
            this.Catalog = new HashSet<Catalog>();
            this.TelephoneNumbers = new HashSet<TelephoneNumbers>();
            this.UserProfile = new HashSet<UserProfile>();
            this.ProjectCategoryOrganisationSettings = new HashSet<ProjectCategoryOrganisationSettings>();
            this.ProcurementMethodOrganisationsettings = new HashSet<ProcurementMethodOrganisationsettings>();
            this.SourceOFFundsOrganisationSettings = new HashSet<SourceOFFundsOrganisationSettings>();
            this.Department = new HashSet<Department>();
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
        public virtual ICollection<Catalog> Catalog { get; set; }
        public virtual Catalog Catalog1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TelephoneNumbers> TelephoneNumbers { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UserProfile> UserProfile { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProjectCategoryOrganisationSettings> ProjectCategoryOrganisationSettings { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProcurementMethodOrganisationsettings> ProcurementMethodOrganisationsettings { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SourceOFFundsOrganisationSettings> SourceOFFundsOrganisationSettings { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Department> Department { get; set; }
    }
}
