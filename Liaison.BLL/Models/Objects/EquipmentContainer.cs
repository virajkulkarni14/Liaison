using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Liaison.BLL.Models.Objects
{
    public class EquipmentContainer
    {
        public EquipmentContainer(string me)
        {
            this.MainEquipment = me;
        }

        public string MainEquipment { get; set; }
        public string SecondaryEquipment { get; set; }
    }
}
