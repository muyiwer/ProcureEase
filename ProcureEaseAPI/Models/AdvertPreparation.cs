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
        public virtual ICollection<AdvertProject> Projects { get; set; }
    }

    public class AdvertProject
    {
        public Guid ProjectCategoryID { get; set; }
        public Guid ProcurementID { get; set; }
        public virtual AdvertPreparation Adverts { get; set; }
    }
}