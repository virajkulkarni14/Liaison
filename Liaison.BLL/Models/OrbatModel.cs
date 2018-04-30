using Liaison.Data.Sql.Edmx;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Liaison.BLL.Models
{
    public class OrbatModel
    {
        string Name { get; set; }
        List<OrbatModel> Subordinates { get; set; }
        
        public OrbatModel(UnitIndex incoming)
        {
            this.Name = incoming.Unit.na
        }

        public object FirstUnit { get; set; }
    }
}
