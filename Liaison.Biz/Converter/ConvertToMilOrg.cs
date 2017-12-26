using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Liaison.Biz.Objects;
using Liaison.Biz.MilOrgs;

namespace Liaison.Biz.Converter
{
    public class ConvertToMilOrg
    {
        public static IMilitaryOrg Convert(CurrentOpsObject coo)
        {
            if (coo.SplitName.EndsWith("Division"))
            {
                return new DivisionOrg
                {
                    Number = int.Parse(new string(coo.SplitName.Where(c => char.IsDigit(c)).ToArray())),
                    UseOrdinal = true,
                    Mission = coo.SplitName.Split(' ')[1],
                    UnitTypeId = Liaison.Helper.Enumerators.UnitType.Division,
                    CurrentOpsRef = coo.Url.Substring(Liaison.Helper.CurrentOpsHelper.ctops.Length),
                    CurrentOpsUrl = coo.Url,
                    CurrentOpsLogo = coo.LogoUrl.EndsWith("png")
                                        ? coo.LogoUrl + "zzzzz"
                                        : coo.LogoUrl.Trim() + "png",
                    ServiceId = coo.FullName.EndsWith("U.S. Army")
                                    ? Helper.Enumerators.Services.Army
                                        : coo.FullName.EndsWith("USMC")
                                        ? Helper.Enumerators.Services.Marines : Helper.Enumerators.Services.Navy,
                    Bases = GetBases(coo.Locations),
                    HigherHqs = GetHigherHq(coo.HigherHq)
                };
            }

            return null;
        }

        private static List<HigherHqOrg> GetHigherHq(List<HigherHqObject> higherHqs)
        {
            var returnable = new List<HigherHqOrg>();
            foreach( var high in higherHqs)
            {
                var higherhq = new HigherHqOrg();
                int?[] daterange = GetDateRange(high.DateRange);
                HigherHqOrg hh = new HigherHqOrg
                {
                    CurrentOpsRef = high.Id,
                    DateFrom=daterange[0],
                    DateUntil=daterange[1],
                    IsCurrent = daterange[1] == null,
                    CommandRelationshipType = GetCommandRelationshipType(high.Type),
                };
                returnable.Add(hh);
            }
            return returnable;
        }

        private static Liaison.Helper.Enumerators.HigherHqType GetCommandRelationshipType(string type)
        {
            switch (type)
            {               
                case "Aligned":
                    {
                        return Helper.Enumerators.HigherHqType.Alligned;
                    }
                case "Assigned":
                    {
                        return Helper.Enumerators.HigherHqType.Assigned;
                    }
            }
            return Helper.Enumerators.HigherHqType.Unknown;
        }

        private static List<BaseOrg> GetBases(List<LocationObject> locations)
        {
            var returnable = new List<BaseOrg>();
            foreach (var location in locations)
            {
                var baseOrg = new BaseOrg();
                int?[] daterange = GetDateRange(location.DateRange);
                BaseOrg bo = new BaseOrg
                {
                    Name = location.BaseName,
                    Location = location.Location,
                    CurrentOpsUrl = location.Url.Trim(),
                    DateFrom = daterange[0],
                    DateUntil = daterange[1],
                    IsCurrent = daterange[1] == null,
                    IsDeployment = location.Deployment,
                    CurrentOpsBaseRef=location.Id,
                };
                returnable.Add(bo);
            }
            return returnable;
        }

        private static int?[] GetDateRange(string dateRange)
        {
            //2006 - Present
            //  ... - 2006
            string[] str = dateRange.Split('-');
            int?[] iii = new int?[2];
            iii[0] = int.TryParse(str[0], out int result0) ? result0 : (int?)null;
            iii[1] = int.TryParse(str[1], out int result1) ? result1 : (int?)null;
            return iii;
        }
    }
}
