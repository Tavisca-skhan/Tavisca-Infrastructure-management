using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItemObject
{
    public class Laptop : Item
    {
      // string laptopId;
        public Laptop()
        {
            UniqueID = "lap" + DateTime.Now.Ticks / 100000;
            Name = "Laptop";
        }

    }
}
