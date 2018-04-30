using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Liaison.Data.Demo.Organisations
{
    public class Command
    {
        public Guid CommandId { get; set; }
        public string Name { get; set; }
        public string Acronym { get; set; }
        public List<string> Keywords { get; set; }
        public Liaison.Helper.Enumerators.ServicesBll Service { get;set; }
    }
}
