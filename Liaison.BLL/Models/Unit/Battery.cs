using System.Text;
using Liaison.BLL.Models.Unit.Abstracts;

namespace Liaison.BLL.Models.Unit
{
    public class Battery : Company
    {
        public Battery(Data.Sql.Edmx.Unit sqlUnit) : base(sqlUnit)
        {
            this.Equipment = sqlUnit.EquipmentOwners.ToEquipmentList();

        }

        public override string GetName()
        {
            StringBuilder sb = new StringBuilder();

            bool unitWithId = !(this.Number == null && this.Letter == null);
            bool ishq = false;

            if (unitWithId)
            {
                if (this.Number != null)
                {
                    sb.Append(this.Number + " ");
                }

                if (this.Letter != null)
                {
                    sb.Append(this.Letter + " ");
                }

                //if (this.LegacyMissionName != null)
                //{
                //    sb.Append("(" + this.LegacyMissionName + ") ");
                //}
            }
            else
            {

                if (this.MissionName != null)
                {
                    string missionname = this.MissionName;
                    if (this.MissionName == ResourceStrings.HQHQ)
                    {
                        sb.Append("HHB");
                        ishq = true;
                    }
                    else if (this.MissionName == ResourceStrings.HQ)
                    {
                        sb.Append("HQB");
                        ishq = true;
                    }
                    else if (this.MissionName == ResourceStrings.HQS)
                    {
                        sb.Append("HSB");
                        ishq = true;
                    }
                    else
                    {
                        sb.Append(this.MissionName + " ");
                        sb.Append("Bty.");
                        AUnit.GetServiceType(sb, this.ServiceType, this.TerritorialDesignation, this.CommandName, true, true);

                        ishq = true;
                    }
                }
            }

            if (!ishq)
            {
                AUnit.GetServiceType(sb, this.ServiceType, this.TerritorialDesignation, this.CommandName, true, true);
            }

            if (unitWithId)
            {
                sb.Append(this.MissionName + " ");
            }


            if (!ishq)
            {
                sb.Append("Bty.");
            }

            var endstring = !string.IsNullOrWhiteSpace(this.CommandName)
                ? this.CommandName.Replace("_", "")
                : this.AdminCorps.UnitDisplayName;

            if (!string.IsNullOrWhiteSpace(endstring))
            {
                sb.Append(", ");

                sb.Append(endstring);

            }

            return sb.ToString();
        }
    }
}