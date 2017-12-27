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
                int divnumber = int.Parse(new string(coo.SplitName.Where(c => char.IsDigit(c)).ToArray()));
                string mission = coo.SplitName.Split(' ')[1];
                return new DivisionOrg
                {
                    Number = divnumber,
                    UseOrdinal = true,
                    Mission = mission,
                    UnitTypeId = Liaison.Helper.Enumerators.UnitType.Division,
                    CurrentOpsRef = coo.Url.Substring(Liaison.Helper.CurrentOpsHelper.ctops.Length),
                    CurrentOpsUrl = coo.Url,
                    CurrentOpsLogo = GetLogoUrl(coo.LogoUrl),
                    ServiceId = coo.FullName.EndsWith("U.S. Army")
                                    ? Helper.Enumerators.Services.Army
                                        : coo.FullName.EndsWith("USMC")
                                        ? Helper.Enumerators.Services.Marines : Helper.Enumerators.Services.Navy,
                    Bases = GetBases(coo.Locations),
                    HigherHqs = GetHigherHq(coo.HigherHq),
                    ServiceTypeIdx = divnumber < 100 || divnumber == 101 ? Helper.Enumerators.ServiceType.Active : Helper.Enumerators.ServiceType.Volunteer,
                    ShortForm = GetShortFormDivision(divnumber, mission)
                };
            }
            else if (coo.SplitName.StartsWith("Headquarters and Headquarters Battalion"))
            {
                //https://currentops.com/unit/us/army/1-id/hhbn
                string url = coo.Url.Substring(0,coo.Url.Length-5);
                int parentnumber = 0;
                if (url.EndsWith("id"))
                {
                    int from = url.LastIndexOf('/')+1;
                    int until = url.IndexOf('-', from);
                    parentnumber = int.Parse(url.Substring(from, (until - from)));
                }
                return new BattalionOrg
                {
                    Number = null,
                    Mission = coo.SplitName.Substring(0, coo.SplitName.IndexOf("Battalion")).Trim(),
                    UnitTypeId = Liaison.Helper.Enumerators.UnitType.Battalion,
                    CurrentOpsRef = coo.Url.Substring(Liaison.Helper.CurrentOpsHelper.ctops.Length),
                    CurrentOpsUrl = coo.Url,
                    CurrentOpsLogo = GetLogoUrl(coo.LogoUrl),
                    ServiceId = coo.FullName.EndsWith("U.S. Army")
                                    ? Helper.Enumerators.Services.Army
                                        : coo.FullName.EndsWith("USMC")
                                        ? Helper.Enumerators.Services.Marines : Helper.Enumerators.Services.Navy,
                    Bases = GetBases(coo.Locations),
                    HigherHqs = GetHigherHq(coo.HigherHq),
                    ServiceTypeIdx= parentnumber < 100 || parentnumber == 101 ? Helper.Enumerators.ServiceType.Active : Helper.Enumerators.ServiceType.Volunteer


                };
            }

            return null;
        }

        private static List<ShortForm> GetShortFormDivision(int divnumber, string mission)
        {
            string ack;
            switch (mission.ToUpper())
            {
                case "INFANTRY":
                    { ack = "Inf"; break; }
                case "ARMOR":
                case "ARMOURED":
                case "ARMORED":
                    { ack = "Arm"; break; }
                case "CAVALRY":
                    { ack = "Cav"; break; }
                case "MARINE":
                    { ack = "Mar"; break; }
                default:
                    { ack = ""; break; }
            }

            string sNumber = GetShortableInt(divnumber);

            return new List<ShortForm>()
                    {
                        new ShortForm
                        {
                            Text=sNumber+" "+ack+". Div.",
                            Type=Helper.Enumerators.ShortFormType.ShortName
                        },
                        new ShortForm
                        {
                            Text=ack.ToUpper()+")"+sNumber,
                            Type=Helper.Enumerators.ShortFormType.IndexName
                        },
                        new ShortForm
                        {
                            Text=sNumber+" "+mission.First()+"D",
                            Type=Helper.Enumerators.ShortFormType.Additional
                        },
                    };

        }

        private static string GetShortableInt(int number)
        {
            if (number < 10)
            {
                return "____" + number;
            }
            else if (number < 100)
            {
                return "___" + number;
            }
            else if (number < 1000)
            {
                return "__" + number;
            }
            else if (number < 10000)
            {
                return "_" + number;
            }
            else if (number < 100000)
            {
                return number.ToString();
            }
            throw new Exception("Number too big");
        }

        private static string GetLogoUrl(string logoUrl)
        {
            return logoUrl.EndsWith("png")
                                          ? logoUrl
                                          : logoUrl.Trim() + "png";
        }

        private static List<HigherHqOrg> GetHigherHq(List<HigherHqObject> higherHqs)
        {
            var returnable = new List<HigherHqOrg>();
            foreach (var high in higherHqs)
            {
                var higherhq = new HigherHqOrg();
                int?[] daterange = GetDateRange(high.DateRange);
                HigherHqOrg hh = new HigherHqOrg
                {
                    CurrentOpsRef = high.Id,
                    DateFrom = daterange[0],
                    DateUntil = daterange[1],
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
                int?[] daterange = string.IsNullOrWhiteSpace(location.DateRange)?new int?[] { null, null }: GetDateRange(location.DateRange);
                BaseOrg bo = new BaseOrg
                {
                    Name = location.BaseName,
                    Location = location.Location,
                    CurrentOpsUrl = location.Url.Trim(),
                    DateFrom = daterange[0],
                    DateUntil = daterange[1],
                    IsCurrent = daterange[1] == null,
                    IsDeployment = location.Deployment,
                    CurrentOpsBaseRef = location.Id,
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
