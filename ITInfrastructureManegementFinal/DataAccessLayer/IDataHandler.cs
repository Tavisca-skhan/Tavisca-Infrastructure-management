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
         void  AssignProduct(string productID,string employeeID,DateTime expireDate, bool isAssign);
         List<Item> GetProdutcs(string flag);
         List<Item> SearchProducts(string searchBy, string text, string flag);
         List<Employee> SearchEmployee(string searchBy, string text);
         List<string> GetBrand(string itemType);
         List<string> GetItemId(string itemType, string brand);
         List<string> GetEmployee();
         string GetEmpName(string empId);
         void AssignTo(string itemType, string itemBrand, string itemId, string tillDate, string empId);
         void WriteToDb(Object item, string path, string rootName);
         List<string> GetAssignBrand(string itemType);
         List<string> GetAssignedItemId(string itemType, string brand);
         //void UnassignTo(string itemtype, string itembrand, string itemId);
         void ExtendDate(string itemType, string itemBrand, string itemId, string extendDate);
         void UnassignProduct(string itemType, string itembrand, string itemId);


    }
}
