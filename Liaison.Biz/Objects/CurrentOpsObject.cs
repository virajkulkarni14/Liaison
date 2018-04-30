using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Liaison.Helper.Enumerators;

namespace Liaison.Biz.Objects
{
    public class SubUnitObject
    {
        public string Url { get; set; }
        public string FullName { get; set; }
        public string Name { get; set; }
        public string Id { get; set; }
        public bool IsIndirect { get; set; }
    }
    public class HigherHqObject
    {
        public string Url { get; set; }
        public string Id { get; set; }
        public string DateRange { get; set; }
        public bool IsCurrent { get; set; }
        public string HHQAcronym { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }
    }
    public class LocationObject
    {
        public string DateRange { get; set; }
        public string BaseName { get; set; }
        public bool Deployment { get; set; }
        public string Location { get; set; }
        public bool IsCurrent { get; set; }
        public string Url { get; set; }
        public string Id { get; set; }
        public string Note { get; set; }
    }
    public class CurrentOpsObject
    {
        public string Url { get; set; }
        public string NameNode { get; set; }
        public string FullName { get; set; }
        public string SplitName { get; set; }
        //public string Service { get; set; }
        public string LogoUrl { get; set; }
        public List<LocationObject> Locations { get; set; }
        public List<HigherHqObject> HigherHq { get; set; }
        public List<SubUnitObject> Children { get; set; }
        public ServicesBll UnitService { get; set; }
        public ServiceTypeBLL UnitComponent { get; set; }
        public string UnitNGState { get; set; }
     
    }
    public class GlobalSecurityObject:CurrentOpsObject
    {
        public string NameFormer { get; set; }
        public List<TableNode> TableNodes { get; set; }
  
        //public List<Tuple<string, string, bool>> Description { get; set; }
        public List<DescriptionObj> Description { get; set; }
        public List<li> Breadcrumbs { get; set; }
        public string NickName { get; set; }
    }
    public class DescriptionObj
    {
        public DescriptionObj() { }
        public DescriptionObj(string nodetype, string innerHtml, bool isItalic)
        {
            this.NodeType = nodetype;
            this.Contents = innerHtml;
            this.IsItalic = isItalic;
        }

        public DescriptionObj(string v1, string v2, string href, bool v3)
        {
            this.NodeType = v1;
            this.Contents = v2;
            this.Href = href;
            this.IsItalic = v3;
        }

        public string NodeType { get; set; }
        public string Contents { get; set; }
        public string Href  { get;set; }
        public bool IsItalic { get; set; }
    }

    public class Description
    {
        public string text { get; set; }
        public List<string> images { get; set; }
    }

    public class li
    {
        public string href { get; set; }
        public string text { get; set; }
        public bool Former { get; set; }
    }
    public class TableNode
    {
        public string href;

        public TableNode()
        {
            this.li = new List<li>();
        }
        public string h3 { get; set; }
        public List<li> li { get; set; }
        public string image { get; set; }
    }
}
