using HtmlAgilityPack;
using Liaison.Biz.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Liaison.Import.CurrentOps
{
    class Program
    {
        static void Main(string[] args)
        {

            using (var webClient = new WebClient())
            {
                // string[] urls = { "https://currentops.com/unit/us/army/doa", "https://currentops.com/unit/us/army/forscom" };
                // string[] urls = { "https://currentops.com/unit/us/army/forscom" };
                //var url = "https://currentops.com/unit/us/army/1-id";
                //var url = "https://currentops.com/unit/us/army/6-cav/1-sqdn";
                //var url = "https://currentops.com/unit/us/army/228-avn/2-bn";

                //var url = "https://currentops.com/unit/us/army/usarc";
                //var url = "https://currentops.com/unit/us/army/forscom";
                //string[] urls = { "https://currentops.com/unit/us/army/1-army" };

                //string[] urls = {
                //        //"https://currentops.com/unit/us/army/osa",
                //        //"https://currentops.com/unit/us/army/tradoc",
                //        //"https://currentops.com/unit/us/army/amc",
                //        //"https://currentops.com/unit/us/army/usarpac",
                //        //"https://currentops.com/unit/us/army/usarcent",

                //        //"https://currentops.com/unit/us/army/usarnorth",
                //        //"https://currentops.com/unit/us/army/usasoc",
                //        //"https://currentops.com/unit/us/army/arstrat",
                //        //"https://currentops.com/unit/us/army/arcyber",
                //        "https://currentops.com/unit/us/army/usarso",
                //        "https://currentops.com/unit/us/army/usaraf",
                //        "https://currentops.com/unit/us/army/sddc",
                //        "https://currentops.com/unit/us/army/imcom",
                //        "https://currentops.com/unit/us/army/usace",
                //        "https://currentops.com/unit/us/army/medcom",
                //        "https://currentops.com/unit/us/army/usma",
                //        "https://currentops.com/unit/us/army/netcom",
                //        "https://currentops.com/unit/us/army/inscom",
                //        "https://currentops.com/unit/us/army/mdw",
                //        "https://currentops.com/unit/us/army/usacidc",
                //        "https://currentops.com/unit/us/army/atec",
                //        "https://currentops.com/unit/us/army/usaasc",
                //        "https://currentops.com/unit/us/army/us-army-corrections-cmd",
                //         "https://currentops.com/unit/us/army/usareur",
                //    };
                // string[] urls =
                // {
                //  "https://currentops.com/unit/us/usn/cno",
                //   "https://currentops.com/unit/us/usmc/usmc"
                //};

                 string[] urls = GetNationalGuard();
                //string[] urls = GetUCCs();

                foreach (var url in urls)
                {
                    var unitcodefilename = url.Substring(Liaison.Helper.CurrentOpsHelper.ctops.Length).Replace("/", "-");

                    GetWebSite(webClient, url);

                    Console.WriteLine("Units: " + masterlist.Count);

                    XmlSerializer writer = new System.Xml.Serialization.XmlSerializer(masterlist.GetType());

                    //XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
                    //ns.Add("", "");

                    System.IO.FileStream file = System.IO.File.Create("D:\\repos\\Liaison\\Liaison.Import.CurrentOps\\output\\" + unitcodefilename + ".xml");

                    writer.Serialize(file, masterlist);
                    masterlist = new List<CurrentOpsObject>();
                    file.Close();
                }
            }
        }


        static List<CurrentOpsObject> masterlist = new List<CurrentOpsObject>();

        private static void GetWebSite(WebClient webClient, string url)
        {
            //if (url == "https://currentops.com/unit/us/army/usarc")
            //{
            //    return;
            //}
            Console.WriteLine("Scanning " + url);
            string html = webClient.DownloadString(url);
            var doc = new HtmlDocument();
            doc.LoadHtml(html);

            var obj = new Biz.Objects.CurrentOpsObject();

            var titleNode = doc.DocumentNode.Descendants().Where(n => n.GetAttributeValue("class", "") == "page-header").Where(n => n.InnerHtml.Contains("<h1>")).FirstOrDefault();
            var componentNode = doc.DocumentNode.Descendants().Where(n => n.GetAttributeValue("class", "") == "dl-horizontal").Where(n => n.InnerHtml.Contains("Component")).FirstOrDefault();
            var nameText = titleNode.InnerText.Trim();

            var fullname = GetTitle(nameText);

            obj.NameNode = nameText;
            obj.Url = url;
            obj.FullName = fullname;
            var split = SplitService(nameText);
            obj.SplitName = split[0];
            //obj.Service = split[1];
            obj.LogoUrl = GetLogoUrl(titleNode.InnerHtml.Trim());

            GetServiceAndComponent(componentNode, obj);
            //var baseNode = doc.DocumentNode.Descendants().Where(n => n.GetAttributeValue("class", "") == "list-group").Where(n => n.InnerHtml.Contains("<a href")).FirstOrDefault();

            var locations = new List<LocationObject>();
            var listGroups = doc.DocumentNode.Descendants().Where(n => n.GetAttributeValue("class", "") == "list-group").Where(n => n.InnerHtml.Contains("<a href"));
            if (listGroups.Any())
            {
                var locationNode = listGroups.ToList()[0];
                locations = GetLocations(locationNode.Descendants().Where(n => n.GetAttributeValue("class", "") == "list-group-item"));
            }
            obj.Locations = locations;

            var higherHq = new List<HigherHqObject>();
            var highHqNode = listGroups.Where(n => n.InnerHtml.Contains("https://currentops.com/unit")).FirstOrDefault();
            if (highHqNode != null)
            {
                higherHq = GetHigherHQ(highHqNode.Descendants().Where(n => n.GetAttributeValue("class", "") == "list-group-item"));
            }
            obj.HigherHq = higherHq;

            var subords = doc.DocumentNode.Descendants().Where(n => n.GetAttributeValue("class", "") == "block subords subords-units");
            obj.Children = new List<SubUnitObject>();
            if (subords.Count() > 0)
            {
                var subunits = subords.ToList()[0].ChildNodes.Where(n => n.Name == "li").FirstOrDefault().ChildNodes.Where(n => n.Name == "ul").FirstOrDefault();
                var details = subunits.ChildNodes.Where(n => n.Name == "li");

                var children = GetChildren(details);
                obj.Children = children;


            }
            masterlist.Add(obj);
            foreach (var child in obj.Children)
            {
                GetWebSite(webClient, child.Url);
            }
        }

        private static void GetServiceAndComponent(HtmlNode componentNode, CurrentOpsObject obj)
        {
            if (componentNode.InnerHtml.Contains("U.S. Army (AC)") || componentNode.InnerHtml.Contains("<dd>U.S. Army</dd>") || componentNode.InnerHtml.Contains("<dd>APS</dd>"))
            {
                obj.UnitService = Helper.Enumerators.Services.Army;
                obj.UnitComponent = Helper.Enumerators.ServiceType.Active;
            }
            else if (componentNode.InnerHtml.Contains("USAF (AC)"))
            {
                obj.UnitService = Helper.Enumerators.Services.AirForce;
                obj.UnitComponent = Helper.Enumerators.ServiceType.Active;
            }
            else if (componentNode.InnerHtml.Contains("USAR"))
            {
                obj.UnitService = Helper.Enumerators.Services.Army;
                obj.UnitComponent = Helper.Enumerators.ServiceType.Reserve;
            }
            else if (componentNode.InnerHtml.Contains("ARNG"))
            {
                obj.UnitService = Helper.Enumerators.Services.Army;
                obj.UnitComponent = Helper.Enumerators.ServiceType.Volunteer;
                obj.UnitNGState = componentNode.ChildNodes.Where(n => n.Name == "dd").FirstOrDefault().InnerText.Substring(0, 2);
                // do the state as well, eg OK from OK ARNG
            }
            else if (componentNode.InnerHtml.Contains("ANG"))
            {
                obj.UnitService = Helper.Enumerators.Services.AirForce;
                obj.UnitComponent = Helper.Enumerators.ServiceType.Volunteer;
                obj.UnitNGState = componentNode.ChildNodes.Where(n => n.Name == "dd").FirstOrDefault().InnerText.Substring(0, 2);
            }
            else if (componentNode.InnerHtml.Contains("U.S. (Joint)"))
            {
                obj.UnitService = Helper.Enumerators.Services.Joint;
                obj.UnitComponent = Helper.Enumerators.ServiceType.Active;
            }
            else if (componentNode.InnerHtml.Contains("USN (AC)"))
            {
                obj.UnitService = Helper.Enumerators.Services.Navy;
                obj.UnitComponent = Helper.Enumerators.ServiceType.Active;
            }
            else if (componentNode.InnerHtml.Contains("USMC"))
            {
                obj.UnitService = Helper.Enumerators.Services.Marines;
                obj.UnitComponent = Helper.Enumerators.ServiceType.Active;
            }
            else if (componentNode.InnerHtml.Contains("USNR"))
            {
                obj.UnitService = Helper.Enumerators.Services.Navy;
                obj.UnitComponent = Helper.Enumerators.ServiceType.Reserve;
            }
            else
            {
                throw new Exception();
            }
        }

        private static List<SubUnitObject> GetChildren(IEnumerable<HtmlNode> details)
        {
            List<SubUnitObject> returnable = new List<SubUnitObject>();
            foreach (var node in details)
            {

                var subunit = new SubUnitObject();
                if (node.InnerText.StartsWith("Indirect reporting"))
                {
                    var childnode = node.ChildNodes.Where(n => n.Name == "ul").FirstOrDefault().ChildNodes.Where(n => n.Name == "li");
                    var indirectSubs = GetChildren(childnode);

                    foreach (var indirectSub in indirectSubs)
                    {
                        indirectSub.IsIndirect = true;
                        returnable.Add(indirectSub);
                    }

                }
                else
                {
                    var childnode = node.ChildNodes.Where(n => n.Name == "div").FirstOrDefault();
                    var name = childnode.InnerText.Trim();
                    var anchor = childnode.ChildNodes.Where(n => n.Name == "a").FirstOrDefault();
                    var url = anchor.Attributes.Where(n => n.Name == "href").FirstOrDefault().Value;
                    var title = anchor.Attributes.Where(n => n.Name == "title").FirstOrDefault().Value;
                    subunit.IsIndirect = false;
                    subunit.Id = url.Substring(Helper.CurrentOpsHelper.ctops.Length);
                    subunit.Url = url;
                    subunit.FullName = title;
                    subunit.Name = name;
                    returnable.Add(subunit);
                }
            }
            return returnable;
        }
       
        private static List<HigherHqObject> GetHigherHQ(IEnumerable<HtmlNode> enumerable)
        {

            List<HigherHqObject> returnable = new List<HigherHqObject>();
            foreach (var node in enumerable)
            {
                var hhq = new HigherHqObject();
                hhq.Url = node.Attributes[0].Value == "list-group-item" ? "" : node.Attributes[0].Value;
                hhq.Id = hhq.Url == null ? null : node.Attributes[0].Value.Substring(Helper.CurrentOpsHelper.ctops.Length);
                hhq.DateRange = node.Descendants().Where(n => n.GetAttributeValue("class", "") == "badge").FirstOrDefault().InnerText.Trim();
                hhq.IsCurrent = hhq.DateRange.EndsWith("Present");
                hhq.HHQAcronym = node.Descendants().Where(n => n.Name == "strong").FirstOrDefault()?.InnerText.Trim();
                hhq.Name = node.Attributes[2].Value;
                hhq.Type = node.Descendants().Where(n => n.Name == "small").FirstOrDefault()?.InnerText.Trim();
                returnable.Add(hhq);
            }

            return returnable;
        }

        private static List<LocationObject> GetLocations(IEnumerable<HtmlNode> baseNodes)
        {
            string locationurl = "https://currentops.com/installations/";
            List<LocationObject> returnable = new List<LocationObject>();

            foreach (var node in baseNodes)
            {
                if (node.Attributes[0].Value.StartsWith(locationurl))
                {
                    var locobj = new LocationObject();
                    locobj.Url = node.Attributes[0].Value == "list-group-item" ? null : node.Attributes[0].Value;
                    locobj.Id = locobj.Url == null ? null : node.Attributes[0].Value.Substring(locationurl.Length);
                    locobj.DateRange = node.Descendants().Where(n => n.GetAttributeValue("class", "") == "badge").FirstOrDefault().InnerText.Trim();
                    locobj.IsCurrent = locobj.DateRange.EndsWith("Present");
                    locobj.BaseName = node.Descendants().Where(n => n.Name == "strong").FirstOrDefault()?.InnerText.Trim();
                    locobj.Deployment = node.ChildNodes[3].InnerText == "Deployment";
                    locobj.Location = Helper.SystemWebHelper.HtmlDecoder(locobj.Deployment ? node.ChildNodes[6].InnerText.Trim() : GetFullAddress(node.ChildNodes[8].InnerText.Trim(), node.ChildNodes[9].InnerText.Trim()));

                    returnable.Add(locobj);
                }
            }
            return returnable;
        }

        private static string GetFullAddress(string parentBase, string city)
        {
            var parentBaseLadder = parentBase.Split('|');
            StringBuilder sb = new StringBuilder();
            foreach (var str in parentBaseLadder)
            {
                var trimmed = str.Trim();
                if (!string.IsNullOrWhiteSpace(trimmed))
                {
                    sb.Append(trimmed + " | ");
                }
            }
            sb.Append(city);
            return sb.ToString();
        }

        private static string GetLogoUrl(string nameText)
        {
            string processable = nameText;
            string url = "";
            if (nameText.Contains("<img src=\""))
            {
                var startpos = nameText.IndexOf("<img src=\"") + 10;
                var endpos = nameText.IndexOf("png", startpos);
                url = nameText.Substring(startpos, (endpos - startpos) + 3);
            }

            return url;
        }

        private static string[] SplitService(string nameText)
        {
            string processable = nameText;
            string[] returnable = new string[2];

            if (nameText.Contains(Liaison.Helper.CurrentOpsHelper.USArmy))
            {
                returnable[1] = Liaison.Helper.CurrentOpsHelper.USArmy;
            }
            else if (nameText.Contains(Liaison.Helper.CurrentOpsHelper.USAF))
            {
                returnable[1] = Liaison.Helper.CurrentOpsHelper.USAF;
            }
            else if (nameText.Contains(Liaison.Helper.CurrentOpsHelper.USN))
            {
                returnable[1] = Liaison.Helper.CurrentOpsHelper.USN;
            }
            else if (nameText.Contains(Liaison.Helper.CurrentOpsHelper.USMC))
            {
                returnable[1] = Liaison.Helper.CurrentOpsHelper.USMC;
            }
            else if (nameText.Contains("U.S."))
            {
                returnable[1] = "U.S.";
            }
            else if (nameText.Contains("Multinational"))
            {
                returnable[1] = "Multinational";
            }
            
            processable = nameText.Replace(returnable[1], "");
            returnable[0] = processable.Trim();

            return returnable;
        }

        private static string GetTitle(string nameText)
        {
            string returnable = nameText.Replace(Environment.NewLine, ",");
            while (returnable.Contains("  "))
            {
                returnable = returnable.Replace("  ", " ");
            }
            return returnable;
        }

        private static string[] GetUCCs()
        {
            return new string[]
            {
                //"https://currentops.com/unit/us/joint/uspacom",
                //"https://currentops.com/unit/us/joint/useucom",
                //"https://currentops.com/unit/us/joint/uscentcom",
                "https://currentops.com/unit/us/joint/usnorthcom",
                //"https://currentops.com/unit/us/joint/ussouthcom",
                //"https://currentops.com/unit/us/joint/usafricom",
                //"https://currentops.com/unit/us/joint/ussocom",
                //"https://currentops.com/unit/us/joint/usstratcom",
                //"https://currentops.com/unit/us/joint/ustranscom"
};
        }
        private static string[] GetNationalGuard()
        {
            return new string[]
            {
               "https://currentops.com/unit/us/army/jfhq-ak/arng-elt",
"https://currentops.com/unit/us/army/jfhq-al/arng-elt",
"https://currentops.com/unit/us/army/jfhq-ar/arng-elt",
"https://currentops.com/unit/us/army/jfhq-az/arng-elt",
"https://currentops.com/unit/us/army/jfhq-ca/arng-elt",
"https://currentops.com/unit/us/army/jfhq-co/arng-elt",
"https://currentops.com/unit/us/army/jfhq-ct/arng-elt",
"https://currentops.com/unit/us/army/jfhq-dc/arng-elt",
"https://currentops.com/unit/us/army/jfhq-de/arng-elt",
"https://currentops.com/unit/us/army/jfhq-fl/arng-elt",
"https://currentops.com/unit/us/army/jfhq-ga/arng-elt",
"https://currentops.com/unit/us/army/jfhq-gu/arng-elt",
"https://currentops.com/unit/us/army/jfhq-hi/arng-elt",
"https://currentops.com/unit/us/army/jfhq-ia/arng-elt",
"https://currentops.com/unit/us/army/jfhq-id/arng-elt",
"https://currentops.com/unit/us/army/jfhq-il/arng-elt",
"https://currentops.com/unit/us/army/jfhq-in/arng-elt",
"https://currentops.com/unit/us/army/jfhq-ks/arng-elt",
"https://currentops.com/unit/us/army/jfhq-ky/arng-elt",
"https://currentops.com/unit/us/army/jfhq-la/arng-elt",
"https://currentops.com/unit/us/army/jfhq-ma/arng-elt",
"https://currentops.com/unit/us/army/jfhq-md/arng-elt",
"https://currentops.com/unit/us/army/jfhq-me/arng-elt",
"https://currentops.com/unit/us/army/jfhq-mi/arng-elt",
"https://currentops.com/unit/us/army/jfhq-mn/arng-elt",
"https://currentops.com/unit/us/army/jfhq-mo/arng-elt",
"https://currentops.com/unit/us/army/jfhq-ms/arng-elt",
"https://currentops.com/unit/us/army/jfhq-mt/arng-elt",
"https://currentops.com/unit/us/army/jfhq-nc/arng-elt",
"https://currentops.com/unit/us/army/jfhq-nd/arng-elt",
"https://currentops.com/unit/us/army/jfhq-ne/arng-elt",
"https://currentops.com/unit/us/army/jfhq-nh/arng-elt",
"https://currentops.com/unit/us/army/jfhq-nj/arng-elt",
"https://currentops.com/unit/us/army/jfhq-nm/arng-elt",
"https://currentops.com/unit/us/army/jfhq-nv/arng-elt",
"https://currentops.com/unit/us/army/jfhq-ny/arng-elt",
"https://currentops.com/unit/us/army/jfhq-oh/arng-elt",
"https://currentops.com/unit/us/army/jfhq-ok/arng-elt",
"https://currentops.com/unit/us/army/jfhq-or/arng-elt",
"https://currentops.com/unit/us/army/jfhq-pa/arng-elt",
"https://currentops.com/unit/us/army/jfhq-pr/arng-elt",
"https://currentops.com/unit/us/army/jfhq-ri/arng-elt",
"https://currentops.com/unit/us/army/jfhq-sc/arng-elt",
"https://currentops.com/unit/us/army/jfhq-sd/arng-elt",
"https://currentops.com/unit/us/army/jfhq-tn/arng-elt",
"https://currentops.com/unit/us/army/jfhq-tx/arng-elt",
"https://currentops.com/unit/us/army/jfhq-ut/arng-elt",
"https://currentops.com/unit/us/army/jfhq-va/arng-elt",
"https://currentops.com/unit/us/army/jfhq-vi/arng-elt",
"https://currentops.com/unit/us/army/jfhq-vt/arng-elt",
"https://currentops.com/unit/us/army/jfhq-wa/arng-elt",
"https://currentops.com/unit/us/army/jfhq-wi/arng-elt",
"https://currentops.com/unit/us/army/jfhq-wv/arng-elt",
"https://currentops.com/unit/us/army/jfhq-wy/arng-elt",
"https://currentops.com/unit/us/usaf/jfhq-ak/ang-elt",
"https://currentops.com/unit/us/usaf/jfhq-al/ang-elt",
"https://currentops.com/unit/us/usaf/jfhq-ar/ang-elt",
"https://currentops.com/unit/us/usaf/jfhq-az/ang-elt",
"https://currentops.com/unit/us/usaf/jfhq-ca/ang-elt",
"https://currentops.com/unit/us/usaf/jfhq-co/ang-elt",
"https://currentops.com/unit/us/usaf/jfhq-ct/ang-elt",
"https://currentops.com/unit/us/usaf/jfhq-dc/ang-elt",
"https://currentops.com/unit/us/usaf/jfhq-de/ang-elt",
"https://currentops.com/unit/us/usaf/jfhq-fl/ang-elt",
"https://currentops.com/unit/us/usaf/jfhq-ga/ang-elt",
"https://currentops.com/unit/us/usaf/jfhq-gu/ang-elt",
"https://currentops.com/unit/us/usaf/jfhq-hi/ang-elt",
"https://currentops.com/unit/us/usaf/jfhq-ia/ang-elt",
"https://currentops.com/unit/us/usaf/jfhq-id/ang-elt",
"https://currentops.com/unit/us/usaf/jfhq-il/ang-elt",
"https://currentops.com/unit/us/usaf/jfhq-in/ang-elt",
"https://currentops.com/unit/us/usaf/jfhq-ks/ang-elt",
"https://currentops.com/unit/us/usaf/jfhq-ky/ang-elt",
"https://currentops.com/unit/us/usaf/jfhq-la/ang-elt",
"https://currentops.com/unit/us/usaf/jfhq-ma/ang-elt",
"https://currentops.com/unit/us/usaf/jfhq-md/ang-elt",
"https://currentops.com/unit/us/usaf/jfhq-me/ang-elt",
"https://currentops.com/unit/us/usaf/jfhq-mi/ang-elt",
"https://currentops.com/unit/us/usaf/jfhq-mn/ang-elt",
"https://currentops.com/unit/us/usaf/jfhq-mo/ang-elt",
"https://currentops.com/unit/us/usaf/jfhq-ms/ang-elt",
"https://currentops.com/unit/us/usaf/jfhq-mt/ang-elt",
"https://currentops.com/unit/us/usaf/jfhq-nc/ang-elt",
"https://currentops.com/unit/us/usaf/jfhq-nd/ang-elt",
"https://currentops.com/unit/us/usaf/jfhq-ne/ang-elt",
"https://currentops.com/unit/us/usaf/jfhq-nh/ang-elt",
"https://currentops.com/unit/us/usaf/jfhq-nj/ang-elt",
"https://currentops.com/unit/us/usaf/jfhq-nm/ang-elt",
"https://currentops.com/unit/us/usaf/jfhq-nv/ang-elt",
"https://currentops.com/unit/us/usaf/jfhq-ny/ang-elt",
"https://currentops.com/unit/us/usaf/jfhq-oh/ang-elt",
"https://currentops.com/unit/us/usaf/jfhq-ok/ang-elt",
"https://currentops.com/unit/us/usaf/jfhq-or/ang-elt",
"https://currentops.com/unit/us/usaf/jfhq-pa/ang-elt",
"https://currentops.com/unit/us/usaf/jfhq-pr/ang-elt",
"https://currentops.com/unit/us/usaf/jfhq-ri/ang-elt",
"https://currentops.com/unit/us/usaf/jfhq-sc/ang-elt",
"https://currentops.com/unit/us/usaf/jfhq-sd/ang-elt",
"https://currentops.com/unit/us/usaf/jfhq-tn/ang-elt",
"https://currentops.com/unit/us/usaf/jfhq-tx/ang-elt",
"https://currentops.com/unit/us/usaf/jfhq-ut/ang-elt",
"https://currentops.com/unit/us/army/jfhq-va/ang-elt",
"https://currentops.com/unit/us/usaf/jfhq-vi/ang-elt",
"https://currentops.com/unit/us/usaf/jfhq-vt/ang-elt",
"https://currentops.com/unit/us/usaf/jfhq-wa/ang-elt",
"https://currentops.com/unit/us/usaf/jfhq-wi/ang-elt",
"https://currentops.com/unit/us/usaf/jfhq-wv/ang-elt",
"https://currentops.com/unit/us/usaf/jfhq-wy/ang-elt"
            };
        }
    }
}