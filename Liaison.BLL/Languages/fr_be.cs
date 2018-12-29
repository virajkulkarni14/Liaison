using System.Text;
using Liaison.BLL.Models;
using Liaison.BLL.Models.Unit;
using Liaison.Helper.Enumerators;

namespace Liaison.BLL.Languages
{
    // ReSharper disable once InconsistentNaming
    public class fr_be : ILanguage
    {
        public string GetBattalionName(Battalion battalion)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(ToOrdinal(battalion.Number, battalion.UseOrdinal) + " ");

            if (battalion.ServiceType == ServiceTypeBLL.Reserve)
            {
                sb.Append("(R) ");
            }
            else if (battalion.ServiceType == ServiceTypeBLL.Volunteer)
            {
                sb.Append("(V) (" + battalion.TerritorialDesignation + ") ");
            }

            sb.Append("bataillon");

            #region cmt

            // List<string> missions = new List<string>() {"Civil Affairs", "Psychological Operations", "Commando"};
            // if (string.IsNullOrWhiteSpace(battalion.UniqueName))
            //{
            //    if (!string.IsNullOrWhiteSpace(battalion.MissionName))
            //    {
            //        if (battalion.Number == null)
            //        {
            //            sb.Append(battalion.MissionName + " ");
            //        }
            //        else
            //        {
            //            if (missions.Contains(battalion.MissionName))
            //            {
            //                sb.Append(battalion.MissionName);
            //                //if (this.MissionName != "Commando")
            //                //{
            //                sb.Append(" ");
            //                //}
            //            }
            //            else
            //            {
            //                sb.Append("(" + battalion.MissionName + ") ");
            //            }
            //        }
            //    }
            //}
            // else
            //{
            //    if (!string.IsNullOrWhiteSpace(battalion.UniqueName))
            //    {
            //        sb.Append("(" + battalion.UniqueName + ") ");
            //    }

            //    sb.Append(battalion.MissionName + " ");
            //}

            #endregion cmt

            sb.Append(" " + battalion.MissionName);

            sb.Append(ResourceStrings.Seperator + battalion.AdminCorps?.UnitDisplayName);
            return sb.ToString().Replace("_", "");
        }

        public string ToOrdinal(int? input, bool useOrdinal)
        {

            if (input == null)
            {
                return "";
            }

            if (!useOrdinal)
            {
                return input.Value.ToString();
            }

            if (input <= 0) return input.ToString();


            if (input.Value == 1)
            {
                return input + "er";
            }

            return input + "e";

        }
    }
}