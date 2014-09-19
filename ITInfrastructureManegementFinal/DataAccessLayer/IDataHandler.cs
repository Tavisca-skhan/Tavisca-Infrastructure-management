using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ItemObject;


namespace DataAccessLayer
{
  public  interface IDataHandler
    {
         void  AssignProduct(string productID,string employeeID,DateTime expireDate, bool isassign);
         List<Item> ReadGridviewValues(string flag);
         List<Item> ReadGridviewValues(string searchby, string text, string flag);
         List<Employee> ReadGridviewEmpValues(string searchby, string text);
         List<string> ReadBrand(string itemType);
         List<string> ReadItemId(string itemType, string brand);
         List<string> ReadEmployee();
         string ReadEmpName(string empid);
         void AssignTo(string itemtype, string itembrand, string itemId, string tillDate, string empid);
         void WriteToDb(Object item, string path, string rootName);
         List<string> ReadAssignBrand(string itemType);
         List<string> ReadAssignedItemId(string itemType, string brand);
         //void UnassignTo(string itemtype, string itembrand, string itemId);
         void ExtendTo(string itemtype, string itembrand, string itemId, string extendDate);
         void UnassignTo(string itemtype, string itembrand, string itemId);


    }
}
