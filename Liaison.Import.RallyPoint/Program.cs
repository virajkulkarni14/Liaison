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
                string[] urls = { "https://www.rallypoint.com/units/centcom-us-central-command-macdill-afb-fl", };

                foreach (var url in urls)
                {
                    var unitcodefilename = url.Substring(Helper.RallyPointHelper.rpoint.Length).Replace("/", "-");

                    GetWebSite(webClient, url);

                    Console.WriteLine("Units: " + masterlist.Count);
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
            var imageText = imageNode.InnerText.Trim().Substring(0, imageNode.InnerText.IndexOf(".png") + 4);

            var fullname = GetTitle(nameText);

            obj.NameNode = nameText;
            obj.Url = url;
            obj.FullName = fullname;
            var split = SplitService(nameText);

            obj.SplitName = split[0];
            obj.LogoUrl = imageText;
            GetServiceAndComponent(titleNode, obj);

            var locations = new List<LocationObject>();
            var listGroups = doc.DocumentNode.Descendants().Where(n => n.GetAttributeValue("class", "") == "location").FirstOrDefault().Descendants().Where(nn => nn.GetAttributeValue("class", "") == "location").FirstOrDefault();
            if (listGroups.Any())
            {
                var locationNode = listGroups.ToList()[0];
                locations = GetLocations();
            }
        }

        private static void GetServiceAndComponent(HtmlNode titleNode, CurrentOpsObject obj)
        {
            
            if (titleNode.InnerHtml.Contains("US "))
            {
                obj.UnitService = Helper.Enumerators.Services.Joint;
                obj.UnitComponent = Helper.Enumerators.ServiceType.Active;
            }
            else
            {
                throw new Exception();
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

            processable = nameText.Replace(returnable[1], "");
            returnable[0] = processable.Trim();

            return returnable;
        }

        private static string GetTitle(string nameText)
        {
            return nameText.Replace("US ", "");
        }
    }
}
