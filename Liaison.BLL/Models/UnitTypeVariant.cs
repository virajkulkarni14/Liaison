using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Liaison.BLL.Models
{
    public static partial class ExtensionMethods
    {
        public static string ToUtvString(this UnitTypeVariant data)
        {
            StringBuilder sb = new StringBuilder();
            if (data.Data.Contains('#'))
            {
                var utvs = data.Data.Split('#');
                foreach (var u in utvs)
                {
                    sb.Append("(" + u + ") ");
                }
            }
            else
            {
                sb.Append("(" + data.Data + ")");
            }

            var r = sb.ToString();
            if (r.EndsWith(" "))
            {
                r = r.Substring(0, r.Length - 1);
            }

            return r;
        }

    }
    public class UnitTypeVariant
    {
        public UnitTypeVariant(string utv)
        {
            this.Data = utv;
        }

    
        public string Data { get; set; }
    }
}
