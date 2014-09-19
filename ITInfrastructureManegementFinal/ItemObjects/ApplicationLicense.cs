using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItemObject
{
    public class ApplicationLicense : Item
    {
        string AppLicenseId;
        public ApplicationLicense()
        {
            AppLicenseId = "AppL" + DateTime.Now.Ticks / 100000;
        }
    }
}
