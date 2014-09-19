using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using ItemObject;



namespace DataAccessLayer
{

    public class XmlHelper : IDataHandler
    {
        ItemObjects.XmlFiles x = new ItemObjects.XmlFiles();

        string productFilePath;
        string employeeFilePath;
        public XmlHelper()
        {
            productFilePath = ConfigurationManager.AppSettings["productFilePath"];//x.ProductFilePath;
            employeeFilePath = ConfigurationManager.AppSettings["employeeFilePath"];//x.EmployeeFilePath; 
        }

        public List<string> ReadBrand(string itemType)
        {



            XElement xmlDoc = XElement.Load(productFilePath);
            var items = from item in xmlDoc.Descendants("Item")
                        where item.Element("Name").Value == itemType
                        select item.Element("Brand").Value;
            return items.ToList<string>();
        }

        public List<string> ReadItemId(string itemType, string brand)
        {

            XElement xmlDoc = XElement.Load(productFilePath);
            var items = from item in xmlDoc.Descendants("Item")
                        where item.Element("Name").Value == itemType && item.Element("Brand").Value == brand && item.Element("IsAssigned").Value == "False"
                        select item.Element("UniqueID").Value;
            return items.ToList<string>();
        }

        public List<string> ReadEmployee()
        {
            XElement xmlDoc = XElement.Load(employeeFilePath);
            var items = from item in xmlDoc.Descendants("Employee")
                        where item.Element("IsAssigned").Value == "false"
                        select item.Element("EmployeeId").Value;
            return items.ToList<string>();
        }
        public string ReadEmpName(string empid)
        {
            string EmpName = "";
            XElement xmlDoc = XElement.Load(employeeFilePath);//(@"C:\Users\skhan\Desktop\C#_assignmnt\XMLfiles\Employee.xml");
            var items = from item in xmlDoc.Descendants("Employee")
                        where item.Element("EmployeeId").Value == empid
                        select item.Element("Name").Value;
            foreach (var item in items)
            {
                EmpName = item;
            }

            return EmpName;
        }

        public void AssignTo(string itemtype, string itembrand, string itemId, string tillDate, string empid)
        {
            XElement xmlDoc = XElement.Load(productFilePath);
            var items = from item in xmlDoc.Descendants("Item")
                        where item.Element("Name").Value == itemtype && item.Element("Brand").Value == itembrand && item.Element("UniqueID").Value == itemId
                        select item;

            foreach (var item in items)
            {
                item.Element("IsAssigned").Value = "True";
                item.Element("DateOfExpiryOfAssignment").Value = tillDate;
                item.Element("AssignedTo").Value = empid;
            }
            xmlDoc.Save(productFilePath);
        }

        public void WriteToDb(Object item, string path, string rootName)
        {
            var itemType = item.GetType();
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(path);
            XmlNode Item = xmlDoc.CreateElement(rootName);
            foreach (var property in itemType.GetProperties())
            {
                string propertyValue = property.ToString();
                XmlNode newNode = xmlDoc.CreateElement(property.Name);
                newNode.InnerText = property.GetValue(item, null).ToString();
                Item.AppendChild(newNode);
            }
            xmlDoc.DocumentElement.AppendChild(Item);
            xmlDoc.Save(path);
        }
        //unassigned
        public List<string> ReadAssignBrand(string itemType)
        {

            XElement xmlDoc = XElement.Load(productFilePath);
            var items = from item in xmlDoc.Descendants("Item")
                        where item.Element("Name").Value == itemType
                        select item.Element("Brand").Value;
            return items.ToList<string>();
        }
        //unassiged
        public List<string> ReadAssignedItemId(string itemType, string brand)
        {
            XElement xmlDoc = XElement.Load(productFilePath);
            var items = from item in xmlDoc.Descendants("Item")
                        where item.Element("Name").Value == itemType && item.Element("Brand").Value == brand && item.Element("IsAssigned").Value == "True"
                        select item.Element("UniqueID").Value;
            return items.ToList<string>();
        }
        //unassigned
        public void UnassignTo(string itemtype, string itembrand, string itemId)
        {
            XElement xmlDoc = XElement.Load(productFilePath);
            var items = from item in xmlDoc.Descendants("Item")
                        where item.Element("Name").Value == itemtype && item.Element("Brand").Value == itembrand && item.Element("UniqueID").Value == itemId
                        select item;

            foreach (var item in items)
            {
                item.Element("IsAssigned").Value = "False";

            }
            xmlDoc.Save(productFilePath);
        }
        //Extend
        public void ExtendTo(string itemtype, string itembrand, string itemId, string extendDate)
        {
            XElement xmlDoc = XElement.Load(productFilePath);
            var items = from item in xmlDoc.Descendants("Item")
                        where item.Element("Name").Value == itemtype && item.Element("Brand").Value == itembrand && item.Element("UniqueID").Value == itemId
                        select item;

            foreach (var item in items)
            {
                item.Element("IsAssigned").Value = "True";
                item.Element("Date_of_expiry_of_assignment").Value = extendDate;
            }
            xmlDoc.Save(productFilePath);
        }

        public void AssignData(string productID, string employeeID)
        {
            //XElement xmlDoc = XElement.Load(productFilePath);
            //var items = from item in xmlDoc.Descendants("Item")
            //            where item.Element("productId-").Value == itemtype && item.Element("Brand").Value == itembrand && item.Element("UniqueID").Value == itemId
            //            select item;

            //foreach (var item in items)
            //{
            //    item.Element("IsAssigned").Value = "False";

            //}
            //xmlDoc.Save(productFilePath);
        }

        public void AssignProduct(string productID, string employeeID, DateTime expireDate, bool isassign)
        {
            XElement xmlDoc = XElement.Load(productFilePath);
            var items = from item in xmlDoc.Descendants("Item")
                        where item.Element("UniqueID").Value == productID
                        select item;
            if (isassign)
            {
                foreach (var item in items)
                {
                    item.Element("IsAssigned").Value = "True";
                    item.Element("DateOfExpiryOfAssignment").Value = expireDate.ToString();
                    item.Element("AssignedDate").Value = DateTime.Now.ToString();

                    item.Element("AssignedTo").Value = employeeID;
                }
            }
            else
            {
                foreach (var item in items)
                {
                    item.Element("IsAssigned").Value = "False";
                    item.Element("DateOfExpiryOfAssignment").Value = "";
                    item.Element("AssignedDate").Value = "";

                    item.Element("AssignedTo").Value = "";
                }
            }
            xmlDoc.Save(productFilePath);

        }
        public List<Item> ReadGridviewValues(string flag)
        {
            Item itemInstance;
            var temp = new List<Item>();
            XElement xmlDoc = XElement.Load(productFilePath);
            var items = (from item in xmlDoc.Descendants("Item")
                         where item.Element("IsAssigned").Value == flag
                         select item).ToList();
            foreach (var item in items)
            {
                itemInstance = new Item();
                itemInstance.Name = item.Element("Name").Value;
                itemInstance.UniqueID = item.Element("UniqueID").Value;
                itemInstance.IsAssigned = item.Element("IsAssigned").Value;
                itemInstance.Brand = item.Element("Brand").Value;
                itemInstance.CreationDate = Convert.ToDateTime(item.Element("CreationDate").Value);
                itemInstance.ActualExpiryDate = Convert.ToDateTime(item.Element("ActualExpiryDate").Value);
                itemInstance.WarrantyExists = item.Element("WarrantyExists").Value;
                itemInstance.WarrantyExpiration = Convert.ToDateTime(item.Element("WarrantyExpiration").Value);
                itemInstance.AssignedTo = item.Element("AssignedTo").Value;
                if (item.Element("DateOfExpiryOfAssignment").Value != "")
                {
                    itemInstance.DateOfExpiryOfAssignment = Convert.ToDateTime(item.Element("DateOfExpiryOfAssignment").Value);
                }
                if (item.Element("AssignedDate").Value != "")
                {
                    itemInstance.AssignedDate = Convert.ToDateTime(item.Element("AssignedDate").Value);
                }

                temp.Add(itemInstance);
            }
            return temp;
        }
        //for search
        public List<Item> ReadGridviewValues(string searchby, string text, string flag)
        {
            Item itemInstance;
            var items = new List<XElement>();
            var temp = new List<Item>();
            XElement xmlDoc = XElement.Load(productFilePath);
            if (flag != "All")
            {
                items = (from item in xmlDoc.Descendants("Item")
                         where item.Element(searchby).Value.StartsWith(text, true, null) && item.Element("IsAssigned").Value == flag
                         select item).ToList();
            }
            else
            {
                items = (from item in xmlDoc.Descendants("Item")
                         where item.Element(searchby).Value.StartsWith(text, true, null)

                         select item).ToList();
            }
            foreach (var item in items)
            {
                itemInstance = new Item();
                itemInstance.Name = item.Element("Name").Value;
                itemInstance.UniqueID = item.Element("UniqueID").Value;
                itemInstance.IsAssigned = item.Element("IsAssigned").Value;
                itemInstance.Brand = item.Element("Brand").Value;
                itemInstance.CreationDate = Convert.ToDateTime(item.Element("CreationDate").Value);
                itemInstance.ActualExpiryDate = Convert.ToDateTime(item.Element("ActualExpiryDate").Value);
                itemInstance.WarrantyExists = item.Element("WarrantyExists").Value;
                itemInstance.WarrantyExpiration = Convert.ToDateTime(item.Element("WarrantyExpiration").Value);
                itemInstance.AssignedTo = item.Element("AssignedTo").Value;
                if (item.Element("DateOfExpiryOfAssignment").Value != "")
                {
                    itemInstance.DateOfExpiryOfAssignment = Convert.ToDateTime(item.Element("DateOfExpiryOfAssignment").Value);
                }
                if (item.Element("AssignedDate").Value != "")
                {
                    itemInstance.AssignedDate = Convert.ToDateTime(item.Element("AssignedDate").Value);
                }

                temp.Add(itemInstance);
            }
            return temp;
        }
        public List<Employee> ReadGridviewEmpValues(string searchby, string text)
        {
            Employee emp;
            var tempempList = new List<Employee>();
            XElement xmlDoc = XElement.Load(employeeFilePath);
            var employees = (from employee in xmlDoc.Descendants("Employee")
                             where employee.Element(searchby).Value.StartsWith(text)
                             select employee).ToList();
            foreach (var item in employees)
            {
                emp = new Employee();
                emp.Name = item.Element("Name").Value;
                emp.EmployeeId = item.Element("EmployeeId").Value;
                emp.EMailAddr = item.Element("EMailAddr").Value;
                emp.Phone = item.Element("Phone").Value;
                emp.Location = item.Element("Location").Value;

                tempempList.Add(emp);
            }
            return tempempList;
        }
      
    }
}