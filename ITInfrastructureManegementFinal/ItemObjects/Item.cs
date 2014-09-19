using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ItemObject
{
    public class Item
    {
        string name;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        string uniqueID;

        public string UniqueID
        {
            get { return uniqueID; }
            set { uniqueID = value; }
        }
        string brand;
        string isAssigned = "";

        public string IsAssigned
        {
            get { return isAssigned; }
            set { isAssigned = value; }
        }
        string warrantyExists;
        public string Brand
        {
            get { return brand; }
            set { brand = value; }
        }
        DateTime creationDate;

        public DateTime CreationDate
        {
            get { return creationDate; }
            set { creationDate = value; }
        }


        DateTime actualExpiryDate;

        public DateTime ActualExpiryDate
        {
            get { return actualExpiryDate; }
            set { actualExpiryDate = value; }
        }


        public string WarrantyExists
        {
            get { return warrantyExists; }
            set { warrantyExists = value; }
        }
        DateTime warrantyExpiration;

        public DateTime WarrantyExpiration
        {
            get { return warrantyExpiration; }
            set { warrantyExpiration = value; }
        }

        string assignedTo="";

        public string AssignedTo
        {
            get { return assignedTo; }
            set { assignedTo = value; }
        }


        DateTime dateOfExpiryOfAssignment;

        public DateTime DateOfExpiryOfAssignment
        {
            get { return dateOfExpiryOfAssignment; }
            set { dateOfExpiryOfAssignment = value; }
        }

        DateTime assignedDate;

        public DateTime AssignedDate
        {
            get { return assignedDate; }
            set { assignedDate = value; }
        }

        

    }
}







/*   public List<Object> ReadXml(string itemType, string itemBrand)
       {
           XElement xmlDoc = XElement.Load(@"C:\Users\akhadake\Desktop\Products.xml");
           var items = from i in xmlDoc.Descendants("Item")
                       where i.Element("Name").Value == itemType &&  i.Element("Brand").Value ==itemBrand
                       select new
                       {
                           Name = i.Element("Name").Value,
                           UniqueID = i.Element("UniqueID").Value,
                           Isssigned = i.Element("Isssigned").Value,
                           Brand = i.Element("Brand").Value,
                           Creation_date = i.Element("Creation_date").Value,
                           Actual_expiry_date = i.Element("Actual_expiry_date").Value,
                           Warranty_exists1 = i.Element("Warranty_exists1").Value,
                           WarrantyExpiration1 = i.Element("WarrantyExpiration1").Value,
                           Date_of_expiry_of_assignment = i.Element("Date_of_expiry_of_assignment").Value,
                           Assigned_date = i.Element("Assigned_date").Value
                       };


           return items.ToList<Object>();
       }*/