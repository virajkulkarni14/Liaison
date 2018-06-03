using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Liaison.BLL.Models
{
    public interface IBase
    {

    }
    public abstract class ABase : IBase
    {

    }

    public class Airbase : ABase
    {

    }

    public class NavalBase : ABase
    {

    }

    public class Garrison : ABase
    {

    }
}
