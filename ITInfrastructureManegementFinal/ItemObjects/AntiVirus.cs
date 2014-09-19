using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItemObject
{
    public class AntiVirus : Item
    {
//string antivirusId;
        public AntiVirus()
        {
            UniqueID = "anv" + DateTime.Now.Ticks / 100000;
            Name = "Antivirus";
        }
    }
}
