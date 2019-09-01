using System;
using System.Text;
using Liaison.Data.Sql.Edmx;

namespace Liaison.BLL.Models.Equipment
{

    public class BLLGroundEquipment : AEquipment, IEquipment
    {
        public BLLGroundEquipment(Data.Sql.Edmx.GroundEquipment ge)
        {
            this.Id = ge.EquipmentGuid;
            this.Designation = ge.Designation;
            this.Name = ge.Name;
            this.FamilyDesignation = ge.FamilyDesignation;
            this.PrintName = ge.PrintName;

            this.AltCode = ge.AltCode;
            this.AltName = ge.AltName;
            this.AltSort = ge.AltSort;
        }

        public string PrintName { get; set; }

        public BLLGroundEquipment(EquipmentOwner eo) : this(eo.GroundEquipment)
        {
            this.Qty = eo.Quantity.ToIntNull();
            this.Notes = eo.Notes;
        }

        public string Designation { get; private set; }
        public string FamilyDesignation { get; private set; }
        public int? Qty { get; set; }
        public string Notes { get; set; }

       public  int GetQty()
        {
            return Qty ?? 0;
        }

        public string GetEquipmentString()
        {
            bool showAltName = true;
            
            StringBuilder sb = new StringBuilder();
            if (this.Qty != null)
            {
                sb.Append(this.Qty + " ");
            }

            sb.Append(this.Designation + " ");
            sb.Append(this.PrintName);
            if (showAltName)
            {
                sb.Append(" [" + this.AltCode + " " + this.AltName + "]");
            }
            return sb.ToString();
        }
        public Guid GetID()
        {
            return Id;
        }
    }
}