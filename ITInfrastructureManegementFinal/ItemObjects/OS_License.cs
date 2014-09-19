using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItemObject
{
    public class OS_License : Item
    {
       //string os_LicenseId;
       public OS_License()
       {
           UniqueID = "OSL" + DateTime.Now.Ticks / 100000;
           Name = "OS_License";
       }
    }
}
