//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Liaison.Data.AzureSql.Edmx
{
    using System;
    using System.Collections.Generic;
    
    public partial class Relationship
    {
        public int RelationshipId { get; set; }
        public System.Guid RelationshipGuid { get; set; }
        public Nullable<System.Guid> RelFrom { get; set; }
        public Nullable<System.Guid> RelTo { get; set; }
        public int RelTypeIdx { get; set; }
        public int RelFromUnitId { get; set; }
        public int RelToUnitId { get; set; }
        public bool DoNotUse { get; set; }
    
        public virtual RelationshipType RelationshipType { get; set; }
        public virtual Unit Unit { get; set; }
        public virtual Unit Unit1 { get; set; }
    }
}
