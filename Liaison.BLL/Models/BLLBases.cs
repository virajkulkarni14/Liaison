using System.Collections.Generic;
using Liaison.Data.Sql.Edmx;

namespace Liaison.BLL.Models
{
    public class BLLBases : List<BLLBase>
    {
        public BLLBases(ICollection<Base> subFacilities)
        {
            foreach (var subfac in subFacilities)
            {
                var b = new BLLBase(subfac);
                this.Add(b);
            }
        }
    }
}