using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Liaison.BLL.Models;
using Liaison.BLL.Models.Unit.Interfaces;

namespace Liaison.BLL.Translators
{
    interface ILiaisonTranslator
    {
        List<string> GetSortOrder();
        IUnit GetTree(string input, int? depth, bool showAll);
        IUnit ConvertUnit(object sqlUnit);

        NewThing CreateAFB(NewThing newThing);
        int? SelectOne(int newThingServiceType, int active, int reserve, int volunteer);
        string GetLongCode(Dictionary<string, string> unitindices, string candidate);
    }
}
