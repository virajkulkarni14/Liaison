using HtmlAgilityPack;
using Liaison.Biz.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Liaison.Import.GlobalSecurity
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var webClient = new WebClient())
            {
                string[] urls = { "https://www.globalsecurity.org/military/agency/army/usaeur.htm"};

                foreach (var url in urls)
                {
                    var unitcodefilename = url.Substring(Liaison.Helper.GlobalSecurityHelper.gsec.Length).Replace("/", "-");
                GetWebSite(webClient, url);

                    Console.WriteLine("Units: " + masterlist.Count);

                    XmlSerializer writer = new XmlSerializer(masterlist.GetType());

                    System.IO.FileStream file = System.IO.File.Create("D:\\repos\\Liaison\\Liaison.Import.GlobalSecurity\\output\\" + unitcodefilename + ".xml");

                    writer.Serialize(file, masterlist);
                    masterlist = new List<CurrentOpsObject>();
                    file.Close();
                }
            }
        }

        private static void GetWebSite(WebClient webClient, string url)
        {
            throw new NotImplementedException();
        }

        static List<CurrentOpsObject> masterlist = new List<CurrentOpsObject>();

    }
}
