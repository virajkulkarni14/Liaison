using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Liaison.BLL.Models.Unit
{
    public class AdminCorps
    {
        public AdminCorps(string code, string name, int id)
        {
            Id = id;
            Code = code;
            Name = name;
        }
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
    }
}
