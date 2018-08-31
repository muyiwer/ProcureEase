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
    
    public partial class Adverts
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Adverts()
        {
            this.AdvertCategoryNumber = new HashSet<AdvertCategoryNumber>();
            this.AdvertLotNumber = new HashSet<AdvertLotNumber>();
        }
    
        public System.Guid AdvertID { get; set; }
        public Nullable<System.Guid> BudgetYearID { get; set; }
        public string Headline { get; set; }
        public string Introduction { get; set; }
        public string ScopeOfWork { get; set; }
        public string EligibiltyRequirement { get; set; }
        public string CollectionOfTenderDocument { get; set; }
        public string BidSubmission { get; set; }
        public string OtherImportantInformation { get; set; }
        public Nullable<System.DateTime> BidOpeningDate { get; set; }
        public Nullable<System.DateTime> BidClosingDate { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> DateCreated { get; set; }
        public Nullable<System.DateTime> DateModified { get; set; }
        public Nullable<int> AdvertStatusID { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AdvertCategoryNumber> AdvertCategoryNumber { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AdvertLotNumber> AdvertLotNumber { get; set; }
        public virtual AdvertStatus AdvertStatus { get; set; }
        public virtual BudgetYear BudgetYear { get; set; }
    }
}
