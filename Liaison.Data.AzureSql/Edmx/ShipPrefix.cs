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
    
    public partial class ShipPrefix
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ShipPrefix()
        {
            this.Ships = new HashSet<Ship>();
        }
    
        public int ShipPrefixId { get; set; }
        public string ShipPrefix1 { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Ship> Ships { get; set; }
    }
}
