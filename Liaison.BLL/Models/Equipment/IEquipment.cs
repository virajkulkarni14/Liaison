using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Liaison.BLL.Models.Objects;

namespace Liaison.BLL.Models.Equipment
{
    public class EquipmentMethods
    {       

        internal static EquipmentContainer GetEquipment(List<IEquipment> equipment)
        {
            if (equipment == null || equipment.Count == 0)
            {
                return null;

            }

            if (equipment.Count == 1)
            {
                return new EquipmentContainer(equipment[0].GetEquipmentString().Replace("_", ""));
            }

            StringBuilder sb = new StringBuilder();

            //var mosts = this.Equipment.OrderBy(eo => eo.GetQty());
            var most = equipment.OrderByDescending(eo => eo.GetQty()).First();

            var returnable = new EquipmentContainer(most.GetEquipmentString().Replace("_", ""));

            foreach (var thing in equipment)
            {
                if (thing.GetID() == most.GetID())
                {
                    continue;
                }
                if (thing.GetType() == typeof(BLLGroundEquipment))
                {
                    if (thing is BLLGroundEquipment ground)
                    {
                        sb.Append(ground.GetEquipmentString());
                    }
                }

                sb.Append(ResourceStrings.Seperator);
                if (equipment.Count > 2)
                {
                    sb.Append(Environment.NewLine);
                }
            }

            var x = sb.ToString();
            returnable.SecondaryEquipment =
                (x.Length > 0 ? x.Substring(0, x.Length - ResourceStrings.Seperator.Length) : x).Replace("_", "");

            return returnable;
         }
    }
    public interface IEquipment
    {
        string GetEquipmentString();
        int GetQty();
        Guid GetID();
        
    }
}