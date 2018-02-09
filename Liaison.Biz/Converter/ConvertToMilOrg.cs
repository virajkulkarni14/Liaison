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
            coo.Url = coo.Url.Trim();
            foreach (var hhq in coo.HigherHq)
            {
                hhq.Url = hhq.Url.Trim();
            }

            if (coo.SplitName.Contains(Helper.Constants.LongForm.Detachment) || coo.SplitName.Contains(Helper.Constants.ShortForm.HHD) || coo.SplitName.StartsWith(Helper.Constants.Initials.HHD))
            {
                string mission = "";
                if (coo.SplitName.Contains(Helper.Constants.ShortForm.HHD) || coo.SplitName.Contains(Helper.Constants.Initials.HHD))
                {
                    mission = Helper.Constants.LongForm.HH;
                }
                string urlIdDetPart = coo.Url.Substring(CurrentOpsHelper.ctopsUSArmy.Length);
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
                    CurrentOpsRef = coo.Url.Substring(CurrentOpsHelper.ctops.Length),
                    CurrentOpsUrl = coo.Url,
                    CurrentOpsLogo = GetLogoUrl(coo.LogoUrl),
                    //ServiceId = coo.FullName.EndsWith(CurrentOpsHelper.USArmy) ? Services.Army : Services.Marines,
                    ServiceId = coo.UnitService,
                    Bases = GetBases(coo.Locations),
                    HigherHqs = higher,
                    //ServiceTypeIdx = isAcDc ? ServiceType.AC_RC : ServiceType.Active, 
                    ServiceTypeIdx = coo.UnitComponent,
                    ShortForm = GetShortFormHHDetachment(urlIdDetPart, coo.UnitComponent),
                    ChildOrgs = GetChildOrgs(coo.Children),
                    USState = coo.UnitNGState,
                };
            }
            else if (coo.SplitName.EndsWith(Helper.Constants.LongForm.Command))
            {
                string urlIdCmdPart = coo.Url.Trim().Substring(CurrentOpsHelper.ctopsUSArmy.Length);
                string mission = GetBrigadeMission(coo.SplitName);
                bool bCmdnumber = int.TryParse(new string(coo.SplitName.Where(c => char.IsDigit(c)).ToArray()), out int cmdNumber);
                int? number = bCmdnumber ? (int?)cmdNumber : null;
                string name = null;
                if (!number.HasValue)
                {
                    name = coo.FullName.Replace(", " + CurrentOpsHelper.USArmy, string.Empty);
                }
                return new CommandOrg
                {
                    Number = number,
                    Name = name,
                    UseOrdinal = number.HasValue,
                    Mission = mission,
                    UnitTypeId = UnitType.Command,
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
                    ChildOrgs = GetFirstArmyChildOrds(GetChildOrgs(coo.Children)),
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
                string reff = coo.Url.Trim().Substring(CurrentOpsHelper.ctops.Length).Trim();
                return new BrigadeOrg
                {
                    Number = brigNumber,
                    UseOrdinal = true,
                    Mission = mission,
                    UnitTypeId = UnitType.Brigade,
                    CurrentOpsRef = reff,
                    CurrentOpsUrl = coo.Url.Trim(),
                    CurrentOpsLogo = GetLogoUrl(coo.LogoUrl),
                    Bases = GetBases(coo.Locations),
                    HigherHqs = GetHigherHq(coo.HigherHq),
                    ChildOrgs = GetChildOrgs(coo.Children),
                    ShortForm = GetShortFormBrigade(urlIdBrigPart, coo.UnitComponent),
                    ServiceId = coo.UnitService,
                    ServiceTypeIdx = coo.UnitComponent,
                    USState = coo.UnitNGState,
                    IsBrigadeCombatTeam = coo.SplitName.EndsWith(Liaison.Helper.Constants.LongForm.BrigadeCT),
                    //ServiceTypeIdx = GetBrigadeServiceType(brigNumber),
                    //ServiceId = coo.FullName.EndsWith(Liaison.Helper.CurrentOpsHelper.USArmy)
                    //                ? Services.Army
                    //                    : coo.FullName.EndsWith(Liaison.Helper.CurrentOpsHelper.USMC)
                    //                    ? Services.Marines : Services.Navy,
                };
            }
            else if (coo.SplitName.EndsWith(Helper.Constants.LongForm.Group))
            {
                int grpnumber = int.Parse(new string(coo.SplitName.Where(c => char.IsDigit(c)).ToArray()));
                string mission = GetBrigadeMission(coo.SplitName);
                string urlIdGrpPart = coo.Url.Substring(CurrentOpsHelper.ctopsUSArmy.Length);

                var grp = new GroupOrg
                {
                    Number = grpnumber,
                    UseOrdinal = true,
                    Mission = mission,
                    UnitTypeId = UnitType.Group,
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
                    ShortForm = GetShortFormGroupFromBrigade(GetShortFormBrigade(urlIdGrpPart, coo.UnitComponent)),
                    ServiceId = coo.UnitService,
                    ServiceTypeIdx = coo.UnitComponent,
                    USState = coo.UnitNGState,
                };
                return grp;
            }
            else if (coo.SplitName.Contains(Helper.Constants.LongForm.Battalion))
            {
                if (coo.SplitName.StartsWith(Helper.Constants.LongForm.BattalionHHB))
                {
                    //https://currentops.com/unit/us/army/1-id/hhbn
                    int parentDivisionNumber = GetParentDivisionNumberFromUrl(coo.Url);

                    string parentID = coo.HigherHq[0].Url.Substring(Liaison.Helper.CurrentOpsHelper.ctopsUSArmy.Length);
                    //parentID = parentID.Substring(0, parentID.LastIndexOf('/'));
                    string urlIdDivPart = coo.HigherHq[0].Url.Substring(Liaison.Helper.CurrentOpsHelper.ctopsUSArmy.Length);
                    string modifier = "";
                    if (coo.UnitComponent == ServiceType.Reserve)
                    {
                        modifier = "(" + Helper.Constants.Initials_Component.Reserve + ")";
                    }
                    else if (coo.UnitComponent == ServiceType.Volunteer)
                    {
                        modifier = "(" + Helper.Constants.Initials_Component.Volunteer + ") (" + coo.UnitNGState + ") ";
                    }

                    string parentShortForm = GetShortFormDivisionFromParent(modifier, parentID, 2, true);

                    var bn = new BattalionOrg
                    {
                        ParentAbbrev = parentShortForm,
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
                else if (coo.SplitName.EndsWith(Helper.Constants.LongForm.Battalion))
                {
                    int bnnumber = int.Parse(new string(coo.SplitName.Where(c => char.IsDigit(c)).ToArray()));
                    string[] thisAndParent = coo.SplitName.Split(',');
                    string parentShortForm = thisAndParent.Length > 1 ? thisAndParent[1] : null;

                    string[] numberAndMission = thisAndParent[0].Split(' ');
                    string mission = GetBattalionMission(numberAndMission);

                    return new BattalionOrg
                    {
                        Number = bnnumber,
                        ParentAbbrev = parentShortForm,
                        UseOrdinal = true,
                        Mission = mission,
                        UnitTypeId = UnitType.Battalion,
                        CurrentOpsRef = coo.Url.Trim().Substring(CurrentOpsHelper.ctops.Length),
                        CurrentOpsUrl = coo.Url.Trim(),
                        CurrentOpsLogo = GetLogoUrl(coo.LogoUrl),
                        Bases = GetBases(coo.Locations),
                        HigherHqs = GetHigherHq(coo.HigherHq),
                        ShortForm = GetShortFormBattalion(coo.Url.Substring(CurrentOpsHelper.ctopsUSArmy.Length), coo.UnitComponent),     //Battery(name, mission, number, threeParentShortForm),
                        ServiceId = coo.UnitService,
                        ServiceTypeIdx = coo.UnitComponent,
                        USState = coo.UnitNGState,
                    };
                }
                else if (coo.SplitName.Contains(Helper.Constants.LongForm.Battalion) && coo.SplitName.Contains(Helper.Constants.LongForm.Regiment))
                {
                    var split = coo.SplitName.Split(' ');
                    int bnnumber= int.Parse(new string(split[0].Where(c => char.IsDigit(c)).ToArray()));
                    string mission = "";
                    var splitOffParent = coo.SplitName.Split();
                    string parentAbbrev = GetAbbrevRegimentParent(coo.SplitName.Substring(coo.SplitName.IndexOf(',')+1).Trim());

                    return new BattalionOrg
                    {
                        Number = bnnumber,
                        ParentAbbrev = parentAbbrev,
                        UseOrdinal = true,
                        Mission = mission,
                        UnitTypeId = UnitType.Battalion,
                        CurrentOpsRef = coo.Url.Trim().Substring(CurrentOpsHelper.ctops.Length),
                        CurrentOpsUrl = coo.Url.Trim(),
                        CurrentOpsLogo = GetLogoUrl(coo.LogoUrl),
                        Bases = GetBases(coo.Locations),
                        HigherHqs = GetHigherHq(coo.HigherHq),
                        ShortForm = GetShortFormBattalion(coo.Url.Substring(CurrentOpsHelper.ctopsUSArmy.Length), coo.UnitComponent),     //Battery(name, mission, number, threeParentShortForm),
                        ServiceId = coo.UnitService,
                        ServiceTypeIdx = coo.UnitComponent,
                        USState = coo.UnitNGState,
                    };
                }
                else
                {
                    throw new Exception(coo.FullName + " doesn't work");
                }
            }
            else if (coo.SplitName.Contains(Helper.Constants.LongForm.Company) || (coo.SplitName.StartsWith(Helper.Constants.Initials.HHC)))
            {
                bool isCompany = true;
                coo.SplitName = coo.SplitName.Replace(Helper.Constants.Initials.HHC, Helper.Constants.LongForm.HH + " " + Helper.Constants.LongForm.Company);
                ServiceType serviceType = ServiceType.Active;
                string parentID = coo.HigherHq[0].Url.Substring(Liaison.Helper.CurrentOpsHelper.ctopsUSArmy.Length);
                var coysplit = coo.SplitName.Split(',');
                var namesplit = coysplit[0].Remove(coysplit[0].IndexOf(Helper.Constants.LongForm.Company), 7);
                string namesplitTrim = namesplit.Trim();
                string urlIdPart;
                string parentShortForm;
                List<ShortForm> threeParentShortForm;

                string modifier = GetModifier(coo.UnitComponent, coo.UnitNGState);

                if (parentID.Contains('/'))
                {
                    var divsplit = parentID.Split('/');
                    parentShortForm = GetShortFormBattalionFromParent(divsplit[1]) + ", " + GetShortFormDivisionFromParent(modifier, divsplit[0], 2, true);
                    serviceType = GetDivisionServiceType(GetParentDivisionNumberFromUrl(divsplit[0]));
                    urlIdPart = divsplit[0];
                    threeParentShortForm = GetShortFormHHQBattalion(GetShortFormDivision(urlIdPart, coo.UnitComponent));
                }
                else if (parentID.Contains("in-bde"))
                {
                    isCompany = true;
                    var bdesplit = parentID.Split('-');
                    parentShortForm = GetShortFormBrigadeFromParent(modifier, parentID, 2, true);
                    serviceType = GetBrigadeServiceType(int.Parse(bdesplit[0]));
                    urlIdPart = parentID;
                    threeParentShortForm = GetShortFormHHCompany(GetShortFormBrigade(urlIdPart, coo.UnitComponent));
                }
                else if (parentID.Contains("-ibct"))
                {
                    var bdesplit = parentID.Split('-');
                    parentShortForm = GetShortFormBrigadeFromParent(modifier, parentID, 2, true);
                    serviceType = GetBrigadeServiceType(int.Parse(bdesplit[0]));
                    urlIdPart = parentID;
                    threeParentShortForm = GetShortFormHHCompany(GetShortFormBrigade(urlIdPart, coo.UnitComponent));
                }
                else if (parentID.Contains("armored-bde"))
                {
                    isCompany = false;
                    var bdesplit = parentID.Split('-');
                    parentShortForm = GetShortFormBrigadeFromParent(modifier, parentID, 2, true);
                    serviceType = GetBrigadeServiceType(int.Parse(bdesplit[0]));
                    urlIdPart = parentID;
                    threeParentShortForm = GetShortFormHHCompany(GetShortFormBrigade(urlIdPart, coo.UnitComponent));
                }
                else if (parentID.EndsWith("avn-bde"))
                {
                    isCompany = false;
                    var bdesplit = parentID.Split('-');
                    parentShortForm = GetShortFormBrigadeFromParent(modifier, parentID, 2, true);
                    serviceType = GetBrigadeServiceType(int.Parse(bdesplit[0]));
                    urlIdPart = parentID;
                    threeParentShortForm = GetShortFormHHCompany(GetShortFormBrigade(urlIdPart, coo.UnitComponent));
                }
                else
                {
                    throw new Exception("parentID: " + parentID);
                }

                string fullname = null;
                string name = null;
                string mission = null;
                int? number = null;

                if (namesplitTrim.Length > 1)
                {
                    fullname = mission + Helper.Constants.ShortForm.Company;
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
                if (isCompany)
                {
                    return new CompanyOrg
                    {
                        Name = name,
                        ParentAbbrev = parentShortForm,
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
                else
                {
                    return new Squadron_CavalryOrg
                    {
                        Name = name,
                        ParentAbbrev = parentShortForm,
                        Number = number,
                        Mission = mission,
                        UnitTypeId = UnitType.SquadronCavalry,
                        CurrentOpsRef = coo.Url.Trim().Substring(Liaison.Helper.CurrentOpsHelper.ctops.Length),
                        CurrentOpsUrl = coo.Url.Trim(),
                        CurrentOpsLogo = GetLogoUrl(coo.LogoUrl),
                        //ServiceId = GetServiceId(coo.Url.Trim()),
                        Bases = GetBases(coo.Locations),
                        HigherHqs = GetHigherHq(coo.HigherHq),
                        //ServiceTypeIdx = serviceType,
                        ShortForm = GetShortFormSquadronCavalry(name, mission, number, threeParentShortForm),
                        ServiceId = coo.UnitService,
                        ServiceTypeIdx = coo.UnitComponent,
                        USState = coo.UnitNGState,
                    };
                }
            }
            else if (coo.SplitName.Contains(Helper.Constants.LongForm.Battery) || (coo.SplitName.StartsWith(Helper.Constants.Initials.HHB) && coo.SplitName.Contains("FA")))
            {
                ServiceType serviceType = ServiceType.Active;
                string parentID = coo.HigherHq[0].Url.Substring(Liaison.Helper.CurrentOpsHelper.ctopsUSArmy.Length);
                var trpsplit = coo.SplitName.Split(',');
                string namesplit = trpsplit[0].Replace(Helper.Constants.LongForm.Battery, string.Empty);
                string namesplitTrim = namesplit.Trim();
                string urlIdPart;
                string parentShortForm;
                List<ShortForm> threeParentShortForm;


                string modifier = GetModifier(coo.UnitComponent, coo.UnitNGState);

                if (parentID.Contains("fa-bde"))
                {
                    var bdesplit = parentID.Split('-');
                    parentShortForm = GetShortFormBrigadeFromParent(modifier, parentID, 2, true);
                    serviceType = GetBrigadeServiceType(int.Parse(bdesplit[0]));
                    urlIdPart = parentID;
                    threeParentShortForm = GetShortFormHHCompany(GetShortFormBrigade(urlIdPart, coo.UnitComponent));
                }
                else
                {
                    throw new Exception();
                }


                string fullname = null;
                string name = null;
                string mission = null;
                int? number = null;


                if (namesplitTrim.Length > 1)
                {
                    fullname = mission + Helper.Constants.ShortForm.Squadron;
                    name = null;
                    mission = namesplitTrim == Helper.Constants.Initials.HHB
                                ? Helper.Constants.LongForm.HH
                                : namesplitTrim;
                    number = null;
                }
                else if (namesplitTrim.Length == 1)
                {
                    name = namesplitTrim;
                    mission = namesplitTrim == Helper.Constants.Initials.HHB
                                ? Helper.Constants.LongForm.HH
                                : null;
                    number = null;
                }
                else
                {
                    throw new Exception();
                }



                return new BatteryOrg
                {
                    ParentShortForm = parentShortForm,
                    Mission = mission,
                    UnitTypeId = UnitType.Battery,
                    CurrentOpsRef = coo.Url.Trim().Substring(CurrentOpsHelper.ctops.Length),
                    CurrentOpsUrl = coo.Url.Trim(),
                    CurrentOpsLogo = GetLogoUrl(coo.LogoUrl),
                    Bases = GetBases(coo.Locations),
                    HigherHqs = GetHigherHq(coo.HigherHq),
                    ShortForm = GetShortFormBattery(name, mission, number, threeParentShortForm),
                    ServiceId = coo.UnitService,
                    ServiceTypeIdx = coo.UnitComponent,
                    USState = coo.UnitNGState,
                };
            }
            else if (coo.SplitName.Contains(Helper.Constants.LongForm.Troop))
            {
                ServiceType serviceType = ServiceType.Active;
                string parentID = coo.HigherHq[0].Url.Substring(Liaison.Helper.CurrentOpsHelper.ctopsUSArmy.Length);
                var trpsplit = coo.SplitName.Split(',');
                string namesplit = trpsplit[0].Replace(Helper.Constants.LongForm.Troop, string.Empty);
                string namesplitTrim = namesplit.Trim();
                string urlIdPart;
                string parentShortForm;
                List<ShortForm> threeParentShortForm;


                string modifier = GetModifier(coo.UnitComponent, coo.UnitNGState);

                if (parentID.Contains("cav-bde"))
                {
                    var bdesplit = parentID.Split('-');
                    parentShortForm = GetShortFormBrigadeFromParent(modifier, parentID, 2, true);
                    serviceType = GetBrigadeServiceType(int.Parse(bdesplit[0]));
                    urlIdPart = parentID;
                    threeParentShortForm = GetShortFormHHCompany(GetShortFormBrigade(urlIdPart, coo.UnitComponent));
                }
                else
                {
                    throw new Exception();
                }


                string fullname = null;
                string name = null;
                string mission = null;
                int? number = null;


                if (namesplitTrim.Length > 1)
                {
                    fullname = mission + Helper.Constants.ShortForm.Squadron;
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
                else
                {
                    throw new Exception();
                }



                return new Squadron_CavalryOrg
                {
                    ParentAbbrev = parentShortForm,
                    Mission = mission,
                    UnitTypeId = UnitType.SquadronCavalry,
                    CurrentOpsRef = coo.Url.Trim().Substring(CurrentOpsHelper.ctops.Length),
                    CurrentOpsUrl = coo.Url.Trim(),
                    CurrentOpsLogo = GetLogoUrl(coo.LogoUrl),
                    Bases = GetBases(coo.Locations),
                    HigherHqs = GetHigherHq(coo.HigherHq),
                    ShortForm = GetShortFormSquadronCavalry(name, mission, number, threeParentShortForm),
                    ServiceId = coo.UnitService,
                    ServiceTypeIdx = coo.UnitComponent,
                    USState = coo.UnitNGState,
                };
            }
            else
            {
                throw new Exception(coo.SplitName + "/" + coo.FullName + " not found");
            }
        }

        private static string GetAbbrevRegimentParent(string v)
        {
             var split = v.Split(' ');
             int rgtnumber = int.Parse(new string(split[0].Where(c => char.IsDigit(c)).ToArray()));
            var rgtstring = Helper.Helper.GetIntWithUnderscores(rgtnumber, false);


            string returnable = v.Replace(rgtnumber.ToString(), rgtstring)
                .Replace(Helper.Constants.LongForm.Regiment, Helper.Constants.ShortForm.Regiment + ".");
            return returnable;

        }

        private static string GetBattalionMission(string[] numberAndMission)
        {
            StringBuilder sb = new StringBuilder();
            for (int i=0; i<numberAndMission.Length;i++)
            {
                if (i!=0 && i!=numberAndMission.Length-1)
                {
                    sb.Append(numberAndMission[i]+" ");
                }
            }
            return sb.ToString().Trim();
        }

        private static List<ShortForm> GetShortFormGroupFromBrigade(List<ShortForm> list)
        {
            var returnable = new List<ShortForm>();
            foreach (var thing in list)
            {
                var newthing = new ShortForm();
                newthing.Type = thing.Type;
                newthing.Text = thing.Text.Replace(Helper.Constants.ShortForm.Brigade.ToUpper(), Helper.Constants.ShortForm.Group.ToUpper());
                
                if (thing.Type == ShortFormType.IndexName)
                {
                    newthing.Type = ShortFormType.Placeholder;                   
                    returnable.Add(new ShortForm
                    {
                        Type = ShortFormType.IndexName,
                        Text = thing.Text.Replace(
                        Helper.Constants.Initials_Service.Army.ToString() + Helper.Constants.Symbol.brigade,
                        Helper.Constants.Initials_Service.Army.ToString() + Helper.Constants.Symbol.regiment)
                    });
                }
                returnable.Add(newthing);
            }
            return returnable;
        }

        private static string GetModifier(ServiceType unitComponent, string unitNGState)
        {
            if (unitComponent == ServiceType.Reserve)
            {
                return "(" + Helper.Constants.Initials_Component.Reserve + ")";
            }
            else if (unitComponent == ServiceType.Volunteer)
            {
                return "(" + Helper.Constants.Initials_Component.Volunteer + ") (" + unitNGState + ") ";
            }
            return "";
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
            return sb.ToString().Replace(" Brigade Combat", "").Replace("Armored", "Armoured");
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
                    Name = child.FullName.Replace("United States", "")
                                        .Replace("Armored", Helper.Constants.LongForm.Armoured).Trim(),
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
            string sNumber = Helper.Helper.GetIntWithUnderscores(split[0], false);
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
                    Text=Helper.Constants.Initials_Service.Army.ToString()+Helper.Constants.Symbol.fieldarmy.ToString()+ sNumber,
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
            else if (urlIdBrigPart.EndsWith("-in-bde") || urlIdBrigPart.EndsWith("-cav-bde") || urlIdBrigPart.EndsWith("armored-bde"))
            {
                sb.Append(ack_r.First());
                sb.Append(Helper.Constants.ShortForm.Brigade.First().ToString());
            }
            else if (urlIdBrigPart.EndsWith("avn-bde"))
            {
                sb.Append(ack_r + " ");
                sb.Append(Helper.Constants.ShortForm.Brigade.ToUpper());
            }
            else if (urlIdBrigPart.EndsWith("-fa-bde"))
            {
                string[] words = ack_r.Split(' ');
                foreach (var word in words)
                {
                    sb.Append(word.First());
                }
                sb.Append(Helper.Constants.ShortForm.Brigade.First().ToString());
            }
            else if (urlIdBrigPart.EndsWith("-msg"))
            {
                string[] words = ack_r.Split(' ');
                foreach (var word in words)
                {
                    sb.Append(word.First());
                }
                sb.Append(Helper.Constants.ShortForm.Group.First().ToString());
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

        private static string GetIndexShortForm(string mission, string symbol, Helper.Enumerators.Services svc)
        {
            StringBuilder sb = new StringBuilder();
            switch (svc)
            {
                case Services.Army:
                    sb.Append(Helper.Constants.Initials_Service.Army);
                    break;
                default:
                    throw new Exception();
            }
            sb.Append(symbol);
            switch (mission.ToLower())
            {
                case "avn":
                    sb.Append(Helper.Constants.ShortForm.Aviation.ToUpper());
                    break;
                case "armored":
                    sb.Append(Helper.Constants.ShortForm.Armoured.ToUpper());
                    break;
                case "cav":
                    sb.Append(Helper.Constants.ShortForm.Cavalry.ToUpper());
                    break;
                case "fa":
                    sb.Append(Helper.Constants.ShortForm.Artilery.ToUpper());
                    break;
                case "in":
                case "ibct":
                    sb.Append(Helper.Constants.ShortForm.Infantry.ToUpper());
                    break;
                case "csb":
                    sb.Append(Helper.Constants.ShortForm.ConSupt.ToUpper());
                    break;
                case "msg":
                    sb.Append(Helper.Constants.ShortForm.MobilisationSupt.ToUpper());
                    break;
                default:
                    throw new Exception(mission.ToLower() + " not there");
            }

            return sb.ToString();
        }
        private static string GetShortFormBattalionFromParent(string v)
        {
            if (v == "hhbn")
            {
                return "HHQ Bn.";
            }
            return "";
        }
        private static List<ShortForm> GetShortFormBattalion(string url, ServiceType unitComponent)
        {
            string[] split = url.Split('-');
            string modifier = "";

            if (unitComponent == ServiceType.Reserve)
            {
                modifier = "(" + Helper.Constants.Initials_Component.Reserve + ") ";
            }
            else if (unitComponent == ServiceType.Volunteer)
            {
                modifier = "(" + Helper.Constants.Initials_Component.Volunteer + ") ";
            }

            if (url.EndsWith("msbn"))
            {
                string number = Helper.Helper.GetIntWithUnderscores(split[0], false);
                string mobsuptbn = Helper.Constants.ShortForm.MobilisationSupt.ToUpper() + ".";
                return new List<ShortForm>
                {
                    new ShortForm
                    {
                        Text=number+" "+modifier+mobsuptbn+" "+ Helper.Constants.ShortForm.Battalion.ToUpper()+"., "+Helper.Constants.ShortForm.GSC,
                        Type=Helper.Enumerators.ShortFormType.ShortName,
                    },
                    new ShortForm
                    {
                        Text=Helper.Constants.ShortForm.GSC+Helper.Constants.Symbol.battalion+number,
                        Type=ShortFormType.IndexName,
                    },
                    new ShortForm
                    {
                        Text=Helper.Constants.Initials_Service.Army+Helper.Constants.Symbol.battalion+Helper.Constants.ShortForm.GSC+number,
                        Type=ShortFormType.Placeholder,
                    },
                    new ShortForm
                    {
                        Text=number+" "+mobsuptbn.Replace(".", "")+Helper.Constants.Symbol.other_bn+Helper.Constants.ShortForm.GSC,
                        Type=ShortFormType.Other,
                    }

                };
            }
            else if (url.EndsWith("bn"))
            {
                var rgtbn = url.Split('/');
                var rgt = rgtbn[0].Split('-');
                var bn = rgtbn[1].Split('-');

                var bnnumb = Helper.Helper.GetIntWithUnderscores(bn[0], false);
                var rgtnumb = Helper.Helper.GetIntWithUnderscores(rgt[0], false);

                var rgtname = "";
                var idxname = "";

                if (rgt[1] == "rgt")
                {
                    rgtname = Helper.Constants.ShortForm.Infantry + ". " + Helper.Constants.ShortForm.Regiment + ".";
                    idxname = Helper.Constants.ShortForm.Infantry.ToUpper();
                }

                return new List<ShortForm>
                {
                    new ShortForm
                    {
                        Text = bnnumb + " " + modifier + Helper.Constants.ShortForm.Battalion.ToUpper() + "., " + rgtnumb + " " + rgtname.ToUpper(),
                        Type = ShortFormType.ShortName,
                    },
                    new ShortForm
                    {
                        Text = idxname + rgtnumb + Helper.Constants.Symbol.battalion + bnnumb,
                        Type = ShortFormType.IndexName,
                    },
                    new ShortForm
                    {
                        Text = bnnumb + Helper.Constants.Symbol.other_bn + rgtnumb + " " + rgtname.ToUpper().Replace(".", ""),
                        Type = ShortFormType.Other,
                    }
                };
            }
            throw new NotImplementedException("url: " + url + ", unitComponent: " + unitComponent);
        }
        private static List<ShortForm> GetShortFormCommand(string urlIdCmdPart)
        {
            urlIdCmdPart = urlIdCmdPart.Trim();
            string[] split = urlIdCmdPart.Split('-');
            string sNumber = Helper.Helper.GetIntWithUnderscores(split[0], false);

            if (int.TryParse(split[0], out int result))
            {
                return new List<ShortForm>
                {
                    new ShortForm
                    {
                        Text = sNumber+" "+ split[1].ToUpper(),
                        Type=ShortFormType.ShortName,
                    },
                    new ShortForm
                    {
                        Text=GetCommandShortFormIndex(split, sNumber),
                        Type=ShortFormType.IndexName,
                    },
                    new ShortForm
                    {
                        Text= sNumber+" "+ split[1].ToUpper(),
                        Type=ShortFormType.Other,
                    }
                };
            }
            else
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
        }

        private static string GetCommandShortFormIndex(string[] split, string sNumber)
        {
            if (split[1]=="arsc")
            {
                return Helper.Constants.Initials_Service.Army.ToString() + Helper.Constants.Symbol.brigade + Helper.Constants.ShortForm.Support.ToUpper() + sNumber;
            }
            else if (split[0]=="amc")
            {
                return split[0].ToUpper();
            }
            else
            {
                throw new Exception(string.Join("-", split) + " not found");
            }
        }

        private static string GetCommandShortFormIndex(string urlIdCmdPart)
        {

            if (urlIdCmdPart == "amc")
            {
                return urlIdCmdPart.ToUpper();
            }
            else if (urlIdCmdPart.Contains("arsc"))
            {
                return urlIdCmdPart.ToUpper();
            }
            else
            {
                throw new Exception(urlIdCmdPart + " not found");
            }
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
                    item.Text = item.Text.StartsWith("H") ? item.Text: "_____/" + item.Text;
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
            urlIdBrigPart = urlIdBrigPart.Trim();
            string[] split = urlIdBrigPart.Split('-');  
            string sNumber = Helper.Helper.GetIntWithUnderscores(split[0], false);
            string ack = GetShortFormMissionFromUrl(split[1], 1).Substring(0);
            string index = GetIndexShortForm(split[1], Helper.Constants.Symbol.brigade, Services.Army);
            string modifier = "";
            if (servicetype == ServiceType.Reserve)
            {
                modifier = "(" + Helper.Constants.Initials_Component.Reserve + ") ";
            }
            else if (servicetype == ServiceType.Volunteer)
            {
                modifier = "(" + Helper.Constants.Initials_Component.Volunteer+ ") ";
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
                    Text=index.ToUpper().Replace(" ", "").Replace(".", "")+sNumber,
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
            string sNumber = Helper.Helper.GetIntWithUnderscores(split[0], false);
            if (split[1] == "army/div")
            {
                // urlIdDivPart = "/1-army/div-east\n"
                split = urlIdDivPart.Split('/');
                var army = split[0].Split('-');
                string iwu = Helper.Helper.GetIntWithUnderscores(army[0], false);
                string armyname = split[1].Split('-')[1].Trim();

                return new List<ShortForm>()
                {
                    new ShortForm
                    {
                        Text=(Helper.Constants.ShortForm.Division+". "+CultureInfo.CurrentCulture.TextInfo.ToTitleCase(armyname)+", "+iwu+" "+Helper.Constants.ShortForm.FieldArmy).ToUpper(),//"+CultureInfo.CurrentCulture.TextInfo.ToTitleCase(army[1]),
                        Type = ShortFormType.ShortName
                    },
                    new ShortForm
                    {
                        Text=Helper.Constants.Initials_Service.Army.ToString()+Helper.Constants.Symbol.fieldarmy.ToString()+iwu+Helper.Constants.Symbol.division.ToString()+armyname.ToUpper(),
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
                    modifier = "(" + Helper.Constants.Initials_Component.Reserve + ") ";
                }
                else if (servicetype == ServiceType.Volunteer)
                {
                    modifier = "(" + Helper.Constants.Initials_Component.Volunteer + ") ";
                }

                var aaaa = new List<ShortForm>()
                {
                    new ShortForm
                    {
                        Text = (sNumber+" "+modifier + ack + ". " + Helper.Constants.ShortForm.Division + ".").ToUpper(),
                        Type = Helper.Enumerators.ShortFormType.ShortName
                    },
                    new ShortForm
                    {
                        Text = Helper.Constants.Initials_Service.Army + ")" + ack.ToUpper() + sNumber,
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
        private static List<ShortForm> GetShortFormBattery(string name, string mission, int? number, List<ShortForm> list)
        {
            string additionalShortName = "";
            string additionalIndexName = "";
            string additionalOtherName = "";
            if (!string.IsNullOrEmpty(name))
            {
                additionalShortName = name + " " + Helper.Constants.ShortForm.Battery.ToUpper() + ".";
                additionalIndexName = Helper.Constants.Symbol.battery + name;
                additionalOtherName = name;
            }
            else if (mission == Helper.Constants.LongForm.HH)
            {
                additionalShortName = Helper.Constants.ShortForm.HHBty.ToUpper() + ".";
                additionalIndexName = Helper.Constants.Symbol.battery + Helper.Constants.Symbol.hq.ToString();
                additionalOtherName = Helper.Constants.Initials.HHBty;
            }
            else if (mission == Helper.Constants.LongForm.HS)
            {
                additionalShortName = Helper.Constants.ShortForm.HSBty.ToUpper() + ".";
                additionalIndexName = Helper.Constants.Symbol.battery + Helper.Constants.Symbol.hq.ToString();
                additionalOtherName = Helper.Constants.Initials.HSBty;
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
        private static List<ShortForm> GetShortFormSquadronCavalry(string name, string mission, int? number, List<ShortForm> list)
        {

            string additionalShortName = "";
            string additionalIndexName = "";
            string additionalOtherName = "";
            if (!string.IsNullOrEmpty(name))
            {
                additionalShortName = name + " " + Helper.Constants.ShortForm.Squadron.ToUpper() + ".";
                additionalIndexName = Helper.Constants.Symbol.company + name;
                additionalOtherName = name;
            }
            else if (mission == Helper.Constants.LongForm.HH)
            {
                additionalShortName = Helper.Constants.ShortForm.HHS.ToUpper() + ".";
                additionalIndexName = Helper.Constants.Symbol.company + Helper.Constants.Symbol.hq.ToString();
                additionalOtherName = Helper.Constants.Initials.HHS;
            }
            else if (mission == Helper.Constants.LongForm.HS)
            {
                additionalShortName = Helper.Constants.ShortForm.HSS.ToUpper() + ".";
                additionalIndexName = Helper.Constants.Symbol.company + Helper.Constants.Symbol.hq.ToString();
                additionalOtherName = Helper.Constants.Initials.HSS;
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

        private static List<ShortForm> GetShortFormHHDetachment(string urlIdDetPart, ServiceType servicetype)
        {
            var returnable = new List<ShortForm>();

           
            var split = urlIdDetPart.Split('/');
            if (split[2].Trim() == "hhd")
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

            string returnable = Helper.Helper.GetIntWithUnderscores(parentspl[0], useOrdinal) + " " +modifier + GetShortFormMissionFromUrl(parentspl[1], variant);

            return returnable;
        }
        private static string GetShortFormDivisionFromParent(string modifier, string parent, int variant, bool useOrdinal)
        {
            string[] parentspl = parent.Split('-');

            StringBuilder returnable = new StringBuilder();
            returnable.Append(Helper.Helper.GetIntWithUnderscores(parentspl[0], useOrdinal) + " ");
            returnable.Append(modifier);
            returnable.Append(GetShortFormMissionFromUrl(parentspl[1], variant));

            return returnable.ToString();
        }
        private static string GetShortFormMissionFromUrl(string url, int variant)
        {
            switch (url.ToUpper())
            {
                case "AVN":
                    {
                        switch(variant)
                        {
                            case 1:
                                return Helper.Constants.ShortForm.Aviation;
                            case 2:
                                return Helper.Constants.ShortForm.Aviation + ". " + Helper.Constants.ShortForm.Brigade + ".";                                
                        }
                        break;
                    }
                case "ARMORED":
                    {
                        switch (variant)
                        {
                            case 1:
                                return Helper.Constants.ShortForm.Armoured;
                            case 2:
                                return Helper.Constants.ShortForm.Armoured + ". " + Helper.Constants.ShortForm.Brigade + ".";
                        }
                        break;
                    }
                case "CAV":
                    {
                        //Cavalry
                        switch (variant)
                        {
                            case 1:
                                return Helper.Constants.ShortForm.Cavalry;
                            case 2:
                                return Helper.Constants.ShortForm.Cavalry + ". " + Helper.Constants.ShortForm.Brigade + ".";
                        }
                        break;
                    }
                case "CSB":
                    {
                        //Contracting Support Brigade
                        switch (variant)
                        {
                            case 1:
                                return Helper.Constants.ShortForm.ConSupt;
                            case 2:
                                return Helper.Constants.ShortForm.ConSupt + ". " + Helper.Constants.ShortForm.Brigade + ".";
                        }
                        break;
                    }
                case "MSG":
                    {
                        // Mobilisation Support Group
                        switch (variant)
                        {
                            case 1:
                                return Helper.Constants.ShortForm.MobilisationSupt;
                        }
                        break;
                    }
                case "IN":
                case "IBCT":
                    {
                        switch (variant)
                        {
                            case 1:
                                return Helper.Constants.ShortForm.Infantry;
                            case 2:
                                return url.ToUpper() == "IN"
                                    ? Helper.Constants.ShortForm.Infantry + ". " + Helper.Constants.ShortForm.Brigade+"."
                                    : Helper.Constants.ShortForm.Infantry + ". " + Helper.Constants.ShortForm.BrigadeCT;
                        }
                        break;
                    }
                case "FA":
                    {
                        switch (variant)
                        {
                            case 1:
                                return Helper.Constants.ShortForm.ArtilleryField;
                            case 2:
                                return Helper.Constants.ShortForm.ArtilleryField + ". " + Helper.Constants.ShortForm.Brigade + ".";
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
                        throw new Exception("url not found: "+ url);
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
            bool isCurrent = false;
            List<HigherHqType> alreadyHasCurrent = new List<HigherHqType>();
            foreach (var high in higherHqs)
            {
                var higherhq = new HigherHqOrg();
                int?[] daterange = GetDateRange(high.DateRange);
                
                var crt = GetCommandRelationshipType(high.Type);
                isCurrent = !alreadyHasCurrent.Contains(crt) && (high.IsCurrent|| daterange[1] == null);

                if (isCurrent)
                {
                    alreadyHasCurrent.Add(crt);
                    alreadyHasCurrent.Add(HigherHqType.Unknown);
                }

                HigherHqOrg hh = new HigherHqOrg
                {
                    CurrentOpsRef = high.Id,
                    DateFrom = daterange[0],
                    DateUntil = daterange[1],
                    IsCurrent = isCurrent,
                    CommandRelationshipType = crt,
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
                case "ADCON":
                    {
                        return HigherHqType.ADCON;
                    }
                case "OPCON":
                    {
                        return HigherHqType.OPCON;
                    }
                case null:
                case "":
                    {
                        return HigherHqType.Unknown;
                    }
                 
            }
            throw new Exception("GetCommandRelationshipType: type: " + type);
        }

        private static List<BaseOrg> GetBases(List<LocationObject> locations)
        {
            var returnable = new List<BaseOrg>();
            bool isCurrent = false;
            bool alreadyHasCurrent = false;
            foreach (var location in locations)
            {
                int?[] daterange = string.IsNullOrWhiteSpace(location.DateRange) ? new int?[] { null, null } : GetDateRange(location.DateRange);
                // isCurrent is true if location.IsCurrent is true or the date range ends with '- Present' or (the date range is entirely null and if alreadyHasCurrent is false) 
                isCurrent = location.IsCurrent || location.DateRange.EndsWith("Present") || ((daterange[0] == null && daterange[1] == null) && !alreadyHasCurrent);
                if (isCurrent)
                {
                    alreadyHasCurrent = true;
                }

                string parentBase = null;
                string city = location.Location;
                if (location.Location.Contains("|"))
                {
                    parentBase = location.Location.Substring(0, location.Location.IndexOf('|')).Trim();
                    city = location.Location.Substring(location.Location.IndexOf('|')+1).Trim();
                }

                BaseOrg bo = new BaseOrg
                {
                    Name = location.BaseName.Replace("United States ", "").Replace("Center", "Centre"),
                    Location = city,
                    ParentBase=parentBase,
                    CurrentOpsUrl = location.Url.Trim(),
                    DateFrom = daterange[0],
                    DateUntil = daterange[1],
                    IsCurrent = isCurrent,
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
