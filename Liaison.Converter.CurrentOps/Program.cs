using Liaison.Biz;
using Liaison.Biz.Objects;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace Liaison.Converter.CurrentOps
{
    class Program
    {
        static void Main(string[] args)
        {
           

            using (var stream = File.OpenRead("C://repos/Liaison//Liaison.Import.CurrentOps//output//us-army-1-id.xml"))
            {
                var serializer = new XmlSerializer(typeof(List<CurrentOpsObject>));
                var currentOpsObject = (List<CurrentOpsObject>)serializer.Deserialize(stream);

                foreach (var coo in currentOpsObject)
                {
                    IMilitaryOrg org = Biz.Converter.ConvertToMilOrg.Convert(coo);
                }
            }

            //XmlDocument doc = new XmlDocument();
            //doc.Load();
            //XmlSerializer serializer = new XmlSerializer(typeof(List<CurrentOpsObject>));

            //List<CurrentOpsObject> list = (List<CurrentOpsObject>)serializer.Deserialize(doc.);
        }
    }
}
