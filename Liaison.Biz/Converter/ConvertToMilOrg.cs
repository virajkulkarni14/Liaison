using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Liaison.Biz.Objects;
using Liaison.Biz.MilOrgs;
using Liaison.Helper.Enumerators;
using Liaison.Helper;

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
                string urlIdDivPart = coo.Url.Substring(Liaison.Helper.CurrentOpsHelper.ctopsUSArmy.Length);
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
                    ServiceTypeIdx = GetDivisionServiceType( divnumber), 
                    ShortForm = GetShortFormDivision(urlIdDivPart), 
                };
            }
            else if (coo.SplitName.StartsWith("Headquarters and Headquarters Battalion"))
            {
                //https://currentops.com/unit/us/army/1-id/hhbn
                int parentDivisionNumber = GetParentDivisionNumberFromUrl(coo.Url);

                string parentID = coo.HigherHq[0].Url.Substring(Liaison.Helper.CurrentOpsHelper.ctopsUSArmy.Length);
                //parentID = parentID.Substring(0, parentID.LastIndexOf('/'));
                string urlIdDivPart = coo.HigherHq[0].Url.Substring(Liaison.Helper.CurrentOpsHelper.ctopsUSArmy.Length);
                string parentShortForm = GetDivisionShortFormFromParent(parentID, 2);
                return new BattalionOrg
                {
                    ParentShortForm = parentShortForm,
                    Number = null,
                    Mission = coo.SplitName.Substring(0, coo.SplitName.IndexOf("Battalion")).Trim(),
                    UnitTypeId = Liaison.Helper.Enumerators.UnitType.Battalion,
                    CurrentOpsRef = coo.Url.Substring(Liaison.Helper.CurrentOpsHelper.ctops.Length),
                    CurrentOpsUrl = coo.Url,
                    CurrentOpsLogo = GetLogoUrl(coo.LogoUrl),
                    ServiceId = GetServiceId(coo.Url),
                    Bases = GetBases(coo.Locations),
                    HigherHqs = GetHigherHq(coo.HigherHq),
                    ServiceTypeIdx = GetDivisionServiceType(parentDivisionNumber),
                    ShortForm = GetShortFormHHQBattalion(GetShortFormDivision(urlIdDivPart)),
                };
            }
            else if (coo.SplitName.Contains("Company"))
            {
                var coysplit = coo.SplitName.Split(',');
                var namesplit = coysplit[0].Remove(coysplit[0].IndexOf("Company"), 7);
                string namesplitTrim = namesplit.Trim();

                string parentID = coo.HigherHq[0].Url.Substring(Liaison.Helper.CurrentOpsHelper.ctopsUSArmy.Length);

                string parentShortForm;
                ServiceType serviceType = ServiceType.Active;
                string urlIdDivPart = "";

                if (parentID.Contains('/'))
                {
                    var divsplit = parentID.Split('/');
                    parentShortForm = GetBattalionShortFormFromParent(divsplit[1]) + ", " + GetDivisionShortFormFromParent(divsplit[0], 2);
                    serviceType = GetDivisionServiceType(GetParentDivisionNumberFromUrl(divsplit[0]));
                    urlIdDivPart = divsplit[0];
                }
                else
                {
                    parentShortForm = "";
                }
                string name = null;
                string mission = null;
                int? number = null;
                
                if (namesplitTrim.Length>1)
                {
                    name = null;
                    mission = namesplitTrim;
                    number = null;
                }
                else if (namesplitTrim.Length==1)
                {
                    name = namesplitTrim;
                    mission = null;
                    number = null;
                }
                
                return new CompanyOrg
                {
                    Name = name,
                    ParentShortForm=parentShortForm,
                    Number=number,
                    Mission = mission,
                    UnitTypeId=UnitType.Company,
                    CurrentOpsRef = coo.Url.Trim().Substring(Liaison.Helper.CurrentOpsHelper.ctops.Length),
                    CurrentOpsUrl = coo.Url.Trim(),
                    CurrentOpsLogo = GetLogoUrl(coo.LogoUrl),
                    ServiceId = GetServiceId(coo.Url.Trim()),
                    Bases = GetBases(coo.Locations),
                    HigherHqs = GetHigherHq(coo.HigherHq),
                    ServiceTypeIdx = serviceType,
                    ShortForm = GetShortFormCompany(name, mission, number, GetShortFormHHQBattalion(GetShortFormDivision(urlIdDivPart))),

                };
            }
            else if (coo.SplitName.EndsWith("Brigade Combat Team")||coo.SplitName.EndsWith("Brigade"))
            {
                return new BrigadeOrg { };
            }
            return null;
        }




        private static int GetParentDivisionNumberFromUrl(string urlorig)
        {            
            if (urlorig.StartsWith(CurrentOpsHelper.ctopsUSArmy))
            {
                urlorig = urlorig.Substring(CurrentOpsHelper.ctopsUSArmy.Length);
            }
            int parentnumber = 0;
            if (urlorig.EndsWith("/hhbn"))
            {
                urlorig = urlorig.Substring(0, urlorig.Length - "/hhbn".Length);
            }
            if (urlorig.EndsWith("-id"))
            {
                string sDivNumber = urlorig.Substring(0, urlorig.Length - "-id".Length);
                //int from = url.LastIndexOf('/') + 1;
                //int until = url.IndexOf('-', from);
                parentnumber = int.Parse(sDivNumber);
            }
            return parentnumber;
        }

        private static ServiceType GetDivisionServiceType(int divnumber)
        {
            return divnumber <= 10 || divnumber == 101 || divnumber == 82 ? Helper.Enumerators.ServiceType.Active : Helper.Enumerators.ServiceType.Volunteer;                  
        }

        private static string GetBattalionShortFormFromParent(string v)
        {
            if (v == "hhbn")
            {
                return "HHQ Bn.";
            }
            return "";
        }

        private static Services GetServiceId(string url)
        {
            if (url.StartsWith(CurrentOpsHelper.ctopsUSArmy))
            {
                return Services.Army;
            }
            return Services.Joint;
        }

        private static List<ShortForm> GetShortFormDivision(string urlIdDivPart)
        {
            string[] split = urlIdDivPart.Split('-');
            string sNumber = GetIntWithUnderscores(split[0]);
            string ack = GetMissionShortFormFromUrl(split[1], 1).Substring(0);
            return new List<ShortForm>()
                    {
                        new ShortForm
                        {
                            Text=sNumber+" "+ack+ ". Div.",
                            Type=Helper.Enumerators.ShortFormType.ShortName
                        },
                        new ShortForm
                        {
                            Text=ack.ToUpper()+")"+sNumber,
                            Type=Helper.Enumerators.ShortFormType.IndexName
                        },
                        new ShortForm
                        {
                            Text=sNumber+" "+ack.First()+"D",
                            Type=Helper.Enumerators.ShortFormType.Other
                        },
                    };            

        }
        private static List<ShortForm> GetShortFormCompany(string name, string mission, int? number, List<ShortForm> list)
        {
            string additionalShortName = "";
            string additionalIndexName = "";
            string additionalOtherName = "";
            if (!string.IsNullOrEmpty(name))
            {
                additionalShortName = name + " Coy.";
                additionalIndexName = "|" + name;
                additionalOtherName = name;
            }
            else if (mission=="Headquarters and Support")
            {
                additionalShortName = "HQ & Supt. Coy.";
                additionalIndexName = "|!";
                additionalOtherName = "HSC";
            }            
            List<ShortForm> returnable = new List<ShortForm>();
            foreach (var item in list)
            {
                if (item.Type == ShortFormType.ShortName)
                {
                    returnable.Add(new ShortForm
                    {
                        Type = ShortFormType.ShortName,
                        Text = additionalShortName + ", " + item.Text
                    });
                }
                else if (item.Type == ShortFormType.IndexName)
                {
                    returnable.Add(new ShortForm
                    {
                        Type = ShortFormType.IndexName,
                        Text = item.Text + additionalIndexName,
                    });
                }
                else if (item.Type == ShortFormType.Other)
                {
                    returnable.Add(new ShortForm
                    {
                        Type = ShortFormType.Other,
                        Text = additionalOtherName + "-" + item.Text,
                    });
                }
            }
            return returnable;
        }
        private static List<ShortForm> GetShortFormHHQBattalion(List<ShortForm> list)
        {
            List<ShortForm> returnable = new List<ShortForm>();
            foreach (var item in list)
            {
                if (item.Type == ShortFormType.ShortName)
                {
                    returnable.Add(new ShortForm
                    {
                        Type = ShortFormType.ShortName,
                        Text = "HHQ Bn., " + item.Text
                    });
                }
                else if (item.Type == ShortFormType.IndexName)
                {
                    returnable.Add(new ShortForm
                    {
                        Type = ShortFormType.IndexName,
                        Text = item.Text + "@!",
                    });
                }
                else if (item.Type == ShortFormType.Other)
                {
                    returnable.Add(new ShortForm
                    {
                        Type = ShortFormType.Other,
                        Text = "HHB/" + item.Text,
                    });
                }
            }
            return returnable;
        }
        

        //private static List<ShortForm> GetShortFormDivision(int divnumber, string mission)
        //{
        //    string ack = GetMissionShortForm(mission);
            

        //    string sNumber = GetIntWithUnderscores(divnumber);

        //    return new List<ShortForm>()
        //            {
        //                new ShortForm
        //                {
        //                    Text=sNumber+" "+ack+". Div.",
        //                    Type=Helper.Enumerators.ShortFormType.ShortName
        //                },
        //                new ShortForm
        //                {
        //                    Text=ack.ToUpper()+")"+sNumber,
        //                    Type=Helper.Enumerators.ShortFormType.IndexName
        //                },
        //                new ShortForm
        //                {
        //                    Text=sNumber+" "+mission.First()+"D",
        //                    Type=Helper.Enumerators.ShortFormType.Additional
        //                },
        //            };

        //}

        private static string GetDivisionShortFormFromParent(string parent, int variant)
        {
            string[] parentspl = parent.Split('-');

            string returnable = GetIntWithUnderscores(parentspl[0])+ " "+ GetMissionShortFormFromUrl(parentspl[1], variant);
            return returnable;
        }
        private static string GetMissionShortFormFromUrl(string url, int variant)
        {
            switch (url.ToUpper())
            {
                case "ID":
                    {
                        switch (variant)
                        {
                            case 1:
                                return "Inf";
                            case 2:
                                return "Inf. Div.";
                        }
                        break;
                    }
                case "AD":
                    {
                        switch (variant)
                        {
                            case 1:
                                return "Arm";
                            case 2:
                                return "Arm. Div.";
                        }
                        break;
                    }
                case "CD":
                    {
                        switch (variant)
                        {
                            case 1:
                                return "Cav";
                            case 2:
                                return "Cav. Div.";
                        }
                        break;
                    }
            }
            return "";
        }

        private static string GetMissionShortForm(string mission)
        {
            switch (mission.ToUpper())
            {
                case "INFANTRY":
                    { return "Inf"; }
                case "ARMOR":
                case "ARMOURED":
                case "ARMORED":
                    { return "Arm"; }
                case "CAVALRY":
                    { return  "Cav";  }
                case "MARINE":
                    { return "Mar"; }
                default:
                    { return ""; }
            }
        }
        private static string GetIntWithUnderscores(string nmber)
        {
          return  GetIntWithUnderscores(int.Parse(nmber));
        }

        private static string GetIntWithUnderscores(int number)
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
            logoUrl = logoUrl.Trim();
            return logoUrl.EndsWith("png")
                                          ? logoUrl
                                          : string.IsNullOrEmpty(logoUrl) ? "" : logoUrl.Trim() + "png";
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
                        return HigherHqType.Alligned;
                    }
                case "Assigned":
                    {
                        return HigherHqType.Assigned;
                    }
                case "Organic":
                    {
                        return HigherHqType.Organic;
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
            if (string.IsNullOrEmpty(dateRange))
            {
                return new int?[2] { null, null };
            }
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
