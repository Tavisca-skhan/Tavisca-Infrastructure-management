using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItemObject
{
    public class Dongle3G : Item
    {
         
        // string dongleID;
      public  Dongle3G()
        {
            UniqueID = "d3G" + DateTime.Now.Ticks/100000;
            Name = "Dongle3G";
        }
        
    }
}
