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
    
    public partial class ProjectCategoryOrganizationSettings
    {
        public System.Guid ProjectCategory { get; set; }
        public Nullable<System.Guid> TenantID { get; set; }
        public Nullable<System.Guid> OrganizationID { get; set; }
        public Nullable<bool> EnableProcurementCategory { get; set; }
        public Nullable<System.DateTime> DateCreated { get; set; }
        public Nullable<System.DateTime> DateModified { get; set; }
    
        public virtual Catalog Catalog { get; set; }
        public virtual OrganizationSettings OrganizationSettings { get; set; }
        public virtual ProjectCategory ProjectCategory1 { get; set; }
    }
}
