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
            string classNameStr = ConfigurationSettings.AppSettings["selectedStorage"];
            Type concreteType = Type.GetType(classNameStr);

            this.dataHandler = (IDataHandler)Activator.CreateInstance(concreteType);
        }
        public void Save(Item item, string path)
        {

            dataHandler.WriteToDb((Object)item, path, "Item");
        }

        public List<string> GetBrand(string itemType)
        {

            List<string> brandlist = dataHandler.GetBrand(itemType);

            return brandlist;
        }

        public List<string> GetEmployee()
        {

            List<string> empList = dataHandler.GetEmployee();

            return empList;

        }
        public string GetEmpName(string empId)
        {

            string empName = dataHandler.GetEmpName(empId);
            return empName;
        }

        public void AssignTo(string itemType, string itemBrand, string itemId, string tillDate, string empId)
        {

            dataHandler.AssignTo(itemType, itemBrand, itemId, tillDate, empId);
        }
        public void Assign(string productID, string employeeID, DateTime expiredDate, bool isAssign)
        {


            dataHandler.AssignProduct(productID, employeeID, expiredDate, isAssign);

        }

        public List<string> ReadItemId(string itemType, string brand)
        {

            List<string> idList = dataHandler.GetItemId(itemType, brand);

            return idList;
        }

        //unassigned
        public void UnassignProduct(string itemtype, string itembrand, string itemId)
        {

            dataHandler.UnassignProduct(itemtype, itembrand, itemId);
        }
        //Extend
        public void ExtendDate(string itemType, string itemBrand, string itemId, string extendDate)
        {

            dataHandler.ExtendDate(itemType, itemBrand, itemId, extendDate);
        }
        //unassiged
        public List<string> GetAssignedItemId(string itemType, string brand)
        {

            List<string> idList = dataHandler.GetAssignedItemId(itemType, brand);

            return idList;
        }
        public List<Item> GetProducts(string flag)
        {

            return dataHandler.GetProdutcs(flag);
        }
        //for Search
        public List<Item> SearchProducts(string searchBy, string text, string flag)
        {
            return dataHandler.SearchProducts(searchBy, text, flag);
        }

        public List<Employee> SearchEmployee(string searchBy, string text)
        {
            return dataHandler.SearchEmployee(searchBy, text);
        }
        public void ColorExpireAssign()
        {
        }



    }
}
