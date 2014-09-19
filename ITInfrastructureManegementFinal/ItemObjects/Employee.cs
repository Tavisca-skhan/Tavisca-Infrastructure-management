using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItemObject
{
    public class Employee
    {
        string name;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        string employeeId;

        public string EmployeeId
        {
            get { return employeeId; }
            set { employeeId = value; }
        }

        string email_ID;

        public string EMailAddr
        {
            get { return email_ID; }
            set { email_ID = value; }
        }

        string phone;

        public string Phone
        {
            get { return phone; }
            set { phone = value; }
        }
        string location;

        public string Location
        {
            get { return location; }
            set { location = value; }
        }
       

        bool isAssigned;

        public bool IsAssigned
        {
            get { return isAssigned; }
            set { isAssigned = value; }
        }
       
    }
}
