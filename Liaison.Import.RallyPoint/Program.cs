using HtmlAgilityPack;
using Liaison.Biz.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Xml;
using System.Xml.Serialization;


namespace Liaison.Import.RallyPoint
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var webClient = new WebClient())
            {
                string[] urls = {
                    //"https://www.rallypoint.com/units/centcom-us-central-command-macdill-afb-fl", 
                    "https://www.rallypoint.com/units/white-house-washington-dc",
                    "https://www.rallypoint.com/units/osd-office-of-secretary-of-defense-pentagon-va",
                    "https://www.rallypoint.com/units/jcs-office-of-the-joint-chiefs-of-staff-pentagon-va",
                    "https://www.rallypoint.com/units/pacom-us-pacific-command-camp-smith-hi",
                    "https://www.rallypoint.com/units/eucom-us-european-command-patch-barracks-germany",
                    "https://www.rallypoint.com/units/africom-us-africa-command-kelley-barracks-germany",
                    "https://www.rallypoint.com/units/northcom-us-northern-command-peterson-afb-co",
                    "https://www.rallypoint.com/units/southcom-us-southern-command-miami-fl",
                    "https://www.rallypoint.com/units/socom-us-special-operations-command-macdill-afb-fl",
                    "https://www.rallypoint.com/units/stratcom-us-strategic-command-offutt-afb-ne",
                    "https://www.rallypoint.com/units/transcom-us-transportation-command-scott-afb-il",
                    "https://www.rallypoint.com/units/nato-nato-north-atlantic-treaty-organization-brussels-belgium",
                    "https://www.rallypoint.com/units/aafes-army-air-force-exchange-service-dallas-tx",
                    "https://www.rallypoint.com/units/non-dod-non-department-of-defense-organizations",
                    "https://www.rallypoint.com/units/hqda-headquarters-department-of-the-army-pentagon-va",
                    "https://www.rallypoint.com/units/forscom-us-army-forces-command-fort-bragg-nc",
                    "https://www.rallypoint.com/units/usarpac-us-army-pacific-fort-shafter-hi",
                    "https://www.rallypoint.com/units/usareur-us-army-europe-heidelberg-germany",
                    "https://www.rallypoint.com/units/usarnorth-us-army-north-fifth-army-joint-base-san-antonio-tx",
                    "https://www.rallypoint.com/units/usarsouth-us-army-south-sixth-army-joint-base-san-antonio-tx",
                    "https://www.rallypoint.com/units/usarcent-formerly-third-army-us-army-central-third-army-shaw-afb-sc",
                    "https://www.rallypoint.com/units/usaraf-setaf-us-army-africa-southern-european-task-force-caserma-ederle-italy",
                    "https://www.rallypoint.com/units/usasoc-us-army-special-operations-command-fort-bragg-nc",
                    "https://www.rallypoint.com/units/tradoc-us-army-training-doctrine-command-joint-base-langley-eustis-jble-va",
                    "https://www.rallypoint.com/units/amc-us-army-materiel-command-army-redstone-arsenal-al",
                    "https://www.rallypoint.com/units/usarng-us-army-national-guard-arlington-va",
                    "https://www.rallypoint.com/units/usarc-us-army-reserve-command-fort-bragg-nc",
                    "https://www.rallypoint.com/units/medcom-us-army-medical-command-joint-base-san-antonio-tx",
                    "https://www.rallypoint.com/units/imcom-installation-management-command-joint-base-san-antonio-tx",
                    "https://www.rallypoint.com/units/usma-united-states-military-academy-west-point-ny",
                    "https://www.rallypoint.com/units/usace-us-army-corps-of-engineers-washington-dc",
                    "https://www.rallypoint.com/units/usacidc-us-army-criminal-investigation-command-mcb-quantico-va",
                    "https://www.rallypoint.com/units/inscom-us-army-intelligence-security-command-fort-belvoir-va",
                    "https://www.rallypoint.com/units/msddc-military-surface-deployment-distribution-command-scott-afb-il",
                    "https://www.rallypoint.com/units/mdw-us-army-military-district-of-washington-direct-reporting-units-fort-mcnair-dc",
"https://www.rallypoint.com/units/atec-us-army-test-evaluation-command-alexandria-va",
"https://www.rallypoint.com/units/hqmc-headquarters-marine-corps-washington-dc",
"https://www.rallypoint.com/units/i-mef-1st-marine-expeditionary-force-1st-mef-camp-pendleton-ca",
"https://www.rallypoint.com/units/ii-mef-2nd-marine-expeditionary-force-2nd-mef-camp-lejeune-nc",
"https://www.rallypoint.com/units/iii-mef-3rd-marine-expeditionary-force-3rd-mef-camp-courtney-japan",
"https://www.rallypoint.com/units/marforres-marine-forces-reserve-mcsf-new-orleans-la",
"https://www.rallypoint.com/units/marsoc-marine-special-operations-command-camp-lejeune-nc",
"https://www.rallypoint.com/units/marine-fleet-commands-marine-corps",
"https://www.rallypoint.com/units/secnav-secretary-of-the-navy-pentagon-va",
"https://www.rallypoint.com/units/opnav-office-of-the-chief-of-naval-operations-pentagon-va",
"https://www.rallypoint.com/units/compacflt-commander-us-pacific-fleet-joint-base-pearl-harbor-hickam-hi",
"https://www.rallypoint.com/units/usfltforcom-us-fleet-forces-command-naval-station-norfolk-va",
"https://www.rallypoint.com/units/nswc-naval-special-warfare-command-coronado-ca",
"https://www.rallypoint.com/units/comnavresfor-commander-navy-reserve-forces-naval-station-norfolk-va",
"https://www.rallypoint.com/units/shore-commands",
"https://www.rallypoint.com/units/navcent-naval-forces-central-command-naval-support-activity-bahrain-bahrain",
"https://www.rallypoint.com/units/cne-cna-c6f-navy-europe-navy-africa-naval-support-activity-naples-italy",
"https://www.rallypoint.com/units/decommissioned-ships",
"https://www.rallypoint.com/units/planned-ships",
"https://www.rallypoint.com/units/hqdaf-headquarters-department-of-the-air-force",
"https://www.rallypoint.com/units/usafe-us-air-forces-in-europe-air-force-ramstein-ab-germany",
"https://www.rallypoint.com/units/pacaf-pacific-air-forces-joint-base-pearl-harbor-hickam-hi",
"https://www.rallypoint.com/units/afspc-air-force-space-command-peterson-afb-co",
"https://www.rallypoint.com/units/acc-air-combat-command-air-force-joint-base-langley-eustis-jble-va",
"https://www.rallypoint.com/units/aetc-air-education-training-command-joint-base-san-antonio-tx",
"https://www.rallypoint.com/units/afgsc-air-force-global-strike-command-barksdale-afb-la",
"https://www.rallypoint.com/units/afsoc-air-force-special-operations-command-hurlburt-field-fl",
"https://www.rallypoint.com/units/amc-air-mobility-command-air-force-scott-afb-il",
"https://www.rallypoint.com/units/afrc-air-force-reserve-command-robins-afb-ga",
"https://www.rallypoint.com/units/ang-air-national-guard-pentagon-va",
"https://www.rallypoint.com/units/afmc-air-force-material-command-wright-patterson-afb-oh",
"https://www.rallypoint.com/units/uscg-hq-uscg-headquarters-offices-washington-dc",
"https://www.rallypoint.com/units/lantarea-uscg-atlantic-area-command-portsmouth-va",
"https://www.rallypoint.com/units/pacarea-uscg-pacific-area-command-alameda-ca",
"https://www.rallypoint.com/units/uscgres-uscg-reserve-washington-dc",
"https://www.rallypoint.com/units/uscgaux-uscg-auxiliary-washington-dc",
"https://www.rallypoint.com/units/decommissioned-ships--2"
                };

                foreach (var url in urls)
                {
                    var unitcodefilename = url.Substring(Helper.RallyPointHelper.rpointunits.Length).Replace("/", "-");

                    GetWebSite(webClient, url);

                    Console.WriteLine("Units: " + masterlist.Count);

                    XmlSerializer writer = new XmlSerializer(masterlist.GetType());

                    System.IO.FileStream file = System.IO.File.Create("D:\\repos\\Liaison\\Liaison.Import.RallyPoint\\output\\" + unitcodefilename + ".xml");

                    writer.Serialize(file, masterlist);
                    masterlist = new List<CurrentOpsObject>();
                    file.Close();
                }

            }
        }
        static List<CurrentOpsObject> masterlist = new List<CurrentOpsObject>();

        private static void GetWebSite(WebClient webClient, string url)
        {

            Console.WriteLine("Scanning " + url);
            string html = webClient.DownloadString(url);

            var doc = new HtmlDocument();
            doc.LoadHtml(html);

            var obj = new CurrentOpsObject();

            var titleNode = doc.DocumentNode.Descendants().Where(n => n.GetAttributeValue("class", "") == "name").FirstOrDefault();
            var nameText = titleNode.InnerText.Trim();
            var imageNode = doc.DocumentNode.Descendants().Where(n => n.GetAttributeValue("class", "") == "img-responsive-narrow").FirstOrDefault();
            var imageText = imageNode.Attributes[2].Value.Substring(0, imageNode.Attributes[2].Value.IndexOf(".png") + 4);

            var fullname = GetTitle(nameText);

            obj.NameNode = nameText;
            obj.Url = url;
            obj.FullName = fullname;
            var split = SplitService(nameText);

            obj.SplitName = split[0];
            obj.LogoUrl = imageText;
            GetServiceAndComponent(titleNode, obj);

            var locations = new List<LocationObject>();
            var locationNode = doc.DocumentNode.Descendants().Where(n => n.GetAttributeValue("class", "") == "block-link light-link").FirstOrDefault();
            if (locationNode != null)
            {                
                locations = GetLocations(locationNode);
            }
            obj.Locations = locations;

            obj.HigherHq = null;

            var subords = doc.DocumentNode.Descendants().Where(n => n.GetAttributeValue("class", "") == "qrc-content-wrap qrc-content-wrap-collapsed");
            obj.Children= new List<SubUnitObject>();
            if (subords.Count()>0)
            {
                var subunits = subords.ToList()[0].ChildNodes.Where(n => n.Name == "a").ToList();

                foreach (var subunit in subunits)
                {
                    var child = new SubUnitObject();
                    child.FullName = subunit.Attributes[2].Value;
                    child.Id = subunit.Attributes[0].Value.Substring("/units/".Length);
                    child.Name = subunit.InnerText;
                    child.Url = Helper.RallyPointHelper.rpoint + subunit.Attributes[0].Value;

                    obj.Children.Add(child);
                }
            }
            masterlist.Add(obj);

            foreach (var child in obj.Children)
            {
                GetWebSite(webClient, child.Url);
            }


        }

        private static List<LocationObject> GetLocations(HtmlNode locationNode)
        {
            var returnable = new List<LocationObject>();
            string baseName = locationNode.InnerHtml;
            if (locationNode.InnerHtml.Contains(','))
            {
                baseName = locationNode.InnerHtml.Substring(0, locationNode.InnerHtml.IndexOf(','));
            }
            returnable.Add(new LocationObject
            {
                BaseName = baseName,
                DateRange = null,
                //Deployment=null,
                Id = locationNode.Attributes[0].Value.Substring(Helper.RallyPointHelper.rpointlocs.Length),
                IsCurrent = true,
                Location = locationNode.InnerHtml,
                Url = locationNode.Attributes[0].Value,
            });
            return returnable;
        }

        private static void GetServiceAndComponent(HtmlNode titleNode, CurrentOpsObject obj)
        {
            
            if (titleNode.InnerHtml.Contains("US "))
            {
                obj.UnitService = Helper.Enumerators.ServicesBll.Joint;
                obj.UnitComponent = Helper.Enumerators.ServiceTypeBLL.Active;
            }
            else
            {
                obj.UnitService = Helper.Enumerators.ServicesBll.Unknown;
                obj.UnitComponent = Helper.Enumerators.ServiceTypeBLL.Unknown;
            }
        }

        private static string[] SplitService(string nameText)
        {
            string processable = nameText;
            string[] returnable = new string[2];
            if (nameText.Contains("US"))
            {
                returnable[1] = "U.S.";
            }
            if (returnable[1] != null)
            {
                processable = nameText.Replace(returnable[1], "");
            }
            returnable[0] = processable.Trim();

            return returnable;
        }

        private static string GetTitle(string nameText)
        {
            return nameText.Replace("US ", "");
        }
    }
}
