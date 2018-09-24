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
    
    public partial class Procurements
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Procurements()
        {
            this.AdvertLotNumber = new HashSet<AdvertLotNumber>();
            this.Items = new HashSet<Items>();
        }
    
        public System.Guid ProcurementID { get; set; }
        public Nullable<System.Guid> BudgetYearID { get; set; }
        public Nullable<System.Guid> DepartmentID { get; set; }
        public string ProjectName { get; set; }
        public Nullable<System.Guid> ProjectCategoryID { get; set; }
        public Nullable<int> ProcurementStatusID { get; set; }
        public Nullable<System.Guid> ProcurementMethodID { get; set; }
        public Nullable<System.Guid> SourceOfFundID { get; set; }
        public Nullable<System.DateTime> DateModified { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> DateCreated { get; set; }
        public Nullable<System.Guid> TenantID { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AdvertLotNumber> AdvertLotNumber { get; set; }
        public virtual BudgetYear BudgetYear { get; set; }
        public virtual Catalog Catalog { get; set; }
        public virtual Department Department { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Items> Items { get; set; }
        public virtual ProcurementMethod ProcurementMethod { get; set; }
        public virtual ProjectCategory ProjectCategory { get; set; }
        public virtual SourceOfFunds SourceOfFunds { get; set; }
        public virtual ProcurementStatus ProcurementStatus { get; set; }
    }
}
