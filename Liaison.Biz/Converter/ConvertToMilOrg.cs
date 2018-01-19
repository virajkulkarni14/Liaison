using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Liaison.Biz.Objects;
using Liaison.Biz.MilOrgs;
using Liaison.Helper.Enumerators;
using Liaison.Helper;
using System.Globalization;

namespace Liaison.Biz.Converter
{
    public class ConvertToMilOrg
    {
        public static IMilitaryOrg Convert(CurrentOpsObject coo)
        {

            if (coo.SplitName.Contains(Helper.Constants.LongForm.Detachment)||coo.SplitName.Contains(Helper.Constants.ShortForm.HHD)||coo.SplitName.StartsWith(Helper.Constants.Initials.HHD))
            {
                string mission = "";
                if (coo.SplitName.Contains(Helper.Constants.ShortForm.HHD)|| coo.SplitName.Contains(Helper.Constants.Initials.HHD))
                {
                    mission = Helper.Constants.LongForm.HH;
                }
                string urlIdDetPart = coo.Url.Substring(CurrentOpsHelper.ctopsUSArmy.Length-1);
                var higher = GetHigherHq(coo.HigherHq);
                var isAcDc = higher[0].CurrentOpsRef.EndsWith("div-east") || higher[0].CurrentOpsRef.EndsWith("div-west");
                var name = GetDetachmentFullName(coo.FullName.Replace(", " + Helper.CurrentOpsHelper.USArmy, string.Empty));
                return new DetachmentOrg
                {
                    Number = null,
                    Name = name,
                    UseOrdinal = false,
                    Mission = mission,
                    UnitTypeId = UnitType.Detachment,
                    CurrentOpsRef = coo.Url.Trim().Substring(CurrentOpsHelper.ctops.Length),
                    CurrentOpsUrl = coo.Url.Trim(),
                    CurrentOpsLogo = GetLogoUrl(coo.LogoUrl.Trim()),
                    //ServiceId = coo.FullName.EndsWith(CurrentOpsHelper.USArmy) ? Services.Army : Services.Marines,
                    ServiceId = coo.UnitService,
                    Bases = GetBases(coo.Locations),
                    HigherHqs = higher,
                    //ServiceTypeIdx = isAcDc ? ServiceType.AC_RC : ServiceType.Active, 
                    ServiceTypeIdx=coo.UnitComponent,
                    ShortForm = GetShortFormHHDetachment(urlIdDetPart, coo.UnitComponent),
                    ChildOrgs = GetChildOrgs(coo.Children),
                    USState=coo.UnitNGState,
                };
            }
            else if (coo.SplitName.EndsWith(Helper.Constants.LongForm.Command))
            {
                string urlIdCmdPart = coo.Url.Trim().Substring(CurrentOpsHelper.ctopsUSArmy.Length);
                return new CommandOrg
                {
                    Number = null,
                    Name = coo.FullName.Replace(", "+CurrentOpsHelper.USArmy, string.Empty),
                    UseOrdinal = false,
                    Mission = null,
                    UnitTypeId=UnitType.Command,
                    CurrentOpsRef = coo.Url.Trim().Substring(CurrentOpsHelper.ctops.Length),
                    CurrentOpsUrl = coo.Url.Trim(),
                    CurrentOpsLogo = GetLogoUrl(coo.LogoUrl.Trim()),
                    Bases = GetBases(coo.Locations),
                    HigherHqs = GetHigherHq(coo.HigherHq),
                    ServiceId = coo.UnitService,
                    ServiceTypeIdx = coo.UnitComponent,
                    ShortForm = GetShortFormCommand(urlIdCmdPart),
                    ChildOrgs = GetChildOrgs(coo.Children),
                    USState = coo.UnitNGState,
                };
            }
            else if (coo.SplitName.StartsWith(Helper.Constants.LongForm.Division))
            {
                string urlIdDivPart = coo.Url.Substring(CurrentOpsHelper.ctopsUSArmy.Length);
                return new DivisionOrg
                {
                    Number = null,
                    Name = coo.FullName.Substring(0, coo.FullName.IndexOf(Liaison.Helper.CurrentOpsHelper.USArmy) - 2).Replace("1st", "First").Trim(),
                    UseOrdinal = false,
                    Mission = string.Empty,
                    UnitTypeId = UnitType.Division,
                    CurrentOpsRef = coo.Url.Trim().Substring(CurrentOpsHelper.ctops.Length),
                    CurrentOpsUrl = coo.Url.Trim(),
                    CurrentOpsLogo = GetLogoUrl(coo.LogoUrl.Trim()),
                    //ServiceId = coo.FullName.EndsWith(Liaison.Helper.CurrentOpsHelper.USArmy)
                    //                ? Services.Army
                    //                    : coo.FullName.EndsWith(Liaison.Helper.CurrentOpsHelper.USMC)
                    //                    ? Services.Marines : Services.Navy,
                    Bases = GetBases(coo.Locations),
                    HigherHqs = GetHigherHq(coo.HigherHq),
                    //ServiceTypeIdx = ServiceType.AC_RC,
                    ServiceId = coo.UnitService,
                    ServiceTypeIdx = coo.UnitComponent,
                    USState = coo.UnitNGState,
                    ShortForm = GetShortFormDivision(urlIdDivPart, coo.UnitComponent),
                    ChildOrgs=GetFirstArmyChildOrds(GetChildOrgs(coo.Children)),
                };
            }
            else if (coo.SplitName.EndsWith(Helper.Constants.LongForm.FieldArmy))
            {
                int armynumber = int.Parse(new string(coo.SplitName.Where(c => char.IsDigit(c)).ToArray()));
                string mission = coo.SplitName.Split(' ')[1] == Helper.Constants.LongForm.FieldArmy ? "" : coo.SplitName.Split(' ')[1];
                string urlIdArmyPart = coo.Url.Substring(CurrentOpsHelper.ctopsUSArmy.Length);
                return new FieldArmyOrg
                {
                    Number = armynumber,
                    UseOrdinal = true,
                    Mission = mission,
                    UnitTypeId = UnitType.FieldArmy,
                    CurrentOpsRef = coo.Url.Substring(CurrentOpsHelper.ctops.Length),
                    CurrentOpsUrl = coo.Url,
                    CurrentOpsLogo = GetLogoUrl(coo.LogoUrl),
                    //ServiceId = Services.Army,
                    Bases = GetBases(coo.Locations),
                    HigherHqs = GetHigherHq(coo.HigherHq),
                    ChildOrgs = GetChildOrgs(coo.Children),
                    //ServiceTypeIdx = ServiceType.AC_RC,
                    ShortForm = GetShortFormArmy(urlIdArmyPart, mission),
                    ServiceId = coo.UnitService,
                    ServiceTypeIdx = coo.UnitComponent,
                };
            }
            else if (coo.SplitName.EndsWith(Helper.Constants.LongForm.Division))
            {
                int divnumber = int.Parse(new string(coo.SplitName.Where(c => char.IsDigit(c)).ToArray()));
                string mission = coo.SplitName.Split(' ')[1];
                string urlIdDivPart = coo.Url.Substring(CurrentOpsHelper.ctopsUSArmy.Length);
                return new DivisionOrg
                {
                    Number = divnumber,
                    UseOrdinal = true,
                    Mission = mission,
                    UnitTypeId = UnitType.Division,
                    CurrentOpsRef = coo.Url.Substring(CurrentOpsHelper.ctops.Length),
                    CurrentOpsUrl = coo.Url,
                    CurrentOpsLogo = GetLogoUrl(coo.LogoUrl),
                    ChildOrgs = GetChildOrgs(coo.Children),
                    //ServiceId = coo.FullName.EndsWith(Liaison.Helper.CurrentOpsHelper.USArmy)
                    //                ? Services.Army
                    //                    : coo.FullName.EndsWith(Liaison.Helper.CurrentOpsHelper.USMC)
                    //                    ? Services.Marines : Services.Navy,
                    Bases = GetBases(coo.Locations),
                    HigherHqs = GetHigherHq(coo.HigherHq),
                    //ServiceTypeIdx = GetDivisionServiceType(divnumber),
                    ShortForm = GetShortFormDivision(urlIdDivPart, coo.UnitComponent),
                    ServiceId = coo.UnitService,
                    ServiceTypeIdx = coo.UnitComponent,
                    USState = coo.UnitNGState,
                };
            }
            else if (coo.SplitName.EndsWith(Helper.Constants.LongForm.BrigadeCT) || coo.SplitName.EndsWith(Helper.Constants.LongForm.Brigade))
            {
                int brigNumber = int.Parse(new string(coo.SplitName.Where(c => char.IsDigit(c)).ToArray()));
                string mission = GetBrigadeMission(coo.SplitName);
                string urlIdBrigPart = coo.Url.Substring(CurrentOpsHelper.ctopsUSArmy.Length);
                return new BrigadeOrg
                {
                    Number = brigNumber,
                    UseOrdinal = true,
                    Mission = mission,
                    UnitTypeId = UnitType.Brigade,
                    CurrentOpsRef = coo.Url.Substring(CurrentOpsHelper.ctops.Length),
                    CurrentOpsUrl = coo.Url,
                    CurrentOpsLogo = GetLogoUrl(coo.LogoUrl),
                    //ServiceId = coo.FullName.EndsWith(Liaison.Helper.CurrentOpsHelper.USArmy)
                    //                ? Services.Army
                    //                    : coo.FullName.EndsWith(Liaison.Helper.CurrentOpsHelper.USMC)
                    //                    ? Services.Marines : Services.Navy,
                    Bases = GetBases(coo.Locations),
                    HigherHqs = GetHigherHq(coo.HigherHq),
                    //ServiceTypeIdx = GetBrigadeServiceType(brigNumber),
                    ChildOrgs = GetChildOrgs(coo.Children),
                    ShortForm = GetShortFormBrigade(urlIdBrigPart, coo.UnitComponent),
                    ServiceId = coo.UnitService,
                    ServiceTypeIdx = coo.UnitComponent,
                    USState = coo.UnitNGState,
                    IsBrigadeCombatTeam = coo.SplitName.EndsWith(Liaison.Helper.Constants.LongForm.BrigadeCT),
                };
            }
            else if (coo.SplitName.StartsWith(Helper.Constants.LongForm.BattalionHHB))
            {
                //https://currentops.com/unit/us/army/1-id/hhbn
                int parentDivisionNumber = GetParentDivisionNumberFromUrl(coo.Url);

                string parentID = coo.HigherHq[0].Url.Substring(Liaison.Helper.CurrentOpsHelper.ctopsUSArmy.Length);
                //parentID = parentID.Substring(0, parentID.LastIndexOf('/'));
                string urlIdDivPart = coo.HigherHq[0].Url.Substring(Liaison.Helper.CurrentOpsHelper.ctopsUSArmy.Length);
                string modifier = "";
                if (coo.UnitComponent == ServiceType.Reserve)
                {
                    modifier = "(" + Helper.Constants.ShortForm.Reserve + ")";
                }
                else if (coo.UnitComponent == ServiceType.Volunteer)
                {
                    modifier = "(" + Helper.Constants.ShortForm.Volunteer + ") (" + coo.UnitNGState + ") ";
                }

                string parentShortForm = GetShortFormDivisionFromParent(modifier, parentID, 2, true);

                var bn =  new BattalionOrg
                {
                    ParentShortForm = parentShortForm,
                    Number = null,
                    Mission = coo.SplitName.Substring(0, coo.SplitName.IndexOf(Liaison.Helper.Constants.LongForm.Battalion)).Trim(),
                    UnitTypeId = Liaison.Helper.Enumerators.UnitType.Battalion,
                    CurrentOpsRef = coo.Url.Substring(Liaison.Helper.CurrentOpsHelper.ctops.Length),
                    CurrentOpsUrl = coo.Url,
                    CurrentOpsLogo = GetLogoUrl(coo.LogoUrl),
                    //ServiceId = GetServiceId(coo.Url),
                    Bases = GetBases(coo.Locations),
                    HigherHqs = GetHigherHq(coo.HigherHq),
                    //ServiceTypeIdx = GetDivisionServiceType(parentDivisionNumber),
                    ShortForm = GetShortFormHHQBattalion(GetShortFormDivision(urlIdDivPart, coo.UnitComponent)),
                    ServiceId = coo.UnitService,
                    ServiceTypeIdx = coo.UnitComponent,
                    USState = coo.UnitNGState,
                };
                return bn;
            }
            else if (coo.SplitName.Contains(Helper.Constants.LongForm.Company))
            {
                var coysplit = coo.SplitName.Split(',');
                var namesplit = coysplit[0].Remove(coysplit[0].IndexOf(Helper.Constants.LongForm.Company), 7);
                string namesplitTrim = namesplit.Trim();

                string parentID = coo.HigherHq[0].Url.Substring(Liaison.Helper.CurrentOpsHelper.ctopsUSArmy.Length);

                string parentShortForm;
                ServiceType serviceType = ServiceType.Active;
                string urlIdPart = "";
                List<ShortForm> threeParentShortForm = new List<ShortForm>();

                string modifier = "";
                if (coo.UnitComponent == ServiceType.Reserve)
                {
                    modifier = "(" + Helper.Constants.ShortForm.Reserve + ")";
                }
                else if (coo.UnitComponent == ServiceType.Volunteer)
                {
                    modifier = "(" + Helper.Constants.ShortForm.Volunteer + ") (" + coo.UnitNGState + ") ";
                }

                if (parentID.Contains('/'))
                {
                    var divsplit = parentID.Split('/');
                    parentShortForm = GetShortFormBattalionFromParent(divsplit[1]) + ", " + GetShortFormDivisionFromParent(modifier, divsplit[0], 2, true);
                    serviceType = GetDivisionServiceType(GetParentDivisionNumberFromUrl(divsplit[0]));
                    urlIdPart = divsplit[0];
                    threeParentShortForm = GetShortFormHHQBattalion(GetShortFormDivision(urlIdPart, coo.UnitComponent));
                }
                else if (parentID.Contains("-ibct"))
                {
                    var bdesplit = parentID.Split('-');
                    parentShortForm = GetShortFormBrigadeFromParent(modifier, parentID, 2, true);
                    serviceType = GetBrigadeServiceType(int.Parse(bdesplit[0]));
                    urlIdPart = parentID;
                    threeParentShortForm = GetShortFormHHCompany(GetShortFormBrigade(urlIdPart, coo.UnitComponent));
                }
                else
                {
                    parentShortForm = "";
                }
                string name = null;
                string mission = null;
                int? number = null;

                if (namesplitTrim.Length > 1)
                {
                    name = null;
                    mission = namesplitTrim;
                    number = null;
                }
                else if (namesplitTrim.Length == 1)
                {
                    name = namesplitTrim;
                    mission = null;
                    number = null;
                }

                return new CompanyOrg
                {
                    Name = name,
                    ParentShortForm = parentShortForm,
                    Number = number,
                    Mission = mission,
                    UnitTypeId = UnitType.Company,
                    CurrentOpsRef = coo.Url.Trim().Substring(Liaison.Helper.CurrentOpsHelper.ctops.Length),
                    CurrentOpsUrl = coo.Url.Trim(),
                    CurrentOpsLogo = GetLogoUrl(coo.LogoUrl),
                    //ServiceId = GetServiceId(coo.Url.Trim()),
                    Bases = GetBases(coo.Locations),
                    HigherHqs = GetHigherHq(coo.HigherHq),
                    //ServiceTypeIdx = serviceType,
                    ShortForm = GetShortFormCompany(name, mission, number, threeParentShortForm),
                    ServiceId = coo.UnitService,
                    ServiceTypeIdx = coo.UnitComponent,
                    USState = coo.UnitNGState,
                };
            }

            return null;
        }

        private static string GetBrigadeMission(string splitName)
        {
            var split = splitName.Split(' ');
            StringBuilder sb = new StringBuilder();
            for (int i=0; i<split.Length-1;i++)
            {
                if (i!=0)
                {
                    if (sb.Length!=0)
                    {
                        sb.Append(" ");
                    }
                    sb.Append(split[i]);
                }
            }
            return sb.ToString().Replace(" Brigade Combat", "");
        }

        private static string GetDetachmentFullName(string fullName)
        {           
            var x= fullName.Replace(Helper.Constants.Initials.HHD, Helper.Constants.LongForm.DetachmentHHD).Replace(Helper.Constants.ShortForm.Division, Helper.Constants.LongForm.Division).Replace("1st", "First");
            return x;
        }

        private static List<ChildOrg> GetFirstArmyChildOrds(List<ChildOrg> list)
        {
            const string firstarmy = "1st Army";
            foreach (var item in list)
            {
                item.Name = item.Name.Replace(firstarmy, "First Army");
            }
            return list;
        }

        private static List<ChildOrg> GetChildOrgs(List<SubUnitObject> children)
        {
            var returnable = new List<ChildOrg>();
            foreach (var child in children)
            {
                returnable.Add(new ChildOrg
                {
                    CurrentOpsRef = child.Id,
                    Name = child.FullName.Replace("United States", "").Trim(),
                    IsIndirect = child.IsIndirect,
                });
            }
            return returnable;
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
        private static ServiceType GetBrigadeServiceType(int brigNumber)
        {
            return Helper.Enumerators.ServiceType.Volunteer;
        }
        private static ServiceType GetDivisionServiceType(int divnumber)
        {
            return divnumber <= 10 || divnumber == 101 || divnumber == 82 ? Helper.Enumerators.ServiceType.Active : Helper.Enumerators.ServiceType.Volunteer;
        }

        private static string GetShortFormBattalionFromParent(string v)
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
        private static List<ShortForm> GetShortFormArmy(string urlIdArmyPart, string mission)
        {
            string[] split = urlIdArmyPart.Split('-');
            string sNumber = GetIntWithUnderscores(split[0], false);
            string army = Helper.Constants.ShortForm.FieldArmy.ToUpper();

            var fa= new List<ShortForm>()
            {
                new ShortForm
                {
                    Text=sNumber+ " "+army,
                    Type=ShortFormType.ShortName
                },
                new ShortForm
                {
                    Text=Helper.Constants.ShortForm.Army.ToString()+Helper.Constants.Symbol.fieldarmy.ToString()+ sNumber,
                    Type=ShortFormType.IndexName
                },
                                new ShortForm
                {
                    Text=sNumber+ " "+army,
                    Type=ShortFormType.Other
                },
            };
            return fa;
        }

        private static string GetShortFormBrigadeOther(string sNumber, string ack, string urlIdBrigPart)
        {
            //.Replace(" ", "")
            StringBuilder sb = new StringBuilder();
            sb.Append(sNumber + " ");
            var ack_r = ack.Replace(".", "").ToUpper();
            
            if (urlIdBrigPart.EndsWith("-ibct"))
            {
                sb.Append(ack_r.First());
                sb.Append(Helper.Constants.ShortForm.BrigadeCT);
            }
            else
            {
                sb.Append(ack_r);
                if (ack.Length <= 4)
                {
                    sb.Append(Helper.Constants.ShortForm.Brigade.First().ToString());
                }
                else
                {
                    sb.Append(" " + Helper.Constants.ShortForm.Brigade.ToUpper());
                }
            }
            return sb.ToString();
        }

        private static string GetIndexShortForm(string mission, char symbol, Helper.Enumerators.Services svc)
        {
            StringBuilder sb = new StringBuilder();
            switch (svc)
            {
                case Services.Army:
                    sb.Append(Helper.Constants.ShortForm.Army);
                    break;
                default:
                    throw new Exception();
            }
            sb.Append(symbol);
            switch (mission.ToLower())
            {
                case "ibct":
                    sb.Append("INF");
                    break;
                case "csb":
                    sb.Append("CONSUPT");
                    break;
                default:
                    throw new Exception();
            }

            return sb.ToString();
        }

        private static List<ShortForm> GetShortFormCommand(string urlIdCmdPart)
        {
            return new List<ShortForm>
            {
                new ShortForm
                {
                    Text = urlIdCmdPart.ToUpper(),
                    Type=ShortFormType.ShortName,
                },
                new ShortForm
                {
                    Text=urlIdCmdPart.ToUpper(),
                    Type=ShortFormType.IndexName,
                },
                new ShortForm
                {
                    Text=urlIdCmdPart.ToUpper(),
                    Type=ShortFormType.Other,
                }
            };
        }
        private static List<ShortForm> GetShortFormHHCompany(List<ShortForm> list)
        {
            foreach (var item in list)
            {
                if (item.Type == ShortFormType.IndexName)
                {
                    item.Text = item.Text;
                }
                else if (item.Type == ShortFormType.Other)
                {
                    item.Text = "_____/" + item.Text;
                }
                else if (item.Type == ShortFormType.ShortName)
                {
                    item.Text = item.Text;
                }
            }
            return list;
        }
        private static List<ShortForm> GetShortFormBrigade(string urlIdBrigPart, ServiceType servicetype)
        {
            string[] split = urlIdBrigPart.Split('-');
            string sNumber = GetIntWithUnderscores(split[0], false);
            string ack = GetShortFormMissionFromUrl(split[1], 1).Substring(0);
            string index = GetIndexShortForm(split[1], Helper.Constants.Symbol.brigade, Services.Army);
            string modifier = "";
            if (servicetype == ServiceType.Reserve)
            {
                modifier = "(" + Helper.Constants.ShortForm.Reserve + ") ";
            }
            else if (servicetype == ServiceType.Volunteer)
            {
                modifier = "(" + Helper.Constants.ShortForm.Volunteer + ") ";
            }

            var l =  new List<ShortForm>()
            {
                new ShortForm
                {
                    Text=(sNumber+" "+modifier+ack+". "+(urlIdBrigPart.EndsWith("-ibct")
                                                    ? Helper.Constants.ShortForm.BrigadeCT
                                                    : Helper.Constants.ShortForm.Brigade + ".")).ToUpper(),
                    Type=ShortFormType.ShortName
                },
                new ShortForm
                {
                    Text=index.ToUpper()+sNumber,
                    Type=ShortFormType.IndexName
                },
                new ShortForm
                {
                    Text=GetShortFormBrigadeOther(sNumber, ack, urlIdBrigPart),
                    Type=ShortFormType.Other
                }
            };
            return l;
        }

        private static List<ShortForm> GetShortFormDivision(string urlIdDivPart, ServiceType servicetype)
        {
            string[] split = urlIdDivPart.Split('-');
            string sNumber = GetIntWithUnderscores(split[0], false);
            if (split[1] == "army/div")
            {
                // urlIdDivPart = "/1-army/div-east\n"
                split = urlIdDivPart.Split('/');
                var army = split[1].Split('-');
                string iwu = GetIntWithUnderscores(army[0], false);
                string armyname = split[2].Split('-')[1].Trim();

                return new List<ShortForm>()
                {
                    new ShortForm
                    {
                        Text=(Helper.Constants.ShortForm.Division+". "+CultureInfo.CurrentCulture.TextInfo.ToTitleCase(armyname)+", "+iwu+" "+Helper.Constants.ShortForm.FieldArmy).ToUpper(),//"+CultureInfo.CurrentCulture.TextInfo.ToTitleCase(army[1]),
                        Type = ShortFormType.ShortName
                    },
                    new ShortForm
                    {
                        Text=Helper.Constants.ShortForm.Army.ToString()+Helper.Constants.Symbol.fieldarmy.ToString()+iwu+Helper.Constants.Symbol.division.ToString()+armyname.ToUpper(),
                        Type = ShortFormType.IndexName
                    },
                    new ShortForm
                    {
                        Text=armyname.First().ToString().ToUpper()+ " "+Helper.Constants.ShortForm.Division.ToUpper()+", "+iwu+" "+Helper.Constants.ShortForm.FieldArmy.First().ToString().ToUpper(),
                        Type=ShortFormType.Other
                    }
                };
            }
            else
            {
                string ack = GetShortFormMissionFromUrl(split[1], 1).Substring(0);

                string modifier = "";
                if (servicetype == ServiceType.Reserve)
                {
                    modifier = "(" + Helper.Constants.ShortForm.Reserve + ") ";
                }
                else if (servicetype == ServiceType.Volunteer)
                {
                    modifier = "(" + Helper.Constants.ShortForm.Volunteer + ") ";
                }

                var aaaa= new List<ShortForm>()
                {
                    new ShortForm
                    {
                        Text = (sNumber+" "+modifier + ack + ". " + Helper.Constants.ShortForm.Division + ".").ToUpper(),
                        Type = Helper.Enumerators.ShortFormType.ShortName
                    },
                    new ShortForm
                    {
                        Text = ack.ToUpper() + ")" + sNumber,
                        Type = Helper.Enumerators.ShortFormType.IndexName
                    },
                    new ShortForm
                    {
                        Text = sNumber + " " + ack.First() + Helper.Constants.ShortForm.Division.First().ToString(),
                        Type = Helper.Enumerators.ShortFormType.Other
                    },
                };
                return aaaa;
            }
        }
        private static List<ShortForm> GetShortFormCompany(string name, string mission, int? number, List<ShortForm> list)
        {

            string additionalShortName = "";
            string additionalIndexName = "";
            string additionalOtherName = "";
            if (!string.IsNullOrEmpty(name))
            {
                additionalShortName = name + " " + Helper.Constants.ShortForm.Company.ToUpper() + ".";
                additionalIndexName = Helper.Constants.Symbol.company + name;
                additionalOtherName = name;
            }
            else if (mission == Helper.Constants.LongForm.HH)
            {
                additionalShortName = Helper.Constants.ShortForm.HHC.ToUpper()+".";
                additionalIndexName = Helper.Constants.Symbol.company+ Helper.Constants.Symbol.hq.ToString();
                additionalOtherName = Helper.Constants.Initials.HHC;
            }
            else if (mission == Helper.Constants.LongForm.HS)
            {
                additionalShortName = Helper.Constants.ShortForm.HSC.ToUpper() + ".";
                additionalIndexName = Helper.Constants.Symbol.company + Helper.Constants.Symbol.hq.ToString();
                additionalOtherName = Helper.Constants.Initials.HSC;
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
                        Text = additionalOtherName + "-" + item.Text,//.Replace("INFBCT", "IBCT"),
                    });
                }
            }
            return returnable;
        }
        //private static List<ShortForm> GetShortFormHHQCompany(List<ShortForm> list)
        //{
        //    List<ShortForm> returnable = new List<ShortForm>();
        //    foreach(var item in list)
        //    {
        //        if (item.Type==ShortFormType.ShortName)
        //        {
        //            returnable.Add(new ShortForm
        //            {
        //                Type = ShortFormType.ShortName,
        //                Text = "HQ & HQ Coy., " + item.Text
        //            });
        //        }
        //        else if (item.Type==ShortFormType.IndexName)
        //        {
        //            returnable.Add(new ShortForm
        //            {
        //                Type = ShortFormType.IndexName,
        //                Text = item.Text + "|!",
        //            });
        //        }
        //        else if (item.Type==ShortFormType.Other)
        //        {
        //            returnable.Add(new ShortForm
        //            {
        //                Type = ShortFormType.Other,
        //                Text = "HHC-_____/" + item.Text,
        //            });
        //        }
        //    }
        //    return returnable;
        //}

        private static List<ShortForm> GetShortFormHHDetachment(string urlIdDetPart, ServiceType servicetype)
        {
            var returnable = new List<ShortForm>();

           
            var split = urlIdDetPart.Split('/');
            if (split[3].Trim() == "hhd")
            {
                //"/1-army/div-west\n"
                var list = GetShortFormDivision(urlIdDetPart.Substring(0, urlIdDetPart.Length - 4), servicetype);

                foreach (var item in list)
                {
                    if (item.Type==ShortFormType.ShortName)
                    {
                        item.Text = ("HQ & HQ Det., ").ToUpper() + item.Text;
                    }
                    else if (item.Type==ShortFormType.IndexName)
                    {
                        item.Text = item.Text+"?!";
                    }
                    else if (item.Type==ShortFormType.Other)
                    {
                        item.Text = "HHD, "+item.Text;
                    }
                    else
                    {
                        throw new Exception();
                    }
                }
                returnable = list;
            }
            else { throw new Exception(); }

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
                        Text = ("HHQ Bn., " + item.Text).ToUpper()
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
        private static string GetShortFormBrigadeFromParent(string modifier, string parent, int variant, bool useOrdinal)
        {
            string[] parentspl = parent.Split('-');

            string returnable = GetIntWithUnderscores(parentspl[0], useOrdinal) + " " +modifier + GetShortFormMissionFromUrl(parentspl[1], variant);

            return returnable;
        }
        private static string GetShortFormDivisionFromParent(string modifier, string parent, int variant, bool useOrdinal)
        {
            string[] parentspl = parent.Split('-');

            StringBuilder returnable = new StringBuilder();
            returnable.Append(GetIntWithUnderscores(parentspl[0], useOrdinal) + " ");
            returnable.Append(modifier);
            returnable.Append(GetShortFormMissionFromUrl(parentspl[1], variant));

            return returnable.ToString();
        }
        private static string GetShortFormMissionFromUrl(string url, int variant)
        {
            switch (url.ToUpper())
            {
                case "CSB":
                    {
                        //Contracting Support Brigade
                        switch (variant)
                        {
                            case 1:
                                return Helper.Constants.ShortForm.ConSupt;
                            case 2:
                                return Helper.Constants.ShortForm.ConSupt + ". " + Helper.Constants.ShortForm.Brigade;
                        }
                        break;
                    }
                case "IBCT":
                    {
                        switch (variant)
                        {
                            case 1:
                                return Helper.Constants.ShortForm.Infantry;
                            case 2:
                                return Helper.Constants.ShortForm.Infantry + ". " + Helper.Constants.ShortForm.BrigadeCT;
                        }
                        break;
                    }
                case "ID":
                    {
                        switch (variant)
                        {
                            case 1:
                                return Helper.Constants.ShortForm.Infantry;
                            case 2:
                                return Helper.Constants.ShortForm.Infantry + ". " + Helper.Constants.ShortForm.Division + ".";
                        }
                        break;
                    }
                case "AD":
                    {
                        switch (variant)
                        {
                            case 1:
                                return Helper.Constants.ShortForm.Armoured;
                            case 2:
                                return Helper.Constants.ShortForm.Armoured + ". " + Helper.Constants.ShortForm.Division + ".";
                        }
                        break;
                    }
                case "CD":
                    {
                        switch (variant)
                        {
                            case 1:
                                return Helper.Constants.ShortForm.Cavalry;
                            case 2:
                                return Helper.Constants.ShortForm.Cavalry + ". " + Helper.Constants.ShortForm.Division + ".";
                        }
                        break;
                    }
                default:
                    {
                        throw new Exception();
                    }
            }
            return "";
        }

        //private static string GetShortFormMission(string mission)
        //{
        //    switch (mission.ToUpper())
        //    {
        //        case "INFANTRY":
        //            { return "Inf"; }
        //        case "ARMOR":
        //        case "ARMOURED":
        //        case "ARMORED":
        //            { return "Arm"; }
        //        case "CAVALRY":
        //            { return  "Cav";  }
        //        case "MARINE":
        //            { return "Mar"; }
        //        default:
        //            { return ""; }
        //    }
        //}
        private static string GetIntWithUnderscores(string nmber, bool useOrdinal)
        {
            if (int.TryParse(nmber, out int result))
            {
                return GetIntWithUnderscores(int.Parse(nmber), useOrdinal);
            }
            else return null;
        }

        private static string GetIntWithUnderscores(int number, bool useOrdinal)
        {
            string append = useOrdinal ? Helper.Helper.AddOrdinal(number) : number.ToString();

            if (number < 10)
            {
                return "____" + append;
            }
            else if (number < 100)
            {
                return "___" + append;
            }
            else if (number < 1000)
            {
                return "__" + append;
            }
            else if (number < 10000)
            {
                return "_" + append;
            }
            else if (number < 100000)
            {
                return append.ToString();
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
                int?[] daterange = string.IsNullOrWhiteSpace(location.DateRange) ? new int?[] { null, null } : GetDateRange(location.DateRange);
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
