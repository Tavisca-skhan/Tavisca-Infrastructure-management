using ItemObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;

using System.Configuration;

namespace BusinessLogicLayer
{
   public class ItemHelper
    {
       IDataHandler dataHandler;

       public ItemHelper()
       {
           string classNameStr = (string)ConfigurationSettings.AppSettings["selectedStorage"];
           Type concreteType = Type.GetType(classNameStr);

           this.dataHandler = (IDataHandler)Activator.CreateInstance(concreteType);
       }
        public void Save(Item item, string path)
        {
           
            dataHandler.WriteToDb((Object)item, path, "Item");
        }

        public  List<string> ReadBrand(string itemType)
        {
          
            List<string> brandlist = dataHandler.ReadBrand(itemType);

            return brandlist;
        }

        public  List<string> ReadEmployee()
        {
           
            List<string> empList =dataHandler.ReadEmployee();

            return empList;

        }
        public  string ReadEmpName(string empid)
        {
          
            string empName = dataHandler.ReadEmpName(empid);
            return empName;
        }

        public  void AssignTo(string itemtype, string itembrand, string itemId, string tillDate, string empid)
        {
          
            dataHandler.AssignTo(itemtype, itembrand, itemId, tillDate, empid);
        }
        public  void Assign(string productID,string employeeID,DateTime expiredDate,bool isassign)
        {
        
            
            dataHandler.AssignProduct(productID, employeeID, expiredDate, isassign);
   
        }

        public  List<string> ReadItemId(string itemType, string brand)
        {
            
            List<string> idList = dataHandler.ReadItemId(itemType, brand);

            return idList;
        }

        //unassigned
        public void UnassignTo(string itemtype, string itembrand, string itemId)
        {
            
            dataHandler.UnassignTo(itemtype, itembrand, itemId);
        }
        //Extend
        public void ExtendTo(string itemtype, string itembrand, string itemId, string extendDate)
        {

            dataHandler.ExtendTo(itemtype, itembrand, itemId, extendDate);
        }
        //unassiged
        public List<string> ReadAssignedItemId(string itemType, string brand)
        {
           
            List<string> idList = dataHandler.ReadAssignedItemId(itemType, brand);

            return idList;
        }
       public List<Item> ReadGridviewValues(string flag)
       {
          
           return dataHandler.ReadGridviewValues(flag);     
       }
       //for Search
        public List<Item> ReadGridviewValues(string searchby,string text,string flag)
        {
                 return dataHandler.ReadGridviewValues(searchby, text, flag); 
         }

        public List<Employee> ReadGridviewEmpValues(string searchby, string text)
        {
            return dataHandler.ReadGridviewEmpValues(searchby, text);
        }

        
   }
}
