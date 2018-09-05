using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProcureEaseAPI.Models
{
    public class DepartmentProject
    {
        public Guid ProcurementID { get; set; }
        public string ProjectName { get; set; }
        public Guid ProcurementMethodID { get; set; }
        public Guid ProjectCategoryID { get; set; }
        public Guid ProcurementStatusID { get; set; }
        public Guid DepartmentID { get; set; }
        public Guid BudgetYearID { get; set; }
        public Guid TenantID { get; set; }
        public bool Deleted { get; set; }
        public bool Approved { get; set; }
        public bool Attested { get; set; }
        public virtual ICollection<DepartmentItems> Items { get; set; }
    }

        public class DepartmentItems
        {

            public Guid ItemID { get; set; }
            public string ItemName { get; set; }
            public string Description { get; set; }
            public string ItemCode { get; set; }
            public Guid ItemCodeID { get; set; }
            public double Quantity { get; set; }
            public double UnitPrice { get; set; }
            public Guid ProcurementID { get; set; }
            public Guid TenantID { get; set; }
            public bool Deleted { get; set; }
            public virtual DepartmentProject Projects { get; set; }
        }
    

}