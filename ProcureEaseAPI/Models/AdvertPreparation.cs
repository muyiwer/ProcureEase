using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProcureEaseAPI.Models
{
    public class AdvertPreparation
    {
        public Guid AdvertID { get; set; }
        public string Headline { get; set; }
        public string Introduction { get; set; }
        public string ScopeOfWork { get; set; }
        public string EligibiltyRequirement { get; set; }
        public string CollectionOfTenderDocument { get; set; }
        public string BidSubmission { get; set; }
        public string OtherImportantInformation { get; set; }
        public Nullable<System.DateTime> BidOpeningDate { get; set; }
        public Nullable<System.DateTime> BidClosingDate { get; set; }
        public virtual ICollection<AdvertProject> Projects { get; set; }
        public virtual ICollection<AdvertProjectCategory> Category { get; set; }
    }

    public class AdvertProject
    {
        public Guid ProcurementID { get; set; }
        public Guid ProjectCategoryID { get; set; }
        public Guid AdvertLotNumberID { get; set; }
        public Guid AdvertCategoryNumberID { get; set; }
        public virtual AdvertPreparation Adverts { get; set; }
    }

    public class AdvertProjectCategory
    {
        public Guid ProcurementID { get; set; }
        public Guid ProjectCategoryID { get; set; }
        public string CategoryName { get; set; }
        public string CategoryLotCode { get; set; }
        public string ProjectName { get; set; }
        public bool Deleted { get; set; }
        public virtual AdvertPreparation Adverts { get; set; }
    }
   
}