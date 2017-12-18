using HtmlAgilityPack;
using Liaison.Biz.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace Liaison.Import
{
    class Program
    {
        static void Main(string[] args)
        {

            using (var webClient = new WebClient())
            {
                //var url = "https://currentops.com/unit/us/army/doa";
                var url = "https://currentops.com/unit/us/army/1-id";

                GetWebSite(webClient, url);


            }
        }

       static List<CurrentOpsObject> masterlist = new List<CurrentOpsObject>();

        private static void GetWebSite(WebClient webClient, string url)
        {
            string html = webClient.DownloadString(url);
            var doc = new HtmlDocument();
            doc.LoadHtml(html);

            var obj = new Biz.Objects.CurrentOpsObject();

            var titleNode = doc.DocumentNode.Descendants().Where(n => n.GetAttributeValue("class", "") == "page-header").Where(n=>n.InnerHtml.Contains("<h1>")).FirstOrDefault();

            var nameText = titleNode.InnerText.Trim();

            var fullname = GetTitle(nameText);

           obj.NameNode = nameText;
            obj.Url = url;
            obj.FullName = fullname;
            var split = SplitService(nameText);
            obj.SplitName = split[0];
            obj.Service = split[1];
            obj.LogoUrl = GetLogoUrl(titleNode.InnerHtml.Trim());

            //var baseNode = doc.DocumentNode.Descendants().Where(n => n.GetAttributeValue("class", "") == "list-group").Where(n => n.InnerHtml.Contains("<a href")).FirstOrDefault();

            var listGroups = doc.DocumentNode.Descendants().Where(n => n.GetAttributeValue("class", "") == "list-group").Where(n => n.InnerHtml.Contains("<a href"));

            var locationNode = listGroups.ToList()[0];
            var locations = GetLocations(locationNode.Descendants().Where(n => n.GetAttributeValue("class", "") == "list-group-item"));
            obj.Locations = locations;

            var highHqNode = listGroups.ToList()[1];
            var higherHq = GetHigherHQ(highHqNode.Descendants().Where(n => n.GetAttributeValue("class", "") == "list-group-item"));
            obj.HigherHq = higherHq;

            var subords = doc.DocumentNode.Descendants().Where(n => n.GetAttributeValue("class", "") == "block subords subords-units");
            var subunits = subords.ToList()[0].ChildNodes.Where(n => n.Name == "li").FirstOrDefault().ChildNodes.Where(n => n.Name == "ul").FirstOrDefault();
            var details = subunits.ChildNodes.Where(n => n.Name == "li");

            var children = GetChildren(details);
            obj.Children = children;

            masterlist.Add(obj);

            foreach (var child in children)
            {
                GetWebSite(webClient, child.Url);
            }

            Console.ReadLine();
            string a = "b";

        }

        private static List<SubUnitObject> GetChildren(IEnumerable<HtmlNode> details)
        {
            List<SubUnitObject> returnable = new List<SubUnitObject>();
            foreach (var node in details)
            {

                var subunit = new SubUnitObject();
                var childnode = node.ChildNodes.Where(n => n.Name == "div").FirstOrDefault();
                var name = childnode.InnerText.Trim();
                var anchor = childnode.ChildNodes.Where(n => n.Name == "a").FirstOrDefault();
                var url = anchor.Attributes.Where(n => n.Name == "href").FirstOrDefault().Value;
                var title = anchor.Attributes.Where(n => n.Name == "title").FirstOrDefault().Value;
                subunit.Id = url.Substring(hhqurl.Length);
                subunit.Url = url;
                subunit.FullName = title;
                subunit.Name = name;
                returnable.Add(subunit);
            }
            return returnable;
        }
       const string hhqurl = "https://currentops.com/unit/";
        private static List<HigherHqObject> GetHigherHQ(IEnumerable<HtmlNode> enumerable)
        {
            
            List<HigherHqObject> returnable = new List<HigherHqObject>();
            foreach (var node  in enumerable)
            {
                var hhq = new HigherHqObject();
                hhq.Url = node.Attributes[0].Value == "list-group-item" ? null : node.Attributes[0].Value;
                hhq.Id = hhq.Url == null ? null : node.Attributes[0].Value.Substring(hhqurl.Length);
                hhq.DateRange = node.Descendants().Where(n => n.GetAttributeValue("class", "") == "badge").FirstOrDefault().InnerText.Trim() ;
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
            string locationurl= "https://currentops.com/installations/";
            List<LocationObject> returnable = new List<LocationObject>();

            foreach (var node in baseNodes)
            {
                var locobj = new LocationObject();
                locobj.Url = node.Attributes[0].Value == "list-group-item" ? null : node.Attributes[0].Value;
                locobj.Id = locobj.Url == null ? null : node.Attributes[0].Value.Substring(locationurl.Length);
                locobj.DateRange = node.Descendants().Where(n => n.GetAttributeValue("class", "") == "badge").FirstOrDefault().InnerText.Trim();
                locobj.IsCurrent = locobj.DateRange.EndsWith("Present");
                locobj.BaseName = node.Descendants().Where(n => n.Name == "strong").FirstOrDefault()?.InnerText.Trim();
                locobj.Deployment = node.ChildNodes[3].InnerText == "Deployment";
                locobj.Location = locobj.Deployment ? node.ChildNodes[6].InnerText.Trim() : node.ChildNodes[9].InnerText.Trim();
                returnable.Add(locobj);
            }
            return returnable;
        }

        private static string GetLogoUrl(string nameText)
        {
            string processable = nameText;
            string url = "";
            if (nameText.Contains("<img src=\""))
            {
                var startpos = nameText.IndexOf("<img src=\"") + 10;
                var endpos = nameText.IndexOf("png", startpos);
                url = nameText.Substring(startpos, endpos-startpos);
            }

            return url;
        }

        private static string[] SplitService(string nameText)
        {
            string processable = nameText;
            string[] returnable = new string[2];

            if (nameText.Contains("U.S. Army"))
            {
                returnable[1] = "U.S. Army";
            }
            else if (nameText.Contains("USAF"))
            {
                returnable[1] = "USAF";
            }
            else if (nameText.Contains("USN"))
            {
                returnable[1] = "USN";
            }
            else if (nameText.Contains("USMC"))
            {
                returnable[1] = "USMC";
            }
            else if (nameText.Contains("U.S."))
            {
                returnable[1] = "U.S.";
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
    }
}
