using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItemObject
{
    class EmployeeHelper
    {
        public static void SaveEmployee(Employee emp, string path)
        {
            DataAccessLayer.XmlHelper xml = new DataAccessLayer.XmlHelper();
           // xml.WriteToXml(emp, path, "Employee");
        }
    }
}
