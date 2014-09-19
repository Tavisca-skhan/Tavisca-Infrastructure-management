using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItemObject
{
   public class HeadPhone :Item
    {
      // string headPhoneID;
       public HeadPhone()
       {
           UniqueID = "hep" + DateTime.Now.Ticks / 100000;
           Name = "HeadPhone";
       }
    }
}
