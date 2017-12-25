using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Liaison.Helper
{
   public class SystemWebHelper
    {
        public static string HtmlDecoder(string s)
        {
            return HttpUtility.HtmlDecode(s);
        }
        public static string HtmlEncoder(string s)
        {
            return HttpUtility.HtmlEncode(s);
        }
    }
}
