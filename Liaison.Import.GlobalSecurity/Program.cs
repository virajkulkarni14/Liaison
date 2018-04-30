using HtmlAgilityPack;
using Liaison.Biz.Objects;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Xml;
using System.Xml.Serialization;
using Liaison.Helper.Enumerators;

namespace Liaison.Import.GlobalSecurity
{

    class Program
    {
        private const string _gsUrl = "https://www.globalsecurity.org";
        private static readonly string[] _h3S = {
            "Supporting Units",
            "Associate Units [ARNG/USAR]",
            "Prior",
            "Amphibious Task Force 03",
            "Pre-Modular",
            "Pre-Modular Division",
            "Units",
            "Major Subordinate Commands",
            "Related Units",
            "Associated Units" };
        static void Main(string[] args)
        {
            using (var webClient = new WebClient())
            {
                string [] urls=
                {
                    "https://www.globalsecurity.org/military/agency/usaf/ang.htm"
                };
                /*string[] urls =
                {
                    //"https://www.globalsecurity.org/military/agency/army/usace.htm",
                   // "https://www.globalsecurity.org/military/agency/army/101abn.htm",
                    "https://www.globalsecurity.org/military/agency/army/xviii-corps.htm",
                    "https://www.globalsecurity.org/military/agency/army/iii-corps.htm",
                    "https://www.globalsecurity.org/military/agency/army/i-corps.htm",
                    "https://www.globalsecurity.org/military/agency/army/arsoc.htm",
                    "https://www.globalsecurity.org/space/agency/arsmdc.htm",
                    "https://www.globalsecurity.org/military/agency/army/usaeur.htm",
                    "https://www.globalsecurity.org/military/agency/army/arnorth.htm",
                    "https://www.globalsecurity.org/military/agency/army/usarso.htm",
                    "https://www.globalsecurity.org/military/agency/army/usarpac.htm",
                    "https://www.globalsecurity.org/military/agency/army/arcent.htm",
                  //  "https://www.globalsecurity.org/military/agency/army/setaf.htm",
                   // "https://www.globalsecurity.org/military/agency/army/mtmc.htm",
                   // "https://www.globalsecurity.org/military/agency/army/asc.htm",
                  //  "https://www.globalsecurity.org/military/agency/army/forscom.htm"
                };*/
           //     string[] urls =
             //   {
                   // "https://www.globalsecurity.org/military/agency/usmc/29mag.htm",
                   // "https://www.globalsecurity.org/military/agency/usmc/marsoc.htm",
                   //    "https://www.globalsecurity.org/military/agency/usmc/marforsouth.htm",
                    //"https://www.globalsecurity.org/military/agency/usmc/marforres.htm",
                    //"https://www.globalsecurity.org/military/agency/usmc/marforlant.htm",
                    //"https://www.globalsecurity.org/military/agency/usmc/marforeur.htm",
                    //"https://www.globalsecurity.org/military/agency/usmc/marforaf.htm",
                    //"https://www.globalsecurity.org/military/agency/usmc/marcent.htm",
                  //  "https://www.globalsecurity.org/military/agency/usmc/marforpac.htm",
                   // "https://www.globalsecurity.org/military/agency/usmc/mcsocom-det.htm",
                   // "https://www.globalsecurity.org/military/agency/usmc/mchq.htm",
                   // "https://www.globalsecurity.org/military/agency/usmc/mcwl.htm",
                   // "https://www.globalsecurity.org/military/agency/usmc/tecom.htm",
                   // "https://www.globalsecurity.org/military/agency/usaf/afrc.htm",
                    //"https://www.globalsecurity.org/military/agency/usaf/amc.htm",
                  //  "https://www.globalsecurity.org/military/agency/usaf/acc.htm",
                 //   "https://www.globalsecurity.org/military/agency/usaf/usafe.htm",
                   // "https://www.globalsecurity.org/military/agency/usaf/afsoc.htm",
                //    "https://www.globalsecurity.org/space/agency/afspc.htm",
                 //   "https://www.globalsecurity.org/military/agency/usaf/aetc.htm",
                  //  "https://www.globalsecurity.org/military/agency/usaf/afcesa.htm",
                  //  "https://www.globalsecurity.org/military/agency/usaf/afdw.htm",
                    //                   "https://www.globalsecurity.org/military/agency/navy/lantflt.htm",
                    //"https://www.globalsecurity.org/military/agency/navy/navso.htm",
                    // "https://www.globalsecurity.org/military/agency/navy/naveur.htm",
                    // "https://www.globalsecurity.org/military/agency/navy/comsceur.htm",
                    // "https://www.globalsecurity.org/military/agency/navy/navcent.htm",
                    // "https://www.globalsecurity.org/military/agency/navy/comsccent.htm",
                    // "https://www.globalsecurity.org/military/agency/navy/pacflt.htm",
                    // "https://www.globalsecurity.org/military/agency/navy/comscfe.htm",
                    // "https://www.globalsecurity.org/military/agency/navy/comnavsurflant.htm",
                    //   "https://www.globalsecurity.org/military/agency/navy/comnavsurfpac.htm",
                    // "https://www.globalsecurity.org/military/agency/navy/minewarcom.htm",
                    // "https://www.globalsecurity.org/military/agency/navy/navspecwarcom.htm",
                    // "https://www.globalsecurity.org/military/agency/navy/comsublant.htm",
                   // "https://www.globalsecurity.org/military/agency/navy/comsubpac.htm",
                   // "https://www.globalsecurity.org/military/agency/navy/comnavairpac.htm",
                  //  "https://www.globalsecurity.org/military/agency/navy/comnavairlant.htm",
                   // "https://www.globalsecurity.org/military/agency/navy/patreconforlant.htm",
                  //  "https://www.globalsecurity.org/military/agency/navy/patreconforpac.htm",
                 //   "https://www.globalsecurity.org/military/agency/navy/fairwestpac.htm",
                 //   "https://www.globalsecurity.org/military/agency/navy/fairkef.htm",
                //    "https://www.globalsecurity.org/military/agency/navy/fairmed.htm",
              //      "https://www.globalsecurity.org/military/agency/navy/msc.htm",
            //        "https://www.globalsecurity.org/military/agency/navy/navresfor.htm",
                   // "https://www.globalsecurity.org/military/agency/navy/faswc.htm",
                   // "https://www.globalsecurity.org/military/agency/navy/seabee.htm",
                   // "https://www.globalsecurity.org/military/agency/navy/cnatra.htm",
                   // "https://www.globalsecurity.org/military/agency/navy/cnet.htm",
                    //"https://www.globalsecurity.org/military/agency/dod/jfcom.htm"
                //};

                //string[] urls =
              //  {
                    //"https://www.globalsecurity.org/military/agency/army/101tc.htm",
                 //   "https://www.globalsecurity.org/military/agency/army/30in-bde.htm",
                   // "https://www.globalsecurity.org/military/agency/army/65tc.htm",
                    //"https://www.globalsecurity.org/military/agency/army/arng-al.htm",
                    //"https://www.globalsecurity.org/military/agency/army/arng-ak.htm",
                    //"https://www.globalsecurity.org/military/agency/army/arng-az.htm",
                  //  "https://www.globalsecurity.org/military/agency/army/arng-ar.htm",
                    //"https://www.globalsecurity.org/military/agency/army/arng-ca.htm",
                    //"https://www.globalsecurity.org/military/agency/army/arng-co.htm",
                    //"https://www.globalsecurity.org/military/agency/army/arng-ct.htm",
                    //"https://www.globalsecurity.org/military/agency/army/arng-de.htm",
                    //"https://www.globalsecurity.org/military/agency/army/arng-dc.htm",
                    //"https://www.globalsecurity.org/military/agency/army/arng-fl.htm",
                    //"https://www.globalsecurity.org/military/agency/army/arng-ga.htm",
                    //"https://www.globalsecurity.org/military/agency/army/arng-gu.htm",
                    //"https://www.globalsecurity.org/military/agency/army/arng-hi.htm",
                    //"https://www.globalsecurity.org/military/agency/army/arng-id.htm",
                    //"https://www.globalsecurity.org/military/agency/army/arng-il.htm",
                    //"https://www.globalsecurity.org/military/agency/army/arng-in.htm",
                    //"https://www.globalsecurity.org/military/agency/army/arng-ia.htm",
                    //"https://www.globalsecurity.org/military/agency/army/arng-ks.htm",
                    //"https://www.globalsecurity.org/military/agency/army/arng-ky.htm",
                    //"https://www.globalsecurity.org/military/agency/army/arng-la.htm",
                    //"https://www.globalsecurity.org/military/agency/army/arng-me.htm",
                    //"https://www.globalsecurity.org/military/agency/army/arng-md.htm",
                    //"https://www.globalsecurity.org/military/agency/army/arng-ma.htm",
                  //  "https://www.globalsecurity.org/military/agency/army/arng-mi.htm",
                    //"https://www.globalsecurity.org/military/agency/army/arng-mn.htm",
                    //"https://www.globalsecurity.org/military/agency/army/arng-ms.htm",
                    //"https://www.globalsecurity.org/military/agency/army/arng-mo.htm",
                    //"https://www.globalsecurity.org/military/agency/army/arng-mt.htm",
                    //"https://www.globalsecurity.org/military/agency/army/arng-ne.htm",
                    //"https://www.globalsecurity.org/military/agency/army/arng-nv.htm",
                    //"https://www.globalsecurity.org/military/agency/army/arng-nh.htm",
                    //"https://www.globalsecurity.org/military/agency/army/arng-nj.htm",
                    //"https://www.globalsecurity.org/military/agency/army/arng-nm.htm",
                    //"https://www.globalsecurity.org/military/agency/army/arng-ny.htm",
                    //"https://www.globalsecurity.org/military/agency/army/arng-nc.htm",
                    //"https://www.globalsecurity.org/military/agency/army/arng-nd.htm",
                    //"https://www.globalsecurity.org/military/agency/army/arng-oh.htm",
                //    "https://www.globalsecurity.org/military/agency/army/arng-ok.htm",
                    //"https://www.globalsecurity.org/military/agency/army/arng-or.htm",
                    //"https://www.globalsecurity.org/military/agency/army/arng-pa.htm",
                   // "https://www.globalsecurity.org/military/agency/army/arng-pr.htm",
                    //"https://www.globalsecurity.org/military/agency/army/arng-ri.htm",
                    //"https://www.globalsecurity.org/military/agency/army/arng-sc.htm",
                    //"https://www.globalsecurity.org/military/agency/army/arng-sd.htm",
                    //"https://www.globalsecurity.org/military/agency/army/arng-tn.htm",
                    //"https://www.globalsecurity.org/military/agency/army/arng-tx.htm",
                    //"https://www.globalsecurity.org/military/agency/army/arng-vi.htm",
                    //"https://www.globalsecurity.org/military/agency/army/arng-ut.htm",
                    //"https://www.globalsecurity.org/military/agency/army/arng-vt.htm",
                    //"https://www.globalsecurity.org/military/agency/army/arng-va.htm",
                    //"https://www.globalsecurity.org/military/agency/army/arng-wa.htm",
                    //"https://www.globalsecurity.org/military/agency/army/arng-wv.htm",
                    //"https://www.globalsecurity.org/military/agency/army/arng-wi.htm",
                    //"https://www.globalsecurity.org/military/agency/army/arng-wy.htm",
                    //"https://www.globalsecurity.org/military/agency/army/ossa.htm",

            //    };
                //string[] urls = {
                //   "https://www.globalsecurity.org/military/agency/army/sbccom.htm"
                //   "https://www.globalsecurity.org/military/agency/army/tacom.htm"
                //   "https://www.globalsecurity.org/military/agency/navy/apsron4.htm"
                //  "https://www.globalsecurity.org/military/agency/army/516sigbde.htm"
                //  "https://www.globalsecurity.org/military/agency/army/58sig.htm"
                //  "https://www.globalsecurity.org/military/agency/army/aps-5.htm",
                //  "https://www.globalsecurity.org/military/agency/army/asc.htm",
                //   "https://www.globalsecurity.org/military/agency/army/jtf-ku.htm"
                //   "https://www.globalsecurity.org/military/agency/army/arcent.htm"
                //      "https://www.globalsecurity.org/military/agency/army/forscom.htm",
                //    "https://www.globalsecurity.org/military/agency/army/cid.htm",
                //    "https://www.globalsecurity.org/military/agency/army/usace.htm",
                //                  "https://www.globalsecurity.org/military/agency/army/mdw.htm",
                // "https://www.globalsecurity.org/military/agency/army/amc.htm",
                //                   "https://www.globalsecurity.org/military/agency/army/amcom.htm"
                //"https://www.globalsecurity.org/military/agency/army/usaeur.htm",
                //  "https://www.globalsecurity.org/military/agency/army/40id.htm" 
                //    "https://www.globalsecurity.org/military/agency/army/hqda.htm",

                //                };

                //string[] urls =
                //{
                // //   "https://www.globalsecurity.org/military/agency/army/262qm.htm",
                //    "https://www.globalsecurity.org/military/agency/army/usaac.htm",
                //    "https://www.globalsecurity.org/military/agency/army/adas.htm",
                //    "https://www.globalsecurity.org/military/agency/army/ags.htm",
                //    "https://www.globalsecurity.org/military/agency/army/usaas.htm",
                //    "https://www.globalsecurity.org/military/agency/army/usaavnc.htm",
                //    "https://www.globalsecurity.org/military/agency/army/usaals.htm",
                //    "https://www.globalsecurity.org/military/agency/army/usachcs.htm",
                //    "https://www.globalsecurity.org/military/agency/army/usacmls.htm",
                //    "https://www.globalsecurity.org/military/agency/army/eschool.htm",
                //    "https://www.globalsecurity.org/military/agency/army/usafas.htm",
                //    "https://www.globalsecurity.org/military/agency/army/finschool.htm",
                //    "https://www.globalsecurity.org/military/agency/army/usaic.htm",
                //    "https://www.globalsecurity.org/military/agency/army/tjagsa.htm",
                //    "https://www.globalsecurity.org/military/agency/army/usamps.htm",
                //    "https://www.globalsecurity.org/military/agency/army/usaocs.htm",
                //    "https://www.globalsecurity.org/military/agency/army/usaommcs.htm",
                //    "https://www.globalsecurity.org/military/agency/army/usaqmcs.htm",
                //    "https://www.globalsecurity.org/military/agency/army/sigcen.htm",
                //    "https://www.globalsecurity.org/military/agency/army/transchool.htm",
                //    "https://www.globalsecurity.org/military/agency/army/usaic.htm",
                //    "https://www.globalsecurity.org/military/agency/army/usatc.htm",
                //    "https://www.globalsecurity.org/military/agency/army/manscen.htm",
                //    "https://www.globalsecurity.org/military/agency/army/whinsec.htm",
                //    "https://www.globalsecurity.org/military/agency/army/cascom.htm",
                //    "https://www.globalsecurity.org/military/agency/army/satmo.htm",
                //    "https://www.globalsecurity.org/military/agency/army/satfa.htm",
                //    "https://www.globalsecurity.org/military/agency/army/mata.htm",
                //    "https://www.globalsecurity.org/military/agency/army/aawo.htm",
                //    "https://www.globalsecurity.org/military/agency/army/soa.htm",
                //    "https://www.globalsecurity.org/military/agency/army/tass.htm"
                //};

                foreach (var url in urls)
                {
                    var unitcodefilename = url.Substring(Liaison.Helper.GlobalSecurityHelper.gsec.Length).Replace("/", "-");
                GetWebSite(webClient, url);

                    Console.WriteLine("Units: " + masterlist.Count);

                    XmlSerializer writer = new XmlSerializer(masterlist.GetType());

                    DirectoryInfo di = new DirectoryInfo("D:\\repos\\Liaison\\Liaison.Import.GlobalSecurity\\output\\");
                    if (!di.Exists)
                    {
                        di.Create();
                    }

                    System.IO.FileStream file = System.IO.File.Create("D:\\repos\\Liaison\\Liaison.Import.GlobalSecurity\\output\\" + unitcodefilename + ".xml");

                    writer.Serialize(file, masterlist);
                    masterlist = new List<GlobalSecurityObject>();
                    file.Close();
                }

                Console.ReadLine();
            }
        }

        private static void GetWebSite(WebClient webClient, string url)
        {
            //url = ReplaceUrls(url);



            Console.WriteLine("Scanning " + url);

            bool ok = false;
            var doc = new HtmlDocument();
            int counter = 0;

            while (!ok)
            {
                counter++;
                Console.WriteLine("Attempt " + counter);
                try
                {
                    string html = webClient.DownloadString(url);
                    doc.LoadHtml(html);
                }
                catch (System.Net.WebException e)
                {
                    ok = true;
                    return;
                }
               
                //var test = doc.DocumentNode.Descendants().Where(n => n.GetAttributeValue("id", "") == "breadcrumbs");
                //if (test.FirstOrDefault() != null)
                //{
                    ok = true;
                //}
            }

            var obj = new GlobalSecurityObject();

            var titleNode = doc.DocumentNode.Descendants().Where(n => n.Name == "h1").FirstOrDefault(n => n.InnerText != "Military");
            if (titleNode==null)
            {
                titleNode = doc.DocumentNode.Descendants().Where(n => n.Name == "h2" && n.InnerText!="Further Reading").FirstOrDefault(n => n.InnerText != "Military");
            }

            var a = doc.DocumentNode.Descendants().FirstOrDefault(n => n.GetAttributeValue("id", "") == "content");
            var b = a.Descendants().Where(n => n.Name == "img");
            var imageNodes = doc.DocumentNode.Descendants().FirstOrDefault(n => n.GetAttributeValue("id", "") == "content").Descendants().Where(n => n.Name == "img");

            var imageNode = imageNodes.FirstOrDefault(n => n.OuterHtml.Contains("src=\"images"));


            //string nameText;
           // string formerName = null ;
            obj.NameNode = titleNode.InnerText.Replace(Environment.NewLine, "").Replace("<strike>", "ex. ").Replace("</strike>", "").Trim();
            obj.Url = url;

            StringBuilder sbName = new StringBuilder();
            StringBuilder sbFormer = new StringBuilder();
            StringBuilder sbNickName = new StringBuilder();
            foreach (var node in titleNode.ChildNodes)
            {
                var text = node.InnerText.Trim();

                if (!string.IsNullOrWhiteSpace(text))
                {
                    if (node.Name == "strike")
                    {
                        if (sbFormer.Length != 0)
                        {
                            sbFormer.Append("; ");
                        }
                        sbFormer.Append(text);
                    }
                    if (node.Name == "#text")
                    {
                        if (text.StartsWith("\"") && text.EndsWith("\""))
                        {
                            sbNickName.Append(text);
                        }
                        else
                        {
                            if (sbName.Length != 0)
                            {
                                sbName.Append("; ");
                            }

                            sbName.Append(text);
                        }
                    }
                }
            }

            obj.FullName = sbName.ToString() ;
            obj.NameFormer = sbFormer.ToString() ;
            obj.NickName = sbNickName.ToString();
            //<img align="left" src="images/usaeur.gif">
            if (imageNode != null)
            {
                var startpos = imageNode.OuterHtml.IndexOf("src=\"") + 5;
                var endpos = imageNode.OuterHtml.IndexOf("\"", startpos);
                var imageSrc = imageNode.OuterHtml.Substring(startpos, endpos - startpos);

                obj.LogoUrl = url.Substring(0, url.LastIndexOf("/") + 1) + imageSrc;
            }

            string urldflt = "";
            string servicetxt = "";
            if (url.Contains("agency"))
            {
                if (url.Contains("military/agency"))
                {
                    urldflt = url.Substring("https://www.globalsecurity.org/military/agency/".Length);
                    servicetxt = urldflt.Substring(0, urldflt.IndexOf("/"));
                }
                else if (url.Contains("space/agency"))
                {
                    urldflt = url.Substring("https://www.globalsecurity.org/space/agency/".Length);
                    servicetxt = "space";
                }
                else if (url.Contains("wmd/agency"))
                {
                    urldflt = url.Substring("https://www.globalsecurity.org/wmd/agency/".Length);
                    servicetxt = "usaf";
                }
                else
                {
                    throw new Exception ();
                }
               
            }

            var breadcrumbnodes = doc.DocumentNode.Descendants().Where(n => n.GetAttributeValue("id", "") == "breadcrumbs");
            var breadcrumbnode = breadcrumbnodes.FirstOrDefault();
           // obj.Breadcrumbs = GetBreadcrumbTrail(breadcrumbnode, url);
            //bool isReserve = breadcrumbnode.InnerHtml.Contains("<a href=\"usar.htm\">Reserve</a>");
            bool isReserve = false;

            var navrightnodes = doc.DocumentNode.Descendants().Where(n => n.GetAttributeValue("id", "") == "navright");            
            var navrightDiv = navrightnodes.FirstOrDefault();
            if (navrightDiv != null)
            {
                var navrightTable = navrightDiv.ChildNodes.FirstOrDefault(n => n.Name == "table");
                var navrightTbody = navrightTable.ChildNodes.FirstOrDefault(n => n.Name == "tbody");
                HtmlNode navrightTr;
                if (navrightTbody == null)
                {
                    navrightTr = navrightTable.ChildNodes.FirstOrDefault(n => n.Name == "tr");
                }
                else
                {
                    navrightTr = navrightTbody.ChildNodes.FirstOrDefault(n => n.Name == "tr");
                }

                var navrightTd = navrightTr.ChildNodes.Where(n => n.Name == "td").FirstOrDefault();

                List<TableNode> list = new List<TableNode>();

                TableNode tablenode = null;

                GetTableNodes(ref list, ref tablenode, navrightTd, url);
                obj.TableNodes = list;

                var tContent = new List<DescriptionObj>();

                var content = doc.DocumentNode.Descendants()
                    .FirstOrDefault(n => n.GetAttributeValue("id", "") == "content");
                foreach (var node in content.ChildNodes)
                {
                    switch (node.Name)
                    {
                        case "h1":
                        case "h2":
                        case "h3":
                        case "h4":
                        {
                            tContent.Add(new DescriptionObj(node.Name, node.InnerHtml, false));
                            break;
                        }
                        case "p":
                        {
                            foreach (var childnode in node.ChildNodes)
                            {
                                switch (childnode.Name)
                                {
                                    case "img":
                                    {
                                        tContent.Add(new DescriptionObj("img",
                                            url.Substring(0, url.LastIndexOf('/') + 1) + childnode.Attributes
                                                .Where(n => n.Name == "src").First().Value, false));
                                        break;
                                    }
                                    case "#text":
                                    case "i":
                                    case "sup":
                                        case "em":
                                    case "strong":
                                    case "u":
                                    case "b":
                                    case "p":
                                    case "strike":
                                    case "o": //wtf?
                                    case "ul":
                                        case "ol":
                                    {
                                        if (!string.IsNullOrWhiteSpace(childnode.InnerText))
                                        {
                                            tContent.Add(new DescriptionObj(childnode.Name, childnode.InnerText.Trim(),
                                                childnode.Name == "i"));
                                        }

                                        break;
                                    }
                                    case "a":
                                    {
                                        var href = childnode.Attributes.Where(anch => anch.Name == "href").First()
                                            .Value;
                                        if (href.StartsWith("/"))
                                        {
                                            href = "https://www.globalsecurity.org" + href;
                                        }
                                        else if (href.StartsWith("images"))
                                        {
                                            href = url.Substring(0, url.LastIndexOf('/') + 1) + href;
                                        }

                                        tContent.Add(new DescriptionObj("a", childnode.InnerText.Trim(), href, false));
                                        break;
                                    }
                                    case "style":
                                    case "br":
                                    case "center":
                                    case "table":
                                    case "#comment":
                                    case "span":
                                    case "div":
                                    case "script":
                                        
                                    {
                                        continue;
                                    }
                                    default:
                                    {
                                        throw new Exception(childnode.Name);
                                    }
                                }
                            }

                            break;
                        }
                        case "hr":
                        case "span":
                        case "table":
                        case "section":
                        case "#text":
                        case "script":
                        case "pmarines":
                        case "style":
                        case "#comment":
                        case "br":
                        case "cp":
                        case "p=\"\"":
                        {
                            continue;
                        }
                        case "div":
                            {
                                string sClass = node.GetAttributeValue("id", "");
                                switch (sClass)
                                {
                                    case "":
                                    case "sidebar":
                                    case "main":
                                    case "cntnt1":
                                    case "crt-2":
                                    case "crt-21":
                                    case "crt-22":
                                    case "crt-3":
                                    case "box1":
                                    case "cbox1":
                                    case "ezoic-content":
                                    case "ezoic-top-image":
                                    case "ez-clearholder-medrectangle-3":
                                    case "ez-clearholder-medrectangle-4":
                                        {
                                            continue;
                                        }
                                    default:
                                        {
                                            throw new Exception(sClass);
                                        }
                                }
                            }
                        case "img":
                        {
                            tContent.Add(new DescriptionObj("img",
                                node.Attributes.Where(n => n.Name == "src").First().Value, false));
                            break;
                        }
                        case "center":
                        {
                            foreach (var ahref in node.Descendants().Where(nn => nn.Name == "a"))
                            {
                                var imgurl = url.Substring(0, url.LastIndexOf('/') + 1) +
                                             ahref.Attributes.Where(n => n.Name == "href").First().Value;
                                tContent.Add(new DescriptionObj("img", imgurl, false));
                            }

                            break;
                        }
                        case "ol":
                        case "ul":
                        case "dl":
                        {
                            foreach (var li in node.Descendants().Where(nn => nn.Name == "li"))
                            {
                                tContent.Add(new DescriptionObj("li", li.InnerText.Trim(), false));
                            }

                            break;
                        }
                        case "li":
                        {
                            tContent.Add(new DescriptionObj("li", node.InnerText.Trim(), false));
                            break;
                        }
                        case "b":
                        case "strong":
                        case "font":
                        case "strike":
                        {
                            tContent.Add(new DescriptionObj(node.Name, node.InnerText.Trim(), false));
                            break;
                        }
                        case "blockquote":
                        {
                            tContent.Add(new DescriptionObj("blockquote", node.InnerText.Trim(), false));
                            break;
                            }
                        case "i":
                        {
                            tContent.Add(new DescriptionObj("i", node.InnerText.Trim(), false));
                            break;
                        }
                        case "a":
                        {
                            try
                            {
                                tContent.Add(new DescriptionObj("a", node.InnerText.Trim(),
                                    node.Attributes.First(ahref => ahref.Name == "href").Value, false));
                            }
                            catch (Exception e)
                            {
                                    //carry on
                                    Console.ReadKey();

                            }

                            break;
                        }
                        case "p.the":
                        {
                            var outer = node.OuterHtml.Replace("<p.", string.Empty);
                            outer = outer.Replace("=\"\"", string.Empty );
                            outer = outer.Replace("></p.the>", string.Empty);
                            tContent.Add(new DescriptionObj("p", outer, false));
                            break;
                        }
                        case "pre":
                        {
                                tContent.Add(new DescriptionObj("pre", node.InnerText.Trim(), false));
                            break;
                        }
                        default:
                        {
                            throw new Exception(node.Name);
                        }
                    }

                }


                obj.Description = tContent;

                var navrightnode = navrightnodes.FirstOrDefault().Descendants().Where(n => n.Name == "h3");
                var nationalguardnodes = navrightnode.Where(n => n.InnerHtml.Contains("National Guard"));
                var isNationalGuard = nationalguardnodes.Any();
                string state = null;
                if (isNationalGuard)
                {
                    state = nationalguardnodes.First().InnerText.Replace("National Guard", "").Trim();
                }

                GetServiceAndComponent(obj, servicetxt, isReserve, isNationalGuard, state);
               
                obj.Locations = GetLocations(obj.TableNodes.FirstOrDefault(tn => tn.h3 == "Facilities"));

                //var x = obj.TableNodes.Where(c => c.li == null).ToList();
                //var y = obj.TableNodes.Where(c => c.li.Count == 0).ToList();
                //var z = obj.TableNodes.Where(c => c.h3 != null).ToList();

                //var xy = obj.TableNodes.Where(c => c.li == null || c.li.Count == 0).ToList();
                //var xyz = obj.TableNodes.Where(c => c.h3 != null).ToList();
               // var xyzz = obj.TableNodes.Where(c => (c.li == null || c.li.Count == 0) && c.h3 != null).ToList();

                obj.HigherHq = GetHigherHq(obj.Breadcrumbs,
                    obj.TableNodes.Where(c => (c.li == null || c.li.Count == 0) && c.h3 != null).ToList());
                //obj.Children = GetChildren(obj.TableNodes.Where(tn => tn.h3 == "Units"|| tn.h3== "Major Subordinate Commands"||tn.h3 == "Related Units").First()); 
                obj.Children = new List<SubUnitObject>();
                
                var units = obj.TableNodes.Where(tn => _h3S.Contains(tn.h3));

                //var units = obj.TableNodes.Where(tn =>
                //    tn.h3 == "Units" || tn.h3 == "Major Subordinate Commands" || tn.h3 == "Related Units" ||
                //    tn.h3 == "Associated Units");
                foreach (var unit in units)
                {
                    var children = GetChildren(unit);
                    foreach (var child in children)
                    {
                        obj.Children.Add(child);
                    }
                }

                masterlist.Add(obj);
                foreach (var child in obj.Children)
                {
                    if (child.Url.StartsWith(_gsUrl) && !child.Url.EndsWith(".htm"))
                    {
                        child.Url = child.Url + ".htm";
                    }
                    GetWebSite(webClient, child.Url);
                }
            }
        }

        private static string ReplaceUrls(string url)
        {
            var list = new Dictionary<string, string>();

            list.Add(
                "https://www.globalsecurity.org/military/agency/army/30in-bde.htm",
                "file:///C:/Users/surfe/Downloads/globalsecurity/30th%20Enhanced%20Heavy%20Separate%20Brigade%20(eHSB)%20(Mech).html");

            if (list.ContainsKey(url))
            {
                return url;
            }

            return list[url];
        }

        private static void GetTableNodes(ref List<TableNode> list, ref TableNode tablenode, HtmlNode navrightTd, string url)
        {
            for (int i = 0; i < navrightTd.ChildNodes.Count; i++)
            {
                var tdChildNode = navrightTd.ChildNodes[i];
                var x = tdChildNode.InnerText;
                if (tdChildNode.Name == "#text" || tdChildNode.Name == "#comment" || tdChildNode.Name == "br" ||
                    tdChildNode.Name == "div")
                {
                    // do nothing   
                    if (!string.IsNullOrWhiteSpace(tdChildNode.InnerText.Trim()))
                    {
                        AddLi(tdChildNode, url, ref tablenode, ref list);
                    }
                }
                else if (tdChildNode.Name == "li")
                {
                    GetTableNodes(ref list, ref tablenode, tdChildNode, url);
                }
                else if (tdChildNode.Name == "h3equipment<")
                {
                    var h3url = tdChildNode?.ChildNodes?.Where(n => n.Name == "a").FirstOrDefault()?.Attributes
                        ?.First(n => n.Name == "href")?.Value;
                    tablenode = new TableNode();
                    tablenode.h3 = "Equipment";
                    tablenode.href = h3url == null ? null : url.Substring(0, url.LastIndexOf('/') + 1) + h3url;

                    if (list.Any(l => l.h3 == tdChildNode.InnerText))
                    {
                        return;
                    }

                    if (navrightTd.ChildNodes.Count - 1 < i + 2)
                    {
                        list.Add(tablenode);
                        tablenode = null;
                    }
                    else
                    {
                        if (navrightTd.ChildNodes[i + 2].Name != "ul")
                        {
                            list.Add(tablenode);
                            tablenode = null;
                        }
                    }
                }            
            else if (tdChildNode.Name=="h2"||tdChildNode.Name == "h3" || tdChildNode.Name == "h4")
                {
                    var h3url = tdChildNode?.ChildNodes?.Where(n => n.Name == "a").FirstOrDefault()?.Attributes
                        ?.First(n => n.Name == "href"||n.Name=="herf")?.Value;
                    tablenode = new TableNode();
                    tablenode.h3 = tdChildNode.InnerText;
                    tablenode.href = h3url == null ? null : url.Substring(0, url.LastIndexOf('/') + 1) + h3url;

                    if (list.Any(l => l.h3 == tdChildNode.InnerText))
                    {
                        return;
                    }

                    if (navrightTd.ChildNodes.Count - 1 < i + 2)
                    {
                        list.Add(tablenode);
                        tablenode = null;
                    }
                    else
                    {
                        if (tdChildNode.InnerText == "Pre-Modular Division")
                        {
                            tablenode = new TableNode();
                            tablenode.h3 = "Pre-Modular Division";

                            GetTableNodes(ref list, ref tablenode, tdChildNode, url);
                        }
                        else if (navrightTd.ChildNodes[i + 2].Name != "ul")
                        {
                            list.Add(tablenode);
                            tablenode = null;
                        }
                    }
                }
                else if (tdChildNode.Name == "b")
                {
                    if (tdChildNode?.ChildNodes?.Where(n => n.Name == "h3").Any() == true)
                    {
                        GetTableNodes(ref list, ref tablenode, tdChildNode, url);
                    }
                    else
                    {
                        var bUrl = tdChildNode?.ChildNodes?.Where(n => n.Name == "a").FirstOrDefault()?.Attributes
                            ?.First(n => n.Name == "href")?.Value;
                        tablenode = new TableNode();
                        tablenode.h3 = tdChildNode.InnerText;
                        tablenode.href = bUrl == null ? null : url.Substring(0, url.LastIndexOf('/') + 1) + bUrl;
                        if (list.Any(l => l.h3 == tdChildNode.InnerText))
                        {
                            return;
                        }

                        if (tdChildNode.InnerText != "Amphibious Task Force 03")
                        {
                            list.Add(tablenode);
                            tablenode = null;
                        }
                    }
                }
                else if (tdChildNode.Name == "ul"||tdChildNode.Name=="ol")
                {
                    if (tablenode == null)
                    {
                        tablenode = new TableNode();
                    }

                    foreach (var nodeul in tdChildNode.ChildNodes)
                    {
                        var nit = nodeul.InnerText;
                        if (nodeul.Name == "#text" || nodeul.Name == "#comment" || nodeul.Name == "br")
                        {
                            // do nothing
                        }
                        else if (nodeul.Name == "li" || nodeul.Name == "b"||nodeul.Name=="u")
                        {
                            AddLi(nodeul, url, ref tablenode, ref list);
                        }
                        else if (nodeul.Name == "strike" || nodeul.Name == "h3")
                        {
                            if (list.Any(l => l.h3 == nodeul.InnerText))
                            {
                                return;
                            }

                            if (nodeul.InnerText == "Pre-Modular")
                            {
                                list.Add(tablenode);
                                tablenode = null;
                                GetTableNodes(ref list, ref tablenode, tdChildNode, url);
                            }
                            else if (nodeul.InnerText == "Pre-Modular Division")
                            {
                                list.Add(tablenode);
                                tablenode = new TableNode();
                                tablenode.h3 = "Pre-Modular Division";
                            }

                            //GetTableNodes(ref list, ref tablenode, tdChildNode, url);
                        }
                        else if (nodeul.Name == "ul"||nodeul.Name=="p")
                        {
                            if (nodeul.InnerText.Trim() == "Amphibious Task Force 03")
                            {
                                list.Add(tablenode);
                                tablenode=null;
                            }
                            GetTableNodes(ref list, ref tablenode, nodeul, url);
                        }
                        else if (nodeul.Name == "img")
                        {
                            var tnimg = new TableNode();
                            tnimg.image = nodeul.Attributes["src"].ToString();
                            tnimg.h3 = "Image";
                            list.Add(tnimg);
                        }
                        else if (nodeul.Name == "a")
                        {
                            AddLi(nodeul, url, ref tablenode, ref list);
                        }
                        else
                        {
                            throw new Exception(nodeul.Name);
                        }
                    }

                    var tnh3 = tablenode.h3;
                    if (list.Any(l => l.h3 == tnh3))
                    {
                        return;
                    }

                    list.Add(tablenode);
                    tablenode = null;
                }
                else if (tdChildNode.Name == "center")
                {
                    foreach (var nodeul in tdChildNode.ChildNodes)
                    {
                        if (nodeul.Name == "#text" || nodeul.Name == "br")
                        {
                            // do nothing
                        }
                        else if (nodeul.Name == "a")
                        {
                            if (nodeul.Attributes.FirstOrDefault().Name == "href")
                            {
                                tablenode = new TableNode();
                                tablenode.image = url.Substring(0, url.LastIndexOf('/') + 1) + nodeul.Attributes.FirstOrDefault(attr => attr.Name == "href").Value;
                                if (list.Any(l => l.h3 == tdChildNode.InnerText))
                                {
                                    return;
                                }


                                list.Add(tablenode);
                                tablenode = null;
                            }
                            else
                            {
                                throw new Exception("a");
                            }
                        }
                        else
                        {
                            throw new Exception(nodeul.Name);
                        }
                    }
                }
                else if (tdChildNode.Name == "p")
                {
                    GetTableNodes(ref list, ref tablenode, tdChildNode, url);
                }
                else if (tdChildNode.Name == "strike")
                {
                    GetTableNodes(ref list, ref tablenode, tdChildNode, url);                    
                }
                else if (tdChildNode.Name == "a")
                {
                    if (tdChildNode.Attributes.FirstOrDefault().Name == "href")
                    {
                        AddLi(tdChildNode, url, ref tablenode, ref list);                        
                    }
                    else
                    {
                        throw new Exception("a");
                    }
                }
                else if (tdChildNode.Name == "hr")
                {
                    // do nothing
                }
                else if (tdChildNode.Name == "img")
                {
                    var tnimg = new TableNode();
                    tnimg.image = tdChildNode.Attributes["src"].ToString();
                    tnimg.h3 = "Image";
                    list.Add(tnimg);
                }
                else
                {
                    throw new Exception(tdChildNode.Name);
                }

            }

        }

        private static void AddLi(HtmlNode nodeul, string url, ref TableNode tablenode, ref List<TableNode> list)
        {
            var li = new li();
            li.text = nodeul.InnerText.Replace("Prior", "").Trim();
            if (nodeul.FirstChild?.Name == "a")
            {
                var xx = nodeul.FirstChild.Attributes.First(c => c.Name == "href").Value;
                if (xx.StartsWith("/"))
                {
                    li.href = _gsUrl + xx;
                }
                else if (xx == "usmepcom.htm")
                {
                    li.text = "Military Entrance Processing Command";
                    li.href = null;
                }
                else if (xx.StartsWith("http"))
                {
                    li.href = xx;
                }
                else if (xx.StartsWith("../../ops") || xx.StartsWith("../../systems")||xx.Contains("341mi")||xx.Contains("415mi"))
                {
                    //do nothing
                }
                else if (xx.StartsWith("../../agency"))
                {
                    var urlagencypos = url.IndexOf("/agency") + 7;
                    var xxstartpos = xx.IndexOf("/agency") + 7;
                    var href = url.Substring(0, urlagencypos) + xx.Substring(xxstartpos);
                    li.href = href;
                }
                else if (xx.StartsWith("../agency"))
                {
                    var urlagencypos = url.IndexOf("/facility");
                    var xxstartpos = xx.IndexOf("/agency");
                    var href = url.Substring(0, urlagencypos) + xx.Substring(xxstartpos);
                    li.href = href;
                }
                else if (xx.StartsWith("../"))
                {
                    var urlagencypos = url.IndexOf("/agency/") + 8;
                    var xxstartpos = xx.IndexOf("../") + 3;
                    var href = url.Substring(0, urlagencypos) + xx.Substring(xxstartpos);
                    li.href = href;
                }
                else
                {
                    li.href = url.Substring(0, url.LastIndexOf('/') + 1) + xx;
                }
            }
            else if (nodeul.Attributes.Any(a => a.Name == "href") && (!string.IsNullOrWhiteSpace(nodeul.InnerText)))
            {
                li.href = _gsUrl + nodeul.Attributes.First(a => a.Name == "href").Value;
            }

            if (tablenode != null && _h3S.Contains(tablenode.h3) && li.href != null && !li.href.StartsWith(_gsUrl))
            {
                li.href = null;
            }

            if (string.IsNullOrWhiteSpace(li.text) && string.IsNullOrEmpty(li.href))
            {
                // don't add
            }
            else
            {
                li.Former = nodeul.XPath.Contains("strike") ||
                            (nodeul.FirstChild != null &&
                             (nodeul.FirstChild.InnerHtml.Contains("<strike>") &&
                              nodeul.FirstChild.InnerHtml.Contains("</strike>")));
                if (tablenode != null)
                {
                    tablenode.li.Add(li);
                    if (nodeul.InnerText.Contains("Prior"))
                    {
                        list.Add(tablenode);
                        tablenode=new TableNode();
                        tablenode.h3 = "Prior";
                    }
                }
                else
                {
                    //throw new Exception();
                }
            }
        }


        private static List<SubUnitObject> GetChildren(TableNode tableNode)
        {
            var suo = new List<SubUnitObject>();
            var agency = "agency/";
            foreach (var child in tableNode.li)
            {
                //if (masterlist.Any(l => l.Url == child.href))
                //{
                //    throw new Exception();
                //}
                if (child.href != null && !masterlist.Any(l => l.Url == child.href) && !child.href.EndsWith("223mi.htm") &&!child.href.EndsWith("260mi.htm"))
                {
                    var startpos = child.href.IndexOf(agency) + agency.Length;
                    var id = child.href.Substring(startpos);
                    suo.Add(new SubUnitObject { FullName = child.text, IsIndirect = false, Id = id, Url = child.href, Name = child.text });
                }
            }
            return suo;
        }

        private static List<HigherHqObject> GetHigherHq(List<li> breadcrumbs, List<TableNode> tableNodes)
        {
            var hh = new List<HigherHqObject>();
            var substringstart = (_gsUrl + "military/agency/").Length;
            //var id = breadcrumbs.Last().href.Substring(substringstart+1);
           // hh.Add(new HigherHqObject { IsCurrent = true, Id = id, Url = breadcrumbs.Last().href, Type ="Assigned", Name = breadcrumbs.Last().text, });
            foreach (var node in tableNodes)
            {
                string nodeid = null;
                if (!string.IsNullOrWhiteSpace(node.href))
                {
                    nodeid = node.href.Substring(substringstart);
                }
                else
                {
                    nodeid = null;
                }
                hh.Add(new HigherHqObject { IsCurrent = true,
                    Url =node.href, Name = node.h3, Id = nodeid,
                    Type = node.h3.Contains("National Guard")? "Part of": "Unknown"});
            }
            return hh;
        }

        private static List<LocationObject> GetLocations(TableNode tableNode)
        {
            if (tableNode == null)
            {
                return null;
            }

            var r = new List<LocationObject>();
            foreach (var l in tableNode.li)
            {
                int endpos = l.text.IndexOf(',');
                if (endpos > 0)
                {
                    var basename = l.text.Substring(0, endpos);
                    var location = l.text.Substring(endpos + 1).Trim();
                    string note = null;
                    if (location.Contains("(") && location.Contains(")"))
                    {
                        var bracketstart = location.IndexOf("(")+1;
                        var bracketend = location.IndexOf(")");
                        var brackets = location.Substring(bracketstart, bracketend - bracketstart).Trim();
                        location = location.Replace("(" + brackets + ")", "").Trim();
                        note = brackets;                        
                    }

                    bool isCurrent = true;
                    if (l.Former)
                    {
                        isCurrent = false;
                    }
                    r.Add(new LocationObject { BaseName = basename, Location =location, IsCurrent = isCurrent, Url = l.href, Deployment = false, Note = note });
                }
                else
                {
                    r.Add(new LocationObject {BaseName = l.text, IsCurrent = true, Url = l.href, Deployment = false});
                }
            }
            return r;
        }

        public static bool Former { get; set; }

        private static List<li> GetBreadcrumbTrail(HtmlNode breadcrumbnode, string currenturl)
        {
            var returnable = new List<li>();
            foreach (var node in breadcrumbnode.ChildNodes.Where(n=>n.Name=="a"))
            {
                string tempurl = node.Attributes.First().Value.StartsWith("/") ? "https://www.globalsecurity.org" : currenturl.Substring(0, currenturl.LastIndexOf('/') + 1);
                var href = (tempurl + node.Attributes.First().Value);//.Replace("//", "/");
                var text = node.InnerText;
                returnable.Add(new li { href = href, text = text });
                //returnable.Add(new li { href = })
            }

            return returnable;
        }

        private static void GetServiceAndComponent(GlobalSecurityObject obj, string servicetxt, bool isReserve, bool isNationalGuard, string ngstate)
        {
            if (isReserve)
            {
                obj.UnitComponent = Helper.Enumerators.ServiceTypeBLL.Reserve;
            }
            else if (isNationalGuard)
            {
                obj.UnitComponent = Helper.Enumerators.ServiceTypeBLL.Volunteer;
                obj.UnitNGState = ngstate;
            }
            else
            {
                obj.UnitComponent = Helper.Enumerators.ServiceTypeBLL.Active;
            }

            switch (servicetxt)
            {
                case "army":
                    obj.UnitService = Helper.Enumerators.ServicesBll.Army;
                    return;
                case "navy":
                    obj.UnitService = Helper.Enumerators.ServicesBll.Navy;
                    return;
                case "dot":
                    obj.UnitService = Helper.Enumerators.ServicesBll.CoastGuard;
                    return;
                case "usaf":
                    obj.UnitService = Helper.Enumerators.ServicesBll.AirForce;
                    return;
                case "usmc":
                    obj.UnitService = Helper.Enumerators.ServicesBll.Marines;
                    return;
                case "dod":
                    obj.UnitService = Helper.Enumerators.ServicesBll.Joint;
                    return;
                default:
                    {
                        if (obj.NameNode.StartsWith("Naval Air Station"))
                        {
                            obj.UnitService = Helper.Enumerators.ServicesBll.Navy;
                        }
                        else if (obj.NameNode.EndsWith("Army Depot")||obj.NameNode.StartsWith("Toby"))
                        {
                            obj.UnitService = Helper.Enumerators.ServicesBll.Army;
                        }
                        else if (obj.NameNode.EndsWith("Air National Guard Base"))
                        {
                            obj.UnitService = ServicesBll.AirForce;
                            obj.UnitComponent = ServiceTypeBLL.Volunteer;
                        }
                        else if (obj.NameNode == "Space")
                        {
                            obj.UnitService = ServicesBll.Army;
                            obj.UnitComponent = ServiceTypeBLL.StateVolunteer;
                            obj.UnitNGState = "CO";
                        }
                        else if (obj.NameNode == "Saudi Arabian National Guard")
                        {
                            obj.UnitService = ServicesBll.Army;
                            obj.UnitComponent = ServiceTypeBLL.StateVolunteer;
                            obj.UnitNGState = "Saudi Arabia";
                        }
                        else if (obj.NameNode.Contains("Enhanced Heavy"))
                        {
                            obj.UnitService = ServicesBll.Army;
                            obj.UnitComponent = ServiceTypeBLL.StateVolunteer;
                            if (obj.TableNodes[0].h3.Contains("National Guard"))
                            {
                                obj.UnitNGState = GetNGState(obj.TableNodes[0].h3);
                            }
                        }
                        else if (obj.NameNode.StartsWith("Geographic"))
                        {
                            obj.UnitService = ServicesBll.Marines;
                            obj.UnitComponent = ServiceTypeBLL.Active;
                        }
                        else if (obj.NameNode.StartsWith("Marine Corps Logistics"))
                        {
                            obj.UnitService = ServicesBll.Marines;
                            obj.UnitComponent = ServiceTypeBLL.Active;
                        }
                        else if (obj.NameNode.StartsWith("Weapons of Mass"))
                        {
                            obj.UnitService = ServicesBll.AirForce;
                            obj.UnitComponent = ServiceTypeBLL.Active;
                        }
                        else if (obj.NameNode.StartsWith("FFG-")
                                 || obj.NameNode.StartsWith("DDG-") 
                                 || obj.NameNode.StartsWith("DD-") 
                                 || obj.NameNode.StartsWith("LHD-") 
                                 || obj.NameNode.StartsWith("LPD") 
                                 || obj.NameNode.StartsWith("LSD-")
                                 || obj.NameNode.StartsWith("Landing Craft")
                                 || obj.NameNode.StartsWith("Mechanized Landing")
                                 || obj.NameNode.StartsWith("SSN-")
                                 || obj.NameNode.StartsWith("Deep Subm")
                                 )
                        {
                            obj.UnitService = ServicesBll.Navy;
                            obj.UnitComponent = ServiceTypeBLL.Active;
                        }
                        else if (obj.NameNode.StartsWith("Manama"))
                        {
                            obj.UnitService = ServicesBll.Navy;
                            obj.UnitComponent = ServiceTypeBLL.Active;
                        }
                        else if (obj.NameNode.StartsWith("T-A")
                        || obj.NameNode.StartsWith("ARDM")
                                 || obj.NameNode.StartsWith("AS-")
                                 || obj.NameNode.StartsWith("AOE")
                                 || obj.NameNode.StartsWith("ARS")
                                 || obj.NameNode.StartsWith("AFS")
                                 || obj.NameNode.StartsWith("USS Dolphin")
                                 )
                        {
                            obj.UnitService = ServicesBll.FleetAuxiliary;
                            obj.UnitComponent = ServiceTypeBLL.Active;
                        }
                        else
                        {
                            throw new Exception("servicetxt: " + servicetxt);
                        }
                        return;
                    }
                    
            }

        }

        private static string GetNGState(string h3)
        {
            var hh3 = h3.Replace("National Guard", "").Trim();
            switch (hh3)
            {
                case "North Carolina":
                    return "NC";
            }

            return string.Empty;
        }

        static List<GlobalSecurityObject> masterlist = new List<GlobalSecurityObject>();

    }
}
