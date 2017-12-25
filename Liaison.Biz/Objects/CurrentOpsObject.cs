using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
    public class CurrentOpsObject
    {
        public string Url { get; set; }
        public string NameNode { get; set; }
        public string FullName { get; set; }
        public string SplitName { get; set; }
        public string Service { get; set; }
        public string LogoUrl { get; set; }
        public List<LocationObject> Locations { get; set; }
        public List<HigherHqObject> HigherHq { get; set; }
        public List<SubUnitObject> Children { get; set; }
    }
}
